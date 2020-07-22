using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>The CONSTANT_Double_info represent 8-byte numeric (double) constants</summary>
	public class DoubleRow : BaseRow<Jvm.CONSTANT>
	{
		/// <summary>The high_bytes and low_bytes items of the CONSTANT_Double_info structure together represent the double value in IEEE 754 floating-point double format (§2.3.2). The bytes of each item are stored in big-endian (high byte first) order. </summary>
		/// <remarks>
		/// The value represented by the CONSTANT_Double_info structure is determined as follows. The high_bytes and low_bytes items are converted into the long constant bits, which is equal to
		/// ((long) high_bytes &lt;&lt; 32) + low_bytes
		/// 
		/// Then:
		/// If bits is 0x7ff0000000000000L, the double value will be positive infinity.
		/// If bits is 0xfff0000000000000L, the double value will be negative infinity.
		/// If bits is in the range 0x7ff0000000000001L through 0x7fffffffffffffffL or in the range 0xfff0000000000001L through 0xffffffffffffffffL, the double value will be NaN.
		/// In all other cases, let s, e, and m be three values that might be computed from bits:
		/// 
		/// int s = ((bits &gt;&gt; 63) == 0) ? 1 : -1;
		/// int e = (int)((bits &gt;&gt; 52) &amp; 0x7ffL);
		/// long m = (e == 0) ?
		///     (bits &amp; 0xfffffffffffffL) &lt;&lt; 1 :
		///     (bits &amp; 0xfffffffffffffL) | 0x10000000000000L;
		///     
		/// Then the floating-point value equals the double value of the mathematical expression s · m · 2e-1075.
		/// </remarks>
		public UInt32 high_bytes { get { return base.GetValue<UInt32>(0); } }

		/// <summary>The high_bytes and low_bytes items of the CONSTANT_Double_info structure together represent the double value in IEEE 754 floating-point double format (§2.3.2). The bytes of each item are stored in big-endian (high byte first) order. </summary>
		public UInt32 low_bytes { get { return base.GetValue<UInt32>(1); } }
	}
}