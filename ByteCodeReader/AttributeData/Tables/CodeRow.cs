using System;
using AlphaOmega.Debug.Data;
using System.Collections.Generic;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>
	/// The Code attribute is a variable-length attribute in the attributes table of a method_info (§4.6) structure.
	/// A Code attribute contains the Java Virtual Machine instructions and auxiliary information for a single method, instance initialization method (§2.9), or class or interface initialization method (§2.9).
	/// Every Java Virtual Machine implementation must recognize Code attributes.
	/// If the method is either native or abstract, its method_info structure must not have a Code attribute.
	/// Otherwise, its method_info structure must have exactly one Code attribute.
	/// </summary>
	public class CodeRow : BaseRow<String>
	{
		/// <summary>The value of the max_stack item gives the maximum depth of the operand stack of this method (§2.6.2) at any point during execution of the method.</summary>
		public UInt16 max_stack { get { return base.GetValue<UInt16>(0); } }

		/// <summary>The value of the max_locals item gives the number of local variables in the local variable array allocated upon invocation of this method (§2.6.1), including the local variables used to pass parameters to the method on its invocation.</summary>
		/// <remarks>The greatest local variable index for a value of type long or double is max_locals - 2. The greatest local variable index for a value of any other type is max_locals - 1.</remarks>
		public UInt16 max_locals { get { return base.GetValue<UInt16>(1); } }

		/// <summary>The value of the code_length item gives the number of bytes in the code array for this method.</summary>
		/// <remarks>The value of code_length must be greater than zero (as the code array must not be empty) and less than 65536.</remarks>
		public UInt32 code_length { get { return (UInt32)this.code.Length; } }

		/// <summary>
		/// The code array gives the actual bytes of Java Virtual Machine code that implement the method.
		/// When the code array is read into memory on a byte-addressable machine, if the first byte of the array is aligned on a 4-byte boundary, the tableswitch and lookupswitch 32-bit offsets will be 4-byte aligned.
		/// (Refer to the descriptions of those instructions for more information on the consequences of code array alignment.)
		/// </summary>
		/// <remarks>The detailed constraints on the contents of the code array are extensive and are given in a separate section (§4.9).</remarks>
		public Byte[] code { get { return base.GetValue<Byte[]>(2); } }

		/// <summary>The value of the exception_table_length item gives the number of entries in the exception_table table.</summary>
		public UInt16 exception_table_length { get { return (UInt16)this.exception_tableI.Length; } }

		private AttributeReference[] exception_tableI { get { return base.GetValue<AttributeReference[]>(3); } }

		/// <summary>
		/// Each entry in the exception_table array describes one exception handler in the code array.
		/// The order of the handlers in the exception_table array is significant (§2.10).
		/// </summary>
		public ExceptionTableRefRow[] exception_table
		{
			get
			{
				AttributeReference[] references = this.exception_tableI;
				BaseTable<ExceptionTableRefRow, String> baseTable = base.Root.File.attribute_pool.ExceptionTableRef;

				return Array.ConvertAll(references, delegate(AttributeReference item) { return baseTable[item.Index]; });
			}
		}

		/// <summary>The value of the attributes_count item indicates the number of attributes of the Code attribute</summary>
		public UInt16 attributes_count { get { return (UInt16)this.attributes.Length; } }

		/// <summary>A Code attribute can have any number of optional attributes associated with it</summary>
		/// <remarks>Each value of the attributes table must be an attribute structure (§4.7)</remarks>
		public AttributeReference[] attributes { get { return base.GetValue<AttributeReference[]>(4); } }
	}
}