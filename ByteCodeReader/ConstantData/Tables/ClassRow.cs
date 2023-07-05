using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>The <see cref="Jvm.CONSTANT_Class_info"/> structure is used to represent a class or an interface</summary>
	public class ClassRow : BaseRow<Jvm.CONSTANT>
	{
		private UInt16 name_indexI { get { return base.GetValue<UInt16>(0); } }

		/// <summary>
		/// The value of the name_index item must be a valid index into the constant_pool table.
		/// The constant_pool entry at that index must be a <see cref="Jvm.CONSTANT_Utf8_info"/> structure (§4.4.7) representing a valid binary class or interface name encoded in internal form (§4.2.1).
		/// </summary>
		public ConstantReference name_index { get { return new ConstantReference(base.Root, Jvm.CONSTANT.Utf8, this.name_indexI); } }

		/// <summary>Reference string representation</summary>
		/// <returns>String</returns>
		public override String ToString()
		{
			return $"{this.GetType().Name}: {{{this.name_index}}}";
		}
	}
}