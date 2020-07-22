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

		/// <summary>Таблица в метаданных</summary>
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
			if(table == null)
				throw new ArgumentNullException("table");

			this._table = table;
		}

		/// <summary>Получить в итерации список всех рядов в таблице метаданных</summary>
		/// <returns>Ряд метаданных детально описывающий структуру таблицы</returns>
		public IEnumerator<E> GetEnumerator()
		{
			foreach(var row in this.Table.Rows)
				yield return new E() { Row = row, };
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Table friendly description</summary>
		/// <returns>String</returns>
		public override String ToString()
		{
			return String.Format("{0}: {{{1}}}", this.GetType().Name, this.Table.Type);
		}
	}
}