# FIAP Cloud Games (FCG)

> Plataforma de venda de jogos digitais e gestão de biblioteca de jogos adquiridos - MVP Fase 1

---

## 📋 Sobre o Projeto

A FIAP Cloud Games é uma plataforma de games voltada para a educação de tecnologia. Este projeto representa a **Fase 1** do MVP, desenvolvido como Tech Challenge da FIAP.

### Problema que Resolve
- Gestão de usuários e biblioteca de jogos
- Autenticação e autorização com diferentes níveis de acesso
- Base sólida para futuras funcionalidades como matchmaking e gerenciamento de servidores

### Principais Funcionalidades
- ✅ Cadastro de usuários com validações robustas
- ✅ Autenticação via JWT com controle de acesso
- ✅ CRUD completo para jogos
- ✅ Biblioteca pessoal de jogos adquiridos
- ✅ API REST documentada com Swagger
- ✅ Middleware para tratamento de erros e logs

### Público-Alvo
Estudantes e profissionais da FIAP, Alura e PM3 interessados em tecnologia e jogos educacionais.

---

## 🚀 Tecnologias e Ferramentas

### Backend
- **.NET 8** - Framework principal
- **ASP.NET Core** - API REST
- **Entity Framework Core** - ORM para persistência
- **SQL Server** - Banco de dados principal
- **JWT Bearer** - Autenticação e autorização

### Arquitetura
- **Domain-Driven Design (DDD)** - Organização do domínio
- **Clean Architecture** - Separação de responsabilidades
- **Repository Pattern** - Abstração de acesso a dados

### Testes
- **xUnit** - Framework de testes
- **NSubstitute** - Mocking
- **Bogus** - Geração de dados fake
- **82 testes unitários** - Cobertura das principais regras de negócio

### DevOps & Ferramentas
- **Swagger/OpenAPI** - Documentação de API
- **Serilog** - Logging estruturado
- **Entity Framework Migrations** - Controle de versão do banco

---

## 📋 Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server) ou LocalDB
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

### 2. Restaurar dependências
```bash
dotnet restore
```

### 3. Configurar banco de dados
```bash
# Aplicar migrations
cd src/FIAPCloudGames.API
dotnet ef database update
```

### 4. Criar usuário administrador inicial
```bash
# Executar SQL manualmente
sqlcmd -S "(localdb)\mssqllocaldb" -d CloudGamesDb -i scripts/insert-admin-user.sql
```

**Credenciais do Admin criado:**
- 📧 **Email**: `admin@fiap.com`
- 🔑 **Senha**: `Admin@123`
- 👤 **Role**: `Admin`

⚠️ **Importante**: Altere a senha após o primeiro login!

### 5. Executar a aplicação
```bash
cd src/FIAPCloudGames.API
dotnet run
```

### 6. Acessar a documentação
- **Swagger UI**: https://localhost:5001/swagger
- **API Base URL**: https://localhost:5001/api

---

## 🧪 Executando os Testes

```bash
# Executar todos os testes
dotnet test

# Executar testes com detalhes
dotnet test --verbosity normal

# Gerar relatório de cobertura
dotnet test --collect:"XPlat Code Coverage"
```

**Resultado Atual**: 82 testes - ✅ Todos passando

---

## 🏗️ Estrutura do Projeto

```
src/
├── FIAPCloudGames.API/          # Camada de apresentação (Controllers, Middleware)
├── FIAPCloudGames.Application/  # Camada de aplicação (Services, DTOs)
├── FIAPCloudGames.Domain/       # Camada de domínio (Entities, Value Objects)
└── FIAPCloudGames.Infra/        # Camada de infraestrutura (Repositories, DbContext)

test/
└── FIAPCloudGames.Tests/        # Testes unitários
```

---

## 🔐 Autenticação e Autorização

### Níveis de Acesso
- **Usuário**: Acesso à plataforma e biblioteca de jogos
- **Administrador**: Pode cadastrar jogos e administrar usuários

### Endpoints Principais

#### Autenticação
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "usuario@example.com",
  "password": "MinhaSenh@123"
}
```

#### Usuários
```http
GET    /api/users           # Listar usuários (Admin)
POST   /api/users           # Cadastrar usuário
GET    /api/users/{id}      # Buscar usuário por ID
PUT    /api/users/{id}      # Atualizar usuário
DELETE /api/users/{id}      # Deletar usuário (Admin)
```

#### Jogos
```http
GET    /api/games           # Listar jogos
POST   /api/games           # Cadastrar jogo (Admin)
GET    /api/games/{id}      # Buscar jogo por ID
PUT    /api/games/{id}      # Atualizar jogo (Admin)
DELETE /api/games/{id}      # Deletar jogo (Admin)
```

#### Biblioteca
```http
GET    /api/library/{userId}        # Ver biblioteca do usuário
POST   /api/library/{userId}/games  # Adquirir jogo
```

---

## 📊 Qualidade de Software

### Validações Implementadas
- ✅ **Email**: Formato válido obrigatório
- ✅ **Senha**: Mínimo 8 caracteres, letras, números e caracteres especiais
- ✅ **Nome**: Não pode ser vazio ou nulo
- ✅ **Preço do Jogo**: Não pode ser negativo

### Patterns Aplicados
- ✅ **Repository Pattern**: Abstração de acesso a dados
- ✅ **Service Pattern**: Regras de negócio
- ✅ **Value Objects**: Email e Password
- ✅ **Exception Handling**: Middleware global

### Metodologias de Teste
- ✅ **Test-Driven Development (TDD)**: Aplicado no módulo de usuários
- ✅ **Testes Unitários**: Cobertura completa das regras de negócio
- ✅ **Mocking**: Isolamento de dependências nos testes

---

## 🎯 Funcionalidades da Fase 1

### ✅ Implementadas
- [x] API REST em .NET 8
- [x] Cadastro de usuários com validações
- [x] Autenticação JWT com níveis de acesso
- [x] CRUD de jogos
- [x] Biblioteca de jogos adquiridos
- [x] Persistência com Entity Framework Core
- [x] Migrations aplicadas
- [x] Testes unitários (82 testes)
- [x] TDD aplicado no módulo de usuários
- [x] Middleware de tratamento de erros
- [x] Documentação Swagger
- [x] Arquitetura DDD

### 🎯 Próximas Fases
- [ ] Matchmaking para partidas online
- [ ] Gerenciamento de servidores
- [ ] Sistema de promoções
- [ ] Integração com sistemas de pagamento

---

## 🤝 Como Contribuir

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

---

## 📝 Licença

Este projeto é desenvolvido para fins educacionais no Tech Challenge da FIAP.

---

## 👥 Equipe de Desenvolvimento

Desenvolvido como projeto acadêmico do Tech Challenge - FIAP

---

## 📞 Suporte

Em caso de dúvidas ou problemas, entre em contato através do Discord da FIAP.

---

## 🎮 FIAP Cloud Games - Educar através dos jogos! 🎮
