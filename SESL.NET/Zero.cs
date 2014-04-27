using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SESL.NET
{
	public struct Zero : IConvertible
	{
		public static bool IsZeroEquivalent(IConvertible item)
		{
			return item is byte && (byte)item == (byte)0
					||
					item is char && (char)item == (char)0
					||
					item is Int32 && (Int32)item == (Int32)0
					||
					item is Int64 && (Int64)item == (Int64)0
					||
					item is UInt32 && (UInt32)item == (UInt32)0
					||
					item is UInt64 && (UInt64)item == (UInt64)0
					||
					item is Single && (Single)item == (Single)0
					||
					item is SByte && (SByte)item == (SByte)0
					||
					item is Double && (Double)item == 0.0
					||
					item is Decimal && (Decimal)item == Decimal.Zero;
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
			return (byte)0;
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
			return Decimal.Zero;
		}

		public double ToDouble(IFormatProvider provider)
		{
			return 0.0;
		}

		public short ToInt16(IFormatProvider provider)
		{
			return (Int16)0;
		}

		public int ToInt32(IFormatProvider provider)
		{
			return (Int32)0;
		}

		public long ToInt64(IFormatProvider provider)
		{
			return (Int64)0;
		}

		public sbyte ToSByte(IFormatProvider provider)
		{
			return (SByte)0;
		}

		public float ToSingle(IFormatProvider provider)
		{
			return (Single)0;
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
			return (UInt16)0;
		}

		public uint ToUInt32(IFormatProvider provider)
		{
			return (UInt32)0;
		}

		public ulong ToUInt64(IFormatProvider provider)
		{
			return (UInt64)0;
		}
	}
}
