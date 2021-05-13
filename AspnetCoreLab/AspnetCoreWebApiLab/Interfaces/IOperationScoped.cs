using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreWebApiLab.Interfaces
{
    public interface IOperationScoped
    {
        string OperationId { get; }
    }

    public class OperationScoped : IOperationScoped
    {
        public string OperationId { get; } = Guid.NewGuid().ToString()[^4..];
    }
}
