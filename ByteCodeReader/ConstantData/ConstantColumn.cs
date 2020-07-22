using System;
using System.Diagnostics;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>Generic constant column</summary>
	[DebuggerDisplay("tag={TableType} Name={Name}")]
	public class ConstantColumn : Column<Jvm.CONSTANT>
	{
		#region Fields
		private readonly ConstantColumnType _columnType;
		#endregion Fields

		/// <summary>Hardcoed column types</summary>
		public ConstantColumnType ColumnType { get { return this._columnType; } }

		internal ConstantColumn(Jvm.CONSTANT tableType, ConstantColumnType columnType, String columnName, UInt16 columnIndex)
			: base(tableType, columnName, columnIndex)
		{
			this._columnType = columnType;
		}
	}
}