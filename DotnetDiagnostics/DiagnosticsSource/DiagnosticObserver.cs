namespace DiagnosticsSource
{
    public class DiagnosticObserver : IObserver<KeyValuePair<string, object?>>
    {
        public void OnCompleted()
        {
            Console.WriteLine("OnCompleted");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"OnError：{error}");
        }

        public void OnNext(KeyValuePair<string, object?> pair)
        {
            // 这里消费诊断数据
            Console.WriteLine($"DiagnosticObserver {pair.Key}-{pair.Value}");
        }
    }
}