using System;
using System.IO;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>Generic attribute table structures</summary>
	public class AttributeTable : Table<String>
	{
		/// <summary>Create instance of the attribute table</summary>
		/// <param name="root">Attribute tables collection</param>
		/// <param name="type">Type of the attribute table</param>
		internal AttributeTable(AttributeTables root, String type)
			: base(root, type, AttributeTable.GetTableDescrption(type))
		{
		}

		internal UInt32 AddRow(BinaryReader reader)
		{
			AttributeCell[] cells = new AttributeCell[base.Columns.Length];
			for(UInt32 loop = 0; loop < cells.Length; loop++)
			{
				AttributeCell cell = new AttributeCell((AttributeTables)base.Root, (AttributeColumn)base.Columns[loop], reader);
				cells[loop] = cell;
			}

			AttributeRow row = new AttributeRow(this, 0, cells);
			return base.AddRow(row);
		}

		private static AttributeColumn[] GetTableDescrption(String type)
		{
			if(type == null)
				throw new ArgumentNullException("type");

			AttributeColumnType[] columnTypes;
			String[] columnNames;

			Jvm.ATTRIBUTE attr = Enum.IsDefined(typeof(Jvm.ATTRIBUTE), type)
				? (Jvm.ATTRIBUTE)Enum.Parse(typeof(Jvm.ATTRIBUTE), type)
				: Jvm.ATTRIBUTE.Undefined;

			switch(attr)
			{
			case Jvm.ATTRIBUTE.ConstantValue:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, };
				columnNames = new String[] { "constantvalue_index", };
				break;
			case Jvm.ATTRIBUTE.Code:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.ByteArray, AttributeColumnType.ExceptionTableRef, AttributeColumnType.AttributeRef, };
				columnNames = new String[] { "max_stack", "max_locals", "code", "exception_table", "attributes", };
				break;
			case Jvm.ATTRIBUTE.ExceptionTableRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { "start_pc", "end_pc", "handler_pc", "catch_type", };
				break;
			case Jvm.ATTRIBUTE.Exceptions:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.ShortArray, };
				columnNames = new String[] { "exception_index_table", };
				break;
			case Jvm.ATTRIBUTE.InnerClasses:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.InnerClassRef, };
				columnNames = new String[] { "classes", };
				break;
			case Jvm.ATTRIBUTE.InnerClassesRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { "inner_class_info_index", "outer_class_info_index", "inner_name_index", "inner_class_access_flags", };
				break;
			case Jvm.ATTRIBUTE.EnclosingMethod:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { "class_index", "method_index", };
				break;
			case Jvm.ATTRIBUTE.Signature:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, };
				columnNames = new String[] { "signature_index", };
				break;
			case Jvm.ATTRIBUTE.SourceFile:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, };
				columnNames = new String[] { "sourcefile_index", };
				break;
			case Jvm.ATTRIBUTE.SourceDebugExtension:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.Raw, };
				columnNames = new String[] { "debug_extension", };
				break;
			case Jvm.ATTRIBUTE.LineNumberTable:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.LineNumberTableRef, };
				columnNames = new String[] { "line_number_table", };
				break;
			case Jvm.ATTRIBUTE.LineNumberTableRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { "start_pc", "line_number", };
				break;

			case Jvm.ATTRIBUTE.LocalVariableTable:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.LocalVariableTableRef, };
				columnNames = new String[] { "local_variable_table", };
				break;
			case Jvm.ATTRIBUTE.LocalVariableTableRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { "start_pc", "length", "name_index", "descriptor_index", "index", };
				break;
			case Jvm.ATTRIBUTE.LocalVariableTypeTable:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.LocalVariableTypeTableRef, };
				columnNames = new String[] { "local_variable_type_table", };
				break;
			case Jvm.ATTRIBUTE.LocalVariableTypeTableRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { "start_pc", "length", "name_index", "signature_index", "index", };
				break;
			case Jvm.ATTRIBUTE.BootstrapMethods:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.BootstrapMethodsRef, };
				columnNames = new String[] { "bootstrap_methods", };
				break;
			case Jvm.ATTRIBUTE.BootstrapMethodsRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.ShortArray, };
				columnNames = new String[] { "bootstrap_method_ref", "bootstrap_arguments", };
				break;
			case Jvm.ATTRIBUTE.MethodParameters:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.MethodParametersRef, };
				columnNames = new String[] { "parameters", };
				break;
			case Jvm.ATTRIBUTE.MethodParametersRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { "name_index", "access_flags", };
				break;
			case Jvm.ATTRIBUTE.Deprecated:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.Raw, };
				columnNames = new String[] { "raw", };
				break;
			case Jvm.ATTRIBUTE.Synthetic:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.Raw, };
				columnNames = new String[] { "raw", };
				break;
			case Jvm.ATTRIBUTE.StackMapTable://TODO: Not implemented
			case Jvm.ATTRIBUTE.RuntimeVisibleAnnotations://TODO: Not implemented
			case Jvm.ATTRIBUTE.RuntimeVisibleParameterAnnotations://TODO: Not implemented
			case Jvm.ATTRIBUTE.RuntimeVisibleTypeAnnotations://TODO: Not implemented
			case Jvm.ATTRIBUTE.RuntimeInvisibleAnnotations://TODO: Not implemented
			case Jvm.ATTRIBUTE.RuntimeInvisibleParameterAnnotations://TODO: Not implemented
			case Jvm.ATTRIBUTE.RuntimeInvisibleTypeAnnotations://TODO: Not implemented
			case Jvm.ATTRIBUTE.AnnotationDefault://TODO: Not implemented
			default://Unknown attribute
				columnTypes = new AttributeColumnType[] { AttributeColumnType.Raw, };
				columnNames = new String[] { "raw", };
				break;
			}

			if(columnTypes.Length != columnNames.Length)
				throw new InvalidOperationException("Length of column type and names must be equal");

			AttributeColumn[] result = new AttributeColumn[columnTypes.Length];
			for(UInt16 loop = 0; loop < columnTypes.Length; loop++)
				result[loop] = new AttributeColumn(type, columnTypes[loop], columnNames[loop], loop);

			return result;
		}
	}
}