README.md – Projeto Ambev Developer Evaluation

# Ambev Developer Evaluation

Este projeto foi desenvolvido como parte do processo seletivo da Ambev Tech. A aplicação segue os princípios da *Clean Architecture*, utilizando C#, .NET 8, Entity Framework Core, PostgreSQL, testes automatizados (unitários e de integração) e autenticação via JWT.

---

## Estrutura do Projeto


src/
├── Ambev.DeveloperEvaluation.WebApi       # Camada de apresentação (API)
├── Ambev.DeveloperEvaluation.Application  # Regras de negócio (Services, DTOs, Interfaces)
├── Ambev.DeveloperEvaluation.Domain       # Entidades e interfaces de repositório
├── Ambev.DeveloperEvaluation.ORM          # Persistência com Entity Framework (PostgreSQL)
├── Ambev.DeveloperEvaluation.IoC          # Injeção de dependência (Module Initializers)
├── Tests/
│   ├── Unit/                               # Testes unitários (Application & Domain)
│   ├── Integration/                        # Testes de integração com banco real (Docker)
│   └── Functional/                         # (Reservado para testes de API)
└── docker-compose/                        # Arquivos Docker


---

 Tecnologias Utilizadas

.NET 8

ASP.NET Core

Entity Framework Core

PostgreSQL

Docker + Docker Compose

Testcontainers para testes de integração

FluentAssertions & Moq

xUnit

JWT Authentication

AutoMapper

MediatR



---

Como Rodar o Projeto

Pré-requisitos

.NET 8 SDK

Docker


1. Subir o PostgreSQL via Docker

docker-compose -f docker-compose.yml up -d

2. Criar as Migrations

cd src/Ambev.DeveloperEvaluation.ORM
dotnet ef migrations add InitialCreate --project Ambev.DeveloperEvaluation.ORM --startup-project ../Ambev.DeveloperEvaluation.WebApi
dotnet ef database update --project Ambev.DeveloperEvaluation.ORM --startup-project ../Ambev.DeveloperEvaluation.WebApi

3. Rodar a API

Via Visual Studio (F5) ou terminal:
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi

A API estará disponível em:
https://localhost:7181/swagger/index.html


---

Testes Automatizados

Testes Unitários
dotnet test tests/Ambev.DeveloperEvaluation.Unit
Testes de Integração (com PostgreSQL real)
dotnet test tests/Ambev.DeveloperEvaluation.Integration
Estes testes utilizam o Testcontainers para rodar uma instância real do PostgreSQL automaticamente via Docker.


---

Funcionalidades Implementadas

[x] Cadastro de vendas com validação de regras de negócio
[x] Aplicação automática de descontos (10% e 20%)
[x] Limite de quantidade por item
[x] Listagem, atualização e cancelamento de vendas
[x] Separação entre controller, service e repository
[x] Testes unitários da camada Application e Domain
[x] Testes de integração com banco real
[x] Injeção de dependência modular (IoC)
[x] Swagger + JWT Authentication



---

 Considerações Finais

Este projeto foi estruturado para demonstrar domínio de:
Boas práticas de desenvolvimento com C# e .NET
Clean Architecture
Testes confiáveis (unit e integration)
Integração com infraestrutura real (banco Docker)
Manutenibilidade e clareza de código.
