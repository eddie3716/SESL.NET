using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET
{
	public struct One : IConvertible
	{
		public static bool IsOneEquivalent(IConvertible item)
		{
			return item is byte && (byte)item == (byte)1
					||
					item is char && (char)item == (char)1
					||
					item is Int32 && (Int32)item == (Int32)1
					||
					item is Int64 && (Int64)item == (Int64)1
					||
					item is UInt32 && (UInt32)item == (UInt32)1
					||
					item is UInt64 && (UInt64)item == (UInt64)1
					||
					item is Single && (Single)item == (Single)1
					||
					item is SByte && (SByte)item == (SByte)1
					||
					item is Double && (Double)item == 1.0
					||
					item is Decimal && (Decimal)item == Decimal.One;
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
			return (byte)1;
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
			return Decimal.One;
		}

		public double ToDouble(IFormatProvider provider)
		{
			return 1.0;
		}

		public short ToInt16(IFormatProvider provider)
		{
			return (Int16)1;
		}

		public int ToInt32(IFormatProvider provider)
		{
			return (Int32)1;
		}

		public long ToInt64(IFormatProvider provider)
		{
			return (Int64)1;
		}

		public sbyte ToSByte(IFormatProvider provider)
		{
			return (SByte)1;
		}

		public float ToSingle(IFormatProvider provider)
		{
			return (Single)1;
		}

		public string ToString(IFormatProvider provider)
		{
			return this.ToString();
		}

		public object ToType(Type conversionType, IFormatProvider provider)
		{
			return Convert.ChangeType(this, conversionType, provider);
		}

		public ushort ToUInt16(IFormatProvider provider)
		{
			return (UInt16)1;
		}

		public uint ToUInt32(IFormatProvider provider)
		{
			return (UInt32)1;
		}

		public ulong ToUInt64(IFormatProvider provider)
		{
			return (UInt64)1;
		}
	}
}
