using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataReader_EFWheel.Tool
{
   public class CallbackHelper
   {
       private Thread thread;
        public CallbackHelper()
        {
         
        }

        public Func<T> ThreadWithReturn<T>(Func<T> funcT)
        {
            T t = default(T);
            ThreadStart startNew = new ThreadStart(
                () =>
                {
                    t = funcT.Invoke();
                });
            Thread thread = new Thread(startNew);
            thread.Start();

            return new Func<T>(() =>
            {
                thread.Join();
                return t;
            });
        }

    }
}
