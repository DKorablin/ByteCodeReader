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
		{ }

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
				columnNames = new String[] { nameof(ConstantValueRow.ConstantValueIndex), };
				break;
			case Jvm.ATTRIBUTE.Code:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.ByteArray, AttributeColumnType.ExceptionTableRef, AttributeColumnType.AttributeRef, };
				columnNames = new String[] { nameof(CodeRow.MaxStack), nameof(CodeRow.MaxLocals), nameof(CodeRow.Code), nameof(CodeRow.ExceptionTable), nameof(CodeRow.Attributes), };
				break;
			case Jvm.ATTRIBUTE.ExceptionTableRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { nameof(ExceptionTableRefRow.StartPc), nameof(ExceptionTableRefRow.EndPc), nameof(ExceptionTableRefRow.HandlerPc), nameof(ExceptionTableRefRow.CatchType), };
				break;
			case Jvm.ATTRIBUTE.Exceptions:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.ShortArray, };
				columnNames = new String[] { nameof(ExceptionsRow.ExceptionIndexTable), };
				break;
			case Jvm.ATTRIBUTE.InnerClasses:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.InnerClassRef, };
				columnNames = new String[] { nameof(InnerClassesRow.Classes), };
				break;
			case Jvm.ATTRIBUTE.InnerClassesRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { nameof(InnerClassesRefRow.InnerClassInfoIndex), nameof(InnerClassesRefRow.OuterClassInfoIndex), nameof(InnerClassesRefRow.InnerNameIndex), nameof(InnerClassesRefRow.InnerClassAccessFlags), };
				break;
			case Jvm.ATTRIBUTE.EnclosingMethod:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { nameof(EnclosingMethodRow.ClassIndex), nameof(EnclosingMethodRow.MethodIndex), };
				break;
			case Jvm.ATTRIBUTE.Signature:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, };
				columnNames = new String[] { nameof(SignatureRow.SignatureIndex), };
				break;
			case Jvm.ATTRIBUTE.SourceFile:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, };
				columnNames = new String[] { nameof(SourceFileRow.SourceFileIndex), };
				break;
			case Jvm.ATTRIBUTE.SourceDebugExtension:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.Raw, };
				columnNames = new String[] { nameof(SourceDebugExtensionRow.DebugExtension), };
				break;
			case Jvm.ATTRIBUTE.LineNumberTable:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.LineNumberTableRef, };
				columnNames = new String[] { nameof(LineNumberTableRow.LineNumberTable), };
				break;
			case Jvm.ATTRIBUTE.LineNumberTableRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { "StartPc", "LineNumber", };
				break;

			case Jvm.ATTRIBUTE.LocalVariableTable:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.LocalVariableTableRef, };
				columnNames = new String[] { nameof(LocalVariableTableRow.LocalVariableTable), };
				break;
			case Jvm.ATTRIBUTE.LocalVariableTableRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { nameof(LocalVariableTableRefRow.StartPc), nameof(LocalVariableTableRefRow.Length), nameof(LocalVariableTableRefRow.NameIndex), nameof(LocalVariableTableRefRow.DescriptorIndex), nameof(LocalVariableTableRefRow.Index), };
				break;
			case Jvm.ATTRIBUTE.LocalVariableTypeTable:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.LocalVariableTypeTableRef, };
				columnNames = new String[] { nameof(LocalVariableTypeTableRow.LocalVariableTypeTable), };
				break;
			case Jvm.ATTRIBUTE.LocalVariableTypeTableRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { nameof(LocalVariableTypeTableRefRow.StartPc), nameof(LocalVariableTypeTableRefRow.Length), nameof(LocalVariableTypeTableRefRow.NameIndex), nameof(LocalVariableTypeTableRefRow.SignatureIndex), nameof(LocalVariableTypeTableRefRow.Index), };
				break;
			case Jvm.ATTRIBUTE.BootstrapMethods:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.BootstrapMethodsRef, };
				columnNames = new String[] { nameof(BootstrapMethodsRow.BootstrapMethods), };
				break;
			case Jvm.ATTRIBUTE.BootstrapMethodsRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.ShortArray, };
				columnNames = new String[] { nameof(BootstrapMethodsRefRow.BootstrapMethodRef), nameof(BootstrapMethodsRefRow.BootstrapArguments), };
				break;
			case Jvm.ATTRIBUTE.MethodParameters:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.MethodParametersRef, };
				columnNames = new String[] { nameof(MethodParametersRow.Parameters), };
				break;
			case Jvm.ATTRIBUTE.MethodParametersRef:
				columnTypes = new AttributeColumnType[] { AttributeColumnType.UInt16, AttributeColumnType.UInt16, };
				columnNames = new String[] { nameof(MethodParametersRefRow.NameIndex), nameof(MethodParametersRefRow.AccessFlags), };
				break;
			case Jvm.ATTRIBUTE.Deprecated:
			case Jvm.ATTRIBUTE.Synthetic:
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
				columnNames = new String[] { "Raw", };
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