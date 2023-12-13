using System;
using System.Collections.Generic;

namespace AlphaOmega.Debug
{
	/// <summary>Generic structure container</summary>
	/// <typeparam name="T">Table type</typeparam>
	public class Table<T> : ITable
	{
		private readonly List<Row<T>> _rows = new List<Row<T>>();

		/// <summary>Tables colection</summary>
		public Tables<T> Root { get; }

		/// <summary>All rows from current table</summary>
		public Row<T>[] Rows => this._rows.ToArray();

		/// <inheritdoc/>
		IEnumerable<IRow> ITable.Rows => this._rows.ToArray();

		/// <summary>Rows count in the table</summary>
		public UInt32 RowsCount => (UInt32)this._rows.Count;

		/// <summary>Columns from current table</summary>
		public Column<T>[] Columns { get; }

		/// <inheritdoc/>
		IColumn[] ITable.Columns => this.Columns;

		/// <summary>Type of current table</summary>
		public T Type { get; }

		/// <inheritdoc/>
		Object ITable.Type => this.Type;

		/// <summary>Gets specified row by index and check that row from current table</summary>
		/// <param name="rowIndex">Row index</param>
		/// <returns>Row by index from current table</returns>
		/// <exception cref="ArgumentException">Row not found in this table</exception>
		public Row<T> this[UInt32 rowIndex]
		{
			get
			{
				foreach(Row<T> row in this._rows)
					if(row.Index == rowIndex)
						return row;

				throw new ArgumentException($"Row with index {rowIndex} not found", nameof(rowIndex));
			}
		}

		/// <inheritdoc/>
		IRow ITable.this[UInt32 rowIndex] => this[rowIndex];

		/// <summary>Create instance of the tables class with variables columns and variable table type</summary>
		/// <param name="root">Tables collection</param>
		/// <param name="type">Table type</param>
		/// <param name="columns">Table columns collection</param>
		/// <exception cref="ArgumentNullException">Tables collection is null</exception>
		/// <exception cref="ArgumentNullException">Table type is null</exception>
		/// <exception cref="ArgumentNullException">Columns collection is null or empty</exception>
		public Table(Tables<T> root, T type, Column<T>[] columns)
		{
			if(type == null)
				throw new ArgumentNullException(nameof(type));
			if(columns == null || columns.Length == 0)
				throw new ArgumentNullException(nameof(columns));

			this.Root = root ?? throw new ArgumentNullException(nameof(root));
			this.Type = type;
			this.Columns = columns;
		}

		/// <summary>Adds row to current table and adds reference to the perforated rows collection of the parent tables collection class</summary>
		/// <param name="row">row to add</param>
		/// <returns>Transparent row index for reference</returns>
		protected UInt32 AddRow(Row<T> row)
		{
			_ = row ?? throw new ArgumentNullException(nameof(row));

			UInt32 rowIndex = this.Root.AddRow(row);
			row.Index = rowIndex;
			this._rows.Add(row);
			return rowIndex;
		}

		/// <summary>Adds row to current table and adds reference to the perforated rows collection of the parent tables collection class</summary>
		/// <param name="rowIndex">Transparent row index</param>
		/// <param name="row">Row to add</param>
		protected void AddRow(UInt32 rowIndex, Row<T> row)
		{
			_ = row ?? throw new ArgumentNullException(nameof(row));

			this.Root.AddRow(rowIndex, row);
			this._rows.Add(row);
		}

		/// <summary>Reference string representation</summary>
		/// <returns>String</returns>
		public override String ToString()
			=> $"{this.GetType().Name}: {{{this.Type}}}";
	}
}