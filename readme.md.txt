For local development 

1. Enable secret storage
Launch a command window
go to the api project directory
> cd  C:\MyApps\back-end\ZaizaiDate.Api\ZaizaiDate.Api

Intialize the secret

> dotnet user-secrets init

Add a secret for the jwt jey signing key and the connection string

> dotnet user-secrets set "AppSecrets:JwtSigningKey" "Super Secret"  
> dotnet user-secrets set "AppSecrets:DatabaseConnectionString" "Data Source=mydatabase.db"   


https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows