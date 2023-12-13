using System;
using AlphaOmega.Debug.Data;
using AlphaOmega.Debug.ConstantData;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>
	/// The EnclosingMethod attribute is an optional fixed-length attribute in the attributes table of a ClassFile structure (§4.1).
	/// </summary>
	/// <remarks>
	/// A class must have an EnclosingMethod attribute if and only if it is a local class or an anonymous class.
	/// A class may have no more than one EnclosingMethod attribute.
	/// </remarks>
	public class EnclosingMethodRow : BaseRow<String>
	{
		private UInt16 ClassIndexI => base.GetValue<UInt16>(0);

		private UInt16 MethodIndexI => base.GetValue<UInt16>(1);

		/// <summary>The <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_Class_info"/> (§4.4.1) structure representing the innermost class that encloses the declaration of the current class</summary>
		/// <remarks>The value of the class_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table</remarks>
		public ConstantReference ClassIndex => new ConstantReference(base.Root.File.ConstantPool, Jvm.CONSTANT.Class, this.ClassIndexI);

		/// <summary>
		/// If the current class is not immediately enclosed by a method or constructor, then the value of the method_index item must be zero.
		/// Otherwise, the value of the method_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table.
		/// The <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_NameAndType_info"/> structure (§4.4.6) representing the name and type of a method in the class referenced by the class_index attribute above.
		/// </summary>
		/// <remarks>It is the responsibility of a Java compiler to ensure that the method identified via the method_index is indeed the closest lexically enclosing method of the class that contains this EnclosingMethod attribute</remarks>
		public ConstantReference MethodIndex
		{
			get
			{
				UInt16 idRef = this.MethodIndexI;
				return idRef == 0
					? null
					: new ConstantReference(base.Root.File.ConstantPool, Jvm.CONSTANT.NameAndType, idRef);
			}
		}
	}
}