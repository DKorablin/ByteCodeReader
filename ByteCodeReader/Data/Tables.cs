using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace AlphaOmega.Debug
{
	/// <summary>Generic tables collection</summary>
	/// <typeparam name="T">Tables type</typeparam>
	public class Tables<T> : IEnumerable<Table<T>>, ITables
	{
		#region Fields
		private readonly ClassFile _file;

		private readonly Dictionary<T, Table<T>> _tables = new Dictionary<T, Table<T>>();
		private Int32 _rowIndex = 0;
		private readonly Dictionary<UInt32, Row<T>> _rows = new Dictionary<UInt32, Row<T>>();
		#endregion Fields

		/// <summary>Gets the table by the table type value</summary>
		/// <param name="type">Table type value</param>
		/// <returns>table with structures by table type</returns>
		public Table<T> this[T type]
		{
			get
			{
				Table<T> result;
				return this._tables.TryGetValue(type, out result)
					? result
					: null;
			}
		}

		ITable ITables.this[Object type] { get { return this[(T)type]; } }

		/// <summary>Total tables count (must be fixed size)</summary>
		public UInt32 Count { get { return (UInt32)this._tables.Count; } }

		/// <summary>Parent class file</summary>
		internal ClassFile File { get { return this._file; } }

		/// <summary>Create instance of the tables collection</summary>
		/// <param name="file">Parent class file</param>
		/// <exception cref="ArgumentNullException">file can't be null</exception>
		public Tables(ClassFile file)
		{
			this._file = file ?? throw new ArgumentNullException(nameof(file));
			this._tables = new Dictionary<T, Table<T>>();
		}

		/// <summary>Add generic table to the tables array</summary>
		/// <param name="type">Type of the table</param>
		/// <param name="table">Instance of the generic table</param>
		/// <exception cref="ArgumentNullException">type is null</exception>
		/// <exception cref="ArgumentNullException">table is null</exception>
		protected void AddTable(T type, Table<T> table)
		{
			if(type == null)
				throw new ArgumentNullException(nameof(type));
			_ = table ?? throw new ArgumentNullException(nameof(table));

			this._tables.Add(type, table);
		}

		/// <summary>Adds row to the transparent row collection</summary>
		/// <param name="row">Row</param>
		/// <returns>Transparent row index</returns>
		public virtual UInt32 AddRow(Row<T> row)
		{
			_ = row ?? throw new ArgumentNullException(nameof(row));

			UInt32 index = (UInt32)Interlocked.Increment(ref this._rowIndex);
			this.AddRow(index, row);
			return index;
		}

		/// <summary>Adds row to the collection with transaprent row index</summary>
		/// <param name="rowIndex">Transparent row index</param>
		/// <param name="row">Row</param>
		/// <exception cref="ArgumentNullException">row is null</exception>
		public virtual void AddRow(UInt32 rowIndex, Row<T> row)
		{
			_ = row ?? throw new ArgumentNullException(nameof(row));

			this._rows.Add(rowIndex, row);
		}

		/// <summary>Gets row by the transparent index</summary>
		/// <param name="rowIndex">Row index</param>
		/// <returns>Found row or exception</returns>
		public Row<T> GetRowByIndex(UInt32 rowIndex)
		{
			return this._rows[rowIndex];
		}

		IRow ITables.GetRowByIndex(UInt32 rowIndex)
		{
			return this.GetRowByIndex(rowIndex);
		}

		/// <summary>Gets all tables in table collection</summary>
		/// <returns>Created tables</returns>
		public IEnumerator<Table<T>> GetEnumerator()
		{
			foreach(Table<T> table in this._tables.Values)
				yield return table;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		IEnumerator<ITable> IEnumerable<ITable>.GetEnumerator()
		{
			foreach(Table<T> table in this._tables.Values)
				yield return table;
		}
	}
}