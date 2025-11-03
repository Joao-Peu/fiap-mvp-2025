-- =============================================================================
-- Script para inserir usuário administrador inicial
-- FIAP Cloud Games - Sistema de Gestão de Jogos
-- =============================================================================
-- 
-- Este script cria o primeiro usuário administrador para permitir
-- o acesso inicial à API e gestão do sistema.
--
-- CREDENCIAIS DO ADMIN:
-- Email: admin@fiap.com
-- Senha: Admin@123
-- Role: Admin
--
-- COMO EXECUTAR:
-- 1. Conecte-se ao banco CloudGamesDb
-- 2. Execute este script
-- 3. Faça login na API usando as credenciais acima
--
-- =============================================================================

USE CloudGamesDb;
GO

-- Verifica se já existe um usuário admin para evitar duplicatas
IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = 'admin@fiap.com')
BEGIN
    -- Insere o usuário administrador inicial
    INSERT INTO Users (
        Id,
        Name,
        Email,
        Password,
        Role,
        IsActive
    )
    VALUES (
        NEWID(),                    -- Gera um novo GUID para o ID
        'Administrador do Sistema', -- Nome do usuário
        'admin@fiap.com',          -- Email para login
        'Admin@123',               -- Senha (?? Em produção, use hash!)
        'Admin',                   -- Role como string
        1                          -- IsActive = true
    );
    
    PRINT '? Usuário administrador criado com sucesso!';
    PRINT '?? Email: admin@fiap.com';
    PRINT '?? Senha: Admin@123';
    PRINT '';
    PRINT '??  IMPORTANTE: Altere a senha após o primeiro login!';
END
ELSE
BEGIN
    PRINT '??  Usuário admin já existe no sistema.';
    PRINT '?? Email: admin@fiap.com';
END

-- Exibe informações do usuário criado/existente
SELECT 
    Id,
    Name,
    Email,
    Role,
    IsActive,
    'Use esta informação para fazer login na API' as Observacao
FROM Users 
WHERE Email = 'admin@fiap.com';

GO