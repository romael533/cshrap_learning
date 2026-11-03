using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewApp2;

namespace NewApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new SimpleBillingApp().BillingApp();
            new CollectionPractice().ListRun();
            new CollectionPractice().DicRun();
            new CollectionPractice().HashSetRun();
            new CollectionPractice().QueueRun();
            new CollectionPractice().ChangeList();
            new CollectionPractice().Group();
        }
    }
}
