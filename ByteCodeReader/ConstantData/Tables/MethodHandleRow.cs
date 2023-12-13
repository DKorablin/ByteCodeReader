using System;
using AlphaOmega.Debug.Data;

namespace AlphaOmega.Debug.ConstantData
{
	/// <summary>The CONSTANT_MethodHandle_info structure is used to represent a method handle</summary>
	/// <remarks>https://docs.oracle.com/javase/specs/jvms/se7/html/jvms-4.html#jvms-4.4.8</remarks>
	public class MethodHandleRow : BaseRow<Jvm.CONSTANT>
	{
		/// <summary>The value denotes the kind of this method handle, which characterizes its bytecode behavior (§5.4.3.5)</summary>
		/// <remarks>The value of the reference_kind item must be in the range 1 to 9</remarks>
		public Jvm.REF ReferenceKind => base.GetValue<Jvm.REF>(0);

		/// <summary> The value of the reference_index item must be a valid index into the constant_pool table</summary>
		private UInt16 ReferenceIndexI => base.GetValue<UInt16>(1);

		/// <summary> The value of the reference_index item must be a valid index into the constant_pool table</summary>
		public ConstantReference ReferenceIndex
		{
			get
			{//TODO: Add field name check from docs
				Jvm.CONSTANT tag;
				switch(this.ReferenceKind)
				{
				case Jvm.REF.getField:
				case Jvm.REF.getStatic:
				case Jvm.REF.putField:
				case Jvm.REF.putStatic:
					tag = Jvm.CONSTANT.Fieldref;
					break;
				case Jvm.REF.invokeVirtual:
				case Jvm.REF.invokeStatic:
				case Jvm.REF.invokeSpecial:
				case Jvm.REF.newInvokeSpecial:
					tag = Jvm.CONSTANT.Methodref;
					break;
				case Jvm.REF.invokeInterface:
					tag = Jvm.CONSTANT.InterfaceMethodref;
					break;
				default:
					throw new NotSupportedException();
				}

				return new ConstantReference(base.Root, tag, this.ReferenceIndexI);
			}
		}
	}
}