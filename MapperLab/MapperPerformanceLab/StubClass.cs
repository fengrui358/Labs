using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperLab
{
    [Serializable]
    public class StubClass
    {
        public string StringA { get; set; }

        public int IntB { get; set; }

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public DateTime CreateTime { get; set; }

        public string Nickname { get; set; }

        public string Phone { get; set; }

        public StubSubClass StubSubClass { get; set; }

        public List<StubSubClass> StringListC { get; set; }

        public Dictionary<string, string> Dictionary { get; set; }

        public StubClass Copy()
        {
            var result = (StubClass)MemberwiseClone();
            result.StringListC = new List<StubSubClass>();

            foreach (var stubSubClass in StringListC)
            {
                result.StringListC.Add(stubSubClass.Copy());
            }

            result.Dictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> keyValuePair in Dictionary)
            {
                result.Dictionary.Add(keyValuePair.Key, keyValuePair.Value);
            }

            return result;
        }
    }

    [Serializable]
    public class StubSubClass
    {
        public string StringA { get; set; }

        public int IntB { get; set; }

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public DateTime CreateTime { get; set; }

        public string Nickname { get; set; }

        public string Phone { get; set; }

        public StubSubClass Copy()
        {
            return (StubSubClass) MemberwiseClone();
        }
    }
}
