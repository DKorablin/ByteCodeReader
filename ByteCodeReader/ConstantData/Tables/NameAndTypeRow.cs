using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>The CONSTANT_NameAndType_info structure is used to represent a field or method, without indicating which class or interface type it belongs to</summary>
	public class NameAndTypeRow : BaseRow<Jvm.CONSTANT>
	{
		private UInt16 name_indexI { get { return base.GetValue<UInt16>(0); } }

		/// <summary>
		/// The value of the name_index item must be a valid index into the constant_pool table.
		/// The constant_pool entry at that index must be a CONSTANT_Utf8_info structure (§4.4.7) representing either the special method name &lt;init&gt; (§2.9) or a valid unqualified name denoting a field or method (§4.2.2).
		/// </summary>
		public ConstantReference name_index { get { return new ConstantReference(base.Root, Jvm.CONSTANT.Utf8, this.name_indexI); } }

		private UInt16 descriptor_indexI { get { return base.GetValue<UInt16>(1); } }

		/// <summary>
		/// The value of the descriptor_index item must be a valid index into the constant_pool table.
		/// The constant_pool entry at that index must be a CONSTANT_Utf8_info structure (§4.4.7) representing a valid field descriptor or method descriptor (§4.3.2, §4.3.3).
		/// </summary>
		public ConstantReference descriptor_index { get { return new ConstantReference(base.Root, Jvm.CONSTANT.Utf8, this.descriptor_indexI); } }
	}
}