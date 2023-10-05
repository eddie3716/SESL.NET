using System;

namespace SESL.NET
{
    public struct Zero : IConvertible
	{
		public static bool IsZeroEquivalent(IConvertible item)
		{
			return item is byte && (byte)item == 0
                    ||
					item is char && (char)item == (char)0
					||
                    item is int && (int)item == 0
                    ||
                    item is long && (long)item == 0
                    ||
                    item is uint && (uint)item == 0
                    ||
                    item is ulong && (ulong)item == 0
                    ||
                    item is float && (float)item == 0
                    ||
                    item is sbyte && (sbyte)item == 0
                    ||
                    item is double && (double)item == 0.0
					||
                    item is decimal && (decimal)item == decimal.Zero;
		}

		public override string ToString()
		{
			return "0";
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Object;
		}

		public bool ToBoolean(IFormatProvider provider)
		{
			return false;
		}

		public byte ToByte(IFormatProvider provider)
		{
			return 0;
		}

		public char ToChar(IFormatProvider provider)
		{
			return (char)0;
		}

		public DateTime ToDateTime(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public decimal ToDecimal(IFormatProvider provider)
		{
			return decimal.Zero;
		}

		public double ToDouble(IFormatProvider provider)
		{
			return 0.0;
		}

		public short ToInt16(IFormatProvider provider)
		{
			return 0;
		}

		public int ToInt32(IFormatProvider provider)
		{
			return 0;
		}

		public long ToInt64(IFormatProvider provider)
		{
			return 0;
		}

		public sbyte ToSByte(IFormatProvider provider)
		{
			return 0;
		}

		public float ToSingle(IFormatProvider provider)
		{
			return 0;
		}

		public string ToString(IFormatProvider provider)
		{
			return ToString();
		}

		public object ToType(Type conversionType, IFormatProvider provider)
		{
			return Convert.ChangeType(this, conversionType, provider);
		}

		public ushort ToUInt16(IFormatProvider provider)
		{
			return 0;
		}

		public uint ToUInt32(IFormatProvider provider)
		{
			return 0;
		}

		public ulong ToUInt64(IFormatProvider provider)
		{
			return 0;
		}
	}
}
