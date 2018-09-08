using BenchmarkDotNet.Running;

namespace TimeItCore.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var switcher = new BenchmarkSwitcher(new[] {
                typeof(DoNothing),
                typeof(Log),
                typeof(Throw)
            });
            
            switcher.Run(args);
        }
    }
}
