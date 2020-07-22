using System;
using System.Diagnostics;
using AlphaOmega.Debug.AttributeData;
using AlphaOmega.Debug.ConstantData;

namespace AlphaOmega.Debug
{
	/// <summary>Each field is described by a field_info structure. No two fields in one class file may have the same name and descriptor (§4.3.2).</summary>
	[DebuggerDisplay("{access_flags} {descriptor.bytes} {name.bytes}")]
	public class Field_Info : ISectionData
	{
		#region Fields
		private readonly ClassFile _file;
		private readonly Jvm.field_info _field;
		private readonly AttributeReference[] _attributes;
		private readonly UInt32 _offset;
		private readonly UInt32 _dataLength;
		#endregion Fields

		/// <summary>Class file owner</summary>
		private ClassFile File { get { return this._file; } }

		/// <summary>Native filed_info structure</summary>
		private Jvm.field_info Field { get { return this._field; } }

		/// <summary>The value of the access_flags item is a mask of flags used to denote access permission to and properties of this field</summary>
		public Jvm.field_info.ACC access_flags { get { return this.Field.access_flags; } }

		/// <summary>
		/// The value of the name_index item must be a valid index into the constant_pool table.
		/// The constant_pool entry at that index must be a CONSTANT_Utf8_info (§4.4.7) structure which must represent a valid unqualified name (§4.2.2) denoting a field.
		/// </summary>
		public Utf8Row name { get { return this.File.constant_pool.Utf8[this.Field.name_index]; } }

		/// <summary>
		/// The value of the descriptor_index item must be a valid index into the constant_pool table.
		/// The constant_pool entry at that index must be a CONSTANT_Utf8_info (§4.4.7) structure that must represent a valid field descriptor (§4.3.2).
		/// </summary>
		public Utf8Row descriptor { get { return this.File.constant_pool.Utf8[this.Field.descriptor_index]; } }

		/// <summary>Each value of the attributes table must be an attribute structure (§4.7). A field can have any number of attributes associated with it.</summary>
		/// <remarks>
		/// The attributes defined by this specification as appearing in the attributes table of a field_info structure are ConstantValue (§4.7.2), Synthetic (§4.7.8), Signature (§4.7.9), Deprecated (§4.7.15), RuntimeVisibleAnnotations (§4.7.16) and RuntimeInvisibleAnnotations (§4.7.17).
		/// A Java Virtual Machine implementation must recognize and correctly read ConstantValue (§4.7.2) attributes found in the attributes table of a field_info structure. If a Java Virtual Machine implementation recognizes class files whose version number is 49.0 or above, it must recognize and correctly read Signature (§4.7.9), RuntimeVisibleAnnotations (§4.7.16) and RuntimeInvisibleAnnotations (§4.7.17) attributes found in the attributes table of a field_info structure of a class file whose version number is 49.0 or above.
		/// A Java Virtual Machine implementation is required to silently ignore any or all attributes that it does not recognize in the attributes table of a field_info structure. Attributes not defined in this specification are not allowed to affect the semantics of the class file, but only to provide additional descriptive information (§4.7.1).
		/// </remarks>
		public AttributeReference[] attributes { get { return this._attributes; } }

		/// <summary>Create instance of Jvm.fileld_info native structure wrapper</summary>
		/// <param name="file">Owner class file</param>
		/// <param name="field">Native structure</param>
		/// <param name="attributes">Field attributes</param>
		/// <param name="offset">Index where method data is started</param>
		/// <param name="dataLength">Length in bytes</param>
		internal Field_Info(ClassFile file, Jvm.field_info field, AttributeReference[] attributes, UInt32 offset, UInt32 dataLength)
		{
			this._file = file;
			this._field = field;
			this._attributes = attributes;
			this._offset = offset;
			this._dataLength = dataLength;
		}

		/// <summary>Gets the raw binary data from class file where current field is stored</summary>
		/// <returns>byte array</returns>
		public Byte[] GetData()
		{
			return this.File.ReadBytes(this._offset, this._dataLength);
		}

		/// <summary>Represents field structure as String</summary>
		/// <returns>String representation</returns>
		public override String ToString()
		{
			return String.Join(" ", new String[] { this.access_flags.ToString(), this.descriptor.bytes, this.name.bytes, });
		}
	}
}