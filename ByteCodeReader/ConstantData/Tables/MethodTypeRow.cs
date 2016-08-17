using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>The CONSTANT_MethodType_info structure is used to represent a method type</summary>
	public class MethodTypeRow : BaseRow<Jvm.CONSTANT>
	{
		/// <summary>
		/// The value of the descriptor_index item must be a valid index into the constant_pool table.
		/// The constant_pool entry at that index must be a CONSTANT_Utf8_info structure (§4.4.7) representing a method descriptor (§4.3.3).
		/// </summary>
		private UInt16 descriptor_indexI { get { return base.GetValue<UInt16>(0); } }

		/// <summary>
		/// The value of the descriptor_index item must be a valid index into the constant_pool table.
		/// The constant_pool entry at that index must be a CONSTANT_Utf8_info structure (§4.4.7) representing a method descriptor (§4.3.3).
		/// </summary>
		public ConstantReference descriptor_index { get { return new ConstantReference(base.Root, Jvm.CONSTANT.Utf8, this.descriptor_indexI); } }
	}
}