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
			if(root == null)
				throw new ArgumentNullException("root");
			if(String.IsNullOrEmpty(type))
				throw new ArgumentNullException("type");

			this._root = root;
			this._type = type;
			this._index = index;
		}

		/// <summary>Gets the constant reference</summary>
		/// <returns>Reference row</returns>
		public Row<String> GetReference()
		{
			Row<String> result = this.TableType == null
				? this.Root.GetRowByIndex(this.Index)
				: this.Root[this.TableType][this.Index];

			if(result == null)
				throw new ArgumentException(String.Format("Reference by index {0} not found", this.Index));
			else
				return result;
		}

		IRow IRowPointer.GetReference()
		{
			return this.GetReference();
		}

		/// <summary>Reference string representation</summary>
		/// <returns>String</returns>
		public override String ToString()
		{
			return String.Format("{0}: {{{1}}}:{{{2}}}", this.GetType().Name, this.TableType, this.Index);
		}
	}
}