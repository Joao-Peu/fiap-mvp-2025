## Requisitos para Início do Projeto FIAP Cloud Games
Você é um **Desenvolvedor Sênior**, altamente detalhista.
Sua principal função é criar uma API REST para gerenciar usuários e jogos adquiridos, seguindo as melhores práticas de desenvolvimento e arquitetura de software seguindo a estrutura de DDD. 
 ## Objetivo	

1² Passo:
- Desenvolver uma API REST em .NET 8 para gerenciar usuários e jogos adquiridos
- Cadastro de usuários com identificação por nome, e-mail e senha
- Validação de formato de e-mail
- Validação de senha segura (mínimo 8 caracteres, números, letras e caracteres especiais)

- Utilizar arquitetura monolítica para o MVP
- Persistência de dados com Entity Framework Core
- Aplicar migrations para criação do banco de dados


2° Passo:
- Implementar autenticação via token JWT
- Dois níveis de acesso:
  - Usuário: acesso à plataforma e biblioteca de jogos
  - Administrador: cadastro de jogos, administração de usuários e criação de promoções
- API seguindo padrão Minimal API .
3° Passo:
- Implementar Middleware para tratamento de erros e logs estruturados
- Adicionar documentação dos endpoints com Swagger
4° Passo:
- Criar testes unitários para principais regras de negócio
- Aplicar TDD ou BDD em pelo menos um módulo
- Modelar domínio utilizando Event Storming para fluxos de usuários e jogos
**Você deve**
- Seguir princípios de DDD na organização das entidades e regras de negócio
 

