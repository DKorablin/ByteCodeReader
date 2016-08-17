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

			return table.AddRow(this, reader);
		}

		private AttributeReference ParseAttribute(Jvm.attribute_info attribute, Byte[] payload)
		{
			Utf8Row constantRow = base.File.constant_pool.Utf8[attribute.attribute_name_index];
			AttributeTable table = this.GetOrCreateTable(constantRow.bytes);

			using(MemoryStream stream = new MemoryStream(payload))
			{
				BinaryReader reader = BinaryEndianReader.CreateReader(Utils.Endian.Big, stream);
				UInt32 rowIndex = table.AddRow(this, reader);
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