using System;
using AlphaOmega.Debug.ConstantData;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>
	/// The Exceptions attribute is a variable-length attribute in the attributes table of a method_info structure (§4.6).
	/// The Exceptions attribute indicates which checked exceptions a method may throw.
	/// There may be at most one Exceptions attribute in each method_info structure.
	/// </summary>
	public class ExceptionsRow : BaseRow<String>
	{
		private UInt16[] exception_index_tableI { get { return base.GetValue<UInt16[]>(0); } }

		/// <summary>The value of the number_of_exceptions item indicates the number of entries in the exception_index_table.</summary>
		public UInt16 number_of_exceptions { get { return (UInt16)this.exception_index_tableI.Length; } }
		/// <summary>
		/// Each value in the exception_index_table array must be a valid index into the constant_pool table.
		/// The constant_pool entry referenced by each table item must be a CONSTANT_Class_info structure (§4.4.1) representing a class type that this method is declared to throw.
		/// </summary>
		/// <remarks>
		/// A method should throw an exception only if at least one of the following three criteria is met:
		/// The exception is an instance of RuntimeException or one of its subclasses.
		/// The exception is an instance of Error or one of its subclasses.
		/// The exception is an instance of one of the exception classes specified in the exception_index_table just described, or one of their subclasses.
		/// These requirements are not enforced in the Java Virtual Machine; they are enforced only at compile-time.
		/// </remarks>
		public ConstantReference[] exception_index_table
		{
			get
			{
				UInt16[] idRef = this.exception_index_tableI;
				ConstantTables tables = base.Root.File.constant_pool;

				return Array.ConvertAll(idRef, delegate(UInt16 id) { return new ConstantReference(tables, Jvm.CONSTANT.Class, id); });
			}
		}
	}
}