# Gestaoaju Commerce

Management platform for buying, selling and renting products.

## Before you start

- Install [.NET Core 2.0](https://github.com/dotnet/core/)
- Install [Node JS](https://nodejs.org/)
- Install [Visual Studio Code](https://code.visualstudio.com/)
- Install [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads/)

Install some basic VS Code extensions:

- ASP.NET Helper
- C#
- C# Extensions
- HTML Snippets
- JavaScript ES6
- Sass

Create a LocalDB instance:  
`sqllocaldb create Gestaoaju`  
`sqllocaldb start Gestaoaju`

## How to run

1. Go to source folder (from root):  
`cd src`

2. Install .NET Core packages:  
`dotnet restore`

3. Run migrations:  
`dotnet ef database update --context AppDbContext`

4. Install Node JS packages:  
`npm install`

5. Run webpack (build assets to wwwroot):  
`npm start`

6. Run the application:  
`dotnet run`

## How to test

1. Go to test folder (from root):  
`cd test`

2. Install .NET Core packages:  
`dotnet restore`

3. Run all tests:  
`dotnet test`
