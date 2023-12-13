using System;
using System.IO;
using System.Diagnostics;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>Generic attribute cell reader</summary>
	[DebuggerDisplay("Column={Column.Name} Value={Value}")]
	public class AttributeCell : Cell<String>
	{
		private readonly AttributeColumnType _columnType;
		//private readonly UInt32 _size;

		internal AttributeCell(AttributeTables tables, AttributeColumn column, BinaryReader reader)
			: base(column)
		{
			this._columnType = column.ColumnType;
			//this._size = (UInt32)reader.BaseStream.Length;

			switch(this._columnType)
			{
			case AttributeColumnType.Byte:
				Byte bValue = reader.ReadByte();
				base.RawValue = bValue;
				base.Value = bValue;
				break;
			case AttributeColumnType.UInt16:
				UInt16 sValue = reader.ReadUInt16();
				base.RawValue = sValue;
				base.Value = sValue;
				break;
			case AttributeColumnType.UInt32:
				UInt32 iValue = reader.ReadUInt32();
				base.RawValue = iValue;
				base.Value = iValue;
				break;
			case AttributeColumnType.ByteArray:
				base.RawValue = reader.ReadUInt32();
				base.Value = reader.ReadBytes(checked((Int32)base.RawValue));
				break;
			case AttributeColumnType.ShortArray:
				base.RawValue = reader.ReadUInt16();
				UInt16[] indexes = new UInt16[base.RawValue];
				for(UInt16 loop = 0; loop < base.RawValue; loop++)
					indexes[loop] = reader.ReadUInt16();
				base.Value = indexes;
				break;
			case AttributeColumnType.Raw:
				base.RawValue = (UInt32)reader.BaseStream.Length;
				base.Value = reader.ReadBytes((Int32)base.RawValue);
				break;
			case AttributeColumnType.ExceptionTableRef:
			case AttributeColumnType.InnerClassRef:
			case AttributeColumnType.LineNumberTableRef:
			case AttributeColumnType.LocalVariableTableRef:
			case AttributeColumnType.LocalVariableTypeTableRef:
			case AttributeColumnType.BootstrapMethodsRef:
			case AttributeColumnType.MethodParametersRef:
				base.RawValue = reader.ReadUInt16();
				AttributeReference[] references = new AttributeReference[base.RawValue];
				for(UInt16 loop = 0; loop < base.RawValue; loop++)
				{
					UInt32 index = tables.ReadSubData(this._columnType.ToString(), reader);
					references[loop] = new AttributeReference(tables, this._columnType.ToString(), index);
				}
				base.Value = references;
				break;
			case AttributeColumnType.AttributeRef:
				base.RawValue = reader.ReadUInt16();
				base.Value = tables.ReadSubAttributes((UInt16)base.RawValue, reader);
				break;
			default:
				throw new NotSupportedException();
			}
		}
	}
}