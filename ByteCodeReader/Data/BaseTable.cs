using System;
using System.Collections;
using System.Collections.Generic;

namespace AlphaOmega.Debug.Data
{
	/// <summary>Basic table for the strongly typed generic table</summary>
	/// <typeparam name="E">Strongly typed row description</typeparam>
	/// <typeparam name="T">Type of the owner generic table</typeparam>
	public class BaseTable<E, T> : IEnumerable<E>
		where E : BaseRow<T>, new()
	{
		#region Fields
		private readonly Table<T> _table;
        #endregion Fields

        /// <summary>Table in metadata</summary>
        public Table<T> Table { get { return this._table; } }

		/// <summary>Gets row by transparent row index</summary>
		/// <param name="rowIndex">Transparent row index</param>
		/// <returns>Strongly typed row by index</returns>
		public E this[UInt32 rowIndex]
		{
			get { return new E() { Row = this.Table[rowIndex], }; }
		}

		/// <summary>Create instance of the strongly typed base table</summary>
		/// <param name="table">Owner generic table</param>
		public BaseTable(Table<T> table)
		{
			this._table = table ?? throw new ArgumentNullException(nameof(table));
		}

        /// <summary>Get in iteration a list of all rows in a metadata table</summary>
        /// <returns>A set of metadata detailing the structure of a table</returns>
        public IEnumerator<E> GetEnumerator()
		{
			foreach(var row in this.Table.Rows)
				yield return new E() { Row = row, };
		}

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Table friendly description</summary>
		/// <returns>String</returns>
		public override String ToString()
		{
			return $"{this.GetType().Name}: {{{this.Table.Type}}}";
		}
	}
}