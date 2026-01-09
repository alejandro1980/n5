namespace N5Company.Application.Elastic;

public interface IElasticLogger
{
    Task LogAsync(string operation, object data);
}