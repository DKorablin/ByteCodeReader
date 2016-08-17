using System;

namespace AlphaOmega.Debug
{
	/// <summary>Reference to the generic table</summary>
	public interface ICellPointer
	{
		/// <summary>Reference type</summary>
		Object TableType { get; }

		/// <summary>Reference index</summary>
		UInt32 Index { get; }

		/// <summary>Gets the generic row reference</summary>
		/// <returns>Reference row</returns>
		IRow GetReference();
	}
}