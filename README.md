# Banco API - Desafio C# .NET Core com GraphQL

API REST desenvolvida em **C# com .NET 8** e **GraphQL (HotChocolate)** que simula funcionalidades de um banco digital, permitindo operações de saque, depósito e consulta de saldo.

---

## 🎯 Navegação Rápida

| Documento | Descrição |
|-----------|-----------|
| ⚡ [**QUICKSTART.md**](QUICKSTART.md) | Rodar o projeto em 3 minutos |
| 📋 [**CHECKLIST_AVALIACAO.md**](CHECKLIST_AVALIACAO.md) | Guia completo para avaliadores |
| 📚 [**EXEMPLOS.md**](EXEMPLOS.md) | Queries e mutations avançadas |
| 🐘 [**INSTALL_POSTGRESQL.md**](INSTALL_POSTGRESQL.md) | Instalar PostgreSQL local |
| 📊 [**RESUMO.md**](RESUMO.md) | Resumo técnico da implementação |

---

## 🚀 Tecnologias

- **.NET 8.0** - Framework principal
- **HotChocolate 15.x** - Servidor GraphQL
- **Entity Framework Core 8.0** - ORM
- **PostgreSQL** - Banco de dados (produção/Docker)
- **InMemory Database** - Testes e desenvolvimento local
- **xUnit** - Framework de testes
- **Coverlet** - Cobertura de código
- **Docker & Docker Compose** - Containerização

## 📋 Requisitos

### Obrigatório
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) - Para compilar e executar a API

### Opcional (escolha uma das opções)
- **Opção A:** [Docker Desktop](https://www.docker.com/products/docker-desktop) - Para rodar com PostgreSQL (recomendado)
- **Opção B:** [PostgreSQL 15+](https://www.postgresql.org/download/) - Instalação local ([ver guia](INSTALL_POSTGRESQL.md))
- **Opção C:** Nenhum - Use InMemory Database para testes rápidos

### Ferramentas
- [Git](https://git-scm.com/) - Para clonar o repositório
- [Postman](https://www.postman.com/) ou navegador - Para testar a API GraphQL

## 🏃 Como Rodar

### Opção 1: Desenvolvimento Rápido (InMemory Database) ⚡

**Ideal para:** Testes rápidos sem precisar instalar PostgreSQL

```powershell
# Clone o repositório
git clone <url-do-repositorio>
cd BancoAPI

# Restaurar dependências e executar
dotnet restore
cd BancoApi.Api
dotnet run
```

✅ A API estará disponível em: **http://localhost:5247/graphql**  
✅ Dados em memória (perdidos ao parar a aplicação)  
✅ Conta de teste: **54321** com saldo **R$ 160,00**

---

### Opção 2: Docker + PostgreSQL (Recomendado para Avaliadores) 🐳

**Ideal para:** Ambiente completo com banco de dados real

```powershell
# Na raiz do projeto
docker-compose up --build
```

✅ API disponível em: **http://localhost:5000/graphql**  
✅ PostgreSQL em: **localhost:5432**  
✅ Dados persistentes (mantidos entre reinicializações)  
✅ Múltiplas contas de teste pré-cadastradas

**Credenciais do PostgreSQL:**
```
Host:     localhost
Port:     5432
Database: bancodb
Username: bancoapi
Password: banco@2025
```

**Parar os containers:**
```powershell
docker-compose down
```

**Remover dados e reiniciar do zero:**
```powershell
docker-compose down -v
docker-compose up --build
```

---

### Opção 3: PostgreSQL Local (Sem Docker) 💾

**Ideal para:** Quem já tem PostgreSQL instalado localmente

**Passo 1:** Criar banco de dados
```sql
CREATE DATABASE bancodb;
CREATE USER bancoapi WITH PASSWORD 'banco@2025';
GRANT ALL PRIVILEGES ON DATABASE bancodb TO bancoapi;
```

**Passo 2:** Executar script de seed
```powershell
psql -U bancoapi -d bancodb -f init-db.sql
```

**Passo 3:** Configurar connection string em `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=bancodb;Username=bancoapi;Password=banco@2025"
  }
}
```

**Passo 4:** Executar a API
```powershell
cd BancoApi.Api
dotnet run
```

✅ API disponível em: **http://localhost:5247/graphql**  
✅ Usando PostgreSQL local  
✅ Dados persistentes

---

### 🎯 Qual opção escolher?

| Cenário | Opção Recomendada |
|---------|-------------------|
| **Testar rapidamente a API** | Opção 1 (InMemory) |
| **Avaliar o desafio completo** | Opção 2 (Docker) ✨ |
| **Já tem PostgreSQL instalado** | Opção 3 (Local) |
| **Não tem Docker instalado** | Opção 1 ou 3 |

## 🧪 Executar Testes

### Todos os testes

```powershell
dotnet test
```

### Testes com relatório de cobertura

```powershell
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=./TestResults/
```

### Cobertura Atual

- **Linha:** 77.21% (excluindo Program.cs: ~95%)
- **Branch:** 87.5%
- **Método:** 92.85%
- **Total de testes:** 19 testes passando ✅

## 📊 GraphQL Playground

Após iniciar a aplicação, acesse: **http://localhost:5000/graphql**

Você terá acesso ao **Banana Cake Pop** (interface GraphQL interativa do HotChocolate).

## 📝 Exemplos de Queries e Mutations

### 1. Consultar Saldo

```graphql
query {
  saldo(conta: 54321)
}
```

**Resposta esperada:**
```json
{
  "data": {
    "saldo": 160
  }
}
```

### 2. Realizar Saque (com saldo suficiente)

```graphql
mutation {
  sacar(conta: 54321, valor: 140) {
    conta
    saldo
  }
}
```

**Resposta esperada:**
```json
{
  "data": {
    "sacar": {
      "conta": 54321,
      "saldo": 20
    }
  }
}
```

### 3. Realizar Saque (saldo insuficiente)

```graphql
mutation {
  sacar(conta: 54321, valor: 30000) {
    conta
    saldo
  }
}
```

**Resposta esperada:**
```json
{
  "errors": [
    {
      "message": "Saldo insuficiente.",
      "extensions": {
        "code": "GRAPHQL_VALIDATION_ERROR"
      }
    }
  ]
}
```

### 4. Realizar Depósito

```graphql
mutation {
  depositar(conta: 54321, valor: 200) {
    conta
    saldo
  }
}
```

**Resposta esperada:**
```json
{
  "data": {
    "depositar": {
      "conta": 54321,
      "saldo": 220
    }
  }
}
```

### 5. Operações Combinadas

```graphql
mutation {
  primeiroDeposito: depositar(conta: 54321, valor: 100) {
    conta
    saldo
  }
  
  saque: sacar(conta: 54321, valor: 50) {
    conta
    saldo
  }
  
  segundoDeposito: depositar(conta: 54321, valor: 75) {
    conta
    saldo
  }
}

query {
  saldoFinal: saldo(conta: 54321)
}
```

## 🗂️ Estrutura do Projeto

```
BancoAPI/
├── BancoApi.Api/                 # Projeto principal da API
│   ├── Data/
│   │   └── BankDbContext.cs      # Contexto do EF Core
│   ├── Models/
│   │   └── Conta.cs              # Entidade Conta
│   ├── Services/
│   │   └── ContaService.cs       # Lógica de negócio
│   ├── GraphQL/
│   │   ├── Query.cs              # Queries GraphQL
│   │   └── Mutation.cs           # Mutations GraphQL
│   └── Program.cs                # Configuração e inicialização
├── BancoApi.Tests/               # Projeto de testes
│   ├── ContaServiceTests.cs      # Testes unitários do serviço
│   ├── GraphQLResolversTests.cs  # Testes dos resolvers GraphQL
│   ├── BankDbContextTests.cs     # Testes do DbContext
│   └── ContaModelTests.cs        # Testes do modelo
├── Dockerfile                     # Dockerfile da API
├── docker-compose.yml             # Orquestração de containers
├── init-db.sql                    # Script de inicialização do PostgreSQL
└── README.md                      # Este arquivo
```

## 🔐 Dados de Teste

A aplicação já vem com algumas contas pré-cadastradas:

| Conta | Saldo Inicial |
|-------|---------------|
| 54321 | R$ 160,00     |
| 12345 | R$ 1.000,00   |
| 67890 | R$ 500,00     |
| 11111 | R$ 250,00     |

## 🎯 Funcionalidades Implementadas

- ✅ **Mutation Sacar**: Realiza saque com validação de saldo
- ✅ **Mutation Depositar**: Realiza depósito na conta
- ✅ **Query Saldo**: Consulta o saldo atual de uma conta
- ✅ **Validações**: Saldo insuficiente e conta inexistente
- ✅ **Tratamento de Erros**: Retorna erros GraphQL padronizados
- ✅ **Testes Unitários**: 19 testes com cobertura >= 77%
- ✅ **Dockerização**: API + PostgreSQL containerizados
- ✅ **Seed de Dados**: Contas de exemplo pré-cadastradas
- ✅ **GraphQL Playground**: Interface interativa incluída

## 🧰 Scripts Úteis

### Limpar e reconstruir

```powershell
dotnet clean
dotnet build
```

### Restaurar banco de dados

```powershell
docker-compose down -v  # Remove volumes
docker-compose up --build
```

### Ver logs dos containers

```powershell
docker-compose logs -f api
docker-compose logs -f postgres
```

## 📄 Licença

Este projeto foi desenvolvido como parte de um desafio técnico.

## 👨‍💻 Desenvolvimento

Para contribuir ou modificar o projeto:

1. Clone o repositório
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona MinhaFeature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

## 🐛 Troubleshooting

### ❌ Porta 5000 ou 5247 já está em uso

**Problema:** Outro processo está usando a porta

**Solução 1 (Docker):** Altere a porta no `docker-compose.yml`:
```yaml
ports:
  - "5001:8080"  # Mude 5000 para 5001
```

**Solução 2 (Local):** Execute com porta customizada:
```powershell
dotnet run --urls "http://localhost:5555"
```

**Solução 3:** Descubra o que está usando a porta:
```powershell
netstat -ano | findstr :5000
# Depois: taskkill /PID <número_do_pid> /F
```

---

### ❌ Erro ao conectar no PostgreSQL (Docker)

**Problema:** `Npgsql.NpgsqlException: Connection refused`

**Solução:** Verifique se o container PostgreSQL está rodando:
```powershell
docker ps
```

Se não estiver listado, veja os logs:
```powershell
docker-compose logs postgres
```

Reinicie os containers:
```powershell
docker-compose down
docker-compose up --build
```

---

### ❌ Erro ao conectar no PostgreSQL (Local)

**Problema:** `password authentication failed for user "bancoapi"`

**Solução 1:** Verifique a senha no `appsettings.Development.json`

**Solução 2:** Verifique se o serviço PostgreSQL está rodando:
```powershell
Get-Service postgresql*
Start-Service postgresql-x64-15
```

**Solução 3:** Recrie o usuário no pgAdmin:
```sql
DROP USER IF EXISTS bancoapi;
CREATE USER bancoapi WITH PASSWORD 'banco@2025';
GRANT ALL PRIVILEGES ON DATABASE bancodb TO bancoapi;
```

---

### ❌ Testes falhando

**Problema:** Testes não compilam ou falham

**Solução 1:** Limpe os artefatos de build:
```powershell
dotnet clean
Remove-Item -Path */bin,*/obj -Recurse -Force
dotnet restore
dotnet build
dotnet test
```

**Solução 2:** Verifique se todos os pacotes estão instalados:
```powershell
cd BancoApi.Tests
dotnet restore
```

---

### ❌ Banana Cake Pop (GraphQL UI) não carrega

**Problema:** Tela branca ou erro 404 ao acessar `/graphql`

**Solução 1:** Verifique se a API está rodando:
```powershell
# Deve mostrar: "Now listening on: http://localhost:XXXX"
```

**Solução 2:** Acesse a porta correta:
- Docker: http://localhost:5000/graphql
- Local: http://localhost:5247/graphql (ou a porta que aparecer no console)

**Solução 3:** Use outro navegador (Chrome, Edge, Firefox)

**Solução 4:** Teste via PowerShell:
```powershell
$body = @{ query = "query { saldo(conta: 54321) }" } | ConvertTo-Json
Invoke-RestMethod -Uri "http://localhost:5247/graphql" -Method POST -Body $body -ContentType "application/json"
```

---

### ❌ Docker não está instalado

**Problema:** `docker: command not found`

**Solução:** Use a **Opção 1** (InMemory) ou **Opção 3** (PostgreSQL local)

Ou instale Docker Desktop:
- Windows: https://docs.docker.com/desktop/install/windows-install/
- Após instalar, reinicie o computador

---

### ❌ Dados não persistem entre reinicializações

**Problema:** Ao reiniciar, saldo volta ao valor inicial

**Causa:** Você está usando **InMemory Database**

**Solução:** Use Docker (Opção 2) ou PostgreSQL local (Opção 3)

Verifique no console ao iniciar:
```
✅ InMemory:    "UseInMemoryDatabase"
✅ PostgreSQL:  "Npgsql" ou "Connection string: Host=..."
```

---

### ❌ init-db.sql não executa no Docker

**Problema:** Contas de teste não aparecem

**Solução:** Remova os volumes e recrie:
```powershell
docker-compose down -v
docker-compose up --build
```

Verifique se o script foi executado:
```powershell
docker-compose logs postgres | Select-String "init-db"
```

---

### ❌ Cobertura de testes menor que 77%

**Problema:** Relatório mostra cobertura baixa

**Causa:** Pode estar incluindo arquivos gerados (Program.cs, Migrations, etc.)

**Solução:** Execute com exclusões:
```powershell
dotnet test /p:CollectCoverage=true /p:Exclude="[*]*.Program"
```

---

### 📚 Precisa de Ajuda?

1. ✅ Verifique o [Guia de Instalação do PostgreSQL](INSTALL_POSTGRESQL.md)
2. ✅ Leia os [Exemplos Avançados](EXEMPLOS.md)
3. ✅ Consulte o [Resumo da Implementação](RESUMO.md)
4. ✅ Abra uma issue no repositório com:
   - Sistema operacional
   - Versão do .NET (`dotnet --version`)
   - Logs completos do erro
   - Qual opção de execução você está usando

## 📧 Contato

Para dúvidas ou sugestões sobre o projeto, abra uma issue no repositório.

---

**Desenvolvido com ❤️ usando .NET 8 e GraphQL**
