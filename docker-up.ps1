# Script: docker-up.ps1
# Descrição: Inicia os containers Docker (API + PostgreSQL)

Write-Host "Iniciando containers Docker..." -ForegroundColor Green
Write-Host ""

docker-compose up --build -d

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "✓ Containers iniciados com sucesso!" -ForegroundColor Green
    Write-Host ""
    Write-Host "Serviços disponíveis:" -ForegroundColor Cyan
    Write-Host "  - API GraphQL: http://localhost:5000/graphql" -ForegroundColor Yellow
    Write-Host "  - PostgreSQL:  localhost:5432" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Para ver os logs:" -ForegroundColor Cyan
    Write-Host "  docker-compose logs -f" -ForegroundColor Gray
    Write-Host ""
    Write-Host "Para parar os containers:" -ForegroundColor Cyan
    Write-Host "  docker-compose down" -ForegroundColor Gray
    Write-Host ""
    
    Start-Sleep -Seconds 3
    Write-Host "Aguardando API inicializar..." -ForegroundColor Yellow
    Start-Sleep -Seconds 5
    
    Write-Host ""
    Write-Host "Abrindo GraphQL Playground no navegador..." -ForegroundColor Green
    Start-Process "http://localhost:5000/graphql"
} else {
    Write-Host ""
    Write-Host "✗ Erro ao iniciar containers" -ForegroundColor Red
    exit 1
}
