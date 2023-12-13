using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>Fields, methods, and interface methods are represented by similar structures</summary>
	public class MethodrefRow : BaseRow<Jvm.CONSTANT>
	{
		private UInt16 ClassIndexI => base.GetValue<UInt16>(0);

		/// <summary>The class_index item of a <see cref="Jvm.CONSTANT_Methodref_info"/> structure must be a class type, not an interface type</summary>
		public ConstantReference ClassIndex => new ConstantReference(base.Root, Jvm.CONSTANT.Class, this.ClassIndexI);

		private UInt16 NameAndTypeIndexI => base.GetValue<UInt16>(1);

		/// <summary>If the name of the method of a <see cref="Jvm.CONSTANT_Methodref_info"/> structure begins with a '&lt;' ('\u003c'), then the name must be the special name &lt;init&gt;, representing an instance initialization method (§2.9)</summary>
		/// <remarks>The return type of such a method must be void</remarks>
		public ConstantReference NameAndTypeIndex => new ConstantReference(base.Root, Jvm.CONSTANT.NameAndType, this.NameAndTypeIndexI);
	}
}