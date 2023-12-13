using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>The <see cref="Jvm.CONSTANT_Long_info"/> and represent 8-byte numeric (long) constants</summary>
	/// <remarks>
	/// All 8-byte constants take up two entries in the <see cref="ClassFile.ConstantPool"/> table of the class file.
	/// If a <see cref="Jvm.CONSTANT_Long_info"/> or <see cref="Jvm.CONSTANT_Double_info"/> structure is the item in the <see cref="ClassFile.ConstantPool"/> table at index n, then the next usable item in the pool is located at index n+2.
	/// The <see cref="ClassFile.ConstantPool"/> index n+1 must be valid but is considered unusable.
	/// 
	/// In retrospect, making 8-byte constants take two constant pool entries was a poor choice.
	/// </remarks>
	public class LongRow : BaseRow<Jvm.CONSTANT>
	{
		/// <summary>The unsigned high_bytes and low_bytes items of the <see cref="Jvm.CONSTANT_Long_info"/> structure together represent the value of the long constant</summary>
		/// <remarks>
		/// ((long) high_bytes &lt;&lt; 32) + low_bytes
		/// where the bytes of each of high_bytes and low_bytes are stored in big-endian (high byte first) order.
		/// The high_bytes and low_bytes items of the <see cref="Jvm.CONSTANT_Double_info"/> structure together represent the double value in IEEE 754 floating-point double format (§2.3.2).
		/// The bytes of each item are stored in big-endian (high byte first) order.
		/// </remarks>
		public UInt32 HighBytes => base.GetValue<UInt32>(0);

		/// <summary>The unsigned high_bytes and low_bytes items of the <see cref="Jvm.CONSTANT_Long_info"/> structure together represent the value of the long constant</summary>
		public UInt32 LowBytes => base.GetValue<UInt32>(1);
	}
}