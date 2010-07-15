using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FancyServe
{
    public abstract class Task<T> where T : new()
    {
        public Task()
        {
            isNew = true;
        }

        public Server<T> Parent { get; set; }
        public WaitHandle Wait { get; private set; }
        private IEnumerator<IAsyncResult> Iter;
        private bool isNew;

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if there is more work left to be done in this task</returns>
        public bool Run()
        {
            bool readyToRun;

            //create the iterator the first time
            if (isNew)
            {
                Iter = RunCore();
                isNew = false;
                readyToRun = true;
            }
            else
                readyToRun = Iter.Current.IsCompleted;

            if (!readyToRun)
                return true;

            //close the native wait handle
            if (Wait != null)
                Wait.Close();

            if (Iter.MoveNext())
            {
                this.Wait = Iter.Current.AsyncWaitHandle;
                return true;
            }
            else
            {
                Wait = null;
                return false;
            }
        }

        protected abstract IEnumerator<IAsyncResult> RunCore();
    }
}
