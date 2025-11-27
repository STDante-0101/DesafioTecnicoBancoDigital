# 🐘 Guia de Instalação do PostgreSQL

Este guia mostra como instalar e configurar o PostgreSQL localmente no Windows para usar com a BancoAPI.

## ⚡ Opção Rápida: Usar Docker (Recomendado)

Se você já tem Docker instalado, **não precisa instalar PostgreSQL**! Basta usar:

```powershell
docker-compose up --build
```

Tudo será configurado automaticamente! ✨

---

## 📦 Instalação Manual do PostgreSQL no Windows

### Passo 1: Download

1. Acesse: https://www.postgresql.org/download/windows/
2. Clique em "Download the installer"
3. Escolha a versão **15.x** ou superior (compatível com a API)
4. Baixe o instalador para Windows

### Passo 2: Instalação

1. Execute o instalador baixado
2. Clique em **Next** nas primeiras telas
3. **Escolha o diretório** de instalação (padrão: `C:\Program Files\PostgreSQL\15`)
4. **Selecione os componentes** (deixe todos marcados):
   - PostgreSQL Server
   - pgAdmin 4
   - Command Line Tools
5. **Diretório de dados**: Deixe o padrão
6. **Senha do superusuário (postgres)**: Crie uma senha forte e **anote**!
7. **Porta**: Deixe `5432` (padrão)
8. **Locale**: Deixe o padrão
9. Clique em **Next** e depois em **Install**
10. Aguarde a instalação concluir

### Passo 3: Configurar PATH (Opcional)

Para usar comandos `psql` no terminal:

1. Abra **Configurações do Sistema** → **Variáveis de Ambiente**
2. Em **Variáveis do Sistema**, encontre **Path** e clique em **Editar**
3. Clique em **Novo** e adicione: `C:\Program Files\PostgreSQL\15\bin`
4. Clique em **OK** em todas as janelas
5. **Reinicie o PowerShell**

### Passo 4: Testar Instalação

Abra um novo PowerShell e execute:

```powershell
psql --version
```

Deve aparecer algo como: `psql (PostgreSQL) 15.x`

---

## ⚙️ Configuração para a BancoAPI

### Passo 1: Criar o Banco de Dados

Abra o **pgAdmin 4** (instalado junto com PostgreSQL):

1. Conecte-se ao servidor local (senha que você criou)
2. Clique com botão direito em **Databases** → **Create** → **Database**
3. **Database name**: `bancodb`
4. **Owner**: `postgres`
5. Clique em **Save**

### Passo 2: Criar o Usuário da Aplicação

No pgAdmin, abra o **Query Tool** (Tools → Query Tool) e execute:

```sql
-- Criar usuário
CREATE USER bancoapi WITH PASSWORD 'banco@2025';

-- Dar permissões
GRANT ALL PRIVILEGES ON DATABASE bancodb TO bancoapi;

-- Conectar ao banco bancodb
\c bancodb

-- Dar permissões no schema public
GRANT ALL ON SCHEMA public TO bancoapi;
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO bancoapi;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO bancoapi;
```

### Passo 3: Popular o Banco com Dados de Teste

No terminal, navegue até a pasta do projeto e execute:

```powershell
cd "C:\Users\Nay Safada\Desktop\BancoAPI"
psql -U bancoapi -d bancodb -f init-db.sql
```

Quando pedir a senha, digite: `banco@2025`

### Passo 4: Configurar a Connection String

Edite o arquivo `BancoApi.Api\appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=bancodb;Username=bancoapi;Password=banco@2025"
  }
}
```

---

## ✅ Testando a Conexão

Execute a API:

```powershell
cd BancoApi.Api
dotnet run
```

Você deve ver no log:

```
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (Xms) [Parameters=[], CommandType='Text', CommandTimeout='30']
```

Se aparecer erros de conexão, verifique:
- PostgreSQL está rodando? (Serviço "postgresql-x64-15")
- Credenciais corretas no appsettings.json?
- Banco `bancodb` foi criado?

---

## 🔧 Comandos Úteis do PostgreSQL

### Verificar se PostgreSQL está rodando

```powershell
Get-Service postgresql*
```

### Iniciar o serviço

```powershell
Start-Service postgresql-x64-15
```

### Parar o serviço

```powershell
Stop-Service postgresql-x64-15
```

### Conectar via linha de comando

```powershell
psql -U bancoapi -d bancodb
```

### Listar tabelas

```sql
\dt
```

### Ver dados da tabela Contas

```sql
SELECT * FROM "Contas";
```

### Sair do psql

```sql
\q
```

---

## 🐛 Problemas Comuns

### Erro: "psql: error: connection to server... failed"

**Solução:** PostgreSQL não está rodando. Execute:
```powershell
Start-Service postgresql-x64-15
```

### Erro: "password authentication failed"

**Solução:** Verifique a senha no `appsettings.Development.json`

### Erro: "database 'bancodb' does not exist"

**Solução:** Crie o banco conforme Passo 1 da configuração

### Porta 5432 já está em uso

**Solução:** Outro serviço está usando a porta. Verifique com:
```powershell
netstat -ano | findstr :5432
```

---

## 🎯 Alternativas Mais Simples

Se a instalação parecer complexa, considere:

### 1. **Usar Docker** (Mais Fácil!)
```powershell
docker-compose up
```
✅ Não precisa instalar PostgreSQL  
✅ Tudo configurado automaticamente  
✅ Isola o ambiente

### 2. **Usar InMemory Database** (Desenvolvimento Rápido)
```powershell
# Deixe appsettings.Development.json com:
"DefaultConnection": ""

dotnet run
```
✅ Zero configuração  
✅ Funciona instantaneamente  
⚠️  Dados perdidos ao reiniciar

---

## 📚 Referências

- [PostgreSQL Documentation](https://www.postgresql.org/docs/)
- [pgAdmin Documentation](https://www.pgadmin.org/docs/)
- [Npgsql Documentation](https://www.npgsql.org/doc/)

---

**Dica:** Para o desafio, recomendamos usar **Docker** (Opção 2 do README) que já vem 100% configurado! 🚀
