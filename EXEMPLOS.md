# Exemplos Avançados de Queries GraphQL

Este documento contém exemplos adicionais de queries e mutations para testar a API.

## Variáveis em GraphQL

### Exemplo com variáveis - Saque

**Query:**
```graphql
mutation SacarComVariaveis($numeroConta: Int!, $valorSaque: Decimal!) {
  sacar(conta: $numeroConta, valor: $valorSaque) {
    conta
    saldo
  }
}
```

**Variables:**
```json
{
  "numeroConta": 54321,
  "valorSaque": 50.00
}
```

### Exemplo com variáveis - Depósito

**Query:**
```graphql
mutation DepositarComVariaveis($numeroConta: Int!, $valorDeposito: Decimal!) {
  depositar(conta: $numeroConta, valor: $valorDeposito) {
    conta
    saldo
  }
}
```

**Variables:**
```json
{
  "numeroConta": 54321,
  "valorDeposito": 100.00
}
```

## Operações em Lote (Batch)

### Múltiplas operações em uma requisição

```graphql
mutation OperacoesEmLote {
  deposito1: depositar(conta: 54321, valor: 100) {
    conta
    saldo
  }
  
  saque1: sacar(conta: 54321, valor: 30) {
    conta
    saldo
  }
  
  deposito2: depositar(conta: 54321, valor: 50) {
    conta
    saldo
  }
}

query ConsultarSaldoFinal {
  saldoFinal: saldo(conta: 54321)
}
```

## Testes de Casos Extremos

### 1. Saque de valor zero

```graphql
mutation {
  sacar(conta: 54321, valor: 0) {
    conta
    saldo
  }
}
```

### 2. Depósito de valor grande

```graphql
mutation {
  depositar(conta: 54321, valor: 999999.99) {
    conta
    saldo
  }
}
```

### 3. Múltiplos saques até zerar conta

```graphql
mutation {
  saque1: sacar(conta: 12345, valor: 500) { conta saldo }
  saque2: sacar(conta: 12345, valor: 300) { conta saldo }
  saque3: sacar(conta: 12345, valor: 200) { conta saldo }
}

query {
  saldoFinal: saldo(conta: 12345)
}
```

### 4. Verificar saldo de conta inexistente

```graphql
query {
  saldo(conta: 99999)
}
```

**Resposta esperada:**
```json
{
  "errors": [
    {
      "message": "Conta não encontrada.",
      "extensions": {
        "code": "GRAPHQL_VALIDATION_ERROR"
      }
    }
  ]
}
```

## Introspection (Schema Explorer)

### Ver schema completo

```graphql
query SchemaIntrospection {
  __schema {
    types {
      name
      kind
      description
    }
  }
}
```

### Ver campos disponíveis em Query

```graphql
query {
  __type(name: "Query") {
    name
    fields {
      name
      type {
        name
        kind
      }
    }
  }
}
```

### Ver campos disponíveis em Mutation

```graphql
query {
  __type(name: "Mutation") {
    name
    fields {
      name
      args {
        name
        type {
          name
          kind
        }
      }
      type {
        name
        kind
      }
    }
  }
}
```

## Simulação de Fluxo Completo

### Cenário: Cliente recebe salário, paga contas e faz compras

```graphql
# 1. Recebe salário
mutation ReceberSalario {
  recebimentoSalario: depositar(conta: 54321, valor: 3000) {
    conta
    saldo
  }
}

# 2. Paga conta de luz
mutation PagarContaLuz {
  pagamentoLuz: sacar(conta: 54321, valor: 150) {
    conta
    saldo
  }
}

# 3. Paga conta de internet
mutation PagarInternet {
  pagamentoInternet: sacar(conta: 54321, valor: 89.90) {
    conta
    saldo
  }
}

# 4. Faz compras no supermercado
mutation CompraSupermercado {
  compraSupermerc: sacar(conta: 54321, valor: 450.50) {
    conta
    saldo
  }
}

# 5. Consulta saldo final
query SaldoFinal {
  saldoRestante: saldo(conta: 54321)
}
```

## Teste de Performance

### 10 operações sequenciais

```graphql
mutation TestePerformance {
  op1: depositar(conta: 67890, valor: 100) { saldo }
  op2: sacar(conta: 67890, valor: 10) { saldo }
  op3: depositar(conta: 67890, valor: 50) { saldo }
  op4: sacar(conta: 67890, valor: 20) { saldo }
  op5: depositar(conta: 67890, valor: 75) { saldo }
  op6: sacar(conta: 67890, valor: 15) { saldo }
  op7: depositar(conta: 67890, valor: 30) { saldo }
  op8: sacar(conta: 67890, valor: 5) { saldo }
  op9: depositar(conta: 67890, valor: 25) { saldo }
  op10: sacar(conta: 67890, valor: 10) { saldo }
}
```

## Usando cURL (Linha de Comando)

### Consultar saldo

```bash
curl -X POST http://localhost:5000/graphql \
  -H "Content-Type: application/json" \
  -d '{"query":"{ saldo(conta: 54321) }"}'
```

### Realizar saque

```bash
curl -X POST http://localhost:5000/graphql \
  -H "Content-Type: application/json" \
  -d '{"query":"mutation { sacar(conta: 54321, valor: 50) { conta saldo } }"}'
```

### Realizar depósito

```bash
curl -X POST http://localhost:5000/graphql \
  -H "Content-Type: application/json" \
  -d '{"query":"mutation { depositar(conta: 54321, valor: 100) { conta saldo } }"}'
```

## PowerShell

### Consultar saldo

```powershell
$body = @{
    query = "{ saldo(conta: 54321) }"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5000/graphql" -Method Post -Body $body -ContentType "application/json"
```

### Realizar saque

```powershell
$body = @{
    query = "mutation { sacar(conta: 54321, valor: 50) { conta saldo } }"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5000/graphql" -Method Post -Body $body -ContentType "application/json"
```

## Dicas

1. **Use aliases** para nomear resultados de operações múltiplas
2. **Variáveis** tornam queries reutilizáveis
3. **Introspection** ajuda a explorar o schema disponível
4. **Banana Cake Pop** (GraphQL Playground) oferece autocompletar e documentação inline
5. Para testes automatizados, use **cURL** ou **PowerShell** em scripts

---

Para mais informações, consulte o [README.md](README.md) principal.
