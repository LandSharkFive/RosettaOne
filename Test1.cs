using RosettaOne;

namespace UnitTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestOne()
        {
            BTree t = new BTree();

            int maxSize = 1000;

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
                Assert.IsTrue(t.Exists(i));
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

            for (int i = 0; i < maxSize; i++)
            {
                Assert.IsFalse(t.Exists(i));
            }

            t.Clear();
        }

        [TestMethod]
        public void TestTwo()
        {
            Random rnd = new Random();

            List<int> a = new List<int>();

            for (int i = 0; i < 1000; i++)
            {
                a.Add(rnd.Next(1000000));
            }

            a = a.Distinct().ToList();

            Console.WriteLine("count " + a.Count);

            BTree t = new BTree();

            DateTime dtBTreeStart = DateTime.Now;

            for (int i = 0; i < a.Count; i++)
            {
                t.Add(a[i]);
            }

            DateTime dtBTreeEnd = DateTime.Now;

            Console.WriteLine("add {0}", dtBTreeEnd - dtBTreeStart);

            Console.WriteLine("height " + t.GetHeight());

            dtBTreeStart = DateTime.Now;

            for (int i = 0; i < a.Count; i++)
            {
                Assert.IsTrue(t.Exists(a[i]));
            }

            dtBTreeEnd = DateTime.Now;

            Console.WriteLine("search {0}", dtBTreeEnd - dtBTreeStart);

            Util.Shuffle(a);

            dtBTreeStart = DateTime.Now;

            for (int i = 0; i < a.Count; i++)
            {
                t.Remove(a[i]);
            }

            dtBTreeEnd = DateTime.Now;

            Console.WriteLine("remove {0}", dtBTreeEnd - dtBTreeStart);

            for (int i = 0; i < a.Count; i++)
            {
                Assert.IsFalse(t.Exists(a[i]));
            }

            t.Clear();
        }

        [TestMethod]
        public void TestThree()
        {
            int maxSize = 1000;

            BTree t = new BTree();

            for (int i = 0; i < maxSize; i++)
            {
                t.Add(i);
            }

            List<int> b = t.GetData();

            Assert.AreEqual(maxSize, b.Count);
            Assert.IsTrue(Util.IsSorted(b));
            Assert.IsFalse(Util.HasDuplicate(b));
            Console.WriteLine("memory {0}", Util.GetMemory());
            t.Clear();
        }

        [TestMethod]
        public void TestFour()
        {
            Random rnd = new Random();

            List<int> a = new List<int>();

            for (int i = 0; i < 1000; i++)
            {
                a.Add(rnd.Next(1000000));
            }

            a = a.Distinct().ToList();

            Console.WriteLine("count " + a.Count);

            BTree t = new BTree();

            for (int i = 0; i < a.Count; i++)
            {
                t.Add(a[i]);
            }

            List<int> b = t.GetData();

            Assert.AreEqual(a.Count, b.Count);
            Assert.IsTrue(Util.IsSorted(b));
            Assert.IsFalse(Util.HasDuplicate(b));
            Console.WriteLine("memory {0}", Util.GetMemory());
            t.Clear();
        }


    }
}
