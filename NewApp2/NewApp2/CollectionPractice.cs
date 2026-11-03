using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace NewApp2
{
    internal record Ticket(string Title, string Owner, string State);


    internal class CollectionPractice
    {
        private readonly List<Ticket> tickets = new List<Ticket>
            {
                new("Write README", "Asha", "Done"),
                new("Add unit tests", "Asha", "InProgress"),
                new("Review PR", "Ravi", "ToDo"),
                new("Set up Docker", "Ravi", "ToDo"),
                new("Fix login bug", "Meera", "InProgress"),
            };
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
            
            var counts = new Dictionary<string, int>();
            foreach (var t in tickets) {
                counts.TryGetValue(t.State, out var count);
                counts[t.State] = count + 1;
            }
            foreach (var (state, count) in counts) {
                Console.WriteLine($"{state}: {count}");
            }
        }

        public void HashSetRun()
        {
            var tags = new List<string> { "api", "API", "ui", "Api", "db", "UI" };
            var newhash = new HashSet<string>(tags, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine(newhash.Count);
            Console.WriteLine(string.Join(",", newhash));
            foreach (var t in newhash)
            {
                Console.WriteLine(t);
            }
        }

        public void QueueRun()
        {
            var queue = new Queue<string>();
            queue.Enqueue("first");
            queue.Enqueue("second");
            queue.Enqueue("third");
            while (queue.Count > 0)
            {
                Console.WriteLine(queue.Dequeue());
            }
            var stack = new Stack<string>();
            stack.Push("create");
            stack.Push("edit");
            stack.Push("delete");
            Console.WriteLine(stack.Peek());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Peek());
            Console.WriteLine(stack.Count);
        }
        public void ChangeList()
        {
            tickets.RemoveAll(t => t.State == "Done");
            Console.WriteLine(tickets.Count);
        }

        public void Group() {
            var group = tickets.GroupBy(t => t.Owner).ToDictionary(g => g.Key, g => g.ToList());
            foreach (var (owner, items) in group)
            {
                 Console.WriteLine($" {owner}:{items.Count}");
                foreach (var item in items)
                {
                    Console.WriteLine($"  {item.Title} - {item.State}");
                }
            }
        }
    }
}
