using System;
using AlphaOmega.Debug.Data;
using AlphaOmega.Debug.ConstantData;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>
	/// The Signature attribute is a fixed-length attribute in the attributes table of a ClassFile, field_info, or method_info structure (§4.1, §4.5, §4.6).
	/// A Signature attribute records a signature (§4.7.9.1) for a class, interface, constructor, method, or field whose declaration in the Java programming language uses type variables or parameterized types.
	/// See The Java Language Specification, Java SE 8 Edition for details about these types.
	/// </summary>
	public class SignatureRow : BaseRow<String>
	{
		private UInt16 signature_indexI { get { return base.GetValue<UInt16>(0); } }

		/// <summary>
		/// The value of the signature_index item must be a valid index into the constant_pool table.
		/// The constant_pool entry at that index must be a CONSTANT_Utf8_info structure (§4.4.7) representing a class signature if this Signature attribute is an attribute of a ClassFile structure; a method signature if this Signature attribute is an attribute of a method_info structure; or a field signature otherwise.
		/// </summary>
		/// <remarks>
		/// Oracle's Java Virtual Machine implementation does not check the well-formedness of Signature attributes during class loading or linking.
		/// Instead, Signature attributes are checked by methods of the Java SE platform class libraries which expose generic signatures of classes, interfaces, constructors, methods, and fields.
		/// Examples include getGenericSuperclass in Class and toGenericString in java.lang.reflect.Executable.
		/// </remarks>
		public ConstantReference signature_index { get { return new ConstantReference(base.Root.File.constant_pool, Jvm.CONSTANT.Utf8, this.signature_indexI); } }
	}
}