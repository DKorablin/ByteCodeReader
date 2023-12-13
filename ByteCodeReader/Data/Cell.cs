using System;
using System.Diagnostics;

namespace AlphaOmega.Debug
{
	/// <summary>Generic cell for dynamic structures</summary>
	/// <typeparam name="T">Type of the owner table</typeparam>
	[DebuggerDisplay("Column={Column.Name} Value={Value}")]
	public class Cell<T> : ICell
	{
		/// <summary>Abstract value stored in the column</summary>
		public Object Value { get; protected set; }

		/// <summary>Here can be cell value or value length or reference to the different table</summary>
		public UInt32 RawValue { get; protected set; }

		/// <summary>Description of the column owner</summary>
		public Column<T> Column { get; }

		/// <inheritdoc/>
		IColumn ICell.Column => this.Column;

		/// <summary>Create instance of the generic cell specifying owner column</summary>
		/// <param name="column">Owner column</param>
		/// <exception cref="ArgumentNullException">column is null</exception>
		public Cell(Column<T> column)
			=> this.Column = column ?? throw new ArgumentNullException(nameof(column));
	}
}