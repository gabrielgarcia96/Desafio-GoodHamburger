using GoodHamburguer.Api.Dtos;
using GoodHamburguer.Application.Interfaces;
using GoodHamburguer.Domain.Entities;
using GoodHamburguer.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburguer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IProdutoRepository _produtoRepository;

    public PedidosController(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository)
    {

        _pedidoRepository = pedidoRepository;
        _produtoRepository = produtoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pedidos = await _pedidoRepository.ListarAsync();

        if (pedidos == null || !pedidos.Any())
            return NotFound("Nenhum pedido encontrado.");

        var response = pedidos.Select(p => new PedidoResponse
        {
            Id = p.Id,
            SubTotal = p.SubTotal,
            Desconto = p.Desconto,
            Total = p.Total,
            Itens = p.Itens.Select(i => new ItemPedidoResponse
            {
                IdProduto = i.IdProduto,
                Quantidade = i.Quantidade
            }).ToList()
        });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var pedido = await _pedidoRepository.ObterPorIdAsync(id);

        if (pedido == null)
            return NotFound();

        var response = new PedidoResponse
        {
            Id = pedido.Id,
            SubTotal = pedido.SubTotal,
            Desconto = pedido.Desconto,
            Total = pedido.Total,
            Itens = pedido.Itens.Select(i => new ItemPedidoResponse
            {
               IdProduto = i.IdProduto,
               Quantidade = i.Quantidade
            }).ToList()
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CriarPedido([FromBody] CriarPedidoRequest criarPedidoRequest)
    {
        if (criarPedidoRequest == null)
            return BadRequest("Pedido inválido.");

        var pedido = new Pedido();

        foreach (var item in criarPedidoRequest.Itens)
        {
            var produto = await _produtoRepository.GetProductById(item.IdProduto);

            if (produto == null)
                return BadRequest($"Produto {item.IdProduto} não encontrado.");

            pedido.Itens.Add(new ItemPedido
            {
                IdProduto = produto.Id,
                Produto = produto,
                Quantidade = item.Quantidade
            });
        }

        pedido.CalcularTotais();

        await _pedidoRepository.AdicionarAsync(pedido);

        var response = new PedidoResponse
        {
            Id = pedido.Id,
            SubTotal = pedido.SubTotal,
            Desconto = pedido.Desconto,
            Total = pedido.Total,
            Itens = pedido.Itens.Select(i => new ItemPedidoResponse
            {
                IdProduto = i.IdProduto,
                Quantidade = i.Quantidade
            }).ToList()
        };

        return CreatedAtAction(nameof(GetById), new { id = pedido.Id }, response);
    }

    [HttpPost("{pedidoId}/itens")]
    public async Task<IActionResult> AdicionarItem(Guid pedidoId, [FromBody] AdicionarItemRequest request)
    {
        var pedido = await _pedidoRepository.ObterPorIdAsync(pedidoId);

        if (pedido == null)
            return NotFound("Pedido não encontrado.");

        var produto = await _produtoRepository.GetProductById(request.IdProduto);

        if (produto == null)
            return BadRequest("Produto não encontrado.");

        var item = new ItemPedido
        {
            IdProduto = produto.Id,
            Produto = produto,
            Quantidade = request.Quantidade
        };

        try
        {
            pedido.AdicionarItem(item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        await _pedidoRepository.AtualizarAsync(pedido);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePedido(Guid id, [FromBody] AtualizarPedidoRequest atualizarPedidoRequest)
    {
        if (atualizarPedidoRequest == null)
            return BadRequest("Dados inválidos.");

        if (id != atualizarPedidoRequest.IdPedido)
            return BadRequest("Id da rota diferente do corpo.");

        var pedidoExistente = await _pedidoRepository.ObterPorIdAsync(id);

        if (pedidoExistente == null)
            return NotFound("Pedido não encontrado.");

        // limpa itens antigos
        pedidoExistente.Itens.Clear();

        foreach (var item in atualizarPedidoRequest.Itens)
        {
            var produto = await _produtoRepository.GetProductById(item.IdProduto);

            if (produto == null)
                return BadRequest($"Produto {item.IdProduto} não encontrado.");

            pedidoExistente.AdicionarItem(new ItemPedido
            {
                IdProduto = produto.Id,
                Produto = produto,
                Quantidade = item.Quantidade
            });
        }

        pedidoExistente.CalcularTotais();

        await _pedidoRepository.AtualizarAsync(pedidoExistente);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarPedido(Guid id)
    {
        var getPedido = await _pedidoRepository.ObterPorIdAsync(id);

        if (getPedido == null)
            return NotFound($"Nenhum pedido com {id}, foi encontrado para exlusao");

        await _pedidoRepository.DeleteAsync(id);

        return NoContent();
    }
}
