using System;
using AlphaOmega.Debug.Data;
using System.Collections.Generic;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>
	/// The LocalVariableTable attribute is an optional variable-length attribute in the attributes table of a Code attribute (§4.7.3)
	/// It may be used by debuggers to determine the value of a given local variable during the execution of a method
	/// </summary>
	/// <remarks>
	/// If multiple LocalVariableTable attributes are present in the attributes table of a Code attribute, then they may appear in any order.
	/// There may be no more than one LocalVariableTable attribute per local variable in the attributes table of a Code attribute.
	/// </remarks>
	public class LocalVariableTableRow : BaseRow<String>
	{
		private AttributeReference[] local_variable_tableI { get { return base.GetValue<AttributeReference[]>(0); } }

		/// <summary>The value of the local_variable_table_length item indicates the number of entries in the local_variable_table array</summary>
		public UInt16 local_variable_table_length { get { return (UInt16)this.local_variable_tableI.Length; } }

		/// <summary>
		/// Each entry in the local_variable_table array indicates a range of code array offsets within which a local variable has a value.
		/// It also indicates the index into the local variable array of the current frame at which that local variable can be found.
		/// </summary>
		public LocalVariableTableRefRow[] local_variable_table
		{
			get
			{
				AttributeReference[] references = this.local_variable_tableI;
				BaseTable<LocalVariableTableRefRow, String> baseTable = base.Root.File.attribute_pool.LocalVariableTableRef;

				return Array.ConvertAll(references, delegate(AttributeReference item) { return baseTable[item.Index]; });
			}
		}
	}
}