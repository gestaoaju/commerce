# Gestaoaju Commerce

Management platform for buying, selling and renting products.

## Before you start

- Install [Node JS](https://nodejs.org/)
- Install [Visual Studio Code](https://code.visualstudio.com/)
- Install [SQL Server Developer](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## How to run

1. Install .NET Core packages:  
`dotnet restore src/Gestaoaju.csproj`

2. Install Node JS packages:  
`cd src`  
`npm install`  
`npm start` (this will build assets to wwwroot folder and watch file changes)  

3. Press F5 to run the application

## How to test

1. Install .NET Core packages:  
`dotnet restore test/Gestaoaju.Test.csproj`

2. Run the tests:  
`dotnet test test/Gestaoaju.Test.csproj`
