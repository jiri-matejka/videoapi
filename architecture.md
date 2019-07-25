# Architecture decisions

### Performance
> Assume that a single account may store hundreds of resume points
> Assume that the service must be highly scalable and fast – including the data backend

I chose to store the data as one document per resume point:
```
{
	accountId: 123456789,
	videoId: 123456780,
	timePoint: 12.3
}
```
This is more like "relational database" style, but has advantage that upsert action is very easy and atomic just with updateOne MongoDb command. Performance of reading operations is not hurt, since we are using an index.
With this approach one account can contain very high number of resume points without any problems.

The `ResumePointRepository` creates the multikey index on `accountId+videoId` which covers all the required queries (on `accountId` separately or on both properties).

Using Docker Compose allows to scale the database and/or API to multiple nodes if needed.

Regarding performance of the website, `async` methods are used in the `AccountsVideoController`'s methods, which brings better request throughput.

### Database provider agnostic data layer interface
The VideoApi.Data assembly contains a repository interface and entity class which are database provider agnostic. Consumers or the data layer would be not bound to MongoDb types, since even entites does not use MongoDb data types.
The MongoDb's specific implementation of the repository in VideoApi.Data.MongoDb is registered in an ASP.NET Core's IoC container.