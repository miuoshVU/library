# Biblioteczka

## Frontend
```bash
npm install
```

## Backend
* Download dotnet sdk
* Download entity framework
```bash
dotnet tool install --global dotnet-ef
``` 
* In Library.CodeFirstDatabase run:
```bash
dotnet ef database update
```
It creates a database using recent migration
* Run C# program
```bash
dotnet run
```