using System;
using System.IO;
using System.Linq;
using System.Web;
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
				foreach(var item in info.Fields)
					Utils.ConsoleWriteMembers(item);
				foreach(var item in info.Methods)
					Utils.ConsoleWriteMembers(item);
				foreach(var item in info.Interfaces)
					Utils.ConsoleWriteMembers(item);

				var doubleRow = info.ConstantPool.Double.FirstOrDefault();
				var codeRow = info.AttributePool.Code.FirstOrDefault();
				if(codeRow != null)
					Utils.ConsoleWriteMembers(codeRow);
				//Utils.ConsoleWriteMembers(info.Header1);

				//var tables = info.constant_pool;
			}
		}
	}
}