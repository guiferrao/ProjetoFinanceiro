# 💰 Sistema Financeiro API

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC292B?logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server/)
[![Docker](https://img.shields.io/badge/Docker-2496ED?logo=docker&logoColor=white)](https://www.docker.com/)
[![Swagger](https://img.shields.io/badge/Scalar-OpenAPI-85EA2D?logo=openapiinitiative&logoColor=black)](https://github.com/scalar/scalar)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

API RESTful robusta desenvolvida para gestão financeira pessoal, implementando autenticação stateless via JWT, arquitetura em camadas (Clean Architecture) e documentação moderna nativa utilizando o Scalar no ecossistema do .NET 10.

---

## 📑 Sumário

- [Visão Geral e Arquitetura](#-visão-geral-e-arquitetura)
- [Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [Funcionalidades Principais](#-funcionalidades-principais)
- [Estrutura do Repositório](#-estrutura-do-repositório)
- [Configurações e Variáveis](#-configurações-e-variáveis)
- [Como Executar Localmente](#-como-executar-localmente)
  - [Pré-requisitos](#pré-requisitos)
  - [Passo a Passo](#passo-a-passo)
- [Endpoints da API](#-endpoints-da-api)
- [Licença](#-licença)

---

## 🏛 Visão Geral e Arquitetura

O ecossistema isola as camadas de apresentação da API, processamento de negócios e persistência, garantindo a segurança dos dados de cada utilizador:

```text
                                [ Cliente HTTP / Navegador / Scalar ]
                                               │
                                               │ (HTTPS: Porta 7123)
                                               ▼
                                    ┌──────────────────────┐
                                    │Sistema Financeiro API│
                                    │ (ASP.NET Core .NET 10)│
                                    └──────────┬───────────┘
                                               │
                                               │ (TCP: Porta 1433 / EF Core)
                                               ▼
                                    ┌──────────────────────┐
                                    │     sqlserver-db     │
                                    │   (Docker Container) │
                                    └──────────────────────┘
```

1. **Backend (REST API):** API construída em ASP.NET Core (.NET 10), executando sobre o servidor Kestrel com injeção de dependência nativa, autenticação JWT e validação de requisições.
2. **Banco de Dados Relacional:** Microsoft SQL Server rodando em container Docker, com volume persistente e manipulado através do Entity Framework Core.
3. **Documentação (Scalar):** Interface visual moderna mapeando o OpenAPI, permitindo testes autenticados diretamente no navegador.

---

## 🛠 Tecnologias Utilizadas

### **Backend**
- **C# / .NET 10** — Plataforma backend moderna e de alta performance.
- **ASP.NET Core Web API** — Construção de controllers e rotas RESTful.
- **Entity Framework Core** — ORM para mapeamento de entidades, queries LINQ e migrações.
- **JWT (JSON Web Tokens) & PasswordHasher** — Autenticação stateless segura e hashing de senhas.
- **Scalar / OpenAPI** — Documentação e teste interativo dos endpoints com suporte nativo.

### **Infraestrutura & DevOps**
- **Docker** — Padronização de ambiente para o Banco de Dados.
- **Microsoft SQL Server 2022 Linux** — SGBD relacional conteinerizado.

---

## ✨ Funcionalidades Principais

- [x] **Autenticação & Autorização:** Registo de utilizadores, criptografia de senhas nativa e geração de tokens JWT seguros.
- [x] **Isolamento de Dados:** Validação baseada em Claims para garantir que um utilizador aceda e gira apenas as suas próprias transações.
- [x] **Gestão Financeira:** Operações completas de CRUD (Criação, Leitura, Atualização e Exclusão) de entradas e saídas.
- [x] **Segurança:** Proteção de endpoints com `[Authorize]` e middlewares nativos de interceptação.
- [x] **Documentação Nativa:** Substituição do antigo Swashbuckle pelo Scalar e `Microsoft.AspNetCore.OpenApi` no padrão .NET 10.

---

## 📂 Estrutura do Repositório

```text
ProjetoFinanceiro/
├── src/                                  # Solução Backend (.NET 10)
│   ├── SistemaFinanceiro.Api/            # Controllers, Middlewares, DI e Program.cs
│   ├── SistemaFinanceiro.Application/    # Casos de uso, DTOs e Interfaces de Serviços
│   ├── ProjetoFinanceiro.Domain/         # Entidades de Domínio, Enums e Contratos
│   └── ProjetoFinanceiro.Infrastructure/ # EF Core, DbContext, Migrations e Repositórios
│
├── .gitignore                            # Arquivos ignorados no Git
├── README.md                             # Documentação do projeto
└── src/SistemaFinanceiro.Api/appsettings.Local.json # Variáveis e chaves secretas (Não versionado)
```

---

## 🔐 Configurações e Variáveis

Crie um arquivo `appsettings.Local.json` na pasta do projeto da API (`src/SistemaFinanceiro.Api`) para armazenar as suas chaves locais:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=FinanceiroDb;User Id=sa;Password=SuaSenhaForte;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Secret": "SuaChaveSecretaComMaisDe32Caracteres"
  }
}
```

---

## 🚀 Como Executar Localmente

### Pré-requisitos
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) instalado.
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) ativo (para rodar o banco de dados).
- [Git](https://git-scm.com/) instalado.

---

### Passo a Passo

1. **Clonar o repositório:**
   ```bash
   git clone https://github.com/guiferrao/ProjetoFinanceiro.git
   cd ProjetoFinanceiro
   ```

2. **Subir o Banco de Dados:**
   Inicie um container do SQL Server utilizando o Docker:
   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SuaSenhaForte" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
   ```

3. **Configurar as credenciais:**
   Certifique-se de que o `appsettings.Local.json` está preenchido corretamente conforme a seção acima.

4. **Executar as Migrações (Opcional):**
   Caso precise de atualizar o banco de dados via Entity Framework:
   ```bash
   dotnet ef database update --project src/ProjetoFinanceiro.Infrastructure --startup-project src/SistemaFinanceiro.Api
   ```

5. **Iniciar a API:**
   ```bash
   dotnet run --project src/SistemaFinanceiro.Api
   ```

6. **Aceder à Documentação Visual:**
   Abra o navegador no endereço retornado no terminal, adicionando `/scalar/v1`:
   - `https://localhost:7123/scalar/v1`

---

## 📡 Endpoints da API

| Método | Rota | Descrição | Autenticação |
| :--- | :--- | :--- | :---: |
| `POST` | `/api/auth/register` | Criação de nova conta de utilizador | Pública |
| `POST` | `/api/auth/login` | Autenticação e retorno do token JWT | Pública |
| `GET` | `/api/transacoes` | Listagem de todas as transações do utilizador | Bearer JWT |
| `POST` | `/api/transacoes` | Registo de uma nova movimentação | Bearer JWT |
| `PUT` | `/api/transacoes/{id}` | Atualização dos dados de uma transação | Bearer JWT |
| `DELETE` | `/api/transacoes/{id}` | Exclusão de uma transação da conta | Bearer JWT |

---

## 📄 Licença

Este projeto é distribuído sob a licença [MIT](LICENSE).

---

<p align="center">
  Desenvolvido por <strong>Guilherme de Oliveira Ferrão</strong> 🚀
</p>