using System;
using System.IO;
using System.Runtime.InteropServices;
using AlphaOmega.Debug.ConstantData;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>Attribute tables collection</summary>
	public class AttributeTables : Tables<String>
	{
		#region Tables
		/// <summary>The ConstantValue attribute is a fixed-length attribute in the attributes table of a field_info structure (§4.5). A ConstantValue attribute represents the value of a constant expression (JLS §15.28)</summary>
		public Data.BaseTable<ConstantValueRow, String> ConstantValue
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.ConstantValue.ToString()];
				return table == null
					? null
					: new Data.BaseTable<ConstantValueRow, String>(table);
			}
		}

		/// <summary>
		/// The Code attribute is a variable-length attribute in the attributes table of a method_info (§4.6) structure.
		/// A Code attribute contains the Java Virtual Machine instructions and auxiliary information for a single method, instance initialization method (§2.9), or class or interface initialization method (§2.9).
		/// Every Java Virtual Machine implementation must recognize Code attributes.
		/// If the method is either native or abstract, its method_info structure must not have a Code attribute.
		/// Otherwise, its method_info structure must have exactly one Code attribute.
		/// </summary>
		public Data.BaseTable<CodeRow, String> Code
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.Code.ToString()];
				return table == null
					? null
					: new Data.BaseTable<CodeRow, String>(table);
			}
		}

		/// <summary>
		/// Each entry in the exception_table array describes one exception handler in the code array.
		/// The order of the handlers in the exception_table array is significant (§2.10).
		/// </summary>
		public Data.BaseTable<ExceptionTableRefRow, String> ExceptionTableRef
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.ExceptionTableRef.ToString()];
				return table == null
					? null
					: new Data.BaseTable<ExceptionTableRefRow, String>(table);
			}
		}

		/// <summary>
		/// The Exceptions attribute is a variable-length attribute in the attributes table of a method_info structure (§4.6).
		/// The Exceptions attribute indicates which checked exceptions a method may throw.
		/// There may be at most one Exceptions attribute in each method_info structure.
		/// </summary>
		public Data.BaseTable<ExceptionsRow, String> Exceptions
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.Exceptions.ToString()];
				return table == null
					? null
					: new Data.BaseTable<ExceptionsRow, String>(table);
			}
		}

		/// <summary>
		/// The InnerClasses attribute is a variable-length attribute in the attributes table of a ClassFile structure (§4.1).
		/// If the constant pool of a class or interface C contains a CONSTANT_Class_info entry which represents a class or interface that is not a member of a package, then C's ClassFile structure must have exactly one InnerClasses attribute in its attributes table.
		/// </summary>
		public Data.BaseTable<InnerClassesRow, String> InnerClasses
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.InnerClasses.ToString()];
				return table == null
					? null
					: new Data.BaseTable<InnerClassesRow, String>(table);
			}
		}

		/// <summary>Every CONSTANT_Class_info entry in the constant_pool table which represents a class or interface C that is not a package member must have exactly one corresponding entry in the classes array.</summary>
		/// <remarks>
		/// If a class has members that are classes or interfaces, its constant_pool table (and hence its InnerClasses attribute) must refer to each such member, even if that member is not otherwise mentioned by the class.
		/// These rules imply that a nested class or interface member will have InnerClasses information for each enclosing class and for each immediate member.
		/// </remarks>
		public Data.BaseTable<InnerClassesRefRow, String> InnerClassesRef
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.InnerClassesRef.ToString()];
				return table == null
					? null
					: new Data.BaseTable<InnerClassesRefRow, String>(table);
			}
		}

		/// <summary>
		/// The EnclosingMethod attribute is an optional fixed-length attribute in the attributes table of a ClassFile structure (§4.1). A class must have an EnclosingMethod attribute if and only if it is a local class or an anonymous class.
		/// A class may have no more than one EnclosingMethod attribute.
		/// </summary>
		public Data.BaseTable<EnclosingMethodRow, String> EnclosingMethod
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.EnclosingMethod.ToString()];
				return table == null
					? null
					: new Data.BaseTable<EnclosingMethodRow, String>(table);
			}
		}

		/// <summary>
		/// The Signature attribute is a fixed-length attribute in the attributes table of a ClassFile, field_info, or method_info structure (§4.1, §4.5, §4.6).
		/// A Signature attribute records a signature (§4.7.9.1) for a class, interface, constructor, method, or field whose declaration in the Java programming language uses type variables or parameterized types.
		/// See The Java Language Specification, Java SE 8 Edition for details about these types.
		/// </summary>
		public Data.BaseTable<SignatureRow, String> Signature
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.Signature.ToString()];
				return table == null
					? null
					: new Data.BaseTable<SignatureRow, String>(table);
			}
		}

		/// <summary>The SourceFile attribute is an optional fixed-length attribute in the attributes table of a ClassFile structure (§4.1).</summary>
		/// <remarks>There may be at most one SourceFile attribute in the attributes table of a ClassFile structure.</remarks>
		public Data.BaseTable<SourceFileRow, String> SourceFile
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.SourceFile.ToString()];
				return table == null
					? null
					: new Data.BaseTable<SourceFileRow, String>(table);
			}
		}

		/// <summary>The SourceDebugExtension attribute is an optional attribute in the attributes table of a ClassFile structure (§4.1).</summary>
		/// <remarks>There may be at most one SourceDebugExtension attribute in the attributes table of a ClassFile structure.</remarks>
		public Data.BaseTable<SourceDebugExtensionRow, String> SourceDebugExtension
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.SourceDebugExtension.ToString()];
				return table == null
					? null
					: new Data.BaseTable<SourceDebugExtensionRow, String>(table);
			}
		}

		/// <summary>
		/// The LineNumberTable attribute is an optional variable-length attribute in the attributes table of a Code attribute (§4.7.3).
		/// It may be used by debuggers to determine which part of the code array corresponds to a given line number in the original source file.
		/// </summary>
		/// <remarks>
		/// If multiple LineNumberTable attributes are present in the attributes table of a Code attribute, then they may appear in any order.
		/// There may be more than one LineNumberTable attribute per line of a source file in the attributes table of a Code attribute. That is, LineNumberTable attributes may together represent a given line of a source file, and need not be one-to-one with source lines.
		/// </remarks>
		public Data.BaseTable<LineNumberTableRow, String> LineNumberTable
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.LineNumberTable.ToString()];
				return table == null
					? null
					: new Data.BaseTable<LineNumberTableRow, String>(table);
			}
		}

		/// <summary>Each entry in the line_number_table array indicates that the line number in the original source file changes at a given point in the code array.</summary>
		public Data.BaseTable<LineNumberTableRefRow, String> LineNumberTableRef
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.LineNumberTableRef.ToString()];
				return table == null
					? null
					: new Data.BaseTable<LineNumberTableRefRow, String>(table);
			}
		}

		/// <summary>The LocalVariableTable attribute is an optional variable-length attribute in the attributes table of a Code attribute (§4.7.3). It may be used by debuggers to determine the value of a given local variable during the execution of a method.</summary>
		/// <remarks>
		/// If multiple LocalVariableTable attributes are present in the attributes table of a Code attribute, then they may appear in any order.
		/// There may be no more than one LocalVariableTable attribute per local variable in the attributes table of a Code attribute.
		/// </remarks>
		public Data.BaseTable<LocalVariableTableRow, String> LocalVariableTable
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.LocalVariableTable.ToString()];
				return table == null
					? null
					: new Data.BaseTable<LocalVariableTableRow, String>(table);
			}
		}

		/// <summary>
		/// Each entry in the local_variable_table array indicates a range of code array offsets within which a local variable has a value.
		/// It also indicates the index into the local variable array of the current frame at which that local variable can be found.
		/// </summary>
		public Data.BaseTable<LocalVariableTableRefRow, String> LocalVariableTableRef
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.LocalVariableTableRef.ToString()];
				return table == null
					? null
					: new Data.BaseTable<LocalVariableTableRefRow, String>(table);
			}
		}

		/// <summary>
		/// The LocalVariableTypeTable attribute is an optional variable-length attribute in the attributes table of a Code attribute (§4.7.3).
		/// It may be used by debuggers to determine the value of a given local variable during the execution of a method.
		/// </summary>
		public Data.BaseTable<LocalVariableTypeTableRow, String> LocalVariableTypeTableRow
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.LocalVariableTypeTable.ToString()];
				return table == null
					? null
					: new Data.BaseTable<LocalVariableTypeTableRow, String>(table);
			}
		}

		/// <summary>
		/// Each entry in the local_variable_type_table array indicates a range of code array offsets within which a local variable has a value.
		/// It also indicates the index into the local variable array of the current frame at which that local variable can be found.
		/// </summary>
		public Data.BaseTable<LocalVariableTypeTableRefRow, String> LocalVariableTypeTableRef
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.LocalVariableTypeTableRef.ToString()];
				return table == null
					? null
					: new Data.BaseTable<LocalVariableTypeTableRefRow, String>(table);
			}
		}

		/// <summary>
		/// The BootstrapMethods attribute is a variable-length attribute in the attributes table of a ClassFile structure (§4.1).
		/// The BootstrapMethods attribute records bootstrap method specifiers referenced by invokedynamic instructions (§invokedynamic).
		/// </summary>
		/// <remarks>
		/// There must be exactly one BootstrapMethods attribute in the attributes table of a ClassFile structure if the constant_pool table of the ClassFile structure has at least one CONSTANT_InvokeDynamic_info entry (§4.4.10).
		/// There may be at most one BootstrapMethods attribute in the attributes table of a ClassFile structure.
		/// </remarks>
		public Data.BaseTable<BootstrapMethodsRow, String> BootstrapMethods
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.BootstrapMethods.ToString()];
				return table == null
					? null
					: new Data.BaseTable<BootstrapMethodsRow, String>(table);
			}
		}

		/// <summary>Each entry in the bootstrap_methods table contains an index to a CONSTANT_MethodHandle_info structure (§4.4.8) which specifies a bootstrap method, and a sequence (perhaps empty) of indexes to static arguments for the bootstrap method.</summary>
		public Data.BaseTable<BootstrapMethodsRefRow, String> BootstrapMethodsRef
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.BootstrapMethodsRef.ToString()];
				return table == null
					? null
					: new Data.BaseTable<BootstrapMethodsRefRow, String>(table);
			}
		}

		/// <summary>
		/// The MethodParameters attribute is a variable-length attribute in the attributes table of a method_info structure (§4.6).
		/// A MethodParameters attribute records information about the formal parameters of a method, such as their names.
		/// </summary>
		/// <remarks>There may be at most one MethodParameters attribute in the attributes table of a method_info structure.</remarks>
		public Data.BaseTable<MethodParametersRow, String> MethodParametersRow
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.MethodParameters.ToString()];
				return table == null
					? null
					: new Data.BaseTable<MethodParametersRow, String>(table);
			}
		}

		/// <summary>Method parameters description</summary>
		public Data.BaseTable<MethodParametersRefRow, String> MethodParametersRef
		{
			get
			{
				AttributeTable table = (AttributeTable)base[Jvm.ATTRIBUTE.MethodParametersRef.ToString()];
				return table == null
					? null
					: new Data.BaseTable<MethodParametersRefRow, String>(table);
			}
		}
		#endregion Tables

		internal AttributeTables(ClassFile file)
			: base(file)
		{
		}

		internal AttributeReference[] ReadAttributes(UInt16 attributes_count, ref UInt32 offset)
		{
			AttributeReference[] attributes = new AttributeReference[attributes_count];
			for(UInt32 loop = 0; loop < attributes_count; loop++)
			{
				Jvm.attribute_info attribute = base.File.PtrToStructure<Jvm.attribute_info>(offset);
				offset += (UInt32)Marshal.SizeOf(typeof(Jvm.attribute_info));

				Byte[] payload = base.File.ReadBytes(offset, attribute.attribute_length);
				offset += attribute.attribute_length;

				attributes[loop] = this.ParseAttribute(attribute, payload);
			}
			return attributes;
		}

		internal AttributeReference[] ReadSubAttributes(UInt16 attributes_count, BinaryReader reader)
		{
			AttributeReference[] attributes = new AttributeReference[attributes_count];
			for(UInt32 loop = 0; loop < attributes_count; loop++)
			{
				Jvm.attribute_info attribute=new Jvm.attribute_info();
				attribute.attribute_name_index = reader.ReadUInt16();
				attribute.attribute_length = reader.ReadUInt32();

				Byte[] payload = reader.ReadBytes((Int32)attribute.attribute_length);

				attributes[loop] = this.ParseAttribute(attribute, payload);
			}
			return attributes;
		}

		internal UInt32 ReadSubData(String type, BinaryReader reader)
		{
			AttributeTable table = this.GetOrCreateTable(type);

			return table.AddRow(reader);
		}

		private AttributeReference ParseAttribute(Jvm.attribute_info attribute, Byte[] payload)
		{
			Utf8Row constantRow = base.File.constant_pool.Utf8[attribute.attribute_name_index];
			AttributeTable table = this.GetOrCreateTable(constantRow.bytes);

			using(MemoryStream stream = new MemoryStream(payload))
			{
				BinaryReader reader = BinaryEndianReader.CreateReader(EndianHelper.Endian.Big, stream);
				UInt32 rowIndex = table.AddRow(reader);
				return new AttributeReference(this, constantRow.bytes, rowIndex);
			}
		}

		private AttributeTable GetOrCreateTable(String type)
		{
			AttributeTable table = (AttributeTable)base[type];
			if(table == null)
			{
				table = new AttributeTable(this, type);
				base.AddTable(type, table);
			}

			return table;
		}
	}
}