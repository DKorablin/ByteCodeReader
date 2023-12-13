using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>The <see cref="Jvm.CONSTANT_Class_info"/> structure is used to represent a class or an interface</summary>
	public class ClassRow : BaseRow<Jvm.CONSTANT>
	{
		private UInt16 NameIndexI => base.GetValue<UInt16>(0);

		/// <summary>
		/// The value of the name_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table.
		/// The <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_Utf8_info"/> structure (§4.4.7) representing a valid binary class or interface name encoded in internal form (§4.2.1).
		/// </summary>
		public ConstantReference NameIndex => new ConstantReference(base.Root, Jvm.CONSTANT.Utf8, this.NameIndexI);

		/// <summary>Reference string representation</summary>
		/// <returns>String</returns>
		public override String ToString()
			=> $"{this.GetType().Name}: {{{this.NameIndex}}}";
	}
}