# VideoApi assignment project
Sample Web API for storing video resume points. .NET Core 2.2, Docker, MongoDb.

## Running
To start the app:
```
dotnet publish VideoApi.sln
docker-compose up
```
Then the web API application is listening on port 23768.

## API
The base path for API is: http://localhost:23768/api/
### GET accounts/[accountId]/videos/[videoId]/resumepoint
Returns the resume point for account and video. The ID's must be in format of Mongo's object id.
### GET accounts/[accountId]/resumepoints
Returns all the resume points for specified account
### POST/PUT accounts/[accountId]/videos/[videoId]/resumepoint
Creates or updates resume point. The body has the following format:
```
{
   resumePoint: 2.4
}
```
Which is video's resume point in seconds.

## Running unit tests
Unit tests can be run directly from host:
```
dotnet test VideoApi.UnitTests/VideoApi.UnitTests.csproj
```

## Running integration tests
Integration tests run in docker containers.
Once the dotnet container videoapi.web:test is finished testing, the docker-compose stops all the other containers.
```
dotnet publish VideoApi.Web/VideoApi.Web.csproj
docker-compose -f docker-compose.yml -f docker-compose.test.yml -p videoapi-integrationtest up --build --abort-on-container-exit
```
