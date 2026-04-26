using GoodHamburguer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GoodHamburguer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository; 
    }

    [HttpGet("produtos")]
    public async Task<IActionResult> GetAllProducts()
    {
        var produtos = await _produtoRepository.GetAllAsync();
        
        if(produtos == null)
            return NotFound("Nao foram encotrados produtos.");

        return Ok(produtos);
    }
}
