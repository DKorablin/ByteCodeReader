using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>The CONSTANT_String_info structure is used to represent constant objects of the type String</summary>
	public class StringRow : BaseRow<Jvm.CONSTANT>
	{
		private UInt16 string_indexI { get { return base.GetValue<UInt16>(0); } }

		/// <summary>
		/// The value of the string_index item must be a valid index into the constant_pool table.
		/// The constant_pool entry at that index must be a CONSTANT_Utf8_info structure (§4.4.7) representing the sequence of Unicode code points to which the String object is to be initialized.
		/// </summary>
		public ConstantReference string_index { get { return new ConstantReference(base.Root, Jvm.CONSTANT.Utf8, this.string_indexI); } }
	}
}