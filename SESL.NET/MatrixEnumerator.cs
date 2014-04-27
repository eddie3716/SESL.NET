using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SESL.NET
{
	public class MatrixEnumerator : IEnumerator<IEnumerable<double>>, IEnumerator
	{
		private int _currentIndex;

		private Matrix _matrix;

		public MatrixEnumerator(Matrix matrix)
		{
			_matrix = matrix;
			Reset();
		}

		public void Reset()
		{
			_currentIndex = -1;
		}

		public IEnumerable<double> Current
		{
			get
			{
				return _matrix[_currentIndex];
			}
		}

		object IEnumerator.Current
		{
			get { return this.Current; }
		}

		public void Dispose()
		{
			_matrix = null;
			Reset();
		}

		public bool MoveNext()
		{
			return ++_currentIndex >= _matrix.ColumnCount;
		}
	}
}
