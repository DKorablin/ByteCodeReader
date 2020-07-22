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
				columnName = new String[] { "name_index", };
				break;
			case Jvm.CONSTANT.Double:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt32, ConstantColumnType.UInt32, };
				columnName = new String[] { "high_bytes", "low_bytes", };
				break;
			case Jvm.CONSTANT.Fieldref:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt16, ConstantColumnType.UInt16, };
				columnName = new String[] { "class_index", "name_and_type_index", };
				break;
			case Jvm.CONSTANT.Float:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt32, };
				columnName = new String[] { "bytes", };
				break;
			case Jvm.CONSTANT.Integer:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt32, };
				columnName = new String[] { "bytes", };
				break;
			case Jvm.CONSTANT.InterfaceMethodref:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt16, ConstantColumnType.UInt16, };
				columnName = new String[] { "class_index", "name_and_type_index", };
				break;
			case Jvm.CONSTANT.InvokeDynamic:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt16, ConstantColumnType.UInt16, };
				columnName = new String[] { "bootstrap_method_attr_index", "name_and_type_index", };
				break;
			case Jvm.CONSTANT.Long:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt32, ConstantColumnType.UInt32, };
				columnName = new String[] { "high_bytes", "low_bytes", };
				break;
			case Jvm.CONSTANT.MethodHandle:
				columnType = new ConstantColumnType[] { ConstantColumnType.Byte, ConstantColumnType.UInt16, };
				columnName = new String[] { "reference_kind", "reference_index", };
				break;
			case Jvm.CONSTANT.Methodref:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt16, ConstantColumnType.UInt16, };
				columnName = new String[] { "class_index", "name_and_type_index", };
				break;
			case Jvm.CONSTANT.MethodType:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt16, };
				columnName = new String[] { "descriptor_index", };
				break;
			case Jvm.CONSTANT.NameAndType:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt16, ConstantColumnType.UInt16, };
				columnName = new String[] { "name_index", "descriptor_index", };
				break;
			case Jvm.CONSTANT.String:
				columnType = new ConstantColumnType[] { ConstantColumnType.UInt16, };
				columnName = new String[] { "string_index", };
				break;
			case Jvm.CONSTANT.Utf8:
				columnType = new ConstantColumnType[] { /*ConstantColumnType.UInt32,*/ ConstantColumnType.Utf8String, };
				columnName = new String[] { /*"length",*/ "bytes", };
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