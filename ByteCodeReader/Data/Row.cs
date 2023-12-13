using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace AlphaOmega.Debug
{
	/// <summary>Generic row for variable structure collection</summary>
	/// <typeparam name="T">Owner table type</typeparam>
	[DebuggerDisplay("Table={Table.Type} Index={Index}")]
	public class Row<T> : IEnumerable<Cell<T>>, IRow
	{
		/// <summary>Row owner table</summary>
		public Table<T> Table { get; }

		/// <inheritdoc/>
		ITable IRow.Table => this.Table;

		/// <summary>Transparent row index</summary>
		public UInt32 Index { get; internal set; }

		/// <summary>Row cells</summary>
		public Cell<T>[] Cells { get; }

		/// <summary>Get the cell by column index</summary>
		/// <param name="columnIndex">Column index</param>
		/// <exception cref="ArgumentOutOfRangeException">There is not such many cells in the current row</exception>
		/// <returns>Cell in specified column index</returns>
		public Cell<T> this[UInt16 columnIndex]
			=> columnIndex < this.Cells.Length
				? this.Cells[columnIndex]
				: throw new ArgumentOutOfRangeException(nameof(columnIndex), "columnIndex is to big");

		/// <summary>Get the cell by column name</summary>
		/// <param name="columnName">Column name</param>
		/// <exception cref="ArgumentNullException">columnName is null</exception>
		/// <exception cref="ArgumentException">Column with specific name not found</exception>
		/// <returns>Cell in specified column index</returns>
		public Cell<T> this[String columnName]
		{
			get
			{
				if(String.IsNullOrEmpty(columnName))
					throw new ArgumentNullException(nameof(columnName));

				foreach(Cell<T> cell in this.Cells)
					if(cell.Column.Name == columnName)
						return cell;
				throw new ArgumentException($"Column with name '{columnName}' not found");
			}
		}

		/// <inheritdoc/>
		ICell[] IRow.Cells => this.Cells;

		/// <inheritdoc/>
		ICell IRow.this[UInt16 columnIndex] => this[columnIndex];

		/// <inheritdoc/>
		ICell IRow.this[String columnName] => this[columnName];

		/// <inheritdoc/>
		ICell IRow.this[IColumn column] => this[column.Index];

		/// <summary>Create instance for the generic row</summary>
		/// <param name="table">Owner table</param>
		/// <param name="index">Transparent row index</param>
		/// <param name="cells">Cell array</param>
		public Row(Table<T> table, UInt32 index, Cell<T>[] cells)
		{
			if(cells == null || cells.Length == 0)
				throw new ArgumentNullException(nameof(cells));

			this.Table = table ?? throw new ArgumentNullException(nameof(table));
			this.Index = index;
			this.Cells = cells;
		}

		/// <summary>Gets all cells</summary>
		/// <returns>Cells stream</returns>
		public IEnumerator<Cell<T>> GetEnumerator()
		{
			foreach(Cell<T> cell in this.Cells)
				yield return cell;
		}

		/// <inheritdoc/>
		IEnumerator<ICell> IEnumerable<ICell>.GetEnumerator()
		{
			foreach(ICell cell in this.Cells)
				yield return cell;
		}

		/// <inheritdoc/>
		IEnumerator IEnumerable.GetEnumerator()
			=> this.GetEnumerator();
	}
}