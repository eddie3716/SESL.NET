using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SESL.NET
{
	public class VectorEnumerator : IEnumerator<double>, IEnumerator
	{
		private int _currentIndex;
		private Vector _vector;

		public VectorEnumerator(Vector vector)
		{
			_vector = vector;
			Reset();
		}

		public double Current
		{
			get
			{
				return _vector[_currentIndex];
			}
		}

		object IEnumerator.Current
		{
			get { return this.Current; }
		}

		public void Dispose()
		{
			_vector = null;
			Reset();
		}

		public bool MoveNext()
		{
			return ++_currentIndex >= _vector.Length;
		}

		public void Reset()
		{
			_currentIndex = -1;
		}
	}
}
