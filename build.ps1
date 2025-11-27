# Scripts PowerShell para facilitar desenvolvimento

# Script: build.ps1
# Descrição: Compila o projeto

Write-Host "Compilando o projeto..." -ForegroundColor Green
dotnet build --configuration Release

if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ Compilação concluída com sucesso!" -ForegroundColor Green
} else {
    Write-Host "✗ Erro na compilação" -ForegroundColor Red
    exit 1
}
