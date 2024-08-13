using EasyLib;

namespace MyTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestBase()
        {
            ByteBuffer buffer = ByteBuffer.Allocate(3);
            buffer.WriteByte(3);
            buffer.WriteByte(4);
            buffer.WriteByte(5);
            buffer.WriteByte(6);
            byte b0 = buffer.ReadByte();
            byte b1 = buffer.ReadByte();
            byte b2 = buffer.ReadByte();
            byte b3 = buffer.ReadByte();

            Console.WriteLine("Êä³öµãÉ¶ b0=" + b0 + ", b1=" + b1 + ", b2=" + b2 + ", b3=" + b3);
            Assert.Pass();
        }
    }
}