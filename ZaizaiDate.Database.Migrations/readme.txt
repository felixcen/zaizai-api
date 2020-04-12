

cd .\ZaizaiDate.Database.Migrations


dotnet ef --startup-project ../ZaizaiDate.Api/ migrations add Initial
Build started...
Build succeeded.
Done. To undo this action, use 'ef migrations remove'
 


dotnet ef database update --startup-project ../ZaizaiDate.Api/