using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>Constant tables collection</summary>
	public class ConstantTables : Tables<Jvm.CONSTANT>, ISectionData
	{
		private readonly UInt32 _offset;

		/// <summary>Binary data length from file</summary>
		public UInt32 DataLength { get; }

		#region Tables
		/// <summary>The <see cref="Jvm.CONSTANT_Class_info"/> structure is used to represent a class or an interface</summary>
		public Data.BaseTable<ClassRow, Jvm.CONSTANT> Class
			=> new Data.BaseTable<ClassRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.Class]);

		/// <summary>Fields, methods, and interface methods are represented by similar structures</summary>
		public Data.BaseTable<FieldrefRow, Jvm.CONSTANT> Fieldref
			=> new Data.BaseTable<FieldrefRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.Fieldref]);

		/// <summary>Fields, methods, and interface methods are represented by similar structures</summary>
		public Data.BaseTable<MethodrefRow, Jvm.CONSTANT> Methodref
			=> new Data.BaseTable<MethodrefRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.Methodref]);

		/// <summary>Fields, methods, and interface methods are represented by similar structures</summary>
		public Data.BaseTable<InterfaceMethodrefRow, Jvm.CONSTANT> InterfaceMethodref
			=> new Data.BaseTable<InterfaceMethodrefRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.InterfaceMethodref]);

		/// <summary>The <see cref="Jvm.CONSTANT_String_info"/> structure is used to represent constant objects of the type String</summary>
		public Data.BaseTable<StringRow, Jvm.CONSTANT> String
			=> new Data.BaseTable<StringRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.String]);

		/// <summary>The <see cref="Jvm.CONSTANT_Integer_info"/> and structures represent 4-byte numeric (int) constants</summary>
		public Data.BaseTable<IntegerRow, Jvm.CONSTANT> Integer
			=> new Data.BaseTable<IntegerRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.Integer]);

		/// <summary>The <see cref="Jvm.CONSTANT_Float_info"/> structures represent 4-byte numeric (float) constants</summary>
		public Data.BaseTable<FloatRow, Jvm.CONSTANT> Float
			=> new Data.BaseTable<FloatRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.Float]);

		/// <summary>The <see cref="Jvm.CONSTANT_Long_info"/> and represent 8-byte numeric (long) constants</summary>
		public Data.BaseTable<LongRow, Jvm.CONSTANT> Long
			=> new Data.BaseTable<LongRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.Long]);

		/// <summary>The <see cref="Jvm.CONSTANT_Double_info"/> represent 8-byte numeric (double) constants</summary>
		public Data.BaseTable<DoubleRow, Jvm.CONSTANT> Double
			=> new Data.BaseTable<DoubleRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.Double]);

		/// <summary>The <see cref="Jvm.CONSTANT_NameAndType_info"/> structure is used to represent a field or method, without indicating which class or interface type it belongs to</summary>
		public Data.BaseTable<NameAndTypeRow, Jvm.CONSTANT> NameAndType
			=> new Data.BaseTable<NameAndTypeRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.NameAndType]);

		/// <summary>The <see cref="Jvm.CONSTANT_Utf8_info"/> structure is used to represent constant string values</summary>
		public Data.BaseTable<Utf8Row, Jvm.CONSTANT> Utf8
			=> new Data.BaseTable<Utf8Row, Jvm.CONSTANT>(this[Jvm.CONSTANT.Utf8]);

		/// <summary>The <see cref="Jvm.CONSTANT_MethodHandle_info"/> structure is used to represent a method handle</summary>
		public Data.BaseTable<MethodHandleRow, Jvm.CONSTANT> MethodHandle
			=> new Data.BaseTable<MethodHandleRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.MethodHandle]);

		/// <summary>The <see cref="Jvm.CONSTANT_MethodType_info"/> structure is used to represent a method type</summary>
		public Data.BaseTable<MethodTypeRow, Jvm.CONSTANT> MethodType
			=> new Data.BaseTable<MethodTypeRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.MethodType]);

		/// <summary>The <see cref="Jvm.CONSTANT_InvokeDynamic_info"/> structure is used by an invokedynamic instruction (§invokedynamic) to specify a bootstrap method, the dynamic invocation name, the argument and return types of the call, and optionally, a sequence of additional constants called static arguments to the bootstrap method</summary>
		public Data.BaseTable<InvokeDynamicRow, Jvm.CONSTANT> InvokeDynamic
			=> new Data.BaseTable<InvokeDynamicRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.InvokeDynamic]);
		#endregion Tables

		internal ConstantTables(ClassFile file, ref UInt32 offset)
			: base(file)
		{
			this._offset = offset;
			foreach(Jvm.CONSTANT item in Enum.GetValues(typeof(Jvm.CONSTANT)))
				base.AddTable(item, new ConstantTable(this, item));

			this.ReadConstants(file.ConstantPoolCount, ref offset);
			this.DataLength = offset - this._offset;
		}

		/// <summary>Gets the raw binary data from class file where all constants are stored</summary>
		/// <returns>byte array</returns>
		public Byte[] GetData()
			=> base.File.ReadBytes(this._offset, this.DataLength);

		private void ReadConstants(UInt16 constantPoolCount, ref UInt32 offset)
		{
			UInt16 constantIndex = 1;
			while(constantIndex < constantPoolCount)
			{
				Jvm.CONSTANT tag = (Jvm.CONSTANT)base.File.ReadBytes(offset, 1)[0];
				offset++;//Skipping tag byte

				ConstantTable table = (ConstantTable)base[tag];
				table.AddRow(constantIndex, ref offset);
				constantIndex++;

				switch(tag)
				{
				case Jvm.CONSTANT.Double:
				case Jvm.CONSTANT.Long:
					/*All 8-byte constants take up two entries in the ClassFile.ConstantPool table of the class file.
					If a CONSTANT_Long_info or CONSTANT_Double_info structure is the item in the ClassFile.ConstantPool table at index n, then the next usable item in the pool is located at index n+2.
					The ClassFile.ConstantPool index n+1 must be valid but is considered unusable. */
					constantIndex++;
					break;
				}
			}
		}
	}
}