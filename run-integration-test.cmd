dotnet publish VideoApi.Web/VideoApi.Web.csproj
docker-compose -f docker-compose.yml -f docker-compose.test.yml -p videoapi-integrationtest up --build 

