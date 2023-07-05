using System;
using System.Collections.Generic;

namespace AlphaOmega.Debug
{
	/// <summary>Generic structure container</summary>
	/// <typeparam name="T">Table type</typeparam>
	public class Table<T> : ITable
	{
		#region Fields
		private readonly T _type;
		private readonly Tables<T> _root;
		private readonly List<Row<T>> _rows;
		private readonly Column<T>[] _columns;
		#endregion Fields

		/// <summary>Tables colection</summary>
		public Tables<T> Root { get { return this._root; } }

		/// <summary>All rows from current table</summary>
		public Row<T>[] Rows { get { return this._rows.ToArray(); } }

        /// <inheritdoc/>
        IEnumerable<IRow> ITable.Rows { get { return this._rows.ToArray(); } }

		/// <summary>Rows count in the table</summary>
		public UInt32 RowsCount { get { return (UInt32)this._rows.Count; } }

		/// <summary>Columns from current table</summary>
		public Column<T>[] Columns { get { return this._columns; } }

        /// <inheritdoc/>
        IColumn[] ITable.Columns { get { return this._columns; } }

		/// <summary>Type of current table</summary>
		public T Type { get { return this._type; } }

        /// <inheritdoc/>
        Object ITable.Type { get { return this._type; } }

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

			this._root = root ?? throw new ArgumentNullException(nameof(root));
			this._type = type;
			this._rows = new List<Row<T>>();
			this._columns = columns;
		}

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
        IRow ITable.this[UInt32 rowIndex]
		{
			get { return this[rowIndex]; }
		}

		/// <summary>Adds row to current table and adds reference to the perforated rows collection of the parent tables collection class</summary>
		/// <param name="row">row to add</param>
		/// <returns>Transparent row index for reference</returns>
		protected UInt32 AddRow(Row<T> row)
		{
			_ = row ?? throw new ArgumentNullException(nameof(row));

			UInt32 rowIndex = this._root.AddRow(row);
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

			this._root.AddRow(rowIndex, row);
			this._rows.Add(row);
		}

		/// <summary>Reference string representation</summary>
		/// <returns>String</returns>
		public override String ToString()
		{
			return $"{this.GetType().Name}: {{{this.Type}}}";
		}
	}
}