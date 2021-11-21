namespace DotNet6MiniWebApi
{
    public class ComputerService : IComputer
    {
        public int RunIt(int x, int y, int z)
        {
            return x - y - z;
        }
    }
}
