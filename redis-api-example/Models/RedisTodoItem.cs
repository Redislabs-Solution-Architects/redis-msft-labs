using Redis.OM.Modeling;
using Swashbuckle.AspNetCore.Annotations;

namespace TodoApi.Models;

[Document(StorageType = StorageType.Json, Prefixes = new []{"TodoItem"})]
public class RedisTodoItem
{
    [RedisIdField]
    [Indexed]
    [SwaggerSchema(ReadOnly = true)]
    public string? Id { get; set; }
    [Indexed]
    public string? Name { get; set; }
    [Indexed]
    public bool IsComplete { get; set; }
}