using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace ElasticSearchLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri = "http://txy.frhello.com:9200";
            var nodes = uri.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(url => new Uri(url));
            IConnectionPool connectionPool = new StaticConnectionPool(nodes);
            var config = new ConnectionConfiguration(connectionPool);
            //config.BasicAuthentication("", "");

            IElasticLowLevelClient client = new ElasticLowLevelClient(config);

            var document = new Dictionary<string, object>
            {
                {"@timestamp", DateTime.Now},
                {"level", "Info"},
                {"message", "test.log"}
            };

            document["traceId"] = "testTraceId";

            object documentInfo = new {index = new {_index = "springboot-elk"}};
            var payload = new List<object> {documentInfo, document};
            var postData = PostData.MultiJson(payload);

            var result = client.Bulk<BytesResponse>(postData);
            var exception = result.Success
                ? null
                : result.OriginalException ??
                  new Exception("No error message. Enable Trace logging for more information.");
            if (exception != null)
            {
                Console.WriteLine($"ElasticSearch: Failed to send log messages. status={result.HttpStatusCode}");
            }

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}