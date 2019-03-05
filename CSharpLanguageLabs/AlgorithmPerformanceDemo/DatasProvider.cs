using System;

namespace AlgorithmPerformanceDemo
{
    public class DatasProvider
    {
        private static object _async = new object();
        private static bool _isCreated;

        public static int Count;

        public static Guid[] DataSource { get; private set; }

        public static Guid NewTarget { get; private set; }

        public static Guid SearchTarget { get; private set; }

        public static void Create()
        {
            lock (_async)
            {
                if (_isCreated)
                {
                    return;
                }

                _isCreated = true;
            }

            DataSource = new Guid[Count];
            for (int i = 0; i < Count; i++)
            {
                DataSource[i] = Guid.NewGuid();

                if (i == Count / 2)
                {
                    SearchTarget = DataSource[i];
                }
            }

            NewTarget = Guid.NewGuid();
        }
    }
}
