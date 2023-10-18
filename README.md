# Library
### Backend: .NET6
### Frontend: Angular TypeScript

# Installation

### Backend
* Download .NET 6.0 SDK from https://dotnet.microsoft.com/en-us/download
* Download Entity Framework
```bash
dotnet tool install --global dotnet-ef
``` 

### Frontend
* Download Node.js from https://nodejs.org/en
* Install all the required packages 
```bash
npm install
```
* Install Angular
```
npm install -g @angular/cli
```

# Running

### Backend
* In Library.CodeFirstDatabase run:
```bash
dotnet ef database update
```
It creates a database using all of migrations
* In Library.API run server:
```bash
dotnet run
```

### Frontend
Run client from Library.Client:
```bash
ng serve
```
or without installing Angular by using npx (installed with npm)
```bash
npx ng serve
```
---
