﻿# VideoApi assignment project

## Assignment
Create a container-hosted microservice in .NET Core to store video playback resume points. The service should contain the following APIs:

1. Store a resume point (video ID, account ID, position)
2. Get resume points for an account 
3. Get a resume point for an account for a specific video.

New resume point overrides any previous one matching the video ID and account ID combination.

## Solution

.NET Core 2 WebApi application with MongoDb data backend. Based on Docker Compose, defines two containers: Mongo and the VideoApi.Web.

There is integration testing set up also with Docker Compose, the integration test run in special container and calls the API endpoints. The database is cleaned every time it's container is started to ensure repeatability of the test.

Detailed architecture decisions can be found [here](architecture.md).

## How to start
To start the app:
```
dotnet publish VideoApi.sln
docker-compose up
```
Then the web API application is listening on port 23768.

## API
The base path for API is: localhost:23768/api/
### GET accounts/[accountId]/videos/[videoId]/resumepoint
Returns the resume point for account and video. The ID's must be in format of Mongo's object id.
### GET accounts/[accountId]/resumepoints
Returns all the resume points for specified account
### POST/PUT accounts/[accountId]/videos/[videoId]/resumepoint
Creates or updates resume point. The body has the following format:
```
{
   timePoint: 2.4
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
Once the testing container (named "test") has finished testing (`dotnet test`) and terminates, the docker-compose stops all the other containers.
```
dotnet publish VideoApi.Web/VideoApi.Web.csproj
docker-compose -f docker-compose.yml -f docker-compose.test.yml -p videoapi-integrationtest up --build --abort-on-container-exit
```

