﻿using System;
using System.Diagnostics;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>The <see cref="Jvm.CONSTANT.Utf8"/>_info structure is used to represent constant string values</summary>
	[DebuggerDisplay("bytes={Bytes}")]
	public class Utf8Row : BaseRow<Jvm.CONSTANT>
	{
		/// <summary>
		/// String content is encoded in modified UTF-8.
		/// Modified UTF-8 strings are encoded so that code point sequences that contain only non-null ASCII characters can be represented using only 1 byte per code point, but all code points in the Unicode codespace can be represented.
		/// Modified UTF-8 strings are not null-terminated
		/// </summary>
		/// <remarks>https://docs.oracle.com/javase/specs/jvms/se8/html/jvms-4.html#jvms-4.4.7</remarks>
		public String Bytes => base.GetValue<String>(0);

		/// <summary>Row friendly description</summary>
		/// <returns></returns>
		public override String ToString()
			=> $"{this.GetType().Name}: {{{this.Bytes}}}";
	}
}