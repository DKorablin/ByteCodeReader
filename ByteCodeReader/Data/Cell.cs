using System;
using System.Diagnostics;

namespace AlphaOmega.Debug
{
	/// <summary>Generic cell for dynamic structures</summary>
	/// <typeparam name="T">Type of the owner table</typeparam>
	[DebuggerDisplay("Column={Column.Name} Value={Value}")]
	public abstract class Cell<T> : ICell
	{
		#region Fields
		private Column<T> _column;
		private UInt32 _rawValue;
		private Object _value;
		#endregion Fields

		/// <summary>Abstract value stored in the column</summary>
		public Object Value
		{
			get { return this._value; }
			protected set { this._value = value; }
		}

		/// <summary>Here can be cell value or value length or ondes to the different table</summary>
		public UInt32 RawValue
		{
			get { return this._rawValue; }
			protected set { this._rawValue = value; }
		}

		/// <summary>Description of the column owner</summary>
		public Column<T> Column { get { return this._column; } }

		IColumn ICell.Column { get { return this.Column; } }

		/// <summary>Size of the cell in bytes</summary>
		public abstract UInt32 Size { get; }

		/// <summary>Create instance of the generic cell specifying owner column</summary>
		/// <param name="column">Owner column</param>
		/// <exception cref="ArgumentNullException">column is null</exception>
		public Cell(Column<T> column)
		{
			if(column == null)
				throw new ArgumentNullException("column");

			this._column = column;
		}
	}
}