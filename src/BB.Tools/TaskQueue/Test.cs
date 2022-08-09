namespace BB.Tools.TaskQueue;

class Test
{
	void Test1()
	{
		TaskManager tm = new TaskManager((obj) =>
		{
			Console.WriteLine("{0} {1}", DateTime.Now.ToString("HH:mm:ss.fff"), obj);
		}, "test");

		tm.TaskExecuted += (guid) =>
		{
			Console.WriteLine("{0} {1}", DateTime.Now.ToString("HH:mm:ss.fff"), guid);
		};

		for (int i = 0; i < 10; i++)
		{
			tm.Append(i);
		}

		Console.WriteLine("{0} end", DateTime.Now.ToString("HH:mm:ss.fff"));

		//Thread.Sleep(5000);

		tm.Dispose();

		Console.WriteLine("{0} end2", DateTime.Now.ToString("HH:mm:ss.fff"));
	}

}