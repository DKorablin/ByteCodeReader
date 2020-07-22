using System;
using System.Diagnostics;

namespace AlphaOmega.Debug
{
	/// <summary>Generic colum for dynamic structures</summary>
	/// <typeparam name="T">Type of the owner table</typeparam>
	[DebuggerDisplay("Type={TableType} Name={Name}")]
	public class Column<T> : IColumn
	{
		#region Fields
		private readonly T _tableType;
		private readonly String _columnName;
		private readonly UInt16 _columnIndex;
		#endregion Fields

		/// <summary>Table type</summary>
		public T TableType { get { return this._tableType; } }

		/// <summary>Name of the column. From CONSTANT structures</summary>
		public String Name { get { return this._columnName; } }

		/// <summary>Zero based index from the beggining of structure</summary>
		public UInt16 Index { get { return this._columnIndex; } }

		/// <summary>Create instance of generic column specifying parent table and column description</summary>
		/// <param name="tableType">Type of the owner table</param>
		/// <param name="columnName">Name of the column</param>
		/// <param name="columnIndex">column index</param>
		public Column(T tableType, String columnName, UInt16 columnIndex)
		{
			if(tableType == null)
				throw new ArgumentNullException("tableType");
			if(String.IsNullOrEmpty(columnName))
				throw new ArgumentNullException("columnName");

			this._tableType = tableType;
			this._columnName = columnName;
			this._columnIndex = columnIndex;
		}
	}
}