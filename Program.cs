namespace RosettaOne
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestOne();
        }

        static void TestOne()
        {
            int maxSize = 1000;

            BTree t = new BTree();

            DateTime dtBTreeStart = DateTime.Now;

            for (int i = 0; i < maxSize; i++)
            {
                t.Add(i);
            }

            DateTime dtBTreeEnd = DateTime.Now;

            Console.WriteLine("add {0}", dtBTreeEnd - dtBTreeStart);

            Console.WriteLine("height " + t.GetHeight());

            dtBTreeStart = DateTime.Now;

            for (int i = 0; i < maxSize; i++)
            {
                if (!t.Exists(i))
                {
                    Console.WriteLine(i + " not found");
                }
            }

            dtBTreeEnd = DateTime.Now;

            Console.WriteLine("search {0}", dtBTreeEnd - dtBTreeStart);

            dtBTreeStart = DateTime.Now;

            for (int i = 0; i < maxSize; i++)
            {
                t.Remove(i);
            }

            dtBTreeEnd = DateTime.Now;

            Console.WriteLine("remove {0}", dtBTreeEnd - dtBTreeStart);
            Console.WriteLine("memory {0}", Util.GetMemory());

            for (int i = 0; i < maxSize; i++)
            {
                if (t.Exists(i))
                {
                    Console.WriteLine(i + " found");
                }
            }

            t.Clear();
        }

        static void TestTwo()
        {
            int maxSize = 1000;

            BTree t = new BTree();

            DateTime dtBTreeStart = DateTime.Now;

            for (int i = 0; i < maxSize; i++)
            {
                t.Add(i);
            }

            Console.WriteLine("height " + t.GetHeight());

            t.Display();
            t.Clear();
        }

    }
}
