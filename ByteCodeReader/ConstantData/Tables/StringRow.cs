using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>The <see cref="Jvm.CONSTANT_String_info"/> structure is used to represent constant objects of the type String</summary>
	public class StringRow : BaseRow<Jvm.CONSTANT>
	{
		private UInt16 StringIndexI => base.GetValue<UInt16>(0);

		/// <summary>The <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_Utf8_info"/> structure (§4.4.7) representing the sequence of Unicode code points to which the String object is to be initialized</summary>
		/// <remarks>The value of the string_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table</remarks>
		public ConstantReference StringIndex => new ConstantReference(base.Root, Jvm.CONSTANT.Utf8, this.StringIndexI);
	}
}