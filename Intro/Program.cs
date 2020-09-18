using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Linq;

namespace Intro
{
    class Program
    {
        static void Main(string[] args)
        {
            async Task WaitAsync()
            {
                // await сохранит текущий контекст ...
                await Task.Delay(TimeSpan.FromSeconds(1));
                // ... и попытается возобновить метод в этой точке с этим контекстом.
            }
            
            void Deadlock()
            {
                // Начать задержку.
                Task task = WaitAsync();
                // Синхронное блокирование с ожиданием завершения async-метода. task.Wait();
                task.Wait();
            }
            
            async Task DoSomethingAsync()
            {
                int value = 13;
                Console.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine();
                await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
                Console.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine(value);
                value *= 2;

                await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
                
                Console.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine(value); 
                Trace.WriteLine(value);
            }

            // DoSomethingAsync();
            // Deadlock();
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Timestamp()
                .Where(x => x.Value % 2 == 0)
                .Select(x => x.Timestamp)
                .Subscribe(x => Trace.WriteLine(x));
            
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}