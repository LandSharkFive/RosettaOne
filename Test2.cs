using RosettaOne;

namespace UnitTest
{
    [TestClass]
    public sealed class Test2
    {
        [TestMethod]
        public void TestFive()
        {
            int maxSize = 1000;

            BTree t = new BTree();

            DateTime dtBTreeStart = DateTime.Now;

            for (int i = 0; i < maxSize; i++)
            {
                t.Add(i);
            }

            t.WriteToFile("a.txt");

            BTree t2 = new BTree();

            t2.ReadFile("a.txt");

            for (int i = 0; i < maxSize; i++)
            {
                Assert.IsTrue(t2.Exists(i));
            }

            Console.WriteLine("height " + t.GetHeight());
            Console.WriteLine("height " + t2.GetHeight());

            t.Clear();
            t2.Clear();
        }


    }
}
