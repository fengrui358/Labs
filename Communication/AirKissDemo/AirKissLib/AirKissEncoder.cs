using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirKissLib
{
    public class AirKissEncoder
    {
        private readonly int[] _encodedData = new int[2 << 14]; //32768
        private int _length;

        private readonly char _randomChar = (char) (new Random().Next(0x7F));

        public AirKissEncoder(string ssid, string passWord)
        {
            var times = 5;
            while (times-- > 0)
            {
                LeadingPart();
                MagicCode(ssid, passWord);

                for (int i = 0; i < 15; ++i) //注意是 ++i
                {
                    PrefixCode(passWord);
                    var data = passWord + _randomChar + ssid;

                    int index;
                    var content = new byte[4];

                    for (index = 0; index < data.Length / 4; ++index)
                    {
                        Array.Copy(Encoding.UTF8.GetBytes(data), index*4, content, 0, content.Length);
                        Sequence(index, content);
                    }

                    if (data.Length % 4 != 0)
                    {
                        content = new byte[data.Length % 4];
                        Array.Copy(Encoding.UTF8.GetBytes(data), index * 4, content, 0, content.Length);
                        Sequence(index, content);
                    }
                }
            }
        }

        public char GetRandomChar()
        {
            return _randomChar;
        }

        public int[] GetEncodedData()
        {
            var encodedData = new int[_length];
            _encodedData.CopyTo(encodedData, _length);

            return encodedData;
        }

        /// <summary>
        /// 构造前导域
        /// </summary>
        private void LeadingPart()
        {
            //发送400ms的前导域。todo：这里应该不止
            for (int i = 0; i < 50; ++i)
            {
                //前导域固定有四个字节组成：{1,2,3,4}
                for (int j = 1; j <= 4; ++j)
                {
                    AppendEncodedData(j);
                }
            }
        }

        private void AppendEncodedData(int length)
        {
            _encodedData[_length++] = length;
        }

        private void MagicCode(string ssid, string passWord)
        {
            var length = ssid.Length + passWord.Length + 1;

            var magicCode = new int[4];

            magicCode[0] = 0x00 | (length >> 4 & 0xF);
            if (magicCode[0] == 0)
            {
                magicCode[0] = 0x08;
            }

            magicCode[1] = 0x10 | (length & 0xF);

            var crc8 = CRC8(ssid);
            magicCode[2] = 0x20 | (crc8 >> 4 & 0xF);
            magicCode[3] = 0x30 | (crc8 & 0xF);

            for (int i = 0; i < 20; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    AppendEncodedData(magicCode[j]);
                }
            }
        }

        private void PrefixCode(string password)
        {
            var length = password.Length;
            var prefixCode = new int[4];
            prefixCode[0] = 0x40 | (length >> 4 & 0xF);
            prefixCode[1] = 0x50 | (length & 0xF);

            var crc8 = CRC8(new byte[] { (byte)length });
            prefixCode[2] = 0x60 | (crc8 >> 4 & 0xF);
            prefixCode[3] = 0x70 | (crc8 & 0xF);

            for (int j = 0; j < 4; ++j)
            {
                AppendEncodedData(prefixCode[j]);
            }
        }

        /// <summary>
        /// CRC8校验码
        /// </summary>
        /// <param name="stringData"></param>
        /// <returns></returns>
        private int CRC8(string stringData)
        {
            return CRC8(Encoding.UTF8.GetBytes(stringData));
        }

        /// <summary>
        /// CRC8校验码
        /// </summary>
        /// <param name="bytesData"></param>
        /// <returns></returns>
        private int CRC8(byte[] bytesData)
        {
            int len = bytesData.Length;
            int i = 0;
            byte crc = 0x00;
            while (len-- > 0)
            {
                byte extract = bytesData[i++];
                for (byte tempI = 8; tempI != 0; tempI--)
                {
                    byte sum = (byte)((crc & 0xFF) ^ (extract & 0xFF));
                    sum = (byte)((sum & 0xFF) & 0x01);
                    crc = (byte)((crc & 0xFF) >> 1);
                    if (sum != 0)
                    {
                        crc = (byte)((crc & 0xFF) ^ 0x8C);
                    }
                    extract = (byte)((extract & 0xFF) >> 1);
                }
            }

            return (crc & 0xFF);
        }

        private void Sequence(int index, byte[] data)
        {
            var content = new byte[data.Length + 1];
            content[0] = (byte)(index & 0xFF);

            Array.Copy(data, 0, content, 1, data.Length);
            int crc8 = CRC8(content);
            AppendEncodedData(0x80 | crc8);
            AppendEncodedData(0x80 | index);

            foreach (var b in data)
            {
                AppendEncodedData(b | 0x100);
            }
        }
    }
}
