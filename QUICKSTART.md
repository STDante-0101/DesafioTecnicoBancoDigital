# ⚡ Quick Start - Banco API

Guia rápido para rodar o projeto em **menos de 5 minutos**.

---

## 🚀 Opção 1: Docker (Recomendado)

**Pré-requisito:** Docker instalado

```powershell
# 1. Clonar o repositório
git clone <URL_DO_REPOSITORIO>
cd BancoAPI

# 2. Subir a aplicação
docker-compose up --build

# 3. Acessar
# Abra no navegador: http://localhost:5000/graphql
```

✅ **Pronto!** PostgreSQL + API rodando

---

## ⚡ Opção 2: Sem Docker (Mais Rápido)

**Pré-requisito:** .NET 8 SDK instalado

```powershell
# 1. Clonar o repositório
git clone <URL_DO_REPOSITORIO>
cd BancoAPI

# 2. Executar
cd BancoApi.Api
dotnet run

# 3. Acessar
# Abra no navegador: http://localhost:5247/graphql
```

✅ **Pronto!** API com InMemory Database

---

## 🧪 Testar os 4 Cenários

Cole cada query no **Banana Cake Pop** (interface GraphQL que abre no navegador):

### 1️⃣ Consultar saldo inicial
```graphql
query {
  saldo(conta: 54321)
}
```
**Resultado:** `160`

### 2️⃣ Sacar 140
```graphql
mutation {
  sacar(conta: 54321, valor: 140) {
    contaNumero
    saldo
  }
}
```
**Resultado:** Saldo vai para `20`

### 3️⃣ Depositar 200
```graphql
mutation {
  depositar(conta: 54321, valor: 200) {
    contaNumero
    saldo
  }
}
```
**Resultado:** Saldo vai para `220`

### 4️⃣ Tentar sacar mais que o saldo
```graphql
mutation {
  sacar(conta: 54321, valor: 30000) {
    contaNumero
    saldo
  }
}
```
**Resultado:** Erro "Saldo insuficiente."

---

## ✅ Executar Testes

```powershell
# Na raiz do projeto
dotnet test
```

**Esperado:** 19 testes passando ✅

---

## 📚 Quer Mais Detalhes?

- **Instruções completas:** [`README.md`](README.md)
- **Queries avançadas:** [`EXEMPLOS.md`](EXEMPLOS.md)
- **Checklist de avaliação:** [`CHECKLIST_AVALIACAO.md`](CHECKLIST_AVALIACAO.md)
- **Instalar PostgreSQL local:** [`INSTALL_POSTGRESQL.md`](INSTALL_POSTGRESQL.md)
- **Resumo técnico:** [`RESUMO.md`](RESUMO.md)

---

## 🐛 Problemas?

### Porta em uso
```powershell
# Execute com outra porta
dotnet run --urls "http://localhost:5555"
```

### Docker não funciona
```powershell
# Use InMemory (Opção 2)
cd BancoApi.Api
dotnet run
```

### Interface GraphQL não carrega
- Verifique se a API está rodando (deve aparecer "Now listening on...")
- Acesse a porta correta mostrada no console
- Teste via PowerShell:
```powershell
$body = @{ query = "query { saldo(conta: 54321) }" } | ConvertTo-Json
Invoke-RestMethod -Uri "http://localhost:5247/graphql" -Method POST -Body $body -ContentType "application/json"
```

---

**Tempo total de setup: ~3 minutos** ⏱️

**Dúvidas?** Consulte o [README.md](README.md) completo!
