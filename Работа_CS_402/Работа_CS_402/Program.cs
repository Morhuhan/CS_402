using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Runtime.CompilerServices;

namespace Работа_CS_402
{
    internal class Worker1
    {
        // Признак работы потока
        private volatile bool runninig = false;

        // Функция потока
        public void process()
        {
            Console.WriteLine("enter....");
            runninig = true;
            while (runninig)
            {
                Console.WriteLine("....processing....");
                Thread.Sleep(1);
            }
            Console.WriteLine("leave....");
        }

        // Метод, сигнализирующий о завершении работы потока
        public void stop_running()
        {
            runninig = false;
        }
    }

    internal class ThreadWithState
    {
        private string sos;
        private int sin;

        public ThreadWithState(string sos, int sin)
        {
            this.sos = sos;
            this.sin = sin;
        }

        public void ThreadProc()
        {
            Console.WriteLine(sos, sin);
        }
    }

    internal class Worker2
    {
        public void DoWork()
        {
            Console.WriteLine("ID of worker is: {0}", 
                Thread.CurrentThread.GetHashCode());

            Console.WriteLine("Worker2 says: ");
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine(i + ", ");
            }
            Console.WriteLine();
        }
    }

    internal class Worker
    {
        private string name = "noname";
        private System.Object locker = null;

        public Worker(string name, object o)
        {
            this.name = name;
            locker = o;
        }


        [MethodImplAttribute(MethodImplOptions.Synchronized)]
        public void DoWork()
        {

            //lock (locker)
            //{
            //    for (int i = 0; i < 5; i++)
            //    {
            //        Console.WriteLine("Worker ");
            //        Thread.Sleep(0);
            //        Console.WriteLine(name);
            //        Console.WriteLine("-");
            //        Console.WriteLine(i + " ");
            //        Thread.Sleep(0);
            //        Console.WriteLine();
            //    }
            //}

            //Monitor.Enter(locker);
            //try
            //{
            //    for (int i = 0; i < 5; i++)
            //    {
            //        Console.WriteLine("Worker ");
            //        Thread.Sleep(0);
            //        Console.WriteLine(name);
            //        Console.WriteLine("-");
            //        Console.WriteLine(i + " ");
            //        Thread.Sleep(0);
            //        Console.WriteLine();
            //    }
            //}
            //finally { Monitor.Exit(locker); }

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Worker ");
                Thread.Sleep(0);
                Console.WriteLine(name);
                Console.WriteLine("-");
                Console.WriteLine(i + " ");
                Thread.Sleep(0);
                Console.WriteLine();
            }

        }
    }


    internal class Program
    {
        // Блокирующий объект
        static System.Object locker = new System.Object();

        // Синхронизирующее событие
        private static AutoResetEvent arEvent;

        static void process()
        {
            Console.WriteLine("Second thread waits....");
            arEvent.WaitOne();
            Console.WriteLine("Second thread resume and ends....");
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("Main starts.");

            //Worker1 w = new Worker1();

            //// Способ создания потока делегатом
            //Thread t = new Thread(new ThreadStart(w.process));

            //// Стартуем вторичный поток
            //t.Start();

            //// Ждем пока активируется нужны поток
            //while (!t.IsAlive) ;

            //// Даем вторичному потоку 100мс для работы
            //Thread.Sleep(100);

            //w.stop_running();

            //// Ожидем завершения вторичного потока
            //t.Join();

            //Console.WriteLine("Main ends.");

            /////////////////// Поток с параметрами

            //ThreadWithState w = new ThreadWithState("Number {0}", 33);

            //Thread t = new Thread(new ThreadStart(w.ThreadProc));

            //t.Start();

            //while (!t.IsAlive) ;

            //t.Join();

            //Console.WriteLine("Main ends.");

            /////////////////// Распараллеливание потоков

            //Console.WriteLine("ID of main: {0}",
            //    Thread.CurrentThread.GetHashCode());

            //Worker2 w = new Worker2();

            //Thread t = new Thread(new ThreadStart(w.DoWork));

            //t.Start();

            //for (int i = 0; i < 500; i++)
            //{
            //    Console.WriteLine("main " + i);
            //}

            //t.Join();

            /////////////////// Cинхронизация locker
            /// Создаем System.Object locker в основном потоке и в классе Worker
            /// В конструкторе класса принимаем locker и блокируем for относительно него

            //Worker w1 = new Worker("A", locker);
            //Thread wA = new Thread(new ThreadStart(w1.DoWork));

            //Worker w2 = new Worker("B", locker);
            //Thread wB = new Thread(new ThreadStart(w2.DoWork));

            //Worker w3 = new Worker("C", locker);
            //Thread wC = new Thread(new ThreadStart(w3.DoWork));

            //wA.Start();
            //wB.Start();
            //wC.Start();

            /////////////////// Cинхронизация monitor

            //Worker w1 = new Worker("A", locker);
            //Thread wA = new Thread(new ThreadStart(w1.DoWork));

            //Worker w2 = new Worker("B", locker);
            //Thread wB = new Thread(new ThreadStart(w2.DoWork));

            //Worker w3 = new Worker("C", locker);
            //Thread wC = new Thread(new ThreadStart(w3.DoWork));

            //wA.Start();
            //wB.Start();
            //wC.Start();

            /////////////////// Cинхронизация Synchronized
            /// Объект класса потока может быть только единственным

            //Worker w = new Worker("X", locker);
            //Thread wA = new Thread(new ThreadStart(w.DoWork));
            //Thread wB = new Thread(new ThreadStart(w.DoWork));
            //Thread wC = new Thread(new ThreadStart(w.DoWork));
            //wA.Start();
            //wB.Start();
            //wC.Start();

            /////////////////// Cинхронизация событий

            Console.WriteLine("Main thread starts....");

            arEvent = new AutoResetEvent(false);

            Thread t = new Thread(process);

            t.Start();

            Thread.Sleep(3000);

            Console.WriteLine("Main thread signals...");

            arEvent.Set();

            t.Join();
            Console.WriteLine("Main thread ends....");
        }
    }
}
