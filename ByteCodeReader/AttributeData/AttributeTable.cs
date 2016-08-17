using System;
using System.IO;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>Generic attribute table structures</summary>
	public class AttributeTable : Table<String>
	{
		/// <summary>Known attributes</summary>
		private enum ATTRIBUTE
		{
			Undefined,
			ConstantValue,
			Code,
			StackMapTable,
			Exceptions,
			InnerClasses,
			EnclosingMethod,
			Synthetic,
			Signature,
			SourceFile,
			SourceDebugExtension,
			LineNumberTable,
			LocalVariableTable,
			LocalVariableTypeTable,
			Deprecated,
			RuntimeVisibleAnnotations,
			RuntimeInvisibleAnnotations,
			RuntimeVisibleParameterAnnotations,
			RuntimeInvisibleParameterAnnotations,
			RuntimeVisibleTypeAnnotations,
			RuntimeInvisibleTypeAnnotations,
			AnnotationDefault,
			BootstrapMethods,
			MethodParameters,

			ExceptionTableRef,
			InnerClassRef,
			LineNumberTableRef,
			LocalVariableTableRef,
			LocalVariableTypeTableRef,
		}

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

			ATTRIBUTE attr = Enum.IsDefined(typeof(ATTRIBUTE), type)
				? (ATTRIBUTE)Enum.Parse(typeof(ATTRIBUTE), type)
				: ATTRIBUTE.Undefined;

			switch(attr)
			{
			case ATTRIBUTE.ConstantValue:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, };
				columnName = new String[] { "constantvalue_index", };
				break;
			case ATTRIBUTE.Code:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.ByteArray, AttributeColumnType.ExceptionTableRef, AttributeColumnType.AttributeRef, };
				columnName = new String[] { "max_stack", "max_locals", "code", "exception_table", "attributes", };
				break;
			case ATTRIBUTE.ExceptionTableRef:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnName = new String[] { "start_pc", "end_pc", "handler_pc", "catch_type", };
				break;
			case ATTRIBUTE.Exceptions:
				columnType = new AttributeColumnType[] { AttributeColumnType.ShortArray, };
				columnName = new String[] { "exception_index_table", };
				break;
			case ATTRIBUTE.InnerClasses:
				columnType = new AttributeColumnType[] { AttributeColumnType.InnerClassRef, };
				columnName = new String[] { "classes", };
				break;
			case ATTRIBUTE.InnerClassRef:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnName = new String[] { "inner_class_info_index", "outer_class_info_index", "inner_name_index", "inner_class_access_flags", };
				break;
			case ATTRIBUTE.EnclosingMethod:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnName = new String[] { "class_index", "method_index", };
				break;
			case ATTRIBUTE.Signature:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, };
				columnName = new String[] { "signature_index", };
				break;
			case ATTRIBUTE.SourceFile:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, };
				columnName = new String[] { "sourcefile_index", };
				break;
			case ATTRIBUTE.SourceDebugExtension:
				columnType = new AttributeColumnType[] { AttributeColumnType.Raw, };
				columnName = new String[] { "debug_extension", };
				break;
			case ATTRIBUTE.LineNumberTable:
				columnType = new AttributeColumnType[] { AttributeColumnType.LineNumberTableRef, };
				columnName = new String[] { "line_number_table", };
				break;
			case ATTRIBUTE.LineNumberTableRef:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnName = new String[] { "start_pc", "line_number", };
				break;

			case ATTRIBUTE.LocalVariableTable:
				columnType = new AttributeColumnType[] { AttributeColumnType.LocalVariableTableRef, };
				columnName = new String[] { "local_variable_table", };
				break;
			case ATTRIBUTE.LocalVariableTableRef:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnName = new String[] { "start_pc", "length", "name_index", "descriptor_index", "index", };
				break;
			case ATTRIBUTE.LocalVariableTypeTable:
				columnType = new AttributeColumnType[] { AttributeColumnType.LocalVariableTypeTableRef, };
				columnName = new String[] { "local_variable_type_table", };
				break;
			case ATTRIBUTE.LocalVariableTypeTableRef:
				columnType = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnName = new String[] { "start_pc", "length", "name_index", "signature_index", "index", };
				break;
			case ATTRIBUTE.Deprecated:
				columnType = new AttributeColumnType[] { AttributeColumnType.Raw, };
				columnName = new String[] { "raw", };
				break;
			case ATTRIBUTE.Synthetic:
				columnType = new AttributeColumnType[] { AttributeColumnType.Raw, };
				columnName = new String[] { "raw", };
				break;
			case ATTRIBUTE.StackMapTable://TODO: Not implemented
			case ATTRIBUTE.RuntimeVisibleAnnotations://TODO: Not implemented
			case ATTRIBUTE.RuntimeVisibleParameterAnnotations://TODO: Not implemented
			case ATTRIBUTE.RuntimeVisibleTypeAnnotations://TODO: Not implemented
			case ATTRIBUTE.RuntimeInvisibleAnnotations://TODO: Not implemented
			case ATTRIBUTE.RuntimeInvisibleParameterAnnotations://TODO: Not implemented
			case ATTRIBUTE.RuntimeInvisibleTypeAnnotations://TODO: Not implemented
			case ATTRIBUTE.AnnotationDefault://TODO: Not implemented
			case ATTRIBUTE.BootstrapMethods://TODO: Not implemented
			case ATTRIBUTE.MethodParameters://TODO: Not implemented
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