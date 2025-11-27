-- Script de inicialização do banco de dados
-- Cria a tabela de contas e insere a conta de exemplo do desafio

CREATE TABLE IF NOT EXISTS "Contas" (
    "ContaNumero" INTEGER PRIMARY KEY,
    "Saldo" NUMERIC(18,2) NOT NULL
);

-- Insere a conta de exemplo usada no desafio (54321 com saldo inicial de 160)
INSERT INTO "Contas" ("ContaNumero", "Saldo") 
VALUES (54321, 160.00)
ON CONFLICT ("ContaNumero") DO NOTHING;

-- Insere algumas contas adicionais para testes
INSERT INTO "Contas" ("ContaNumero", "Saldo") 
VALUES 
    (12345, 1000.00),
    (67890, 500.00),
    (11111, 250.00)
ON CONFLICT ("ContaNumero") DO NOTHING;
