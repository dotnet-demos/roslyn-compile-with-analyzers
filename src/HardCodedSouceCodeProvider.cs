namespace ConsoleApp
{
    // Hard coded source code. It can read from file system.
    class HardCodedSouceCodeProvider : ISourceCodeProvider
    {
        string ISourceCodeProvider.Get()
        {
            return @"
                using System;

                namespace RoslynCompileSample
                {
                    public class writer
                    {
                        public void Write(string message)
                        {
                            Console.WriteLine(message);
                        }
                    }
                }";
        }
    }
}