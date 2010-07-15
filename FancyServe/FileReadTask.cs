using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FancyServe
{
    class FileReadTask : Task<object>
    {
        public FileReadTask()
        {
            this.mLocation = new Uri(GetType().Assembly.CodeBase).LocalPath;
        }

        private string mLocation;
        const int readCount = 0x100;
        private byte[] data = new byte[readCount];

        protected override IEnumerator<IAsyncResult> RunCore()
        {
            Console.WriteLine("{0}: entering", this.GetHashCode());

            var fs = new FileStream(mLocation, FileMode.Open, FileAccess.Read);
            IAsyncResult ar;

            int ret = 0;
            int totalBytes = 0;

            do
            {
                if (totalBytes > data.Length - readCount)
                {
                    Console.WriteLine("{0}: new array", this.GetHashCode());
                    var nu = new byte[data.Length * 2];
                    Array.Copy(data, nu, totalBytes);
                    data = nu;
                }
                yield return ar = fs.BeginRead(data, totalBytes, readCount, null, null);
                ret = fs.EndRead(ar);
                totalBytes += ret;
                Console.WriteLine("{0}: got more", this.GetHashCode());
            }
            while (ret != 0);

            Console.WriteLine("{1}:read {0} bytes", totalBytes, this.GetHashCode());
        }
    }
}
