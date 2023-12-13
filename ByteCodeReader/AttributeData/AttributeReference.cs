using System;
using System.Diagnostics;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>Reference to the attributes tables</summary>
	[DebuggerDisplay("Type={TableType} Index={Index}")]
	public class AttributeReference : IRowPointer
	{
		/// <summary>Attribute tables array</summary>
		private readonly Tables<String> _root;

		ITables IRowPointer.Root => this._root;

		/// <summary>Attribute type</summary>
		public String TableType { get; }

		/// <inheritdoc/>
		Object IRowPointer.TableType => this.TableType;

		/// <summary>Attribute index</summary>
		public UInt32 Index { get; }

		/// <summary>Create instance to the attributes table</summary>
		/// <param name="root">Attributes tables storage</param>
		/// <param name="type">Attribute type</param>
		/// <param name="index">Transparent index to the attributes table</param>
		/// <exception cref="ArgumentNullException">root is null</exception>
		/// <exception cref="ArgumentNullException">type is null or empty</exception>
		public AttributeReference(Tables<String> root, String type, UInt32 index)
		{
			this._root = root ?? throw new ArgumentNullException(nameof(root));
			this.TableType = type ?? throw new ArgumentNullException(nameof(type));
			this.Index = index;
		}

		/// <summary>Gets the constant reference</summary>
		/// <returns>Reference row</returns>
		/// <exception cref="InvalidOperationException">Reference not found</exception>
		public Row<String> GetReference()
		{
			Row<String> result = this.TableType == null
				? this._root.GetRowByIndex(this.Index)
				: this._root[this.TableType][this.Index];

			return result ?? throw new InvalidOperationException($"Reference by index {this.Index} not found");
		}

		/// <inheritdoc/>
		IRow IRowPointer.GetReference()
			=> this.GetReference();

		/// <summary>Reference string representation</summary>
		/// <returns>String</returns>
		public override String ToString()
			=> $"{this.GetType().Name}: {{{this.TableType}}}:{{{this.Index}}}";
	}
}