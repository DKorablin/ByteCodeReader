using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>The <see cref="Jvm.CONSTANT_MethodType_info"/> structure is used to represent a method type</summary>
	public class MethodTypeRow : BaseRow<Jvm.CONSTANT>
	{
		/// <summary>The <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_Utf8_info"/> structure (§4.4.7) representing a method descriptor (§4.3.3)</summary>
		/// <remarks>The value of the descriptor_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table</remarks>
		private UInt16 DescriptorIndexI => base.GetValue<UInt16>(0);

		/// <summary>The <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_Utf8_info"/> structure (§4.4.7) representing a method descriptor (§4.3.3)</summary>
		/// <remarks>The value of the descriptor_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table</remarks>
		public ConstantReference DescriptorIndex => new ConstantReference(base.Root, Jvm.CONSTANT.Utf8, this.DescriptorIndexI);
	}
}