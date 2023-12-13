using System;
using AlphaOmega.Debug.Data;
using AlphaOmega.Debug.ConstantData;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>The SourceFile attribute is an optional fixed-length attribute in the attributes table of a <see cref="ClassFile"/> structure (§4.1).</summary>
	/// <remarks>There may be at most one SourceFile attribute in the attributes table of a <see cref="ClassFile"/> structure.</remarks>
	public class SourceFileRow : BaseRow<String>
	{
		private UInt16 SourceFileIndexI => base.GetValue<UInt16>(0);

		/// <summary>
		/// The value of the sourcefile_index item must be a valid index into the <see cref="ClassFile.ConstantPool"/> table.
		/// The <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_Utf8_info"/> structure (§4.4.7) representing a string.
		/// </summary>
		/// <remarks>
		/// The string referenced by the sourcefile_index item will be interpreted as indicating the name of the source file from which this class file was compiled.
		/// It will not be interpreted as indicating the name of a directory containing the file or an absolute path name for the file; such platform-specific additional information must be supplied by the run-time interpreter or development tool at the time the file name is actually used.
		/// </remarks>
		public ConstantReference SourceFileIndex => new ConstantReference(base.Root.File.ConstantPool, Jvm.CONSTANT.Utf8, this.SourceFileIndexI);
	}
}