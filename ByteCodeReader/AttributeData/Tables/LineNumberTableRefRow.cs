using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>Each entry in the line_number_table array indicates that the line number in the original source file changes at a given point in the code array.</summary>
	public class LineNumberTableRefRow : BaseRow<String>
	{
		/// <summary>The value of the start_pc item must indicate the index into the code array at which the code for a new line in the original source file begins.</summary>
		/// <remarks>The value of start_pc must be less than the value of the code_length item of the Code attribute of which this LineNumberTable is an attribute.</remarks>
		public UInt16 start_pc { get { return base.GetValue<UInt16>(0); } }

		/// <summary>The value of the line_number item must give the corresponding line number in the original source file. </summary>
		public UInt16 line_number { get { return base.GetValue<UInt16>(1); } }
	}
}