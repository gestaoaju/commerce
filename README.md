# Gestaoaju Commerce

Management platform for buying, selling and renting products.

[![Build status](https://ci.appveyor.com/api/projects/status/652f6nc6ph97mw4t?svg=true)](https://ci.appveyor.com/project/marxjmoura/commerce)

## Before you start

- Install [.NET Core 2.0](https://github.com/dotnet/core/)
- Install [Node JS](https://nodejs.org/)
- Install [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads/)
- Install [VS Code](https://code.visualstudio.com/) (or your preferred editor)

Install some basic VS Code extensions:

- ASP.NET Helper
- C#
- C# Extensions
- HTML Snippets
- JavaScript ES6
- Sass

## How to run

1. Configure your `appsettings.json`  
(there is an `appsettings.sample.json` file to guide you)

2. Go to source folder (from root):  
`cd src`

3. Install .NET Core packages:  
`dotnet restore`

4. Run migrations:  
`dotnet ef database update --context AppDbContext`

5. Install Node JS packages:  
`npm install`

6. Build assets to wwwroot:  
`npm start`

7. Run the application:  
`dotnet run`

## How to test

1. Go to test folder (from root):  
`cd test`

2. Install .NET Core packages:  
`dotnet restore`

3. Run all tests:  
`dotnet test`
