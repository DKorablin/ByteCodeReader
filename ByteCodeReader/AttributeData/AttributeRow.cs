using System;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>Generic constant row</summary>
	public class AttributeRow : Row<String>
	{
		internal AttributeRow(AttributeTable table, UInt16 index, AttributeCell[] cells)
			: base(table, index, cells)
		{
		}
	}
}