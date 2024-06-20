Claro! Aqui está o `README.md` ajustado, incluindo as correções feitas:

```markdown
# Ofertas API

A **Ofertas API** é um serviço RESTful para gerenciar ofertas. Esta API permite criar, ler, atualizar e excluir ofertas.

## Índice

- [Configuração](#configuração)
- [Executar a Aplicação](#executar-a-aplicação)
- [Endpoints](#endpoints)
  - [GET /api/ofertas](#get-apiofertas)
  - [GET /api/ofertas/{id}](#get-apiofertasid)
  - [POST /api/ofertas](#post-apiofertas)
  - [PUT /api/ofertas/{id}](#put-apiofertasid)
  - [DELETE /api/ofertas/{id}](#delete-apiofertasid)
- [Swagger](#swagger)
- [Exemplo de Uso](#exemplo-de-uso)
- [Testes](#testes)
- [Licença](#licença)

## Configuração

Clone o repositório e instale as dependências:

```bash
git clone https://github.com/SeuUsuario/ofertas-solution.git
cd ofertas-solution
```

Instale as dependências do projeto:

```bash
dotnet restore
```

## Executar a Aplicação

A aplicação pode ser executada diretamente com o Docker:

```bash
docker-compose up --build
```

Isso construirá e iniciará todos os contêineres necessários. A API estará disponível em http://localhost:5000.

### Configurações do Docker

Certifique-se de que o `docker-compose.yml` está configurado corretamente:

```yaml
version: '3.4'

services:
  ofertas-api:
    image: ofertas-api
    build:
      context: .
      dockerfile: Ofertas.API/Dockerfile
    ports:
      - "5000:8080"  # Mapeamento da porta do contêiner para a do host
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
```

## Endpoints

### GET /api/ofertas

Retorna todas as ofertas.

**Resposta Exemplo:**

```json
[
    { "id": "00000000-0000-0000-0000-000000000001", "titulo": "Oferta 1", "descricao": "Descrição da Oferta 1", "preco": 100.0, "dataCriacao": "2024-06-20T00:00:00Z" },
    { "id": "00000000-0000-0000-0000-000000000002", "titulo": "Oferta 2", "descricao": "Descrição da Oferta 2", "preco": 150.0, "dataCriacao": "2024-06-20T00:00:00Z" }
]
```

### GET /api/ofertas/{id}

Retorna uma oferta específica pelo ID.

**Parâmetros:**

- `id` (guid): O ID da oferta.

**Resposta Exemplo:**

```json
{ "id": "00000000-0000-0000-0000-000000000001", "titulo": "Oferta 1", "descricao": "Descrição da Oferta 1", "preco": 100.0, "dataCriacao": "2024-06-20T00:00:00Z" }
```

### POST /api/ofertas

Cria uma nova oferta.

**Corpo da Requisição:**

```json
{
    "titulo": "Nova Oferta",
    "descricao": "Descrição da nova oferta",
    "preco": 200.0,
    "dataCriacao": "2024-06-20T00:00:00Z"
}
```

**Resposta Exemplo:**

```json
{ "id": "00000000-0000-0000-0000-000000000004", "titulo": "Nova Oferta", "descricao": "Descrição da nova oferta", "preco": 200.0, "dataCriacao": "2024-06-20T00:00:00Z" }
```

### PUT /api/ofertas/{id}

Atualiza uma oferta existente.

**Parâmetros:**

- `id` (guid): O ID da oferta.

**Corpo da Requisição:**

```json
{
    "titulo": "Oferta Atualizada",
    "descricao": "Descrição atualizada da oferta",
    "preco": 250.0,
    "dataCriacao": "2024-06-20T00:00:00Z"
}
```

**Resposta Exemplo:**

```json
{ "id": "00000000-0000-0000-0000-000000000001", "titulo": "Oferta Atualizada", "descricao": "Descrição atualizada da oferta", "preco": 250.0, "dataCriacao": "2024-06-20T00:00:00Z" }
```

### DELETE /api/ofertas/{id}

Exclui uma oferta pelo ID.

**Parâmetros:**

- `id` (guid): O ID da oferta.

**Resposta Exemplo:**

```json
{ "message": "Oferta excluída com sucesso." }
```

## Swagger

A API possui documentação interativa com Swagger. Acesse [http://localhost:5000/swagger](http://localhost:5000/swagger) para visualizar e testar os endpoints diretamente no navegador.

## Exemplo de Uso

### GET todas as ofertas:

```bash
curl http://localhost:5000/api/ofertas
```

### POST nova oferta:

```bash
curl -X POST http://localhost:5000/api/ofertas -H "Content-Type: application/json" -d '{"titulo": "Oferta Nova", "descricao": "Descrição da nova oferta", "preco": 300.0, "dataCriacao": "2024-06-20T00:00:00Z"}'
```

## Testes

Os testes estão implementados. Você pode executá-los com o comando:

```bash
dotnet test
```

### Testes de Unidade

Os testes estão localizados na pasta `ofertas-solution.Tests` e cobrem os serviços e controllers da aplicação. Aqui está um exemplo de como executar os testes:

```bash
dotnet test ofertas-solution.Tests/Ofertas.Tests.csproj
```

**Exemplo de Testes para o Serviço:**

```csharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Moq;
using Xunit;
using Ofertas.Application.Services;
using Ofertas.Application.ViewModels;
using Ofertas.Domain.Entidades;
using Ofertas.Infrastructure.Data;
using Ofertas.Infrastructure.Interfaces;

namespace Ofertas.Tests
{
    public class OfertaServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IValidator<Oferta>> _mockValidator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly OfertaService _service;

        public OfertaServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockValidator = new Mock<IValidator<Oferta>>();
            _mockMapper = new Mock<IMapper>();
            _service = new OfertaService(_mockUnitOfWork.Object, _mockValidator.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOfertaResponses()
        {
            // Arrange
            var ofertas = new List<Oferta> { new Oferta { Id = Guid.NewGuid(), Titulo = "Oferta 1" }, new Oferta { Id = Guid.NewGuid(), Titulo = "Oferta 2" } };
            _mockUnitOfWork.Setup(u => u.Ofertas.GetAllAsync()).ReturnsAsync(ofertas);
            _mockMapper.Setup(m => m.Map<IEnumerable<OfertaResponse>>(ofertas)).Returns(new List<OfertaResponse> 
            { 
                new OfertaResponse { Id = ofertas[0].Id, Titulo = "Oferta 1" }, 
                new OfertaResponse { Id = ofertas[1].Id, Titulo = "Oferta 2" } 
            });

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsOfertaResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var oferta = new Oferta { Id = id, Titulo = "Oferta 1" };
            _mockUnitOfWork.Setup(u => u.Ofertas.GetByIdAsync(id)).ReturnsAsync(oferta);
            _mockMapper.Setup(m => m.Map<OfertaResponse>(oferta)).Returns(new OfertaResponse { Id = id, Titulo = "Oferta 1" });

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task AddAsync_ValidOferta_AddsOferta()
        {
            // Arrange
            var ofertaRequest = new OfertaRequest { Titulo = "Nova Oferta", Descricao = "Descrição da Nova Oferta", Preco = 100, DataCriacao = DateTime.UtcNow };
            var oferta = new Oferta { Titulo = "Nova Oferta", Descricao = "Descrição da Nova Oferta", Preco = 100, DataCriacao = DateTime.UtcNow };
            _mockMapper.Setup(m => m.Map<Oferta>(ofertaRequest)).Returns(oferta);
            _mockValidator.Setup(v => v.ValidateAsync(oferta, default)).Returns