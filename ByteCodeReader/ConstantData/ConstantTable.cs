using System;
using System.Diagnostics;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>Generic constant table structures</summary>
	[DebuggerDisplay("Type={Type} RowsCount={RowsCount}")]
	public class ConstantTable : Table<Jvm.CONSTANT>
	{
		internal ConstantTable(ConstantTables root, Jvm.CONSTANT tag)
			: base(root, tag, ConstantTable.GetTableDescription(tag))
		{
		}

		internal void AddRow(UInt16 constantIndex, ref UInt32 offset)
		{
			ConstantCell[] cells = new ConstantCell[base.Columns.Length];
			for(UInt32 loop = 0; loop < cells.Length; loop++)
			{
				ConstantCell cell = new ConstantCell(base.Root.File, (ConstantColumn)base.Columns[loop], offset);
				offset += cell.Size;
				cells[loop] = cell;
			}

			ConstantRow row = new ConstantRow(this, constantIndex, cells);
			base.AddRow(constantIndex, row);
		}

		private static ConstantColumn[] GetTableDescription(Jvm.CONSTANT tag)
		{
			ConstantColumnType[] columnType;
			String[] columnName;

			switch(tag)
			{
			case Jvm.CONSTANT.Class:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt16, };
				columnName = new String[] { nameof(ClassRow.name_index), };
				break;
			case Jvm.CONSTANT.Double:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt32, ConstantColumnType.UInt32, };
				columnName = new String[] { nameof(DoubleRow.high_bytes), nameof(DoubleRow.low_bytes), };
				break;
			case Jvm.CONSTANT.Fieldref:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt16, ConstantColumnType.UInt16, };
				columnName = new String[] { nameof(FieldrefRow.class_index), nameof(FieldrefRow.name_and_type_index), };
				break;
			case Jvm.CONSTANT.Float:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt32, };
				columnName = new String[] { nameof(FloatRow.bytes), };
				break;
			case Jvm.CONSTANT.Integer:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt32, };
				columnName = new String[] { nameof(IntegerRow.bytes), };
				break;
			case Jvm.CONSTANT.InterfaceMethodref:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt16, ConstantColumnType.UInt16, };
				columnName = new String[] { nameof(InterfaceMethodrefRow.class_index), nameof(InterfaceMethodrefRow.name_and_type_index), };
				break;
			case Jvm.CONSTANT.InvokeDynamic:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt16, ConstantColumnType.UInt16, };
				columnName = new String[] { nameof(InvokeDynamicRow.bootstrap_method_attr_index), nameof(InvokeDynamicRow.name_and_type_index), };
				break;
			case Jvm.CONSTANT.Long:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt32, ConstantColumnType.UInt32, };
				columnName = new String[] { nameof(LongRow.high_bytes), nameof(LongRow.low_bytes), };
				break;
			case Jvm.CONSTANT.MethodHandle:
				columnType = new ConstantColumnType[] { ConstantColumnType.Byte, ConstantColumnType.UInt16, };
				columnName = new String[] { nameof(MethodHandleRow.reference_kind), nameof(MethodHandleRow.reference_index), };
				break;
			case Jvm.CONSTANT.Methodref:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt16, ConstantColumnType.UInt16, };
				columnName = new String[] { nameof(MethodrefRow.class_index), nameof(MethodrefRow.name_and_type_index), };
				break;
			case Jvm.CONSTANT.MethodType:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt16, };
				columnName = new String[] { nameof(MethodTypeRow.descriptor_index), };
				break;
			case Jvm.CONSTANT.NameAndType:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt16, ConstantColumnType.UInt16, };
				columnName = new String[] { nameof(NameAndTypeRow.name_index), nameof(NameAndTypeRow.descriptor_index), };
				break;
			case Jvm.CONSTANT.String:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt16, };
				columnName = new String[] { nameof(StringRow.string_index), };
				break;
			case Jvm.CONSTANT.Utf8:
				columnType = new ConstantColumnType[] { /*ConstantColumnType.UInt32,*/ ConstantColumnType.Utf8String, };
				columnName = new String[] { /*"length",*/ nameof(Utf8Row.bytes), };
				break;
			default:
				throw new NotSupportedException();
			}

			if(columnType.Length != columnName.Length)
				throw new InvalidOperationException("Length of column type and names must be equal");

			ConstantColumn[] result = new ConstantColumn[columnType.Length];
			for(UInt16 loop = 0; loop < columnType.Length; loop++)
				result[loop] = new ConstantColumn(tag, columnType[loop], columnName[loop], loop);

			return result;
		}
	}
}