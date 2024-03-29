﻿using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>Fields, methods, and interface methods are represented by similar structures</summary>
	public class FieldrefRow : BaseRow<Jvm.CONSTANT>
	{
		/// <summary>
		/// The class_index item of a <see cref="Jvm.CONSTANT_Fieldref_info"/> structure may be either a class type or an interface type.
		/// </summary>
		/// <remarks>
		/// The value of the class_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table.
		/// The <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_Class_info"/> structure (§4.4.1) representing a class or interface type that has the field or method as a member.
		/// </remarks>
		private UInt16 ClassIndexI => base.GetValue<UInt16>(0);

		/// <summary>The class_index item of a <see cref="Jvm.CONSTANT_Fieldref_info"/> structure may be either a class type or an interface type.</summary>
		/// <remarks>
		/// The value of the class_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table.
		/// The <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_Class_info"/> structure (§4.4.1) representing a class or interface type that has the field or method as a member.
		/// </remarks>
		public ConstantReference ClassIndex => new ConstantReference(base.Root, Jvm.CONSTANT.Class, this.ClassIndexI);

		/// <summary>
		/// In a <see cref="Jvm.CONSTANT_Fieldref_info"/>, the indicated descriptor must be a field descriptor (§4.3.2).
		/// Otherwise, the indicated descriptor must be a method descriptor (§4.3.3).
		/// </summary>
		/// <remarks>
		/// The value of the name_and_type_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table.
		/// The <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_NameAndType_info"/> structure (§4.4.6).
		/// This <see cref="ClassFile.ConstantPool"/> entry indicates the name and descriptor of the field or method.
		/// </remarks>
		private UInt16 NameAndTypeIndexI => base.GetValue<UInt16>(1);

		/// <summary>
		/// In a <see cref="Jvm.CONSTANT_Fieldref_info"/>, the indicated descriptor must be a field descriptor (§4.3.2).
		/// Otherwise, the indicated descriptor must be a method descriptor (§4.3.3).
		/// </summary>
		/// <remarks>
		/// The value of the name_and_type_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table.
		/// The <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_NameAndType_info"/> structure (§4.4.6).
		/// This <see cref="ClassFile.ConstantPool"/> entry indicates the name and descriptor of the field or method.
		/// </remarks>
		public ConstantReference NameAndTypeIndex => new ConstantReference(base.Root, Jvm.CONSTANT.NameAndType, this.NameAndTypeIndexI);
	}
}