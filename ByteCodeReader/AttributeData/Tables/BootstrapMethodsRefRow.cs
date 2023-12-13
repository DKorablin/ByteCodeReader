using System;
using AlphaOmega.Debug.Data;
using AlphaOmega.Debug.ConstantData;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>Each entry in the bootstrap_methods table contains an index to a <see cref="Jvm.CONSTANT_MethodHandle_info"/> structure (§4.4.8) which specifies a bootstrap method, and a sequence (perhaps empty) of indexes to static arguments for the bootstrap method.</summary>
	public class BootstrapMethodsRefRow : BaseRow<String>
	{
		private UInt16 BootstrapMethodRefI => base.GetValue<UInt16>(0);

		private UInt16[] BootstrapArgumentsI => base.GetValue<UInt16[]>(1);

		/// <summary>
		/// The value of the bootstrap_method_ref item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table.
		/// The <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_MethodHandle_info"/> structure (§4.4.8).
		/// </summary>
		/// <remarks>
		/// The form of the method handle is driven by the continuing resolution of the call site specifier in §invokedynamic, where execution of invoke in java.lang.invoke.MethodHandle requires that the bootstrap method handle be adjustable to the actual arguments being passed, as if by a call to java.lang.invoke.MethodHandle.asType.
		/// Accordingly, the reference_kind item of the <see cref="Jvm.CONSTANT_MethodHandle_info"/> structure should have the value 6 or 8 (§5.4.3.5), and the reference_index item should specify a static method or constructor that takes three arguments of type java.lang.invoke.MethodHandles.Lookup, String, and java.lang.invoke.MethodType, in that order.
		/// Otherwise, invocation of the bootstrap method handle during call site specifier resolution will complete abruptly.
		/// </remarks>
		public ConstantReference BootstrapMethodRef => new ConstantReference(base.Root.File.ConstantPool, Jvm.CONSTANT.MethodHandle, this.BootstrapMethodRefI);

		/// <summary>
		/// Each entry in the bootstrap_arguments array must be a valid index into the <see cref="ClassFile.ConstantPool"/> table.
		/// The <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_String_info"/>, <see cref="Jvm.CONSTANT_Class_info"/>, <see cref="Jvm.CONSTANT_Integer_info"/>, <see cref="Jvm.CONSTANT_Long_info"/>, <see cref="Jvm.CONSTANT_Float_info"/>, <see cref="Jvm.CONSTANT_Double_info"/>, <see cref="Jvm.CONSTANT_MethodHandle_info"/>, or <see cref="Jvm.CONSTANT_MethodType_info"/> structure (§4.4.3, §4.4.1, §4.4.4, §4.4.5, §4.4.8, §4.4.9).
		/// </summary>
		public ConstantReference[] BootstrapArguments
		{
			get
			{
				Tables<Jvm.CONSTANT> tables = base.Root.File.ConstantPool;
				return Array.ConvertAll(this.BootstrapArgumentsI, delegate(UInt16 index) { return new ConstantReference(tables, index); });
			}
		}
	}
}