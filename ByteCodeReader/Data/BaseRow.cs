using System;

namespace AlphaOmega.Debug.Data
{
	/// <summary>Basic row for the strongly typed generic row</summary>
	/// <typeparam name="T">Type of the owner table</typeparam>
	public class BaseRow<T> : IBaseRow
	{
		#region Fields
		private Row<T> _row;
		#endregion Fields

		/// <summary>Generic row</summary>
		internal Row<T> Row
		{
			get { return this._row; }
			set
			{
				if(value == null)
					throw new ArgumentNullException("value");
				this._row = value;
			}
		}

		/// <summary>Transparent row id</summary>
		public UInt32 Id { get { return this.Row.Index; } }

		/// <summary>Generic interface row</summary>
		IRow IBaseRow.Row { get { return this.Row; } }

		/// <summary>Shortcut to the tables root</summary>
		protected Tables<T> Root { get { return this._row.Table.Root; } }

		/// <summary>Получить значение из колонки ряда по значению</summary>
		/// <typeparam name="V">Column Value type</typeparam>
		/// <param name="columnIndex">Индекс колонки в таблице метаданных</param>
		/// <returns>Значение колонки в таблице метаданных</returns>
		protected V GetValue<V>(UInt16 columnIndex)
		{
			return (V)this.Row[columnIndex].Value;
		}
	}
}