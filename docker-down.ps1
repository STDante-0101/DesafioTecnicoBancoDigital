# Script: docker-down.ps1
# Descrição: Para e remove os containers Docker

Write-Host "Parando containers Docker..." -ForegroundColor Yellow

docker-compose down

if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ Containers parados com sucesso!" -ForegroundColor Green
} else {
    Write-Host "✗ Erro ao parar containers" -ForegroundColor Red
    exit 1
}
