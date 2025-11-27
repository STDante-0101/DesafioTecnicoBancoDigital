# Banco API - Desafio C# .NET Core com GraphQL

API REST desenvolvida em **C# com .NET 8** e **GraphQL (HotChocolate)** que simula funcionalidades de um banco digital, permitindo operações de saque, depósito e consulta de saldo.

## 🚀 Tecnologias

- **.NET 8.0** - Framework principal
- **HotChocolate 15.x** - Servidor GraphQL
- **Entity Framework Core 8.0** - ORM
- **xUnit** - Framework de testes
- **Docker** - Containerização

## 📦 Instalação

```powershell
git clone https://github.com/seu-usuario/BancoAPI.git
cd BancoAPI
dotnet restore
```

## ▶️ Como Executar

```powershell
dotnet run
```

Acesse: `http://localhost:5000/graphql`

## 🧪 Testes

```powershell
dotnet test
```

Com cobertura:
```powershell
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
```

## 📝 Exemplos GraphQL

### Consultar Saldo
```graphql
query {
  saldo(conta: 54321)
}
```

### Realizar Saque
```graphql
mutation {
  sacar(conta: 54321, valor: 140) {
    conta
    saldo
  }
}
```

### Realizar Depósito
```graphql
mutation {
  depositar(conta: 54321, valor: 200) {
    conta
    saldo
  }
}
```