# ===========================
# Clean Architecture Setup Script (AutoMapper Fixed)
# Author: ChatGPT
# ===========================

Write-Host "Installing Clean Architecture dependencies..." -ForegroundColor Cyan

# ---------- Project Paths ----------
$InfraPath = "Evo.Infrastructure"
$AppPath   = "Evo.Application"
$ApiPath   = "Evo.API"

# ---------- Packages ----------
$InfraPackages = @(
    "Microsoft.EntityFrameworkCore",
    "Microsoft.EntityFrameworkCore.SqlServer",
    "Microsoft.EntityFrameworkCore.Design",
    "Microsoft.EntityFrameworkCore.Tools",
    "Microsoft.Extensions.Configuration",
    "Microsoft.Extensions.Configuration.Binder",
    "Microsoft.Extensions.Configuration.Json",
    "Microsoft.Extensions.Configuration.EnvironmentVariables"
    "Microsoft.Extensions.Configuration.Json"
)

$AppPackages = @(
    "MediatR",
    "MediatR.Extensions.Microsoft.DependencyInjection",
    "FluentValidation",
    "FluentValidation.DependencyInjectionExtensions"
)

$ApiPackages = @(
    "Microsoft.EntityFrameworkCore.Design",
    "Microsoft.AspNetCore.Authentication.JwtBearer",
    "System.IdentityModel.Tokens.Jwt",
    "Microsoft.IdentityModel.Tokens",
    "Swashbuckle.AspNetCore",
    "MediatR.Extensions.Microsoft.DependencyInjection",
    "Serilog.AspNetCore",
    "Microsoft.Extensions.Configuration.UserSecrets"
)

# ---------- AutoMapper Packages with Versions ----------
$AppMapperPackages = @(
    @{ Name = "AutoMapper"; Version = "12.0.1" },
    @{ Name = "AutoMapper.Extensions.Microsoft.DependencyInjection"; Version = "12.0.1" }
)

$ApiMapperPackages = @(
    @{ Name = "AutoMapper"; Version = "12.0.1" },
    @{ Name = "AutoMapper.Extensions.Microsoft.DependencyInjection"; Version = "12.0.1" }
)

# ---------- Function to Install Regular Packages ----------
function Install-Packages {
    param(
        [string]$Path,
        [string[]]$Packages
    )

    Write-Host "Installing packages for $Path..." -ForegroundColor Yellow
    Push-Location $Path

    foreach ($pkg in $Packages) {
        $pkgName = $pkg.Split(' ')[0]
        $existing = dotnet list package | Select-String $pkgName
        if (-not $existing) {
            Write-Host "Installing $pkg..." -ForegroundColor Cyan
            dotnet add package $pkg
        } else {
            Write-Host "$pkgName is already installed." -ForegroundColor Green
        }
    }

    Pop-Location
}

# ---------- Function to Install Packages with Version ----------
function Install-Versioned-Packages {
    param(
        [string]$Path,
        [array]$VersionedPackages
    )

    Write-Host "Installing versioned packages for $Path..." -ForegroundColor Yellow
    Push-Location $Path

    foreach ($pkg in $VersionedPackages) {
        $existing = dotnet list package | Select-String $pkg.Name
        if (-not $existing) {
            Write-Host "Installing $($pkg.Name) version $($pkg.Version)..." -ForegroundColor Cyan
            dotnet add package $pkg.Name --version $pkg.Version
        } else {
            Write-Host "$($pkg.Name) is already installed." -ForegroundColor Green
        }
    }

    Pop-Location
}

# ---------- Install Packages ----------
Install-Packages -Path $InfraPath -Packages $InfraPackages
Install-Packages -Path $AppPath -Packages $AppPackages
Install-Packages -Path $ApiPath -Packages $ApiPackages

Install-Versioned-Packages -Path $AppPath -VersionedPackages $AppMapperPackages
Install-Versioned-Packages -Path $ApiPath -VersionedPackages $ApiMapperPackages

# ---------- Install EF CLI Globally if Needed ----------
if (-not (Get-Command dotnet-ef -ErrorAction SilentlyContinue)) {
    Write-Host "Installing EF Core CLI globally..." -ForegroundColor Yellow
    dotnet tool install --global dotnet-ef
} else {
    Write-Host "EF Core CLI is already installed." -ForegroundColor Green
}

# ---------- Migration Instructions ----------
$EfProject = $InfraPath
$StartupProject = $ApiPath
$MigrationDir = "Persistence/Migrations"

Write-Host "All dependencies installed successfully!" -ForegroundColor Green
Write-Host "To create the initial migration, run:" -ForegroundColor Cyan
Write-Host "dotnet ef migrations add InitialCreate --project $EfProject --startup-project $StartupProject --output-dir $MigrationDir" -ForegroundColor Cyan
Write-Host "To apply the migration to the database, run:" -ForegroundColor Cyan
Write-Host "dotnet ef database update --project $EfProject --startup-project $StartupProject" -ForegroundColor Cyan
