using System;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>Constant column types</summary>
	public enum ConstantColumnType
	{
		/// <summary>Byte column</summary>
		Byte,
		/// <summary>Unsigned short</summary>
		UInt16,
		/// <summary>Unsigned integer</summary>
		UInt32,
		/// <summary>Specific constant for <see cref="AlphaOmega.Debug.Jvm.CONSTANT.Utf8"/></summary>
		Utf8String,
	}
}