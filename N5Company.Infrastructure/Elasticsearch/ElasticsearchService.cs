using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Configuration;
using N5Company.Domain.Entities;

namespace N5Company.Infrastructure.Elasticsearch
{

    public class ElasticsearchService
    {
        private readonly ElasticsearchClient _client;
        private readonly string _index;

        public ElasticsearchService(IConfiguration configuration)
        {
            _index = configuration["Elasticsearch:Index"];

            var settings = new ElasticsearchClientSettings(
                new Uri(configuration["Elasticsearch:Uri"])
            );

            _client = new ElasticsearchClient(settings);
        }

        public async Task LogPermissionAsync(Permission permission)
        {
            await _client.IndexAsync(permission, i => i.Index(_index));
        }
    }
}
