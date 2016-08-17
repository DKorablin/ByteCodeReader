using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>Constant tables collection</summary>
	public class ConstantTables : Tables<Jvm.CONSTANT>, ISectionData
	{
		private readonly UInt32 _offset;
		private readonly UInt32 _dataLength;

		/// <summary>Binary data length from file</summary>
		public UInt32 DataLength { get { return this._dataLength; } }

		#region Tables
		/// <summary>The CONSTANT_Class_info structure is used to represent a class or an interface</summary>
		public Data.BaseTable<ClassRow, Jvm.CONSTANT> Class
		{
			get { return new Data.BaseTable<ClassRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.Class]); }
		}

		/// <summary>Fields, methods, and interface methods are represented by similar structures</summary>
		public Data.BaseTable<FieldrefRow, Jvm.CONSTANT> Fieldref
		{
			get { return new Data.BaseTable<FieldrefRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.Fieldref]); }
		}

		/// <summary>Fields, methods, and interface methods are represented by similar structures</summary>
		public Data.BaseTable<MethodrefRow, Jvm.CONSTANT> Methodref
		{
			get { return new Data.BaseTable<MethodrefRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.Methodref]); }
		}

		/// <summary>Fields, methods, and interface methods are represented by similar structures</summary>
		public Data.BaseTable<InterfaceMethodrefRow, Jvm.CONSTANT> InterfaceMethodref
		{
			get { return new Data.BaseTable<InterfaceMethodrefRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.InterfaceMethodref]); }
		}

		/// <summary>The CONSTANT_String_info structure is used to represent constant objects of the type String</summary>
		public Data.BaseTable<StringRow, Jvm.CONSTANT> String
		{
			get { return new Data.BaseTable<StringRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.String]); }
		}

		/// <summary>The CONSTANT_Integer_info and structures represent 4-byte numeric (int) constants</summary>
		public Data.BaseTable<IntegerRow, Jvm.CONSTANT> Integer
		{
			get { return new Data.BaseTable<IntegerRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.Integer]); }
		}

		/// <summary>The CONSTANT_Float_info structures represent 4-byte numeric (float) constants</summary>
		public Data.BaseTable<FloatRow, Jvm.CONSTANT> Float
		{
			get { return new Data.BaseTable<FloatRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.Float]); }
		}

		/// <summary>The CONSTANT_Long_info and represent 8-byte numeric (long) constants</summary>
		public Data.BaseTable<LongRow, Jvm.CONSTANT> Long
		{
			get { return new Data.BaseTable<LongRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.Long]); }
		}

		/// <summary>The CONSTANT_Double_info represent 8-byte numeric (double) constants</summary>
		public Data.BaseTable<DoubleRow, Jvm.CONSTANT> Double
		{
			get { return new Data.BaseTable<DoubleRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.Double]); }
		}

		/// <summary>The CONSTANT_NameAndType_info structure is used to represent a field or method, without indicating which class or interface type it belongs to</summary>
		public Data.BaseTable<NameAndTypeRow, Jvm.CONSTANT> NameAndType
		{
			get { return new Data.BaseTable<NameAndTypeRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.NameAndType]); }
		}

		/// <summary>The CONSTANT_Utf8_info structure is used to represent constant string values</summary>
		public Data.BaseTable<Utf8Row, Jvm.CONSTANT> Utf8
		{
			get { return new Data.BaseTable<Utf8Row, Jvm.CONSTANT>(this[Jvm.CONSTANT.Utf8]); }
		}

		/// <summary>The CONSTANT_MethodHandle_info structure is used to represent a method handle</summary>
		public Data.BaseTable<MethodHandleRow, Jvm.CONSTANT> MethodHandle
		{
			get { return new Data.BaseTable<MethodHandleRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.MethodHandle]); }
		}

		/// <summary>The CONSTANT_MethodType_info structure is used to represent a method type</summary>
		public Data.BaseTable<MethodTypeRow, Jvm.CONSTANT> MethodType
		{
			get { return new Data.BaseTable<MethodTypeRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.MethodType]); }
		}

		/// <summary>The CONSTANT_InvokeDynamic_info structure is used by an invokedynamic instruction (§invokedynamic) to specify a bootstrap method, the dynamic invocation name, the argument and return types of the call, and optionally, a sequence of additional constants called static arguments to the bootstrap method</summary>
		public Data.BaseTable<InvokeDynamicRow, Jvm.CONSTANT> InvokeDynamic
		{
			get { return new Data.BaseTable<InvokeDynamicRow, Jvm.CONSTANT>(this[Jvm.CONSTANT.InvokeDynamic]); }
		}

		#endregion Tables

		internal ConstantTables(ClassFile file, ref UInt32 offset)
			: base(file)
		{
			this._offset = offset;
			foreach(Jvm.CONSTANT item in Enum.GetValues(typeof(Jvm.CONSTANT)))
				base.AddTable(item, new ConstantTable(this, item));

			this.ReadConstants(file.constant_pool_count, ref offset);
			this._dataLength = offset - this._offset;
		}

		/// <summary>Gets the raw binary data from class file where all constants are stored</summary>
		/// <returns>byte array</returns>
		public Byte[] GetData()
		{
			return base.File.ReadBytes(this._offset, this._dataLength);
		}

		private void ReadConstants(UInt16 constant_pool_count, ref UInt32 offset)
		{
			UInt16 constantIndex = 1;
			while(constantIndex < constant_pool_count)
			{
				Jvm.CONSTANT tag = (Jvm.CONSTANT)base.File.ReadBytes(offset, 1)[0];
				offset++;//Skipping tag byte

				UInt32 rowOffset = offset;
				ConstantTable table = (ConstantTable)base[tag];
				table.AddRow(base.File, constantIndex, ref offset);
				constantIndex++;

				switch(tag)
				{
				case Jvm.CONSTANT.Double:
				case Jvm.CONSTANT.Long:
					/*All 8-byte constants take up two entries in the constant_pool table of the class file.
					If a CONSTANT_Long_info or CONSTANT_Double_info structure is the item in the constant_pool table at index n, then the next usable item in the pool is located at index n+2.
					The constant_pool index n+1 must be valid but is considered unusable. */
					constantIndex++;
					break;
				}
			}
		}
	}
}