using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GuidGenerateLab
{
    /// <summary>
    /// https://github.com/MonkSoul/Furion/blob/master/framework/Furion/DistributedIDGenerator/Generators/SequentialGuidIDGenerator.cs
    /// </summary>
    public class SequentialGuidIDGenerator
    {
        /// <summary>
        /// 随机数生成器
        /// </summary>
        private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

        /// <summary>
        /// 生成逻辑
        /// </summary>
        /// <param name="idGeneratorOptions"></param>
        /// <returns></returns>
        public object Create(object idGeneratorOptions = null)
        {
            // According to RFC 4122:
            // dddddddd-dddd-Mddd-Ndrr-rrrrrrrrrrrr
            // - M = RFC version, in this case '4' for random UUID
            // - N = RFC variant (plus other bits), in this case 0b1000 for variant 1
            // - d = nibbles based on UTC date/time in ticks
            // - r = nibbles based on random bytes

            var options = idGeneratorOptions as SequentialGuidSettings;

            var randomBytes = new byte[7];
            _rng.GetBytes(randomBytes);
            var ticks = (ulong)(options?.TimeNow == null ? DateTimeOffset.UtcNow : options.TimeNow.Value).Ticks; // 设置当前 UTC 时间，共 8 字节，64 位，时间戳只占用其中60位

            var uuidVersion = (ushort)4; // 设置 UUID 版本为 v4，共 2 字节
            var uuidVariant = (ushort)0b1000;

            var ticksAndVersion = (ushort)((ticks << 48 >> 52) | (ushort)(uuidVersion << 12)); // 时间戳和版本，时间戳抹掉高位 48 bit(60 - 48)，低位 4 bit，仅保留 4-12bit
            var ticksAndVariant = (byte)((ticks << 60 >> 60) | (byte)(uuidVariant << 4));

            if (options?.LittleEndianBinary16Format == true)
            {
                var guidBytes = new byte[16];
                var tickBytes = BitConverter.GetBytes(ticks);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(tickBytes);
                }

                Buffer.BlockCopy(tickBytes, 0, guidBytes, 0, 6);
                guidBytes[6] = (byte)(ticksAndVersion << 8 >> 8);
                guidBytes[7] = (byte)(ticksAndVersion >> 8);
                guidBytes[8] = ticksAndVariant;
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 9, 7);

                return new Guid(guidBytes);
            }

            var guid = new Guid((uint)(ticks >> 32), (ushort)(ticks << 32 >> 48), ticksAndVersion,
                ticksAndVariant,
                randomBytes[0],
                randomBytes[1],
                randomBytes[2],
                randomBytes[3],
                randomBytes[4],
                randomBytes[5],
                randomBytes[6]);

            return guid;
        }
    }

    /// <summary>
    /// 连续 GUID 配置
    /// </summary>
    public class SequentialGuidSettings
    {
        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTimeOffset? TimeNow { get; set; }

        /// <summary>
        /// LittleEndianBinary 16 格式化
        /// </summary>
        public bool LittleEndianBinary16Format { get; set; }
    }
}
