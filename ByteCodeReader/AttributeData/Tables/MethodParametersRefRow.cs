﻿using System;
using AlphaOmega.Debug.Data;
using AlphaOmega.Debug.ConstantData;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>Method parameters description</summary>
	public class MethodParametersRefRow : BaseRow<String>
	{
		/// <summary>Method parameter access flag</summary>
		[Flags]
		public enum ACC : UInt16
		{
			/// <summary> Indicates that the formal parameter was declared final.</summary>
			FINAL = 0x0010,
			/// <summary>Indicates that the formal parameter was not explicitly or implicitly declared in source code, according to the specification of the language in which the source code was written (JLS §13.1)</summary>
			/// <remarks>The formal parameter is an implementation artifact of the compiler which produced this class file</remarks>
			SYNTHETIC = 0x1000,
			/// <summary>Indicates that the formal parameter was implicitly declared in source code, according to the specification of the language in which the source code was written (JLS §13.1)</summary>
			/// <remarks>The formal parameter is mandated by a language specification, so all compilers for the language must emit it</remarks>
			MANDATED = 0x8000,
		}

		private UInt16 NameIndexI => base.GetValue<UInt16>(0);

		/// <summary>The value of the name_index item must either be zero or a valid index into the <see cref="ClassFile.ConstantPool"/> table</summary>
		/// <remarks>
		/// If the value of the name_index item is zero, then this parameters element indicates a formal parameter with no name.
		/// If the value of the name_index item is nonzero, the <see cref="ClassFile.ConstantPool"/> entry at that index must be a <see cref="Jvm.CONSTANT_Utf8_info"/> structure representing a valid unqualified name denoting a formal parameter (§4.2.2).
		/// </remarks>
		public ConstantReference NameIndex
		{
			get
			{
				UInt16 idRef = this.NameIndexI;

				return idRef == 0
					? null
					: new ConstantReference(base.Root.File.ConstantPool, Jvm.CONSTANT.Utf8, idRef);
			}
		}

		/// <summary>Method parameter access flag</summary>
		public ACC AccessFlags => base.GetValue<ACC>(1);
	}
}