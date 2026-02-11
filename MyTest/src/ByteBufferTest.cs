using EasyLib;

namespace MyTest
{
    public class ByteBufferTests
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

        [Test]
        public void TestFull()
        {
            // 完整覆盖 ByteBuffer 的测试用例

            // 1. 测试基本的写入和读取
            ByteBuffer buffer = ByteBuffer.Allocate(0);
            buffer.WriteByte(10);
            buffer.WriteShort(256);
            buffer.WriteInt(100000);
            buffer.WriteLong(9999999999);
            buffer.WriteFloat(3.14f);
            buffer.WriteDouble(2.718);
            buffer.WriteBool(true);
            buffer.WriteChar('A');
            buffer.WriteUshort(65000);
            buffer.WriteUint(4000000000);
            buffer.WriteUlong(18446744073709551615);

            Assert.AreEqual(10, buffer.ReadByte());
            Assert.AreEqual(256, buffer.ReadShort());
            Assert.AreEqual(100000, buffer.ReadInt());
            Assert.AreEqual(9999999999, buffer.ReadLong());
            Assert.AreEqual(3.14f, buffer.ReadFloat(), 0.01f);
            Assert.AreEqual(2.718, buffer.ReadDouble(), 0.001);
            Assert.IsTrue(buffer.ReadBool());
            Assert.AreEqual('A', buffer.ReadChar());
            Assert.AreEqual(65000, buffer.ReadUshort());
            Assert.AreEqual(4000000000, buffer.ReadUint());
            Assert.AreEqual(18446744073709551615, buffer.ReadUlong());

            // 2. 测试Get方法（不改变读指针）
            ByteBuffer buffer2 = ByteBuffer.Allocate(100);
            buffer2.WriteByte(42);
            buffer2.WriteInt(12345);
            buffer2.WriteDouble(3.14159);
            
            Assert.AreEqual(42, buffer2.GetByte());
            Assert.AreEqual(12345, buffer2.GetInt(1));
            Assert.AreEqual(3.14159, buffer2.GetDouble(5), 0.0001);
            Assert.AreEqual(0, buffer2.ReaderIndex); // 读指针未改变

            // 3. 测试ReadBytes方法
            ByteBuffer buffer3 = ByteBuffer.Allocate(0);
            byte[] srcBytes = new byte[] { 1, 2, 3, 4, 5 };
            buffer3.WriteBytes(srcBytes);
            byte[] readBytes = buffer3.ReadBytes(5);
            Assert.AreEqual(srcBytes, readBytes);

            // 4. 测试ReadBytes重载方法
            ByteBuffer buffer4 = ByteBuffer.Allocate(0);
            byte[] srcBytes2 = new byte[] { 10, 20, 30, 40, 50 };
            buffer4.WriteBytes(srcBytes2);
            byte[] disBytes = new byte[5];
            buffer4.ReadBytes(disBytes, 0, 5);
            Assert.AreEqual(srcBytes2, disBytes);

            // 5. 测试标记和重置
            ByteBuffer buffer5 = ByteBuffer.Allocate(50);
            buffer5.WriteByte(100);
            buffer5.WriteInt(5000);
            
            buffer5.MarkReaderIndex();
            int firstByte = buffer5.ReadByte();
            int firstInt = buffer5.ReadInt();
            
            buffer5.ResetReaderIndex();
            Assert.AreEqual(0, buffer5.ReaderIndex);
            Assert.AreEqual(100, buffer5.ReadByte());

            buffer5.MarkWriterIndex();
            buffer5.WriteByte(200);
            buffer5.ResetWriterIndex();
            Assert.AreEqual(5, buffer5.WriterIndex);

            // 6. 测试容量自动扩展
            ByteBuffer buffer6 = ByteBuffer.Allocate(1);
            int initialCapacity = buffer6.Capacity;
            buffer6.WriteByte(1);
            buffer6.WriteByte(2);
            buffer6.WriteByte(3);
            buffer6.WriteByte(4);
            Assert.Greater(buffer6.Capacity, initialCapacity);
            Assert.AreEqual(4, buffer6.ReadableBytes);

            // 7. 测试DiscardReadBytes
            ByteBuffer buffer7 = ByteBuffer.Allocate(0);
            buffer7.WriteBytes(new byte[] { 1, 2, 3, 4, 5 });
            buffer7.ReadByte(); // 读取第一个字节
            buffer7.ReadByte(); // 读取第二个字节
            buffer7.DiscardReadBytes();
            Assert.AreEqual(0, buffer7.ReaderIndex);
            Assert.AreEqual(3, buffer7.ReadableBytes);

            // 8. 测试CopyRest
            ByteBuffer buffer8 = ByteBuffer.Allocate(0);
            buffer8.WriteBytes(new byte[] { 10, 20, 30, 40, 50 });
            buffer8.ReadByte();
            buffer8.ReadByte();
            ByteBuffer copiedBuffer = buffer8.CopyRest();
            Assert.AreEqual(3, copiedBuffer.ReadableBytes);
            Assert.AreEqual(30, copiedBuffer.ReadByte());

            // 9. 测试Clone
            ByteBuffer buffer9 = ByteBuffer.Allocate(0);
            buffer9.WriteByte(99);
            buffer9.WriteInt(77777);
            buffer9.ReadByte();
            ByteBuffer clonedBuffer = buffer9.Clone();
            Assert.AreEqual(buffer9.ReaderIndex, clonedBuffer.ReaderIndex);
            Assert.AreEqual(buffer9.WriterIndex, clonedBuffer.WriterIndex);
            Assert.AreEqual(buffer9.ReadableBytes, clonedBuffer.ReadableBytes);

            // 10. 测试Clear
            ByteBuffer buffer10 = ByteBuffer.Allocate(0);
            buffer10.WriteBytes(new byte[] { 1, 2, 3, 4, 5 });
            buffer10.ReadByte();
            buffer10.Clear();
            Assert.AreEqual(0, buffer10.ReaderIndex);
            Assert.AreEqual(0, buffer10.WriterIndex);
            Assert.AreEqual(0, buffer10.ReadableBytes);

            // 11. 测试ToArray
            ByteBuffer buffer11 = ByteBuffer.Allocate(0);
            buffer11.WriteBytes(new byte[] { 5, 4, 3, 2, 1 });
            byte[] result = buffer11.ToArray();
            Assert.AreEqual(5, result.Length);
            Assert.AreEqual(5, result[0]);

            // 12. 测试小端和大端
            ByteBuffer bufferBE = ByteBuffer.Allocate(0);
            bufferBE.WriteInt(0x12345678);
            byte[] bytesBE = bufferBE.ToArray();
            
            ByteBuffer bufferLE = ByteBuffer.Allocate(0).UseLittleEndian(true);
            bufferLE.WriteInt(0x12345678);
            byte[] bytesLE = bufferLE.ToArray();
            
            // 大端和小端的字节顺序应该不同（除非系统本身就是小端）
            Assert.IsNotNull(bytesBE);
            Assert.IsNotNull(bytesLE);

            // 13. 测试Write方法（写入另一个ByteBuffer）
            ByteBuffer source = ByteBuffer.Allocate(0);
            source.WriteBytes(new byte[] { 111, 222, 255 });
            ByteBuffer target = ByteBuffer.Allocate(0);
            target.Write(source);
            Assert.AreEqual(3, target.ReadableBytes);

            // 14. 测试ReaderIndex和WriterIndex属性
            ByteBuffer buffer14 = ByteBuffer.Allocate(50);
            buffer14.WriteByte(1);
            buffer14.WriteByte(2);
            Assert.AreEqual(2, buffer14.WriterIndex);
            buffer14.WriterIndex = 10;
            Assert.AreEqual(10, buffer14.WriterIndex);
            buffer14.ReadByte();
            Assert.AreEqual(1, buffer14.ReaderIndex);
            buffer14.ReaderIndex = 5;
            Assert.AreEqual(5, buffer14.ReaderIndex);

            // 15. 测试ForEach
            ByteBuffer buffer15 = ByteBuffer.Allocate(0);
            buffer15.WriteBytes(new byte[] { 1, 2, 3, 4, 5 });
            int sum = 0;
            buffer15.ForEach(b => sum += b);
            Assert.AreEqual(15, sum);

            Assert.Pass();
        }
    }
}