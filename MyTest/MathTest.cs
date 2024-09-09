using EasyLib;

namespace MyTest
{
    public class MathTest
    {
        [Test]
        public void TestSize()
        {
            Size size0 = new();
            Size size1 = new(1.2f, 3.4f);
            Console.WriteLine("size0={0} size1={1}", size0, size1);
        }
    
    }
}
