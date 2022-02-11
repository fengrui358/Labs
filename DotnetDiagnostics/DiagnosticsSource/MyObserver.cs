namespace DiagnosticsSource
{
    public class MyObserver<T> : IObserver<T>
    {
        private Action<T> _next;

        public MyObserver(Action<T> next)
        {
            _next = next;
        }

        public void OnCompleted()
        {
            Console.WriteLine("OnCompleted");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"OnError：{error}");
        }

        public void OnNext(T value)
        {
            Console.WriteLine($"OnNext：{value}");
        }
    }
}
