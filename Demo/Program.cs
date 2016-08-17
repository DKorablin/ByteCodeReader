using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlphaOmega.Debug;

namespace Demo
{
	class Program
	{
		static void Main(String[] args)
		{
			String classFile = @"C:\Visual Studio Projects\Java\HelloWorld\out\production\untitled104\AlphaOmega\Test\PrintTest.class";

			ReadClassFile(classFile);
		}

		private static void ReadClassFile(String classFile)
		{
			using(ClassFile info = new ClassFile(StreamLoader.FromFile(classFile)))
			{
				foreach(var item in info.fields)
					Utils.ConsoleWriteMembers(item);
				foreach(var item in info.methods)
					Utils.ConsoleWriteMembers(item);
				foreach(var item in info.interfaces)
					Utils.ConsoleWriteMembers(item);

				var doubleRow = info.constant_pool.Double.FirstOrDefault();
				Double d = (Double)(doubleRow.high_bytes << 32) + doubleRow.low_bytes;
				//Utils.ConsoleWriteMembers(info.Header1);

				//var tables = info.constant_pool;
			}
		}
	}
}