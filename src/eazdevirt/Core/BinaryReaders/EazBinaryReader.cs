using dnlib.IO;
using System;
using System.IO;

namespace eazdevirt.Core
{
    internal class EazBinaryReader : BinaryReader
    {
        public EazBinaryReader(Stream input) : base(input)
        {
        }

        public override short ReadInt16()
        {
            var bytes = this.ReadBytes(2);
            return (short)((int)bytes[1] << 8 | (int)bytes[0]);
        }

        public override ushort ReadUInt16()
        {
            var bytes = this.ReadBytes(2);
            return (ushort)((int)bytes[0] | (int)bytes[1] << 8);
        }

        public override Int32 ReadInt32()
        {
            var bytes = this.ReadBytes(4);
            return (int)bytes[2] | (int)bytes[3] << 24 | (int)bytes[0] << 16 | (int)bytes[1] << 8;
        }

        public override long ReadInt64()
        {
            var bytes = this.ReadBytes(8);
            return (long)((ulong)((int)bytes[1] << 24 | (int)bytes[7] | (int)bytes[4] << 16 | (int)bytes[0] << 8) | (ulong)((ulong)((long)((int)bytes[6] << 8 | (int)bytes[3] | (int)bytes[5] << 16 | (int)bytes[2] << 24)) << 32));
        }

        public override ulong ReadUInt64()
        {
            var bytes = this.ReadBytes(8);
            return (ulong)((int)bytes[7] << 16 | (int)bytes[6] | (int)bytes[0] << 8 | (int)bytes[5] << 24) | (ulong)((ulong)((long)((int)bytes[3] | (int)bytes[4] << 8 | (int)bytes[2] << 24 | (int)bytes[1] << 16)) << 32);
        }

        public override uint ReadUInt32()
        {
            var bytes = this.ReadBytes(4);
            return (uint)((int)bytes[2] | (int)bytes[0] << 8 | (int)bytes[1] << 16 | (int)bytes[3] << 24);
        }

        public override float ReadSingle()
        {
            var bytes = this.ReadBytes(4);
            byte[] array = new byte[4];
            array[0] = bytes[2];
            array[1] = bytes[3];
            array[3] = bytes[1];
            array[2] = bytes[0];
            return ToBinaryReader(array).ReadSingle();
        }

        public override double ReadDouble()
        {
            var bytes = this.ReadBytes(8);
            byte[] array = new byte[8];
            array[4] = bytes[0];
            array[0] = bytes[3];
            array[6] = bytes[1];
            array[7] = bytes[2];
            array[2] = bytes[4];
            array[3] = bytes[6];
            array[5] = bytes[7];
            array[1] = bytes[5];
            return ToBinaryReader(array).ReadDouble();
        }

        public override decimal ReadDecimal()
        {
            var bytes = this.ReadBytes(16);
            byte[] array = new byte[16];
            array[14] = bytes[2];
            array[10] = bytes[9];
            array[0] = bytes[0];
            array[4] = bytes[7];
            array[12] = bytes[14];
            array[15] = bytes[15];
            array[3] = bytes[4];
            array[7] = bytes[12];
            array[6] = bytes[11];
            array[2] = bytes[6];
            array[13] = bytes[3];
            array[5] = bytes[1];
            array[11] = bytes[8];
            array[1] = bytes[13];
            array[9] = bytes[10];
            array[8] = bytes[5];
            return ToBinaryReader(array).ReadDecimal();
        }

        private BinaryReader ToBinaryReader(byte[] input)
        {
            MemoryStream memoryStream = new MemoryStream(8);
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            binaryReader.BaseStream.Position = 0L;
            memoryStream.Write(input, 0, input.Length);
            memoryStream.Position = 0L;
            return binaryReader;
        }
    }
}
