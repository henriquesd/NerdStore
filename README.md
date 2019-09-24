# NerdStore
Projeto utilizando .NET Core 2.2


## Pacotes instalados na camada Data (Services\Catalogo\NerdStore.Catalogo.Data e Services\Vendas\NerdStore.Vendas.Data):
```
Install-Package Microsoft.EntityFrameworkCore
```
```
Install-Package Microsoft.EntityFrameworkCore.Design
```
```
Install-Package Microsoft.EntityFrameworkCore.sqlserver
```

## Pacote instalado na camada Domain (Services\Catalogo\NerdStore.Catalogo.Domain):
```
Install-Package mediatr
```

## Pacote instalado na camada Core (Services\Core\NerdStore.Core):
```
Install-Package mediatr
Install-Package FluentValidation
```

## Pacote instalado na camada Application (Services\Catalogo\NerdStore.Catalogo.Application):
```
Install-Package automapper
```

## Pacote instalado na aplicação MVC (WebApps\NerdStore.WebApp.MVC)
```
Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection
Install-Package MediatR.Extensions.Microsoft.DependencyInjection
```


## Comandos EF

### Comando para criar migrations:
PowerShell:
```
Add-Migration Initial -Context CatalogoContext
Add-Migration Initial -Context VendasContext
```
Console (Acesse o projeto NerdStore.Catalogo.Data e execute):
```
dotnet ef migrations add Initial --startup-project ..\NerdStore.WebApp.MVC\ --context CatalogoContext
```

Console (Acesse o projeto NerdStore.Vendas.Data e execute):
```
dotnet ef migrations add Pedidos --startup-project ..\NerdStore.WebApp.MVC\ --context VendasContext
```


### Comando para atualizar o banco de dados
PowerShell:
```
Update-Database -Context CatalogoContext
Update-Database -Context VendasContext
```
Console (Acesse o projeto NerdStore.Catalogo.Data e execute):
```
dotnet ef database update --startup-project ..\NerdStore.WebApp.MVC\ --context CatalogoContext
```

Console (Acesse o projeto NerdStore.Vendas.Data e execute):
```
dotnet ef database update --startup-project ..\NerdStore.WebApp.MVC\ --context VendasContext
```