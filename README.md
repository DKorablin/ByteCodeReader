Byte Code reader
========

Java Virtual Machine bytecode reader

Usage:
<pre>
using(ClassFile cls = new ClassFile(StreamLoader.FromFile(@"C:\Test\CString.class")))
{
	if(cls.IsValid)
	{
		foreach(var item in info.fields)
			//...
		foreach(var item in info.methods)
			//...
		foreach(var item in info.interfaces)
			//...

		//...
	}
}
</pre>

Suppordes tructures:
<ul>
	<li><i>Header</i> &mdash; Each class file contains the definition of a single class or interface.</li>
	<li><i>constant_pool</i> &mdash; The constant_pool is a table of structures (§4.4) representing various string constants, class and interface names, field names, and other constants that are referred to within the ClassFile structure and its substructures.</li>
	<li><i>this_class</i> &mdash; The value of the this_class item must be a valid index into the constant_pool table.</li>
	<li><i>super_class</i> &mdash; For a class, the value of the super_class item either must be zero or must be a valid index into the constant_pool table.</li>
	<li><i>interfaces</i> &mdash; Each value in the interfaces array must be a valid index into the constant_pool table.</li>
	<li><i>fields</i> &mdash; Each value in the fields table must be a field_info (§4.5) structure giving a complete description of a field in this class or interface.</li>
	<li><i>methods</i> &mdash; Each value in the methods table must be a method_info (§4.6) structure giving a complete description of a method in this class or interface.</li>
	<li><i>attributes</i> &mdash; Each value of the attributes table must be an attribute_info (§4.7) structure</li>
	<li><i>attribute_pool</i> &mdash; All attributes from all structures</li>
</ul>