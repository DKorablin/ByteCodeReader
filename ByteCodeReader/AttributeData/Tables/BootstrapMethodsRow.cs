using System;
using System.Collections.Generic;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>
	/// The BootstrapMethods attribute is a variable-length attribute in the attributes table of a ClassFile structure (§4.1).
	/// The BootstrapMethods attribute records bootstrap method specifiers referenced by invokedynamic instructions (§invokedynamic).
	/// </summary>
	/// <remarks>
	/// There must be exactly one BootstrapMethods attribute in the attributes table of a ClassFile structure if the constant_pool table of the ClassFile structure has at least one CONSTANT_InvokeDynamic_info entry (§4.4.10).
	/// There may be at most one BootstrapMethods attribute in the attributes table of a ClassFile structure.
	/// </remarks>
	public class BootstrapMethodsRow : BaseRow<String>
	{
		private AttributeReference[] bootstrap_methodsI { get { return base.GetValue<AttributeReference[]>(0); } }

		/// <summary>Each entry in the bootstrap_methods table contains an index to a <see cref="Jvm.CONSTANT_MethodHandle_info"/> structure (§4.4.8) which specifies a bootstrap method, and a sequence (perhaps empty) of indexes to static arguments for the bootstrap method</summary>
		public BootstrapMethodsRefRow[] bootstrap_methods
		{
			get
			{
				AttributeReference[] references = this.bootstrap_methodsI;
				BaseTable<BootstrapMethodsRefRow, String> baseTable = base.Root.File.attribute_pool.BootstrapMethodsRef;

				return Array.ConvertAll(references, delegate(AttributeReference item) { return baseTable[item.Index]; });
			}
		}
	}
}