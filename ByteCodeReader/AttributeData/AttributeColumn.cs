using System;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>Generic attribute column</summary>
	public class AttributeColumn : Column<String>
	{
		private readonly AttributeColumnType _columnType;

		/// <summary>Hardcoded column type</summary>
		public AttributeColumnType ColumnType { get { return this._columnType; } }

		internal AttributeColumn(String tableType, AttributeColumnType columnType, String columnName, UInt16 columnIndex)
			: base(tableType, columnName, columnIndex)
		{
			this._columnType = columnType;
		}
	}
}