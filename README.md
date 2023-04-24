# Redis on Azure: Labs and Sample Code
This repository is referenced in workshop labs which are available in Microsoft's Azure [GitHub](https://github.com/Azure/redis-on-azure-workshop).

The sample projects are basic examples to accompany the JSON lab.

## Prerequisites

### Running the apps with Azure Cache for Redis Enterprise

1. Configure an Azure Cache for Redis Enterprise deployment in a [supported region](https://azure.microsoft.com/en-gb/explore/global-infrastructure/products-by-region/?products=redis-cache&regions=all).
2. Enable the JSON and Search modules at point of creation.
3. Switch to Enterprise clustering policy mode (not OSS mode)

### Redis Stack

1. Refer to the Redis Stack [documentation](https://redis.io/docs/stack/get-started/install/).

## Setup

If you are using Redis Stack, default connection strings are provided for a local Redis instance.

For Azure Cache for Redis Enterprise, update appsettings.development.json or appsettings.json with the following (substituting values for your own deployment):

```
"REDIS_CONNECTION_STRING": "<CACHE-NAME>.<REGION>.redisenterprise.cache.azure.net:<PORT>,password=<ACCESS-KEY>,ssl=True"
```

## Running the sample projects

* Redis-Json-Example is a console app using NRedisStack.
* Redis-Api-Example is a web API using RedisOM.

Both can be started by running the following:

```
dotnet run
```

For the API, navigate to the following URL in a browser:

```
https://localhost:7127/swagger/index.html
````




