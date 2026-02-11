# WebApi - API de Controle de Alunos

API REST para controle de alunos e matérias de cada aluno, desenvolvida em ASP.NET Core.

## Versão do .NET

Este projeto utiliza o **.NET 10.0**.

## Tecnologias

- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL (Npgsql)
- Swagger/OpenAPI
- JWT (JSON Web Tokens) para autenticação

## Swagger

A API utiliza o **Swashbuckle** (Swagger) para documentação interativa. Com a aplicação em execução, acesse:

- **Swagger UI:** `/swagger`
- **Documento OpenAPI (JSON):** `/swagger/v1/swagger.json`

Através do Swagger UI você pode visualizar todos os endpoints disponíveis, testar as requisições diretamente no navegador e consultar os modelos de dados utilizados pela API.

## Rotas da API

Base path: `api/v1/estudantes`

| Método | Rota | Descrição |
|--------|------|-----------|
| `POST` | `/api/v1/estudantes` | Cadastra um novo aluno e retorna um token JWT |
| `GET` | `/api/v1/estudantes` | Lista todos os alunos cadastrados |
| `GET` | `/api/v1/estudantes/login?email={email}&password={password}` | Realiza login do aluno e retorna um token JWT |

## Autenticação

A API utiliza autenticação JWT. Para endpoints protegidos, inclua o token no header da requisição:

```
Authorization: Bearer {seu-token-jwt}
```

## Executando o projeto

1. Certifique-se de ter o PostgreSQL configurado com o banco `db_employeer`
2. Atualize a connection string em `Program.cs` se necessário
3. Execute o projeto:

```bash
dotnet run
```
