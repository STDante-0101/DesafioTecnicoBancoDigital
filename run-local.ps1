# Script: run-local.ps1
# Descrição: Executa a API localmente (modo desenvolvimento com InMemory)

Write-Host "Iniciando API localmente (InMemory Database)..." -ForegroundColor Green
Write-Host "A API estará disponível em: http://localhost:5000/graphql" -ForegroundColor Cyan
Write-Host ""
Write-Host "Pressione Ctrl+C para parar" -ForegroundColor Yellow
Write-Host ""

Set-Location -Path "BancoApi.Api"
$env:ASPNETCORE_ENVIRONMENT = "Development"
$env:ASPNETCORE_URLS = "http://localhost:5000"

dotnet run
