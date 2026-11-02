using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace NewApp2
{
    internal record Ticket(string Title, string Owner, string State);

    internal class CollectionPractice
    {
        public void ListRun()
        {
            var namestest = new List<string> { "Write README", "Add unit tests", "Review PR", "Set up Docker", "Fix login bug" };
            namestest.Add("Deploy to test");
            namestest.Remove("Review PR");
            namestest.Sort();

            Console.WriteLine(string.Join(", ", namestest));
            foreach (var n in namestest)
            {
                Console.Write(n + ", ");
            }
            Console.WriteLine("\n");
            var finding = namestest.Find(t => t.IndexOf("test", StringComparison.OrdinalIgnoreCase) >= 0);
            Console.Write(finding);
        }

        public void DicRun()
        {
            var tickets = new List<Ticket>
            {
                new("Write README", "Asha", "Done"),
                new("Add unit tests", "Asha", "InProgress"),
                new("Review PR", "Ravi", "ToDo"),
                new("Set up Docker", "Ravi", "ToDo"),
                new("Fix login bug", "Meera", "InProgress"),
            };
        }

    }
}
