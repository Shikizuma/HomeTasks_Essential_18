using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionary
{
	public class MyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>, IEnumerable, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator
	{
		private List<TKey> keys;
		private List<TValue> values;
		int position = -1;

		public TKey Key { get => keys[position]; }
		public TValue Value { get => values[position]; }

		public MyDictionary()
		{
			keys = new List<TKey>();
			values = new List<TValue>();
		}

		public TValue this[TKey key]
		{
			get
			{
				int index = keys.IndexOf(key);
				if (index >= 0)
					return values[index];
				else
					throw new KeyNotFoundException();
			}
			set
			{
				int index = keys.IndexOf(key);
				if (index >= 0)
					values[index] = value;
				else
					throw new KeyNotFoundException();
			}
		}
		public object? this[object key]
		{
			get
			{
				int index = keys.IndexOf((TKey)key);
				if (index >= 0)
					return values[index];
				else
					throw new KeyNotFoundException();
			}
			set
			{
				int index = keys.IndexOf((TKey)key);
				if (index >= 0)
					values[index] = (TValue)value;
				else
					throw new KeyNotFoundException();
			}
		}

		public ICollection<TKey> Keys => keys;

		public ICollection<TValue> Values => values;

		public int Count => keys.Count;

		public bool IsReadOnly => false;

		public bool IsFixedSize => false;

		public bool IsSynchronized => false;

		public object SyncRoot => throw new NotImplementedException();

		public KeyValuePair<TKey, TValue> Current
		{
			get
			{
				if (position < 0 || position >= keys.Count)
					throw new IndexOutOfRangeException();

				TKey key = keys[position];
				TValue value = values[position];
				return new KeyValuePair<TKey, TValue>(key, value);
			}
		}

		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => keys;

		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => values;

		object IEnumerator.Current => Current;

		public void Add(TKey key, TValue value)
		{
			if (keys.Contains(key))
			{
				throw new ArgumentException();
			}
			else
			{
				keys.Add(key);
				values.Add(value);
			}
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			TKey key = item.Key;
			TValue value = item.Value;

			if (keys.Contains(key))
			{
				throw new ArgumentException();
			}
			else
			{
				keys.Add(key);
				values.Add(value);
			}
		}

		public void Add(object key, object? value)
		{
			if (!(key is TKey))
			{
				throw new ArgumentException();
			}

			TKey typedKey = (TKey)key;
			TValue typedValue = (TValue)value;

			if (keys.Contains(typedKey))
			{
				throw new ArgumentException();
			}
			else
			{
				keys.Add(typedKey);
				values.Add(typedValue);
			}
		}

		public void Clear()
		{
			keys = new List<TKey>();
			values = new List<TValue>();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			TKey key = item.Key;
			TValue value = item.Value;

			int index = keys.IndexOf(key);

			if (index >= 0 && Equals(values[index], value))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool Contains(object key)
		{
			if (keys.Contains((TKey)key))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool ContainsKey(TKey key)
		{
			if (keys.Contains(key))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}

			if (arrayIndex < 0 || arrayIndex >= array.Length)
			{
				throw new ArgumentOutOfRangeException();
			}

			if (array.Length - arrayIndex < keys.Count)
			{
				throw new ArgumentException();
			}

			for (int i = arrayIndex; i < keys.Count; i++)
			{
				array[i] = new KeyValuePair<TKey, TValue>(keys[i], values[i]);
			}

		}

		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}

			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException();
			}

			if (array.Length - index < keys.Count)
			{
				throw new ArgumentException();
			}

			for (int i = index; i < keys.Count; i++)
			{
				array.SetValue(values[i], i);
			}
		}

		public void Dispose()
		{

		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this;
		}

		public bool MoveNext()
		{
			if (position < keys.Count - 1)
			{
				position++;
				return true;
			}

			Reset();
			return false;
		}

		public bool Remove(TKey key)
		{
			if (keys.Contains(key))
			{
				values.RemoveAt(keys.IndexOf(key));
				keys.RemoveAt(keys.IndexOf(key));
				return true;
			}

			return false;
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			TKey key = item.Key;
			TValue value = item.Value;

			int index = keys.IndexOf(key);

			if (keys.Contains(key) && Equals(values[index], value))
			{
				values.RemoveAt(index);
				keys.RemoveAt(index);
				return true;
			}
			else
			{
				return false;
			}
		}

		public void Remove(object key)
		{
			if (keys.Contains((TKey)key))
			{
				values.RemoveAt(keys.IndexOf((TKey)key));
				keys.RemoveAt(keys.IndexOf((TKey)key));
			}
		}

		public void Reset()
		{
			position = -1;
		}

		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			int index = keys.IndexOf(key);
			if (index >= 0)
			{
				value = values[index];
				return true;
			}
			value = default;
			return false;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}