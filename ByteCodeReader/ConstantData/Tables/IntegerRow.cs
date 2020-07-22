using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>The CONSTANT_Integer_info and structures represent 4-byte numeric (int) constants</summary>
	public class IntegerRow : BaseRow<Jvm.CONSTANT>
	{
		/// <summary>
		/// The bytes item of the CONSTANT_Integer_info structure represents the value of the int constant.
		/// The bytes of the value are stored in big-endian (high byte first) order.
		/// </summary>
		public UInt32 bytes { get { return base.GetValue<UInt32>(0); } }
	}
}