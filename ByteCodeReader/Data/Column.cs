using System;
using System.Diagnostics;

namespace AlphaOmega.Debug
{
	/// <summary>Generic colum for dynamic structures</summary>
	/// <typeparam name="T">Type of the owner table</typeparam>
	[DebuggerDisplay("Type={"+nameof(TableType)+"} Name={"+nameof(Name)+"}")]
	public class Column<T> : IColumn
	{
		/// <summary>Table type</summary>
		public T TableType { get; }

		/// <summary>Name of the column</summary>
		/// <remarks>From CONSTANT structures</remarks>
		public String Name { get; }

		/// <summary>Zero based index from the beggining of structure</summary>
		public UInt16 Index { get; }

		/// <summary>Create instance of generic column specifying parent table and column description</summary>
		/// <param name="tableType">Type of the owner table</param>
		/// <param name="columnName">Name of the column</param>
		/// <param name="columnIndex">column index</param>
		public Column(T tableType, String columnName, UInt16 columnIndex)
		{
			if(tableType == null)
				throw new ArgumentNullException(nameof(tableType));
			if(String.IsNullOrEmpty(columnName))
				throw new ArgumentNullException(nameof(columnName));

			this.TableType = tableType;
			this.Name = columnName;
			this.Index = columnIndex;
		}
	}
}