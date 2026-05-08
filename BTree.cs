
namespace RosettaOne
{
    public class BTree
    {
        public Node Root;

        public BTree()
        {
            Root = null;
        }

        public bool Exists(int Target)
        {
            Node targetNode = null;
            int targetPosition = 0;
            return Search(Target, Root, ref targetNode, ref targetPosition);
        }

        bool Search(int Target, Node Root, ref Node targetNode, ref int targetPosition)
        {
            if (Root == null)
                return false;

            if (SearchNode(Target, Root, ref targetPosition))
            {
                targetNode = Root;
                return true;
            }
            else
            {
                return Search(Target, Root.Children[targetPosition], ref targetNode, ref targetPosition);
            }
        }

        bool SearchNode(int Target, Node Root, ref int Position)
        {
            int iCompare = Target.CompareTo(Root.Keys[0]);
            if (iCompare < 0)
            {
                Position = 0;
                return false;
            }
            else
            {
                Position = Root.Count;
                iCompare = Target.CompareTo(Root.Keys[Position - 1]);
                while (iCompare < 0 && Position > 1)
                {
                    Position--;
                    iCompare = Target.CompareTo(Root.Keys[Position - 1]);
                }
                return iCompare == 0;
            }
        }

        public void Add(int newKey)
        {
            Insert(newKey, ref Root);
        }

        void Insert(int newKey, ref Node root)
        {
            int x;
            Node xr;

            if (PushDown(newKey, root, out x, out xr))
            {
                Node p = new Node();
                p.Count = 1;
                p.Keys[0] = x;
                p.Children[0] = root;
                p.Children[1] = xr;
                root = p;
            }
        }

        bool PushDown(int newKey, Node p, out int x, out Node xr)
        {
            bool pushUp = false;
            int k = 1;

            if (p == null)
            {
                pushUp = true;
                x = newKey;
                xr = null;
            }
            else
            {
                if (SearchNode(newKey, p, ref k)) throw new EntryAlreadyExistsException();

                if (PushDown(newKey, p.Children[k], out x, out xr))
                {
                    if (p.Count < (int)Limits.Maximum)
                    {
                        pushUp = false;
                        PushIn(x, xr, ref p, k);
                    }
                    else
                    {
                        pushUp = true;
                        Split(x, xr, p, k, ref x, ref xr);
                    }
                }
            }

            return pushUp;
        }

        void PushIn(int x, Node xr, ref Node p, int k)
        {
            for (int i = p.Count; i >= k + 1; i--)
            {
                p.Keys[i] = p.Keys[i - 1];
                p.Children[i + 1] = p.Children[i];
            }
            p.Keys[k] = x;
            p.Children[k + 1] = xr;
            p.Count++;
        }

        bool Split(int x, Node xr, Node p, int k, ref int y, ref Node yr)
        {
            int median = k <= (int)Limits.Minimum ? (int)Limits.Minimum : (int)Limits.Minimum + 1;

            yr = new Node();

            for (int i = median + 1; i <= (int)Limits.Maximum; i++)
            {
                yr.Keys[i - median - 1] = p.Keys[i - 1];
                yr.Children[i - median] = p.Children[i];
            }

            yr.Count = (int)Limits.Maximum - median;
            p.Count = median;

            if (k <= (int)Limits.Minimum)
                PushIn(x, xr, ref p, k);
            else
                PushIn(x, xr, ref yr, k - median);

            y = p.Keys[p.Count - 1];
            yr.Children[0] = p.Children[p.Count];

            p.Count--;

            return true;

        }

        public void Remove(int newKey) { Delete(newKey, ref Root); }

        void Delete(int Target, ref Node root)
        {
            if (!RecDelete(Target, Root))
                throw new EntryNotFoundException();
            else if (root.Count == 0)
            {
                root = root.Children[0];
            }
        }

        bool RecDelete(int Target, Node p)
        {
            int k = 0;
            bool found = false;

            if (p == null)
                return false;
            else
            {
                found = SearchNode(Target, p, ref k);
                if (found)
                {
                    if (p.Children[k - 1] == null)
                        p.Remove(k);
                    else
                    {
                        p.Successor(k);
                        if (!RecDelete(p.Keys[k - 1], p.Children[k]))
                            throw new EntryNotFoundException();
                    }
                }
                else
                    found = RecDelete(Target, p.Children[k]);

                if (p.Children[k] != null)
                    if (p.Children[k].Count < (int)Limits.Minimum)
                        p.Restore(k);

                return found;
            }
        }

        public int GetHeight()
        {
            if (Root == null)
                return 0;
            return Node.GetHeight(Root);
        }

        public int NodeCount()
        {
            if (Root == null)
                return 0;
            return Root.GetNodeCount();
        }

        public List<int> GetData()
        {
            if (Root == null)
            {
                return new List<int>();
            }
            return Root.GetData(Root);
        }

        public void WriteToFile(string fileName)
        {
            if (Root == null)
                return;

            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                Root.WriteToStream(sw, Root);
            }
        }

        public void ReadFile(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    int a = 0;
                    if (int.TryParse(line, out a))
                    {
                        Add(a);
                    }
                }
            }
        }

        public void Clear()
        {
            if (Root == null)
            {
                return;
            }

            Root.Clear(Root);
            Root = null;
        }

        /// <summary>
        /// Display the tree in depth first order.
        /// The root is first.
        /// </summary>
        public void Display()
        {
            if (Root == null)
            {
                return;
            }

            Root.DisplayInner(Root);
        }


    }

}
