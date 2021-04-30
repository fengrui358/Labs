using System;
using AspnetCoreWebApiLab.Interfaces;

namespace AspnetCoreWebApiLab.Services
{
    public class MyService : IMyService
    {
        public string WriteMessage(string message)
        {
            var s = $"MyDependency.WriteMessage Message: {message}";
            Console.WriteLine(s);
            return s;
        }
    }
}
