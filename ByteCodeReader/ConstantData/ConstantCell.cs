using System;
using System.Diagnostics;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>Generic constant cell reader</summary>
	[DebuggerDisplay("Column={Column.Name} Value={Value}")]
	public class ConstantCell : Cell<Jvm.CONSTANT>
	{
		private readonly ConstantColumnType _columnType;

		/// <summary>Size in bytes of the column</summary>
		public override UInt32 Size
		{
			get
			{
				switch(this._columnType)
				{
				case ConstantColumnType.Byte:
					return sizeof(Byte);
				case ConstantColumnType.UInt16:
					return sizeof(UInt16);
				case ConstantColumnType.UInt32:
					return sizeof(UInt32);
				case ConstantColumnType.Utf8String:
					return sizeof(UInt16) + base.RawValue;//Here is the length of a column is stored
				default:
					throw new NotImplementedException();
				}
			}
		}

		internal ConstantCell(ClassFile file, ConstantColumn column, UInt32 offset)
			: base(column)
		{
			this._columnType = column.ColumnType;

			switch(this._columnType)
			{
			case ConstantColumnType.Byte:
				base.RawValue = file.ReadBytes(offset, 1)[0];
				base.Value = (Byte)base.RawValue;
				break;
			case ConstantColumnType.UInt16:
				base.RawValue = file.PtrToStructure<UInt16>(offset);
				base.Value = (UInt16)base.RawValue;
				break;
			case ConstantColumnType.UInt32:
				base.RawValue = file.PtrToStructure<UInt32>(offset);
				base.Value = (UInt32)base.RawValue;
				break;
			case ConstantColumnType.Utf8String:
				base.RawValue = file.PtrToStructure<UInt16>(offset);
				Byte[] bytes = file.ReadBytes(offset + sizeof(UInt16), base.RawValue);
				base.Value = System.Text.Encoding.UTF8.GetString(bytes);
				break;
			default:
				throw new NotImplementedException();
			}
		}
	}
}