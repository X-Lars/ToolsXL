using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolsXL;

namespace Test
{
    class Program
    {
        [Flags]
        public enum TestEnum
        {
            Ready = 0,
            Error = 1,
            Info = 2,
            Message = 4
        }

        static void Main(string[] args)
        {
            var status = new Status<TestEnum>();
            
            status.Changed += Status_Changed;

            status += TestEnum.Error;
            status += TestEnum.Message;
            status -= TestEnum.Error;
            status -= TestEnum.Message;


            status = TestEnum.Info;

            Console.WriteLine(status.Flags);

            Console.ReadKey();
        }

        private static void Status_Changed(object sender, StatusChangeEventArgs<TestEnum> e)
        {
            Console.WriteLine("Status Changed: " + e.Status);
        }
    }
}
