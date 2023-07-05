using System;
using System.IO;
using System.Linq;
using AlphaOmega.Debug;

namespace Demo
{
	class Program
	{
		static void Main(String[] args)
		{
			foreach(String classFile in Directory.GetFiles(@"C:\Visual Studio Projects\C#\Shared.Classes\AlphaOmega.Debug\FileReader\Samples", "*.class", SearchOption.AllDirectories))
				ReadClassFile(classFile);
		}

		private static void ReadClassFile(String classFile)
		{
			Console.WriteLine("Reading file: {0}", classFile);
			using(ClassFile info = new ClassFile(StreamLoader.FromFile(classFile)))
			{
				foreach(var item in info.fields)
					Utils.ConsoleWriteMembers(item);
				foreach(var item in info.methods)
					Utils.ConsoleWriteMembers(item);
				foreach(var item in info.interfaces)
					Utils.ConsoleWriteMembers(item);

				var doubleRow = info.constant_pool.Double.FirstOrDefault();
				var codeRow = info.attribute_pool.Code.FirstOrDefault();
				if(codeRow != null)
					Utils.ConsoleWriteMembers(codeRow);
				//Utils.ConsoleWriteMembers(info.Header1);

				//var tables = info.constant_pool;
			}
		}
	}
}