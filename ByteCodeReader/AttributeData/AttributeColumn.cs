using System;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>Generic attribute column</summary>
	public class AttributeColumn : Column<String>
	{
		/// <summary>Hardcoded column type</summary>
		public AttributeColumnType ColumnType { get; }

		internal AttributeColumn(String tableType, AttributeColumnType columnType, String columnName, UInt16 columnIndex)
			: base(tableType, columnName, columnIndex)
			=> this.ColumnType = columnType;
	}
}