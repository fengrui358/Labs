using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AspnetCoreWebApiLab.Interfaces;

namespace AspnetCoreWebApiLab.Services.HttpClients
{
    public class OperationHandler : DelegatingHandler
    {
        private readonly IOperationScoped _operationService;

        public OperationHandler(IOperationScoped operationScoped)
        {
            _operationService = operationScoped;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine("OperationHandler:" + GetHashCode());

            request.Headers.Add("X-OPERATION-ID", _operationService.OperationId);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
