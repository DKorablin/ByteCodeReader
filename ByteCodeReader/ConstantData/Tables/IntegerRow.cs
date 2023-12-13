using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>The <see cref="Jvm.CONSTANT_Integer_info"/> and structures represent 4-byte numeric (int) constants</summary>
	public class IntegerRow : BaseRow<Jvm.CONSTANT>
	{
		/// <summary>The bytes item of the <see cref="Jvm.CONSTANT_Integer_info"/> structure represents the value of the int constant</summary>
		/// <remarks>The bytes of the value are stored in big-endian (high byte first) order</remarks>
		public UInt32 Bytes => base.GetValue<UInt32>(0);
	}
}