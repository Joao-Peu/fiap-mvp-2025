# Funcionalidades Implantadas

- API REST desenvolvida em .NET 8
- Estrutura monolítica seguindo padrões DDD
- Cadastro de usuários (nome, e-mail, senha)
- Validação de formato de e-mail
- Validação de senha segura (mínimo 8 caracteres, letras, números e caracteres especiais)
- Autenticação via token JWT
- Controle de acesso por nível (Usuário/Admin) nos endpoints
- Separação de controllers para autenticação, usuários e jogos
- DTOs definidos fora dos controllers
- Endpoints CRUD para jogos implementados
- Middleware global para tratamento de erros e logs estruturados
- Persistência de dados com Entity Framework Core
- Repositórios para usuários e jogos implementados
- Serviços ajustados para persistência real via EF Core
- Endpoints para biblioteca de jogos adquiridos por usuário implementados
- Relacionamento User <-> Game para biblioteca de jogos adquirido
- Documentação dos endpoints com Swagger aprimorada (comentários XML nos controllers e DTOs)

# Funcionalidades a Implementar 

- Aplicar migrations para criação do banco de dados
- Refatorar para Minimal API (se obrigatório)
- Testes unitários das principais regras de negócio
- Aplicar TDD ou BDD em pelo menos um módulo
- Modelar domínio utilizando Event Storming para fluxos de usuários e jogos
