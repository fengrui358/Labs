using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyReferenceCheck
{
    /// <summary>
    /// 引用管理器
    /// </summary>
    public class ReferenceManager
    {
        private static readonly IList<string> SourceFilePaths = new List<string>();
        private static readonly Dictionary<string, ReferenceInfo> AllReferenceInfos = new Dictionary<string, ReferenceInfo>();

        public static void CheckFile(string filePath)
        {
            if (File.Exists(filePath) && !SourceFilePaths.Contains(filePath))
            {
                var childDomain = BuildChildDomain(AppDomain.CurrentDomain);
                var referenceClass = childDomain.CreateInstanceFrom(typeof(ReferenceClass).Assembly.Location, typeof(ReferenceClass).FullName).Unwrap() as ReferenceClass;

                try
                {
                    var references = referenceClass.ReflectionAssemblyPath(filePath);

                    if (references != null)
                    {
                        SourceFilePaths.Add(filePath);

                        foreach (var assemblyName in references)
                        {
                            if (!AllReferenceInfos.ContainsKey(assemblyName.Name))
                            {
                                var referenceInfo = new ReferenceInfo(assemblyName.Name);
                                referenceInfo.Add(assemblyName.FullName, filePath);

                                AllReferenceInfos.Add(assemblyName.Name, referenceInfo);
                            }
                            else
                            {
                                var referenceInfo = AllReferenceInfos[assemblyName.Name];
                                referenceInfo.Add(assemblyName.FullName, filePath);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    //Console.WriteLine("Can't reflection " + filePath);
                }
                finally
                {
                    AppDomain.Unload(childDomain);
                }
            }
        }

        public static IEnumerable<string> GetConflicts()
        {
            var result = new List<string>();
            foreach (var referenceInfo in AllReferenceInfos)
            {
                string s;
                if (referenceInfo.Value.TryGetConflicts(out s))
                {
                    result.Add(s);
                }
            }

            return result;
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// Creates a new AppDomain based on the parent AppDomains 
        /// Evidence and AppDomainSetup
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><param name="parentDomain">The parent AppDomain</param></span>
        /// <span class="code-SummaryComment"><returns>A newly created AppDomain</returns></span>
        private static AppDomain BuildChildDomain(AppDomain parentDomain)
        {
            Evidence evidence = new Evidence(parentDomain.Evidence);
            AppDomainSetup setup = parentDomain.SetupInformation;
            return AppDomain.CreateDomain("DiscoveryRegion",
                evidence, setup);
        }
    }
}
