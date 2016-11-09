using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    [ServiceContract(Name = "CalculatorService", Namespace = "http://www.artech.com")]
    public interface IHelloWord
    {
        [OperationContract]
        string HelloWord();

        [OperationContract(Name = "sdfassdfsa")]
        string HelloWord(string s);
    }
}
