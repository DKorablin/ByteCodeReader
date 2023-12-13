using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>The SourceDebugExtension attribute is an optional attribute in the attributes table of a ClassFile structure (§4.1).</summary>
	/// <remarks>There may be at most one SourceDebugExtension attribute in the attributes table of a ClassFile structure.</remarks>
	public class SourceDebugExtensionRow : BaseRow<String>
	{
		/// <summary>
		/// The debug_extension array holds extended debugging information which has no semantic effect on the Java Virtual Machine.
		/// The information is represented using a modified UTF-8 string (§4.4.7) with no terminating zero byte.
		/// </summary>
		/// <remarks>Note that the debug_extension array may denote a string longer than that which can be represented with an instance of class String.</remarks>
		public Byte[] DebugExtension => base.GetValue<Byte[]>(0);
	}
}