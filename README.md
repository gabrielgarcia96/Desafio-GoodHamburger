# Good Hamburger API

API REST em **C# / ASP.NET Core** para registrar pedidos da lanchonete Good Hamburger, com cálculo automático de subtotal, desconto e total final conforme as regras do desafio.

## Funcionalidades implementadas

- CRUD completo de pedidos:
  - Criar pedido
  - Listar pedidos
  - Buscar pedido por ID
  - Atualizar pedido
  - Remover pedido
- Endpoint para consultar cardápio (produtos)
- Cálculo de subtotal, desconto e total final
- Validação de regras de negócio para itens duplicados por tipo
- Persistência com Entity Framework Core + PostgreSQL
- Seed inicial de produtos do cardápio

## 📋 Regras de negócio

### Cardápio

**Sanduíches**
- X Burger — R$ 5,00
- X Egg — R$ 4,50
- X Bacon — R$ 7,00

**Acompanhamentos**
- Batata frita — R$ 2,00
- Refrigerante — R$ 2,50

### Regras de desconto

- Sanduíche + batata + refrigerante → **20%**
- Sanduíche + refrigerante → **15%**
- Sanduíche + batata → **10%**

### Restrições

Cada pedido pode conter no máximo:
- 1 item do tipo hambúrguer
- 1 item do tipo batata frita
- 1 item do tipo refrigerante

Se houver tentativa de duplicar tipo de produto no mesmo pedido, a API retorna erro com mensagem clara.

## Arquitetura do projeto

Solução organizada em camadas:

- `GoodHamburguer.Api`: controllers, DTOs e configuração da aplicação
- `GoodHamburguer.Application`: interfaces e serviços de aplicação
- `GoodHamburguer.Domain`: entidades e regras de negócio
- `GoodHamburguer.Infrastructure`: contexto EF Core, migrations e repositórios
- `GoodHamburguer.Blazor`: frontend opcional (estrutura inicial)

## Como executar

### Pré-requisitos

- .NET SDK 8.0+
- PostgreSQL

### 1) Configurar connection string

Na API, use o arquivo de exemplo para criar seu `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=5432;Database=goodhamburguer;Uid=postgres;Pwd=postgres;"
  }
}
```

> Arquivo base disponível em: `src/GoodHamburguer.Api/appsettings.Example.json`.

### 2) Aplicar migrations

Na raiz do repositório:

```bash
dotnet ef database update --project src/GoodHamburguer.Infrastructure --startup-project src/GoodHamburguer.Api
```

### 3) Executar a API

```bash
dotnet run --project src/GoodHamburguer.Api
```

Com ambiente `Development`, o Swagger fica disponível automaticamente.

## 🔌 Endpoints principais

Base path: `api`

### Produtos (cardápio)

- `GET /api/Produto/produtos` → lista os produtos

### Pedidos

- `GET /api/Pedidos` → lista todos os pedidos
- `GET /api/Pedidos/{id}` → busca pedido por ID
- `POST /api/Pedidos` → cria pedido
- `PUT /api/Pedidos/{id}` → atualiza pedido
- `DELETE /api/Pedidos/{id}` → remove pedido
- `POST /api/Pedidos/{pedidoId}/itens` → adiciona item a pedido existente

## Exemplos de payload

### Criar pedido

`POST /api/Pedidos`

```json
{
  "itens": [
    { "idProduto": "11111111-1111-1111-1111-111111111111", "quantidade": 1 },
    { "idProduto": "44444444-4444-4444-4444-444444444444", "quantidade": 1 },
    { "idProduto": "55555555-5555-5555-5555-555555555555", "quantidade": 1 }
  ]
}
```

### Atualizar pedido

`PUT /api/Pedidos/{id}`

```json
{
  "idPedido": "GUID_DO_PEDIDO",
  "itens": [
    { "idProduto": "33333333-3333-3333-3333-333333333333", "quantidade": 1 },
    { "idProduto": "55555555-5555-5555-5555-555555555555", "quantidade": 1 }
  ]
}
```

##  Decisões técnicas

- **Desconto calculado no domínio (`Pedido`)** para centralizar regras de negócio.
- **Validação de item duplicado por tipo** no método `AdicionarItem` da entidade.
- **Repositórios com interfaces** para desacoplamento entre aplicação e persistência.
- **Seed de produtos por migration/model builder** para iniciar com cardápio padrão.

## ⚠️ Pontos de atenção / melhorias futuras

- Adicionar testes automatizados unitários para regras de desconto e validações.
- Revisar status code de listagem vazia (hoje retorna `404` quando sem pedidos).
- Evoluir frontend Blazor para fluxo completo de pedidos.
