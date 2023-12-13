using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>The <see cref="Jvm.CONSTANT_NameAndType_info"/> structure is used to represent a field or method, without indicating which class or interface type it belongs to</summary>
	public class NameAndTypeRow : BaseRow<Jvm.CONSTANT>
	{
		private UInt16 NameIndexI => base.GetValue<UInt16>(0);

		/// <summary>The <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_Utf8_info"/> structure (§4.4.7) representing either the special method name &lt;init&gt; (§2.9) or a valid unqualified name denoting a field or method (§4.2.2)</summary>
		/// <remarks>The value of the name_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table</remarks>
		public ConstantReference NameIndex => new ConstantReference(base.Root, Jvm.CONSTANT.Utf8, this.NameIndexI);

		private UInt16 DescriptorIndexI => base.GetValue<UInt16>(1);

		/// <summary>The <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_Utf8_info"/> structure (§4.4.7) representing a valid field descriptor or method descriptor (§4.3.2, §4.3.3)</summary>
		/// <remarks>The value of the descriptor_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table</remarks>
		public ConstantReference DescriptorIndex => new ConstantReference(base.Root, Jvm.CONSTANT.Utf8, this.DescriptorIndexI);
	}
}