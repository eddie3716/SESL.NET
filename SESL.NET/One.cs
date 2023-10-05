using System;

namespace SESL.NET
{
    public struct One : IConvertible
	{
		public static bool IsOneEquivalent(IConvertible item)
		{
			return item is byte v && v == 1
                    ||
					item is char v1 && v1 == (char)1
					||
                    item is int v2 && v2 == 1
                    ||
                    item is long v3 && v3 == 1
                    ||
                    item is uint v4 && v4 == 1
                    ||
                    item is ulong v5 && v5 == 1
                    ||
                    item is float v6 && v6 == 1
                    ||
                    item is sbyte v7 && v7 == 1
                    ||
                    item is double v8 && v8 == 1.0
					||
                    item is decimal v9 && v9 == decimal.One;
		}

		public override string ToString()
		{
			return "1";
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Object;
		}

		public bool ToBoolean(IFormatProvider provider)
		{
			return true;
		}

		public byte ToByte(IFormatProvider provider)
		{
			return 1;
		}

		public char ToChar(IFormatProvider provider)
		{
			return (char)1;
		}

		public DateTime ToDateTime(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public decimal ToDecimal(IFormatProvider provider)
		{
			return decimal.One;
		}

		public double ToDouble(IFormatProvider provider)
		{
			return 1.0;
		}

		public short ToInt16(IFormatProvider provider)
		{
			return 1;
		}

		public int ToInt32(IFormatProvider provider)
		{
			return 1;
		}

		public long ToInt64(IFormatProvider provider)
		{
			return 1;
		}

		public sbyte ToSByte(IFormatProvider provider)
		{
			return 1;
		}

		public float ToSingle(IFormatProvider provider)
		{
			return 1;
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
			return 1;
		}

		public uint ToUInt32(IFormatProvider provider)
		{
			return 1;
		}

		public ulong ToUInt64(IFormatProvider provider)
		{
			return 1;
		}
	}
}
