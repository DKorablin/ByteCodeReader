using System;
using AlphaOmega.Debug.Data;
using System.Collections.Generic;

namespace AlphaOmega.Debug.AttributeData
{
	/// <summary>
	/// The InnerClasses attribute is a variable-length attribute in the attributes table of a ClassFile structure (§4.1).
	/// If the constant pool of a class or interface C contains a CONSTANT_Class_info entry which represents a class or interface that is not a member of a package, then C's ClassFile structure must have exactly one InnerClasses attribute in its attributes table.
	/// </summary>
	public class InnerClassesRow : BaseRow<String>
	{
		private AttributeReference[] classesI { get { return base.GetValue<AttributeReference[]>(0); } }

		/// <summary>The value of the number_of_classes item indicates the number of entries in the classes array.</summary>
		public UInt16 number_of_classes { get { return (UInt16)this.classesI.Length; } }

		/// <summary>Every CONSTANT_Class_info entry in the constant_pool table which represents a class or interface C that is not a package member must have exactly one corresponding entry in the classes array.</summary>
		/// <remarks>
		/// If a class has members that are classes or interfaces, its constant_pool table (and hence its InnerClasses attribute) must refer to each such member, even if that member is not otherwise mentioned by the class.
		/// These rules imply that a nested class or interface member will have InnerClasses information for each enclosing class and for each immediate member.
		/// </remarks>
		public InnerClassesRefRow[] classes
		{
			get
			{
				AttributeReference[] references = this.classesI;
				BaseTable<InnerClassesRefRow, String> baseTable = base.Root.File.attribute_pool.InnerClassesRef;

				return Array.ConvertAll(references, delegate(AttributeReference item) { return baseTable[item.Index]; });
			}
		}
	}
}