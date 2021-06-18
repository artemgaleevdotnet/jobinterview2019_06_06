using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace splunk
{
    class Program
    {
        public class Node
        {
            public char Value { get; set; } // root string.Empty
            public Node[] Nodes { get; set; }
            public bool isEnd { get; set; }

            public IEnumerable<string> FindByPrefix(string prefix)
            {
                if (string.IsNullOrWhiteSpace(prefix) || Nodes == null)
                {
                    return Enumerable.Empty<string>();
                }

                var prefixChars = prefix.ToCharArray();
                var firstLetter = prefixChars.First();

                var currentNode = Nodes.FirstOrDefault(x => x.Value.Equals(firstLetter));

                for (int i = 1; i < prefixChars.Length && currentNode != null; i++)
                {
                    currentNode = currentNode.Nodes?.FirstOrDefault(x => x.Value.Equals(prefixChars[i]));
                }

                if (currentNode == null)
                {
                    return Enumerable.Empty<string>();
                }

                return GetWords(new StringBuilder(prefix), currentNode).Select(s => s.ToString());
            }

            private IEnumerable<string> GetWords(StringBuilder prefix, Node n)
            {
                var result = n.isEnd ? new[] { prefix.ToString() } : Enumerable.Empty<string>();
                
                if (n.Nodes != null && n.Nodes.Length > 0)
                {
                    return result.Union(n.Nodes.SelectMany(n => GetWords(new StringBuilder(prefix.ToString()).Append(n.Value), n)));
                }

                return result;
            }
        }

        static void Main(string[] args)
        {
            var strs = rootNode.FindByPrefix("goo");

            foreach(var str in strs)
            {
                Console.WriteLine(str);
            }
        }

        static Node rootNode = new Node
        {
            Nodes = new[]
                {
                    new Node
                    {
                        Value = 'g',
                        Nodes = new[]
                        {
                            new Node
                            {
                                Value = 'o',
                                Nodes = new[]
                                {
                                    new Node
                                    {
                                        Value = 'o',
                                        Nodes = new[]
                                        {
                                            new Node
                                            {
                                                Value = 'd',
                                                isEnd = true
                                            },
                                            new Node
                                            {
                                                Value = 'g',
                                                Nodes = new[]
                                                {
                                                    new Node
                                                    {
                                                        Value = 'l',
                                                        Nodes = new[]
                                                        {
                                                            new Node
                                                            {
                                                                Value = 'e',
                                                                isEnd = true
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    },
                                    new Node
                                    {
                                        Value = 'a',
                                        Nodes= new[]
                                        {
                                            new Node
                                            {
                                                Value = 'l',
                                                isEnd = true
                                            }
                                        }
                                    }
                                },
                                isEnd = true
                            }
                        }
                    }
                }
        };
    }
}
