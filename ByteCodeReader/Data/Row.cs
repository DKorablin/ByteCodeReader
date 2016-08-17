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
		#region Fields
		private UInt32 _index;
		private readonly Table<T> _table;
		private readonly Cell<T>[] _cells;
		#endregion Fields

		/// <summary>Row owner table</summary>
		public Table<T> Table { get { return this._table; } }
		ITable IRow.Table { get { return this.Table; } }

		/// <summary>Transparent row index</summary>
		public UInt32 Index
		{
			get { return this._index; }
			internal set { this._index = value; }
		}

		/// <summary>Row cells</summary>
		public Cell<T>[] Cells { get { return this._cells; } }

		/// <summary>Get the cell by column index</summary>
		/// <param name="columnIndex">Column index</param>
		/// <exception cref="ArgumentOutOfRangeException">There is not such many cells in the current row</exception>
		/// <returns>Cell in specified column index</returns>
		public Cell<T> this[UInt16 columnIndex]
		{
			get
			{
				if(columnIndex < this._cells.Length)
					return this._cells[columnIndex];
				else
					throw new ArgumentOutOfRangeException("columnIndex is to big");
			}
		}

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
					throw new ArgumentNullException("columnName");

				foreach(Cell<T> cell in this._cells)
					if(cell.Column.Name == columnName)
						return cell;
				throw new ArgumentException(String.Format("Column with name '{0}' not found", columnName));
			}
		}

		ICell[] IRow.Cells { get { return this.Cells; } }

		ICell IRow.this[UInt16 columnIndex] { get { return this[columnIndex]; } }

		ICell IRow.this[String columnName] { get { return this[columnName]; } }

		ICell IRow.this[IColumn column] { get { return this[column.Index]; } }

		/// <summary>Create instance for the generic row</summary>
		/// <param name="table">Owner table</param>
		/// <param name="index">Transparent row index</param>
		/// <param name="cells">Cell array</param>
		public Row(Table<T> table, UInt32 index, Cell<T>[] cells)
		{
			if(table == null)
				throw new ArgumentNullException("table");
			if(cells == null || cells.Length == 0)
				throw new ArgumentNullException("cells");

			this._table = table;
			this._index = index;
			this._cells = cells;
		}

		/// <summary>Gets all cells</summary>
		/// <returns>Cells stream</returns>
		public IEnumerator<Cell<T>> GetEnumerator()
		{
			foreach(Cell<T> cell in this.Cells)
				yield return cell;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		IEnumerator<ICell> IEnumerable<ICell>.GetEnumerator()
		{
			foreach(ICell cell in this.Cells)
				yield return cell;
		}

	}
}