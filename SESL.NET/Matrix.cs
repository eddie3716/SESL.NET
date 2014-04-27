using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using SESL.NET.Exception;

namespace SESL.NET
{
	public class Matrix : IConvertible, IEnumerable<IEnumerable<double>>
	{
		public Matrix()
		{
			_inner = new Vector[0];
			this.RowCount = 0;
			this.ColumnCount = 0;
		}

		public Matrix(Matrix matrix)
			: this((IEnumerable<IEnumerable<double>>)matrix)
		{
		}

		public Matrix(IEnumerable<IEnumerable<double>> enumerable)
		{
			_inner = enumerable.Select(x => new Vector(x)).ToArray();
			this.RowCount = _inner.Select(x => x.Length).OrderByDescending(x => x).FirstOrDefault();
			this.ColumnCount = _inner.Count();

			foreach (var vector in _inner)
			{
				if (vector.Length != this.RowCount)
				{
					throw new MatrixInitializationException();
				}
			}
		}

		private Vector[] _inner;

		public int RowCount { get; private set; }

		public int ColumnCount { get; private set; }

		public double this[int i, int j]
		{
			get { return _inner[i][j]; }
		}

		public IEnumerable<double> this[int i] 
		{ 
			get { return _inner[i]; }
		}

		public IEnumerator<IEnumerable<double>> GetEnumerator()
		{
			return new MatrixEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public TypeCode GetTypeCode()
		{
			return TypeCode.Object;
		}

		public bool ToBoolean(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public byte ToByte(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public char ToChar(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public DateTime ToDateTime(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public decimal ToDecimal(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public double ToDouble(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public short ToInt16(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public int ToInt32(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public long ToInt64(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public sbyte ToSByte(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public float ToSingle(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public string ToString(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public object ToType(Type conversionType, IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public ushort ToUInt16(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public uint ToUInt32(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public ulong ToUInt64(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}
	}
}
