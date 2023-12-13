using System;
using AlphaOmega.Debug.Data;
using AlphaOmega.Debug.ConstantData;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>
	/// Each entry in the exception_table array describes one exception handler in the code array.
	/// The order of the handlers in the exception_table array is significant (§2.10).
	/// </summary>
	public class ExceptionTableRefRow : BaseRow<String>
	{
		/// <summary>
		/// The values of the two items start_pc and end_pc indicate the ranges in the code array at which the exception handler is active.
		/// The value of start_pc must be a valid index into the code array of the opcode of an instruction.
		/// The value of end_pc either must be a valid index into the code array of the opcode of an instruction or must be equal to code_length, the length of the code array.
		/// The value of start_pc must be less than the value of end_pc.
		/// </summary>
		public UInt16 StartPc => base.GetValue<UInt16>(0);

		/// <summary>
		/// The values of the two items start_pc and end_pc indicate the ranges in the code array at which the exception handler is active.
		/// The value of start_pc must be a valid index into the code array of the opcode of an instruction.
		/// The value of end_pc either must be a valid index into the code array of the opcode of an instruction or must be equal to code_length, the length of the code array.
		/// The value of start_pc must be less than the value of end_pc.
		/// </summary>
		public UInt16 EndPc => base.GetValue<UInt16>(1);

		/// <summary>
		/// The value of the handler_pc item indicates the start of the exception handler.
		/// The value of the item must be a valid index into the code array and must be the index of the opcode of an instruction.
		/// </summary>
		public UInt16 HandlerPc => base.GetValue<UInt16>(2);

		private UInt16 CatchTypeI => base.GetValue<UInt16>(3);

		/// <summary>
		/// If the value of the catch_type item is nonzero, it must be a valid index into the <see cref="ClassFile.ConstantPool"/> table.
		/// The <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_Class_info"/> structure (§4.4.1) representing a class of exceptions that this exception handler is designated to catch.
		/// The exception handler will be called only if the thrown exception is an instance of the given class or one of its subclasses.
		/// If the value of the catch_type item is zero, this exception handler is called for all exceptions.
		/// This is used to implement finally (§3.13).
		/// </summary>
		public ConstantReference CatchType
		{
			get
			{
				UInt16 idRef = this.CatchTypeI;

				return idRef == 0
					? null
					: new ConstantReference(base.Root.File.ConstantPool, Jvm.CONSTANT.Class, idRef);
			}
		}
	}
}