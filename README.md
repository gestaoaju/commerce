# Gestaoaju Commerce

Management platform for buying, selling and renting products.

## Before you start

- Install [Node JS](https://nodejs.org/)
- Install [Visual Studio Code](https://code.visualstudio.com/)
- Install [SQL Server Developer](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

Some basic extensions for VS Code:

- ASP.NET Helper
- C#
- C# Extensions
- HTML Snippets
- JavScript ES6
- Sass

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

6. Run the application by pressing F5

## How to test

1. Go to test folder (from root):  
`cd test`

2. Install .NET Core packages:  
`dotnet restore`

3. Run all tests:  
`dotnet test`
