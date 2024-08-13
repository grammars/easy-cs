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
            Console.WriteLine("容量1="+buffer.Capacity+ ", ReadableBytes=" + buffer.ReadableBytes);
            buffer.WriteByte(6);
            Console.WriteLine("容量2=" + buffer.Capacity+ ", ReadableBytes=" + buffer.ReadableBytes);
            //byte b0 = buffer.ReadByte();
            //byte b1 = buffer.ReadByte();
            //byte b2 = buffer.ReadByte();
            //byte b3 = buffer.ReadByte();
            //Console.WriteLine("输出点啥 b0=" + b0 + ", b1=" + b1 + ", b2=" + b2 + ", b3=" + b3);

            byte[] all = new byte[buffer.ReadableBytes];
            buffer.ReadBytes(all, 0, all.Length);
            Console.WriteLine("输出all b0=" + all[0] + ", b1=" + all[1] + ", b2=" + all[2] + ", b3=" + all[3]);
            Assert.Pass();
        }

        [Test]
        public void TestEndian()
        {
            ByteBuffer buffer = ByteBuffer.Allocate(0);
            buffer.WriteInt(18);
            var bytes = buffer.ToArray();
            Console.WriteLine("大端:"+ByteTool.BytesToHex(bytes));

            ByteBuffer bufferLE = ByteBuffer.Allocate(0).UseLittleEndian(true);
            bufferLE.WriteInt(18);
            var bytesLE = bufferLE.ToArray();
            Console.WriteLine("小端:" + ByteTool.BytesToHex(bytesLE));
        }
    }
}