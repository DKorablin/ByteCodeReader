﻿using System;
using AlphaOmega.Debug.ConstantData;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>The ConstantValue attribute is a fixed-length attribute in the attributes table of a field_info structure (§4.5)</summary>
	/// <remarks>A ConstantValue attribute represents the value of a constant expression (JLS §15.28)</remarks>
	public class ConstantValueRow : BaseRow<String>
	{
		private UInt16 ConstantValueIndexI => base.GetValue<UInt16>(0);

		/// <summary>
		/// The value of the constantvalue_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table.
		/// The <see cref="ClassFile.ConstantPool"/> entry at that index gives the constant value represented by this attribute
		/// </summary>
		/// <remarks>
		/// Index can be from one of the following constant tables:
		/// <see cref="Jvm.CONSTANT.Long"/>
		/// <see cref="Jvm.CONSTANT.Float"/>
		/// <see cref="Jvm.CONSTANT.Double"/>
		/// <see cref="Jvm.CONSTANT.Integer"/>
		/// <see cref="Jvm.CONSTANT.String"/>
		/// </remarks>
		public ConstantReference ConstantValueIndex => new ConstantReference(base.Root.File.ConstantPool, this.ConstantValueIndexI);
	}
}