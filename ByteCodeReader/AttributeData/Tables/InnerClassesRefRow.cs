using System;
using AlphaOmega.Debug.ConstantData;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>Every <see cref="Jvm.CONSTANT_Class_info"/> entry in the <see cref="ClassFile.ConstantPool"/> table which represents a class or interface C that is not a package member must have exactly one corresponding entry in the classes array</summary>
	/// <remarks>
	/// If a class has members that are classes or interfaces, its <see cref="ClassFile.ConstantPool"/> table (and hence its InnerClasses attribute) must refer to each such member, even if that member is not otherwise mentioned by the class.
	/// These rules imply that a nested class or interface member will have InnerClasses information for each enclosing class and for each immediate member.
	/// </remarks>
	public class InnerClassesRefRow : BaseRow<String>
	{
		/// <summary>Inner class access flags</summary>
		[Flags]
		public enum ACC : UInt16
		{
			/// <summary>Marked or implicitly public in source.</summary>
			PUBLIC = 0x0001,
			/// <summary>Marked private in source.</summary>
			ACC_PRIVATE = 0x0002,
			/// <summary>Marked protected in source</summary>
			ACC_PROTECTED = 0x0004,
			/// <summary>Marked or implicitly static in source</summary>
			ACC_STATIC = 0x0008,
			/// <summary>Marked final in source</summary>
			ACC_FINAL = 0x0010,
			/// <summary>Was an interface in source</summary>
			ACC_INTERFACE = 0x0200,
			/// <summary>Marked or implicitly abstract in source</summary>
			ACC_ABSTRACT = 0x0400,
			/// <summary>Declared synthetic; not present in the source code</summary>
			ACC_SYNTHETIC = 0x1000,
			/// <summary>Declared as an annotation type</summary>
			ACC_ANNOTATION = 0x2000,
			/// <summary>Declared as an enum type</summary>
			ACC_ENUM = 0x4000
		}

		/// <summary>
		/// The value of the inner_class_info_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table.
		/// The <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_Class_info"/> structure (§4.4.1) representing C.
		/// The remaining items in the classes array entry give information about C.
		/// </summary>
		private UInt16 InnerClassInfoIndexI => base.GetValue<UInt16>(0);

		private UInt16 OuterClassInfoIndexI => base.GetValue<UInt16>(1);

		private UInt16 InnerNameIndexI => base.GetValue<UInt16>(2);

		/// <summary>
		/// The value of the inner_class_access_flags item is a mask of flags used to denote access permissions to and properties of class or interface C as declared in the source code from which this class file was compiled.
		/// It is used by a compiler to recover the original information when source code is not available.
		/// </summary>
		public ACC InnerClassAccessFlags => base.GetValue<ACC>(3);

		/// <summary>
		/// The value of the inner_class_info_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table.
		/// The <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_Class_info"/> structure (§4.4.1) representing C.
		/// The remaining items in the classes array entry give information about C.
		/// </summary>
		public ConstantReference InnerClassInfoIndex
			=> new ConstantReference(base.Root.File.ConstantPool, Jvm.CONSTANT.Class, this.InnerClassInfoIndexI);

		/// <summary>
		/// If C is not a member of a class or an interface (that is, if C is a top-level class or interface (JLS §7.6) or a local class (JLS §14.3) or an anonymous class (JLS §15.9.5)), the value of the outer_class_info_index item must be zero.
		/// Otherwise, the value of the outer_class_info_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table, and the entry at that index must be a <see cref="Jvm.CONSTANT_Class_info"/> (§4.4.1) structure representing the class or interface of which C is a member.
		/// </summary>
		/// <remarks>If a class file has a version number that is greater than or equal to 51.0, and has an InnerClasses attribute in its attributes table, then for all entries in the classes array of the InnerClasses attribute, the value of the outer_class_info_index item must be zero if the value of the inner_name_index item is zero</remarks>
		public ConstantReference OuterClassInfoIndex
		{
			get
			{
				UInt16 idRef = this.OuterClassInfoIndexI;

				return idRef == 0
					? null
					: new ConstantReference(base.Root.File.ConstantPool, Jvm.CONSTANT.Class, idRef);
			}
		}

		/// <summary>
		/// If C is anonymous (JLS §15.9.5), the value of the inner_name_index item must be zero.
		/// Otherwise, the value of the inner_name_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table, and the entry at that index must be a <see cref="Jvm.CONSTANT_Utf8_info"/> (§4.4.7) structure that represents the original simple name of C, as given in the source code from which this class file was compiled.
		/// </summary>
		public ConstantReference InnerNameIndex
		{
			get
			{
				UInt16 idRef = this.InnerNameIndexI;

				return idRef == 0
					? null
					: new ConstantReference(base.Root.File.ConstantPool, Jvm.CONSTANT.Utf8, idRef);
			}
		}
	}
}