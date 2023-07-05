using System;
using System.Diagnostics;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>Reference to the attributes tables</summary>
	[DebuggerDisplay("Type={TableType} Index={Index}")]
	public class AttributeReference : IRowPointer
	{
		#region Fields
		private readonly Tables<String> _root;
		private readonly String _type;
		private readonly UInt32 _index;
		#endregion Fields

		/// <summary>Attribute tables array</summary>
		private Tables<String> Root { get { return this._root; } }
		ITables IRowPointer.Root { get { return this._root; } }

		/// <summary>Attribute type</summary>
		public String TableType { get { return this._type; } }

		/// <inheritdoc/>
		Object IRowPointer.TableType { get { return this._type; } }

		/// <summary>Attribute index</summary>
		public UInt32 Index { get { return this._index; } }

		/// <summary>Create instance to the attributes table</summary>
		/// <param name="root">Attributes tables storage</param>
		/// <param name="type">Attribute type</param>
		/// <param name="index">Transparent index to the attributes table</param>
		/// <exception cref="ArgumentNullException">root is null</exception>
		/// <exception cref="ArgumentNullException">type is null or empty</exception>
		public AttributeReference(Tables<String> root, String type, UInt32 index)
		{
			this._root = root ?? throw new ArgumentNullException(nameof(root));
			this._type = type ?? throw new ArgumentNullException(nameof(type));
			this._index = index;
		}

		/// <summary>Gets the constant reference</summary>
		/// <returns>Reference row</returns>
		/// <exception cref="ArgumentException">Reference not found</exception>
		public Row<String> GetReference()
		{
			Row<String> result = this.TableType == null
				? this.Root.GetRowByIndex(this.Index)
				: this.Root[this.TableType][this.Index];

			return result ?? throw new ArgumentException($"Reference by index {this.Index} not found", nameof(result));
		}

		/// <inheritdoc/>
		IRow IRowPointer.GetReference()
		{
			return this.GetReference();
		}

		/// <summary>Reference string representation</summary>
		/// <returns>String</returns>
		public override String ToString()
		{
			return $"{this.GetType().Name}: {{{this.TableType}}}:{{{this.Index}}}";
		}
	}
}