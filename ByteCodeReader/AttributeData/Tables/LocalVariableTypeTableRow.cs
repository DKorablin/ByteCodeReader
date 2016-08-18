using System;
using AlphaOmega.Debug.Data;
using System.Collections.Generic;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>
	/// The LocalVariableTypeTable attribute is an optional variable-length attribute in the attributes table of a Code attribute (§4.7.3).
	/// It may be used by debuggers to determine the value of a given local variable during the execution of a method.
	/// </summary>
	/// <remarks>
	/// If multiple LocalVariableTypeTable attributes are present in the attributes table of a given Code attribute, then they may appear in any order.
	/// There may be no more than one LocalVariableTypeTable attribute per local variable in the attributes table of a Code attribute.
	/// 
	/// The LocalVariableTypeTable attribute differs from the LocalVariableTable attribute (§4.7.13) in that it provides signature information rather than descriptor information.
	/// This difference is only significant for variables whose type uses a type variable or parameterized type.
	/// Such variables will appear in both tables, while variables of other types will appear only in LocalVariableTable.
	/// </remarks>
	public class LocalVariableTypeTableRow : BaseRow<String>
	{
		private AttributeReference[] local_variable_type_tableI { get { return base.GetValue<AttributeReference[]>(0); } }

		/// <summary>The value of the local_variable_type_table_length item indicates the number of entries in the local_variable_type_table array.</summary>
		public UInt16 local_variable_type_table_length { get { return (UInt16)this.local_variable_type_tableI.Length; } }

		/// <summary>
		/// Each entry in the local_variable_type_table array indicates a range of code array offsets within which a local variable has a value.
		/// It also indicates the index into the local variable array of the current frame at which that local variable can be found.
		/// </summary>
		public LocalVariableTypeTableRefRow[] local_variable_type_table
		{
			get
			{
				AttributeReference[] references = this.local_variable_type_tableI;
				BaseTable<LocalVariableTypeTableRefRow, String> baseTable = base.Root.File.attribute_pool.LocalVariableTypeTableRef;

				return Array.ConvertAll(references, delegate(AttributeReference item) { return baseTable[item.Index]; });
			}
		}
	}
}