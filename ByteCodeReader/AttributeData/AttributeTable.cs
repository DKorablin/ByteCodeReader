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
			_ = type ?? throw new ArgumentNullException(nameof(type));

			AttributeColumnType[] columnTypes;
			String[] columnNames;

			Jvm.ATTRIBUTE attr = Enum.IsDefined(typeof(Jvm.ATTRIBUTE), type)
				? (Jvm.ATTRIBUTE)Enum.Parse(typeof(Jvm.ATTRIBUTE), type)
				: Jvm.ATTRIBUTE.Undefined;

			switch(attr)
			{
			case Jvm.ATTRIBUTE.ConstantValue:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, };
				columnNames = new String[] { nameof(ConstantValueRow.constantvalue_index), };
				break;
			case Jvm.ATTRIBUTE.Code:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.ByteArray, AttributeColumnType.ExceptionTableRef, AttributeColumnType.AttributeRef, };
				columnNames = new String[] { nameof(CodeRow.max_stack), nameof(CodeRow.max_locals), nameof(CodeRow.code), nameof(CodeRow.exception_table), nameof(CodeRow.attributes), };
				break;
			case Jvm.ATTRIBUTE.ExceptionTableRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { nameof(ExceptionTableRefRow.start_pc), nameof(ExceptionTableRefRow.end_pc), nameof(ExceptionTableRefRow.handler_pc), nameof(ExceptionTableRefRow.catch_type), };
				break;
			case Jvm.ATTRIBUTE.Exceptions:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.ShortArray, };
				columnNames = new String[] { nameof(ExceptionsRow.exception_index_table), };
				break;
			case Jvm.ATTRIBUTE.InnerClasses:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.InnerClassRef, };
				columnNames = new String[] { nameof(InnerClassesRow.classes), };
				break;
			case Jvm.ATTRIBUTE.InnerClassesRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { nameof(InnerClassesRefRow.inner_class_info_index), nameof(InnerClassesRefRow.outer_class_info_index), nameof(InnerClassesRefRow.inner_name_index), nameof(InnerClassesRefRow.inner_class_access_flags), };
				break;
			case Jvm.ATTRIBUTE.EnclosingMethod:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { nameof(EnclosingMethodRow.class_index), nameof(EnclosingMethodRow.method_index), };
				break;
			case Jvm.ATTRIBUTE.Signature:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, };
				columnNames = new String[] { nameof(SignatureRow.signature_index), };
				break;
			case Jvm.ATTRIBUTE.SourceFile:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, };
				columnNames = new String[] { nameof(SourceFileRow.sourcefile_index), };
				break;
			case Jvm.ATTRIBUTE.SourceDebugExtension:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.Raw, };
				columnNames = new String[] { nameof(SourceDebugExtensionRow.debug_extension), };
				break;
			case Jvm.ATTRIBUTE.LineNumberTable:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.LineNumberTableRef, };
				columnNames = new String[] { nameof(LineNumberTableRow.line_number_table), };
				break;
			case Jvm.ATTRIBUTE.LineNumberTableRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { "start_pc", "line_number", };
				break;

			case Jvm.ATTRIBUTE.LocalVariableTable:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.LocalVariableTableRef, };
				columnNames = new String[] { nameof(LocalVariableTableRow.local_variable_table), };
				break;
			case Jvm.ATTRIBUTE.LocalVariableTableRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { nameof(LocalVariableTableRefRow.start_pc), nameof(LocalVariableTableRefRow.length), nameof(LocalVariableTableRefRow.name_index), nameof(LocalVariableTableRefRow.descriptor_index), nameof(LocalVariableTableRefRow.index), };
				break;
			case Jvm.ATTRIBUTE.LocalVariableTypeTable:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.LocalVariableTypeTableRef, };
				columnNames = new String[] { nameof(LocalVariableTypeTableRow.local_variable_type_table), };
				break;
			case Jvm.ATTRIBUTE.LocalVariableTypeTableRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { nameof(LocalVariableTypeTableRefRow.start_pc), nameof(LocalVariableTypeTableRefRow.length), nameof(LocalVariableTypeTableRefRow.name_index), nameof(LocalVariableTypeTableRefRow.signature_index), nameof(LocalVariableTypeTableRefRow.index), };
				break;
			case Jvm.ATTRIBUTE.BootstrapMethods:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.BootstrapMethodsRef, };
				columnNames = new String[] { nameof(BootstrapMethodsRow.bootstrap_methods), };
				break;
			case Jvm.ATTRIBUTE.BootstrapMethodsRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.ShortArray, };
				columnNames = new String[] { nameof(BootstrapMethodsRefRow.bootstrap_method_ref), nameof(BootstrapMethodsRefRow.bootstrap_arguments), };
				break;
			case Jvm.ATTRIBUTE.MethodParameters:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.MethodParametersRef, };
				columnNames = new String[] { nameof(MethodParametersRow.parameters), };
				break;
			case Jvm.ATTRIBUTE.MethodParametersRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { nameof(MethodParametersRefRow.name_index), nameof(MethodParametersRefRow.access_flags), };
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