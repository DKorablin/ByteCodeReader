using System;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>Generic constant row</summary>
	public class ConstantRow : Row<Jvm.CONSTANT>
	{
		internal ConstantRow(ConstantTable table, UInt16 index, ConstantCell[] cells)
			: base(table, index, cells)
		{
		}
	}
}