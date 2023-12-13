using System;
using System.Diagnostics;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>Reference to the constant tables</summary>
	[DebuggerDisplay("Type={TableType} Index={Index}")]
	public class ConstantReference : IRowPointer
	{
		/// <summary><see cref="ClassFile.ConstantPool"/> array</summary>
		private readonly Tables<Jvm.CONSTANT> _root;

		/// <inheritdoc/>
		ITables IRowPointer.Root => this._root;

		/// <summary>Constant type</summary>
		public Jvm.CONSTANT? TableType { get; }

		/// <inheritdoc/>
		Object IRowPointer.TableType => this.TableType;

		/// <summary>Constant index</summary>
		public UInt32 Index { get; }

		/// <inheritdoc/>
		UInt32 IRowPointer.Index => this.Index;

		/// <summary>Create instance to the constants table with the exactly tag type</summary>
		/// <param name="root">Constants tables storage</param>
		/// <param name="type">Tag type</param>
		/// <param name="index">Transparent index to the constants table</param>
		public ConstantReference(Tables<Jvm.CONSTANT> root, Jvm.CONSTANT type, UInt32 index)
			: this(root, index)
			=> this.TableType = type;

		/// <summary>Create instance to the constans table with the transparent index</summary>
		/// <param name="root">Constants tables storage</param>
		/// <param name="index">Transparent index</param>
		public ConstantReference(Tables<Jvm.CONSTANT> root, UInt32 index)
		{
			this._root = root ?? throw new ArgumentNullException(nameof(root));
			this.Index = index;
		}

		/// <summary>Gets the constant reference</summary>
		/// <returns>Reference row</returns>
		/// <exception cref="ArgumentException">Reference not found</exception>
		public Row<Jvm.CONSTANT> GetReference()
		{
			Row<Jvm.CONSTANT> result = this.TableType == null
				? this._root.GetRowByIndex(this.Index)
				: this._root[this.TableType.Value][this.Index];

			return result ?? throw new ArgumentException($"Reference by index {this.Index} not found");
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