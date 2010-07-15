using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FancyServe
{
    public class Server<T> where T : new()
    {
        public Server()
        {
            this.Context = new T();
        }

        public T Context { get; private set; }
        private List<Task<T>> tasks = new List<Task<T>>();
        private List<Task<T>> newTasks = new List<Task<T>>();

        public void Run()
        {
            tasks.AddRange(newTasks);
            newTasks.Clear();

            while (tasks.Count != 0)
            {
                Console.Write("\tWaiting...");

                var waitableTasks = tasks.Where(s => s.Wait != null).Select(s => s.Wait).ToArray();
                if (waitableTasks.Length != 0)
                    WaitHandle.WaitAny(waitableTasks);

                Console.WriteLine(" Got");

                var aliveTasks = new List<Task<T>>();
                foreach (var task in tasks)
                {
                    if (task.Run())
                    {
                        aliveTasks.Add(task);
                    }
                }

                tasks = aliveTasks;

                tasks.AddRange(newTasks);
                newTasks.Clear();
            }
        }

        public void AddTask(Task<T> task)
        {
            task.Parent = this;
            this.newTasks.Add(task);
        }
    }
}
