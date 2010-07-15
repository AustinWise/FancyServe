using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace FancyServe
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Program().Run();
            var srv = new Server<object>();
            srv.AddTask(new FileReadTask());
            srv.AddTask(new FileReadTask());

            srv.Run();
        }

        //private class Task
        //{
        //    public Task()
        //    {
        //        this.isNew = true;
        //    }

        //    public WaitHandle Wait { get; set; }
        //    public IEnumerator<IAsyncResult> Iter { get; set; }
        //    private bool isNew;

        //    public bool Turn()
        //    {
        //        this.isNew = false;

        //        if (Wait != null)
        //            Wait.Close();
        //        if (Iter.MoveNext())
        //        {
        //            this.Wait = Iter.Current.AsyncWaitHandle;
        //            return true;
        //        }
        //        else
        //        {
        //            Wait = null;
        //            return false;
        //        }
        //    }

        //    public bool IsReadyToRun
        //    {
        //        get
        //        {
        //            return this.isNew || this.Iter.Current.IsCompleted;
        //        }
        //    }
        //}

        //private List<Task> tasks = new List<Task>();
        //private List<Task> newTasks = new List<Task>();

        //void Run()
        //{
        //    var thing = new Thing(this, new Uri(typeof(Program).Assembly.CodeBase).LocalPath);

        //    tasks.Add(new Task() { Iter = thing.GetEnumerator() });

        //    while (tasks.Count != 0)
        //    {
        //        Console.Write("\tWaiting...");

        //        var waitableTasks = tasks.Where(s => s.Wait != null).Select(s => s.Wait).ToArray();
        //        if (waitableTasks.Length != 0)
        //            WaitHandle.WaitAny(waitableTasks);

        //        Console.WriteLine(" Got");

        //        var aliveTasks = new List<Task>();
        //        foreach (var task in tasks)
        //        {
        //            if (task.IsReadyToRun)
        //            {
        //                if (task.Turn())
        //                {
        //                    aliveTasks.Add(task);
        //                }
        //            }
        //            else
        //                aliveTasks.Add(task);
        //        }

        //        tasks = aliveTasks;

        //        tasks.AddRange(newTasks);
        //        newTasks.Clear();
        //    }

        //    Console.WriteLine("Done");

        //    //foreach (var h in t)
        //    //{
        //    // h.AsyncWaitHandle.WaitOne();
        //    //}
        //}

        //public void AddTask(IEnumerable<IAsyncResult> item)
        //{
        //    this.newTasks.Add(new Task() { Iter = item.GetEnumerator() });
        //}
    }

    //class Thing : IEnumerable<IAsyncResult>
    //{

    //    private Program parent;
    //    private string mLocation;
    //    const int readCount = 0x100;
    //    private byte[] data = new byte[readCount];

    //    public Thing(Program prog, string loc)
    //    {
    //        this.parent = prog;
    //        this.mLocation = loc;
    //    }

    //    public IEnumerator<IAsyncResult> GetEnumerator()
    //    {
    //        Console.WriteLine("{0}: entering", this.GetHashCode());

    //        var fs = new FileStream(mLocation, FileMode.Open, FileAccess.Read);
    //        IAsyncResult ar;

    //        int ret = 0;
    //        int totalBytes = 0;

    //        do
    //        {
    //            if (totalBytes > data.Length - readCount)
    //            {
    //                Console.WriteLine("{0}: new array", this.GetHashCode());
    //                var nu = new byte[data.Length * 2];
    //                Array.Copy(data, nu, totalBytes);
    //                data = nu;
    //            }
    //            yield return ar = fs.BeginRead(data, totalBytes, readCount, null, null);
    //            ret = fs.EndRead(ar);
    //            totalBytes += ret;
    //            Console.WriteLine("{0}: got more", this.GetHashCode());
    //        }
    //        while (ret != 0);

    //        Console.WriteLine("{1}:read {0} bytes", totalBytes, this.GetHashCode());

    //        //for (int i = 0; i < totalBytes; i++)
    //        //{
    //        // byte b = data[i];
    //        // Console.WriteLine("{0:x}", b);
    //        //}
    //    }

    //    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    //    {
    //        return this.GetEnumerator();
    //    }
    //}
}
