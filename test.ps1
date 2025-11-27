# Script: test.ps1
# Descrição: Executa todos os testes e gera relatório de cobertura

Write-Host "Executando testes..." -ForegroundColor Green

dotnet test `
    /p:CollectCoverage=true `
    /p:CoverletOutputFormat=cobertura `
    /p:CoverletOutput=./TestResults/ `
    --logger "console;verbosity=normal"

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "✓ Todos os testes passaram!" -ForegroundColor Green
    Write-Host ""
    Write-Host "Relatório de cobertura gerado em:" -ForegroundColor Cyan
    Write-Host "  BancoApi.Tests\TestResults\coverage.cobertura.xml" -ForegroundColor Yellow
} else {
    Write-Host ""
    Write-Host "✗ Alguns testes falharam" -ForegroundColor Red
    exit 1
}
