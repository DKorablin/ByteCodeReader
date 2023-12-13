using AlphaOmega.Debug.AttributeData;
using AlphaOmega.Debug.ConstantData;
using System.Diagnostics;
using System;

namespace AlphaOmega.Debug
{
	/// <summary>Each method, including each instance initialization method (§2.9) and the class or interface initialization method (§2.9), is described by a method_info structure.</summary>
	/// <remarks>No two methods in one class file may have the same name and descriptor (§4.3.3)</remarks>
	[DebuggerDisplay("{AccessFlags} {Descriptor.bytes} {Name.bytes}")]
	public class MethodInfo : ISectionData
	{
		/// <summary>Class file owner</summary>
		private readonly ClassFile _file;
		/// <summary>Native method_info structure</summary>
		private readonly Jvm.method_info _method;
		private readonly UInt32 _offset;
		private readonly UInt32 _dataLength;

		/// <summary>The value of the access_flags item is a mask of flags used to denote access permission to and properties of this method</summary>
		public Jvm.method_info.ACC AccessFlags => this._method.access_flags;

		/// <summary>The value of the name_index item must be a valid index into the constant_pool table</summary>
		/// <remarks>The constant_pool entry at that index must be a <see cref="Jvm.CONSTANT_Utf8_info"/> (§4.4.7) structure representing either one of the special method names (§2.9) &lt;init&gt; or &lt;clinit&gt;, or a valid unqualified name (§4.2.2) denoting a method</remarks>
		public Utf8Row Name => this._file.ConstantPool.Utf8[this._method.name_index];

		/// <summary>The constant_pool entry at that index must be a <see cref="Jvm.CONSTANT_Utf8_info"/> (§4.4.7) structure representing a valid method descriptor (§4.3.3)</summary>
		/// <remarks>The value of the descriptor_index item must be a valid index into the constant_pool table</remarks>
		public Utf8Row Descriptor => this._file.ConstantPool.Utf8[this._method.descriptor_index];

		/// <summary>Each value of the attributes table must be an attribute structure (§4.7)</summary>
		/// <remarks>A method can have any number of optional attributes associated with it</remarks>
		public AttributeReference[] Attributes { get; }

		/// <summary>Create instance of <see cref="Jvm.method_info"/> native structure wrapper</summary>
		/// <param name="file">Owner class file</param>
		/// <param name="method">Native structure</param>
		/// <param name="attributes">Method attributes</param>
		/// <param name="offset">Index where method data is started</param>
		/// <param name="dataLength">Length in bytes</param>
		internal MethodInfo(ClassFile file, Jvm.method_info method, AttributeReference[] attributes, UInt32 offset, UInt32 dataLength)
		{
			this._file = file;
			this._method = method;
			this.Attributes = attributes;
			this._offset = offset;
			this._dataLength = dataLength;
		}

		/// <summary>Gets the raw binary data from class file where current method is stored</summary>
		/// <returns>byte array</returns>
		public Byte[] GetData()
			=> this._file.ReadBytes(this._offset, this._dataLength);

		/// <summary>Represents method structure as String</summary>
		/// <returns>String representation</returns>
		public override String ToString()
			=> String.Join(" ", new String[] { this.AccessFlags.ToString(), this.Descriptor.Bytes, this.Name.Bytes, });
	}
}