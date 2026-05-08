using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RosettaOne
{

    // Exceptions

    public class EntryAlreadyExistsException : Exception
    {
        static System.String message = "The entry already exists in the collection.";

        public EntryAlreadyExistsException() : base(message) { }
    }

    public class EntryNotFoundException : Exception
    {
        static System.String message = "The requested entry is not found in the collection.";

        public EntryNotFoundException() : base(message) { }
    }

    enum Limits { Maximum = 40, Minimum = 20 }

    public class Node
    {
        public int Count;
        public int[] Keys;
        public Node[] Children;

        public Node()
        {
            Count = 0;
            Keys = new int[(int)Limits.Maximum];
            Children = new Node[(int)Limits.Maximum + 1];
        }

        public void MoveLeft(int k)
        {
            Children[k - 1].Count++;
            Children[k - 1].Keys[Children[k - 1].Count - 1] = Keys[k - 1];
            Children[k - 1].Children[Children[k - 1].Count] = Children[k].Children[0];

            Keys[k - 1] = Children[k].Keys[0];
            Children[k].Children[0] = Children[k].Children[1];
            Children[k].Count--;

            for (int c = 1; c <= Children[k].Count; c++)
            {
                Children[k].Keys[c - 1] = Children[k].Keys[c];
                Children[k].Children[c] = Children[k].Children[c + 1];
            }
        }

        public void MoveRight(int k)
        {
            for (int c = Children[k].Count; c >= 1; c--)
            {
                Children[k].Keys[c] = Children[k].Keys[c - 1];
                Children[k].Children[c + 1] = Children[k].Children[c];
            }

            Children[k].Children[1] = Children[k].Children[0];
            Children[k].Count++;
            Children[k].Keys[0] = Keys[k - 1];

            Keys[k - 1] = Children[k - 1].Keys[Children[k - 1].Count - 1];
            Children[k].Children[0] = Children[k - 1].Children[Children[k - 1].Count];
            Children[k - 1].Count--;
        }

        public void Combine(int k)
        {
            Node q = Children[k];

            Children[k - 1].Count++;
            Children[k - 1].Keys[Children[k - 1].Count - 1] = Keys[k - 1];
            Children[k - 1].Children[Children[k - 1].Count] = q.Children[0];

            for (int c = 1; c <= q.Count; c++)
            {
                Children[k - 1].Count++;
                Children[k - 1].Keys[Children[k - 1].Count - 1] = q.Keys[c - 1];
                Children[k - 1].Children[Children[k - 1].Count] = q.Children[c];
            }

            for (int c = k; c <= Count - 1; c++)
            {
                Keys[c - 1] = Keys[c];
                Children[c] = Children[c + 1];
            }
            Count--;
        }


        public void Successor(int k)
        {
            Node q = Children[k];
            while (q.Children[0] != null) q = q.Children[0];
            Keys[k - 1] = q.Keys[0];
        }

        public void Restore(int k)
        {
            if (k == 0)
            {
                if (Children[1].Count > (int)Limits.Minimum)
                    MoveLeft(1);
                else
                    Combine(1);
            }
            else if (k == Count)
            {
                if (Children[k - 1].Count > (int)Limits.Minimum)
                    MoveRight(k);
                else
                    Combine(k);
            }
            else
            {
                if (Children[k - 1].Count > (int)Limits.Minimum)
                    MoveRight(k);
                else if (Children[k + 1].Count > (int)Limits.Minimum)
                    MoveLeft(k + 1);
                else
                    Combine(k);
            }
        }

        public void Remove(int k)
        {
            for (int i = k + 1; i <= Count; i++)
            {
                Keys[i - 2] = Keys[i - 1];
                Children[i - 1] = Children[i];
            }
            Count--;
        }

        public static int GetHeight(Node n)
        {
            if (n == null)
            {
                return 0;
            }
            int height = 0;
            if (n.Count > 0)
            {
                height = Node.GetHeight(n.Children[0]);
            }
            return height + 1;
        }

        public int GetNodeCount()
        {
            int result = 0;
            for (int i = 0; i < Count; i++)
            {
                Node a = Children[i];
                if (a == null)
                    continue;
                result += a.GetNodeCount();
            }
            return result + 1;
        }

        public List<int> GetData(Node node)
        {
            if (node == null)
            {
                return new List<int>();
            }

            List<int> data = new List<int>();
            GetDataInOrder(node, data);
            return data;
        }


        public void GetDataInOrder(Node node, List<int> data)
        {
            if (node == null)
                return;

            for (int i = 0; i < node.Count + 1; i++)
            {
                GetDataInOrder(node.Children[i], data);
                if (i < node.Count)
                {
                    data.Add(node.Keys[i]);
                }
            }
        }

        public void WriteToStream(StreamWriter sw, Node node)
        {
            if (node == null)
                return;

            for (int i = 0; i < node.Count + 1; i++)
            {
                WriteToStream(sw, node.Children[i]);
                if (i < node.Count)
                {
                    sw.WriteLine(node.Keys[i]);
                }
            }
        }

        public void Clear(Node n)
        {
            if (n == null)
            {
                return;
            }
            foreach (var c in n.Children)
            {
                Clear(c);
            }
            Array.Clear(n.Children);
            Array.Clear(n.Keys);
        }

        /// <summary>
        /// Display the tree in pre-order.  Recursive.
        /// The root is first. 
        /// </summary>
        /// <param name="cursor"></param>
        public void DisplayInner(Node cursor)
        {
            //pre-order
            if (cursor != null)
            {
                for (int i = 0; i < cursor.Count; i++)
                {
                    Console.Write(cursor.Keys[i] + " ");
                }
                Console.WriteLine();
                for (int i = 0; i < cursor.Count + 1; i++)
                {
                    DisplayInner(cursor.Children[i]);
                }
            }
        }



    }
}


