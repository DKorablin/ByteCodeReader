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

		internal UInt32 AddRow(AttributeTables tables, BinaryReader reader)
		{
			AttributeCell[] cells = new AttributeCell[base.Columns.Length];
			for(UInt32 loop = 0; loop < cells.Length; loop++)
			{
				AttributeCell cell = new AttributeCell(tables, (AttributeColumn)base.Columns[loop], reader);
				cells[loop] = cell;
			}

			AttributeRow row = new AttributeRow(this, 0, cells);
			return base.AddRow(row);
		}

		private static AttributeColumn[] GetTableDescrption(String type)
		{
			if(type == null)
				throw new ArgumentNullException("type");

			AttributeColumnType[] columnType;
			String[] columnName;

			Jvm.ATTRIBUTE attr = Enum.IsDefined(typeof(Jvm.ATTRIBUTE), type)
				? (Jvm.ATTRIBUTE)Enum.Parse(typeof(Jvm.ATTRIBUTE), type)
				: Jvm.ATTRIBUTE.Undefined;

			switch(attr)
			{
			case Jvm.ATTRIBUTE.ConstantValue:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, };
				columnName = new String[] { "constantvalue_index", };
				break;
			case Jvm.ATTRIBUTE.Code:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.ByteArray, AttributeColumnType.ExceptionTableRef, AttributeColumnType.AttributeRef, };
				columnName = new String[] { "max_stack", "max_locals", "code", "exception_table", "attributes", };
				break;
			case Jvm.ATTRIBUTE.ExceptionTableRef:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnName = new String[] { "start_pc", "end_pc", "handler_pc", "catch_type", };
				break;
			case Jvm.ATTRIBUTE.Exceptions:
				columnType = new AttributeColumnType[] { AttributeColumnType.ShortArray, };
				columnName = new String[] { "exception_index_table", };
				break;
			case Jvm.ATTRIBUTE.InnerClasses:
				columnType = new AttributeColumnType[] { AttributeColumnType.InnerClassRef, };
				columnName = new String[] { "classes", };
				break;
			case Jvm.ATTRIBUTE.InnerClassRef:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnName = new String[] { "inner_class_info_index", "outer_class_info_index", "inner_name_index", "inner_class_access_flags", };
				break;
			case Jvm.ATTRIBUTE.EnclosingMethod:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnName = new String[] { "class_index", "method_index", };
				break;
			case Jvm.ATTRIBUTE.Signature:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, };
				columnName = new String[] { "signature_index", };
				break;
			case Jvm.ATTRIBUTE.SourceFile:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, };
				columnName = new String[] { "sourcefile_index", };
				break;
			case Jvm.ATTRIBUTE.SourceDebugExtension:
				columnType = new AttributeColumnType[] { AttributeColumnType.Raw, };
				columnName = new String[] { "debug_extension", };
				break;
			case Jvm.ATTRIBUTE.LineNumberTable:
				columnType = new AttributeColumnType[] { AttributeColumnType.LineNumberTableRef, };
				columnName = new String[] { "line_number_table", };
				break;
			case Jvm.ATTRIBUTE.LineNumberTableRef:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnName = new String[] { "start_pc", "line_number", };
				break;

			case Jvm.ATTRIBUTE.LocalVariableTable:
				columnType = new AttributeColumnType[] { AttributeColumnType.LocalVariableTableRef, };
				columnName = new String[] { "local_variable_table", };
				break;
			case Jvm.ATTRIBUTE.LocalVariableTableRef:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnName = new String[] { "start_pc", "length", "name_index", "descriptor_index", "index", };
				break;
			case Jvm.ATTRIBUTE.LocalVariableTypeTable:
				columnType = new AttributeColumnType[] { AttributeColumnType.LocalVariableTypeTableRef, };
				columnName = new String[] { "local_variable_type_table", };
				break;
			case Jvm.ATTRIBUTE.LocalVariableTypeTableRef:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnName = new String[] { "start_pc", "length", "name_index", "signature_index", "index", };
				break;
			case Jvm.ATTRIBUTE.Deprecated:
				columnType = new AttributeColumnType[] { AttributeColumnType.Raw, };
				columnName = new String[] { "raw", };
				break;
			case Jvm.ATTRIBUTE.Synthetic:
				columnType = new AttributeColumnType[] { AttributeColumnType.Raw, };
				columnName = new String[] { "raw", };
				break;
			case Jvm.ATTRIBUTE.StackMapTable://TODO: Not implemented
			case Jvm.ATTRIBUTE.RuntimeVisibleAnnotations://TODO: Not implemented
			case Jvm.ATTRIBUTE.RuntimeVisibleParameterAnnotations://TODO: Not implemented
			case Jvm.ATTRIBUTE.RuntimeVisibleTypeAnnotations://TODO: Not implemented
			case Jvm.ATTRIBUTE.RuntimeInvisibleAnnotations://TODO: Not implemented
			case Jvm.ATTRIBUTE.RuntimeInvisibleParameterAnnotations://TODO: Not implemented
			case Jvm.ATTRIBUTE.RuntimeInvisibleTypeAnnotations://TODO: Not implemented
			case Jvm.ATTRIBUTE.AnnotationDefault://TODO: Not implemented
			case Jvm.ATTRIBUTE.BootstrapMethods://TODO: Not implemented
			case Jvm.ATTRIBUTE.MethodParameters://TODO: Not implemented
			default://Unknown attribute
				columnType = new AttributeColumnType[] { AttributeColumnType.Raw, };
				columnName = new String[] { "raw", };
				break;
			}

			if(columnType.Length != columnName.Length)
				throw new InvalidOperationException("Length of column type and names must be equal");

			AttributeColumn[] result = new AttributeColumn[columnType.Length];
			for(UInt16 loop = 0; loop < columnType.Length; loop++)
				result[loop] = new AttributeColumn(type, columnType[loop], columnName[loop], loop);

			return result;
		}
	}
}