Entendi, você quer um `README.md` completo e coeso. Aqui está o conteúdo ajustado para um `README.md` que você pode usar diretamente no seu repositório:

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
    { "id": 1, "title": "Oferta 1", "description": "Descrição da Oferta 1" },
    { "id": 2, "title": "Oferta 2", "description": "Descrição da Oferta 2" },
    { "id": 3, "title": "Oferta 3", "description": "Descrição da Oferta 3" }
]
```

### GET /api/ofertas/{id}

Retorna uma oferta específica pelo ID.

**Parâmetros:**

- `id` (int): O ID da oferta.

**Resposta Exemplo:**

```json
{ "id": 1, "title": "Oferta 1", "description": "Descrição da Oferta 1" }
```

### POST /api/ofertas

Cria uma nova oferta.

**Corpo da Requisição:**

```json
{
    "title": "Nova Oferta",
    "description": "Descrição da nova oferta"
}
```

**Resposta Exemplo:**

```json
{ "id": 4, "title": "Nova Oferta", "description": "Descrição da nova oferta" }
```

### PUT /api/ofertas/{id}

Atualiza uma oferta existente.

**Parâmetros:**

- `id` (int): O ID da oferta.

**Corpo da Requisição:**

```json
{
    "title": "Oferta Atualizada",
    "description": "Descrição atualizada da oferta"
}
```

**Resposta Exemplo:**

```json
{ "id": 1, "title": "Oferta Atualizada", "description": "Descrição atualizada da oferta" }
```

### DELETE /api/ofertas/{id}

Exclui uma oferta pelo ID.

**Parâmetros:**

- `id` (int): O ID da oferta.

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
curl -X POST http://localhost:5000/api/ofertas -H "Content-Type: application/json" -d '{"title": "Oferta Nova", "description": "Descrição da nova oferta"}'
```

## Testes

Os testes estão a ser implementados. Você pode executá-los com o comando:

```bash
dotnet test
```

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).

---

Obrigado por utilizar a **Ofertas API**!
```

Este `README.md` fornece uma documentação clara e completa para quem for utilizar ou contribuir para a sua API. Se precisar de mais alguma coisa, é só avisar! 🚀