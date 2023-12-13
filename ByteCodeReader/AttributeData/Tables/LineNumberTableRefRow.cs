using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>Each entry in the line_number_table array indicates that the line number in the original source file changes at a given point in the code array.</summary>
	public class LineNumberTableRefRow : BaseRow<String>
	{
		/// <summary>The value of the <see cref="StartPc"/> item must indicate the index into the code array at which the code for a new line in the original source file begins.</summary>
		/// <remarks>The value of <see cref="StartPc"/> must be less than the value of the code_length item of the Code attribute of which this LineNumberTable is an attribute.</remarks>
		public UInt16 StartPc => base.GetValue<UInt16>(0);

		/// <summary>The value of the line_number item must give the corresponding line number in the original source file. </summary>
		public UInt16 LineNumber => base.GetValue<UInt16>(1);
	}
}