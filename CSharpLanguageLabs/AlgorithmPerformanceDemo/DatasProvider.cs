using System;

namespace AlgorithmPerformanceDemo
{
    public class DatasProvider
    {
        public static int Count;

        public static string[] DataSource { get; private set; }

        public static string NewTarget { get; private set; }

        public static string SearchTarget { get; private set; }

        public static void Create()
        {
            DataSource = new string[Count];
            for (int i = 0; i < Count; i++)
            {
                DataSource[i] = Guid.NewGuid().ToString("N");

                if (i == Count / 2)
                {
                    SearchTarget = DataSource[i];
                }
            }

            NewTarget = Guid.NewGuid().ToString("N");
        }
    }
}
