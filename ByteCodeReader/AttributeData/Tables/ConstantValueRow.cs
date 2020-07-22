using System;
using AlphaOmega.Debug.ConstantData;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>The ConstantValue attribute is a fixed-length attribute in the attributes table of a field_info structure (§4.5). A ConstantValue attribute represents the value of a constant expression (JLS §15.28)</summary>
	public class ConstantValueRow : BaseRow<String>
	{
		private UInt16 constantvalue_indexI { get { return base.GetValue<UInt16>(0); } }

		/// <summary>The value of the constantvalue_index item must be a valid index into the constant_pool table. The constant_pool entry at that index gives the constant value represented by this attribute.</summary>
		/// <remarks>
		/// Index can be from one of the following constant tables:
		/// CONSTANT_Long
		/// CONSTANT_Float
		/// CONSTANT_Double
		/// CONSTANT_Integer
		/// CONSTANT_String
		/// </remarks>
		public ConstantReference constantvalue_index { get { return new ConstantReference(base.Root.File.constant_pool, this.constantvalue_indexI); } }
	}
}