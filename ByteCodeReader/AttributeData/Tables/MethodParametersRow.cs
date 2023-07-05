using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>
	/// The MethodParameters attribute is a variable-length attribute in the attributes table of a method_info structure (§4.6).
	/// A MethodParameters attribute records information about the formal parameters of a method, such as their names.
	/// </summary>
	/// <remarks>There may be at most one MethodParameters attribute in the attributes table of a method_info structure</remarks>
	public class MethodParametersRow : BaseRow<String>
	{
		private AttributeReference[] parametersI { get { return base.GetValue<AttributeReference[]>(0); } }

		/// <summary>The value of the parameters_count item indicates the number of parameter descriptors in the method descriptor (§4.3.3) referenced by the descriptor_index of the attribute's enclosing method_info structure</summary>
		/// <remarks>
		/// This is not a constraint which a Java Virtual Machine implementation must enforce during format checking (§4.8).
		/// The task of matching parameter descriptors in a method descriptor against the items in the parameters array below is done by the reflection libraries of the Java SE platform.
		/// </remarks>
		public Byte parameters_count { get { return (Byte)this.parametersI.Length; } }

		/// <summary>Method parameters description</summary>
		public MethodParametersRefRow[] parameters
		{
			get
			{
				AttributeReference[] references = this.parametersI;
				BaseTable<MethodParametersRefRow, String> baseTable = base.Root.File.attribute_pool.MethodParametersRef;

				return Array.ConvertAll(references, delegate(AttributeReference item) { return baseTable[item.Index]; });
			}
		}
	}
}