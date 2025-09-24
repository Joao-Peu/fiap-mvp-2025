# FIAP Cloud Games

> Plataforma para gestão de usuários e aquisição de jogos digitais, com autenticação JWT e persistência em banco de dados.

---

## 📋 Sobre o Projeto

O FIAP Cloud Games é uma API desenvolvida em .NET 8, voltada para o gerenciamento de usuários e jogos digitais. Permite cadastro, autenticação, aquisição de jogos e consulta da biblioteca do usuário. O projeto é estruturado em múltiplas camadas, seguindo boas práticas de arquitetura e separação de responsabilidades.

### Camadas do Projeto

- **FIAPCloudGames.API**: Camada de apresentação (Web API), responsável por expor endpoints REST para autenticação, usuários e jogos. Utiliza autenticação JWT, Swagger para documentação e middleware para tratamento global de erros.
- **FIAPCloudGames.Application**: Camada de aplicação, responsável pela lógica de negócio e orquestração dos serviços. Implementa regras de cadastro, autenticação, aquisição de jogos e manipulação dos dados recebidos dos DTOs.
- **FIAPCloudGames.Domain**: Camada de domínio, contém as entidades principais (User, Game), value objects (Email, Password) e interfaces de repositório. Centraliza as regras de negócio e validações essenciais.
- **FIAPCloudGames.Infra**: Camada de infraestrutura, responsável pela persistência dos dados via Entity Framework Core. Implementa os repositórios concretos e o DbContext.

---

## 🚀 Tecnologias e Ferramentas

### Backend
- **.NET 8.0**
- **ASP.NET Core**
- **Entity Framework Core 9.0.9** (InMemory para testes, pode ser adaptado para SQL Server)
- **Microsoft.AspNetCore.Authentication.JwtBearer 8.0.0**
- **Swashbuckle.AspNetCore 6.6.2** (Swagger)
- **System.IdentityModel.Tokens.Jwt 8.14.0**

### Testes
- Não há projeto de testes implementado neste repositório.

### DevOps & Ferramentas
- **Swagger/OpenAPI** para documentação dos endpoints

---

## 📋 Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server) (opcional, para produção)
- [Git](https://git-scm.com/)

### IDEs Recomendadas
- **Visual Studio 2022**
- **Visual Studio Code**
- **JetBrains Rider**

---

## 🛠️ Instalação e Configuração

### 1. Clone o repositório
```bash
git clone https://github.com/seu-usuario/fiap-cloud-games.git
cd fiap-cloud-games
```

### 2. Execute a aplicação
```bash
dotnet build
cd FIAPCloudGames.API
dotnet run
```

Acesse o Swagger em `https://localhost:xxxx/swagger` para explorar os endpoints.

---

## 📁 Estrutura das Camadas

- **API**: Controllers para autenticação, usuários e jogos.
- **Application**: Serviços para regras de negócio e DTOs.
- **Domain**: Entidades, value objects e interfaces de repositório.
- **Infra**: DbContext e repositórios concretos.

---

## 📦 Principais Dependências

- Microsoft.AspNetCore.Authentication.JwtBearer: 8.0.0
- Microsoft.EntityFrameworkCore: 9.0.9
- Microsoft.EntityFrameworkCore.InMemory: 9.0.9
- Swashbuckle.AspNetCore: 6.6.2
- System.IdentityModel.Tokens.Jwt: 8.14.0

---

## ℹ️ Observações

- O projeto utiliza banco InMemory para facilitar testes e desenvolvimento local. Para produção, recomenda-se configurar SQL Server.
- O Swagger está habilitado apenas em ambiente de desenvolvimento.
- Não há frontend implementado neste repositório.

---

## 📄 Licença

Este projeto é apenas para fins educacionais na Pós-Graduação FIAP.
