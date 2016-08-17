using System;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>Types of the known attribute cells</summary>
	public enum AttributeColumnType
	{
		/// <summary>Byte column</summary>
		Byte,

		/// <summary>Unsigned short</summary>
		UInt16,

		/// <summary>Unsigned integer</summary>
		UInt32,

		/// <summary>Basic attribute info contains 2 fields: attribute_length (u4) &amp; info[attribute_length] (u1)</summary>
		ByteArray,

		/// <summary>ExceptionsAttribute index</summary>
		ShortArray,

		/// <summary>Undefined structure</summary>
		Raw,

		/// <summary>Generic attribute references</summary>
		AttributeRef,

		/// <summary>CodeAttribute exception_table references</summary>
		ExceptionTableRef,

		/// <summary>InnerClassAttribute class references</summary>
		InnerClassRef,

		/// <summary>LineNumberTable class reference</summary>
		LineNumberTableRef,

		/// <summary>LocalVariableTable class reference</summary>
		LocalVariableTableRef,

		/// <summary>LocalVariableTypeTable class reference</summary>
		LocalVariableTypeTableRef,
	}
}