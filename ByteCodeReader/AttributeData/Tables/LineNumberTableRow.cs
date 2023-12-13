using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>
	/// The LineNumberTable attribute is an optional variable-length attribute in the attributes table of a Code attribute (§4.7.3).
	/// It may be used by debuggers to determine which part of the code array corresponds to a given line number in the original source file.
	/// </summary>
	/// <remarks>
	/// If multiple LineNumberTable attributes are present in the attributes table of a Code attribute, then they may appear in any order.
	/// There may be more than one LineNumberTable attribute per line of a source file in the attributes table of a Code attribute. That is, LineNumberTable attributes may together represent a given line of a source file, and need not be one-to-one with source lines.
	/// </remarks>
	public class LineNumberTableRow : BaseRow<String>
	{
		private AttributeReference[] LineNumberTableI => base.GetValue<AttributeReference[]>(0);

		/// <summary>The value of the line_number_table_length item indicates the number of entries in the line_number_table array</summary>
		public UInt16 LineNumberTableLength => (UInt16)this.LineNumberTableI.Length;

		/// <summary>Each entry in the line_number_table array indicates that the line number in the original source file changes at a given point in the code array</summary>
		public LineNumberTableRefRow[] LineNumberTable
		{
			get
			{
				AttributeReference[] references = this.LineNumberTableI;
				BaseTable<LineNumberTableRefRow, String> baseTable = base.Root.File.AttributePool.LineNumberTableRef;

				return Array.ConvertAll(references, delegate(AttributeReference item) { return baseTable[item.Index]; });
			}
		}
	}
}