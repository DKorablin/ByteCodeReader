using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>The <see cref="Jvm.CONSTANT_Float_info"/> structures represent 4-byte numeric (float) constants</summary>
	public class FloatRow : BaseRow<Jvm.CONSTANT>
	{
		/// <summary>
		/// The bytes item of the <see cref="Jvm.CONSTANT_Float_info"/> structure represents the value of the float constant in IEEE 754 floating-point single format (§2.3.2).
		/// The bytes of the single format representation are stored in big-endian (high byte first) order
		/// </summary>
		/// <remarks>
		/// The value represented by the <see cref="Jvm.CONSTANT_Float_info"/> structure is determined as follows.
		/// The bytes of the value are first converted into an int constant bits.
		/// Then:
		/// If bits is 0x7f800000, the float value will be positive infinity.
		/// If bits is 0xff800000, the float value will be negative infinity.
		/// If bits is in the range 0x7f800001 through 0x7fffffff or in the range 0xff800001 through 0xffffffff, the float value will be NaN.
		/// In all other cases, let s, e, and m be three values that might be computed from bits: 
		/// 
		/// int s = ((bits &gt;&gt; 31) == 0) ? 1 : -1;
		/// int e = ((bits &gt;&gt; 23) &amp; 0xff);
		/// int m = (e == 0) ?
		///     (bits &amp; 0x7fffff) &lt;&lt; 1 :
		///     (bits &amp; 0x7fffff) | 0x800000;
		/// 
		/// Then the float value equals the result of the mathematical expression s · m · 2e-150.
		/// </remarks>
		public UInt32 bytes { get { return base.GetValue<UInt32>(0); } }
	}
}