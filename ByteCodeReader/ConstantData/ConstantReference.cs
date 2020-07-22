using System;
using System.Diagnostics;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>Reference to the constant tables</summary>
	[DebuggerDisplay("Type={TableType} Index={Index}")]
	public class ConstantReference : IRowPointer
	{
		#region Fields
		private readonly Tables<Jvm.CONSTANT> _root;
		private readonly Jvm.CONSTANT? _type;
		private readonly UInt32 _index;
		#endregion Fields

		/// <summary>constant_pool array</summary>
		private Tables<Jvm.CONSTANT> Root { get { return this._root; } }
		ITables IRowPointer.Root { get { return this._root; } }

		/// <summary>Constant type</summary>
		public Jvm.CONSTANT? TableType { get { return this._type; } }
		Object IRowPointer.TableType { get { return this._type; } }

		/// <summary>Constant index</summary>
		public UInt32 Index { get { return this._index; } }
		UInt32 IRowPointer.Index { get { return this.Index; } }

		/// <summary>Create instance to the constants table with the exactly tag type</summary>
		/// <param name="root">Constants tables storage</param>
		/// <param name="type">Tag type</param>
		/// <param name="index">Transparent index to the constants table</param>
		public ConstantReference(Tables<Jvm.CONSTANT> root, Jvm.CONSTANT type, UInt32 index)
			: this(root, index)
		{
			this._type = type;
		}

		/// <summary>Create instance to the constans table with the transparent index</summary>
		/// <param name="root">Constants tables storage</param>
		/// <param name="index">Transparent index</param>
		public ConstantReference(Tables<Jvm.CONSTANT> root, UInt32 index)
		{
			if(root == null)
				throw new ArgumentNullException("root");

			this._root = root;
			this._index = index;
		}

		/// <summary>Gets the constant reference</summary>
		/// <returns>Reference row</returns>
		public Row<Jvm.CONSTANT> GetReference()
		{
			Row<Jvm.CONSTANT> result = this.TableType == null
				? this.Root.GetRowByIndex(this.Index)
				: this.Root[this.TableType.Value][this.Index];

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