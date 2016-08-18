using System;
using AlphaOmega.Debug.Data;
using AlphaOmega.Debug.AttributeData;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>The CONSTANT_InvokeDynamic_info structure is used by an invokedynamic instruction (§invokedynamic) to specify a bootstrap method, the dynamic invocation name, the argument and return types of the call, and optionally, a sequence of additional constants called static arguments to the bootstrap method</summary>
	public class InvokeDynamicRow : BaseRow<Jvm.CONSTANT>
	{
		private UInt16 bootstrap_method_attr_indexI { get { return base.GetValue<UInt16>(0); } }

		/// <summary>The value of the bootstrap_method_attr_index item must be a valid index into the bootstrap_methods array of the bootstrap method table (§4.7.23) of this class file.</summary>
		public AttributeReference bootstrap_method_attr_index { get { return new AttributeReference(base.Root.File.attribute_pool, Jvm.ATTRIBUTE.BootstrapMethods.ToString(), this.bootstrap_method_attr_indexI); } }

		/// <summary>
		/// The value of the name_and_type_index item must be a valid index into the constant_pool table.
		/// The constant_pool entry at that index must be a CONSTANT_NameAndType_info structure (§4.4.6) representing a method name and method descriptor (§4.3.3).
		/// </summary>
		private UInt16 name_and_type_indexI { get { return base.GetValue<UInt16>(1); } }

		/// <summary>
		/// The value of the name_and_type_index item must be a valid index into the constant_pool table.
		/// The constant_pool entry at that index must be a CONSTANT_NameAndType_info structure (§4.4.6) representing a method name and method descriptor (§4.3.3).
		/// </summary>
		public ConstantReference name_and_type_index { get { return new ConstantReference(base.Root, Jvm.CONSTANT.NameAndType, this.name_and_type_indexI); } }
	}
}