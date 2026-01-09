using Nest;

namespace N5Company.Application.Elastic;

public class ElasticLogger : IElasticLogger
{
    private readonly IElasticClient _client;

    public ElasticLogger(IElasticClient client)
    {
        _client = client;
    }

    public async Task LogAsync(string operation, object data)
    {
        var document = new
        {
            Id = Guid.NewGuid(),
            Operation = operation,
            Timestamp = DateTime.UtcNow,
            Data = data
        };

        await _client.IndexAsync(document, i => i
            .Index("permissions-operations")
        );
    }
}