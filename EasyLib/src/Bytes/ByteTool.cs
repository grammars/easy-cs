using System.Text;

namespace EasyLib
{
    public class ByteTool
    {
        // 十六进制下数字到字符的映射数
        private static readonly string[] hexDigits = {"0", "1", "2", "3", "4", "5",
            "6", "7", "8", "9", "a", "b", "c", "d", "e", "f"};

        // 把byte转为字符串的bit
        public static string ByteToBitStr(byte b)
        {
            return "" + (byte)((b >> 7) & 0x1) + (byte)((b >> 6) & 0x1)
                    + (byte)((b >> 5) & 0x1) + (byte)((b >> 4) & 0x1)
                    + (byte)((b >> 3) & 0x1) + (byte)((b >> 2) & 0x1)
                    + (byte)((b >> 1) & 0x1) + (byte)((b >> 0) & 0x1);
        }

        /**
     * 转换字节数组16进制字串
     *
     * @param b 字节数组
     * @return 十六进制字串
     */
        public static string BytesToHex(byte[] b)
        {
            StringBuilder resultSb = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
            {
                resultSb.Append(ByteToHex(b[i]));
            }
            return resultSb.ToString();
        }

        /**
         * 将一个字节转化成16进制形式的字符串
         */
        public static string ByteToHex(byte b)
        {
            int n = b;
            if (n < 0)
                n = 256 + n;
            int d1 = n / 16;
            int d2 = n % 16;
            return hexDigits[d1] + hexDigits[d2];
        }

    }
}
