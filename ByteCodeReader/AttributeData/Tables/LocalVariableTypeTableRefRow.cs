using System;
using AlphaOmega.Debug.Data;
using AlphaOmega.Debug.ConstantData;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>
	/// Each entry in the local_variable_type_table array indicates a range of code array offsets within which a local variable has a value.
	/// It also indicates the index into the local variable array of the current frame at which that local variable can be found.
	/// </summary>
	public class LocalVariableTypeTableRefRow : BaseRow<String>
	{
		/// <summary>The given local variable must have a value at indices into the code array in the interval [start_pc, start_pc + length), that is, between start_pc inclusive and start_pc + length exclusive</summary>
		/// <remarks>
		/// The value of start_pc must be a valid index into the code array of this Code attribute and must be the index of the opcode of an instruction.
		/// The value of start_pc + length must either be a valid index into the code array of this Code attribute and be the index of the opcode of an instruction, or it must be the first index beyond the end of that code array.
		/// </remarks>
		public UInt16 StartPc => base.GetValue<UInt16>(0);

		/// <summary>The given local variable must have a value at indices into the code array in the interval [start_pc, start_pc + length), that is, between start_pc inclusive and start_pc + length exclusive</summary>
		public UInt16 Length => base.GetValue<UInt16>(1);

		private UInt16 NameIndexI => base.GetValue<UInt16>(2);

		/// <summary>
		/// The value of the name_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table.
		/// The <see cref="ClassFile.ConstantPool"/> entry at that index must contain a <see cref="Jvm.CONSTANT_Utf8_info"/> structure (§4.4.7) representing a valid unqualified name denoting a local variable (§4.2.2).
		/// </summary>
		public ConstantReference NameIndex => new ConstantReference(base.Root.File.ConstantPool, Jvm.CONSTANT.Utf8, this.NameIndexI);

		private UInt16 SignatureIndexI => base.GetValue<UInt16>(3);

		/// <summary>The <see cref="ClassFile.ConstantPool"/> entry at that index must contain a <see cref="Jvm.CONSTANT_Utf8_info"/> structure (§4.4.7) representing a field signature which encodes the type of a local variable in the source program (§4.7.9.1)</summary>
		/// <remarks>The value of the signature_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table</remarks>
		public ConstantReference SignatureIndex => new ConstantReference(base.Root.File.ConstantPool, Jvm.CONSTANT.Utf8, this.SignatureIndexI);

		/// <summary>The given local variable must be at index in the local variable array of the current frame</summary>
		/// <remarks>If the local variable at index is of type double or long, it occupies both index and index + 1</remarks>
		public UInt16 Index => base.GetValue<UInt16>(4);
	}
}