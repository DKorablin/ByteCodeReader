using System;

namespace AlphaOmega.Debug.Data
{
	/// <summary>Basic row for the strongly typed generic row</summary>
	/// <typeparam name="T">Type of the owner table</typeparam>
	public class BaseRow<T> : IBaseRow
	{
		private Row<T> _row;

		/// <summary>Generic row</summary>
		internal Row<T> Row
		{
			get => this._row;
			set => this._row = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>Transparent row id</summary>
		public UInt32 Id => this.Row.Index;

		/// <summary>Generic interface row</summary>
		IRow IBaseRow.Row => this.Row;

		/// <summary>Shortcut to the tables root</summary>
		protected Tables<T> Root => this._row.Table.Root;

		/// <summary>Get the value from the column of the row by value</summary>
		/// <typeparam name="V">Column Value type</typeparam>
		/// <param name="columnIndex">Column index from metadata table</param>
		/// <returns>Column value from metadata table</returns>
		protected V GetValue<V>(UInt16 columnIndex)
			=> (V)this.Row[columnIndex].Value;
	}
}