using MyDictionary;

namespace HomeTasks_Essential_18
{
	internal class Program
	{
		static void Main(string[] args)
		{
			MyDictionary<int, string> test = new MyDictionary<int, string>();
			test.Add(1, "Слово");
			test.Add(2, "Слово2");
			test.Add(3, "Слово3");
			Console.WriteLine(test.Contains(new KeyValuePair<int, string>(1, "Слово")));

			KeyValuePair<int, string>[] array = new KeyValuePair<int, string>[3];

			test.CopyTo(array, 0);

			foreach (var item in array)
			{
				Console.WriteLine(item + " ");
			}

			foreach (KeyValuePair<int, string> item in test)
			{
				Console.WriteLine(item.Key + " " + item.Value);
			}

			while (test.MoveNext())
			{
				Console.WriteLine(test.Key + " " + test.Value);
			}

		}
	}
}