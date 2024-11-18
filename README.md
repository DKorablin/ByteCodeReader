## Byte Code reader
[![Auto build](https://github.com/DKorablin/ByteCodeReader/actions/workflows/release.yml/badge.svg)](https://github.com/DKorablin/ByteCodeReader/releases/latest)
[![Nuget](https://img.shields.io/nuget/v/AlphaOmega.ByteCodeReader)](https://www.nuget.org/packages/AlphaOmega.ByteCodeReader)

Java Virtual Machine bytecode reader

Usage:

    using(ClassFile cls = new ClassFile(StreamLoader.FromFile(@"C:\Test\CString.class")))
    {
        if(cls.IsValid)
        {
            foreach(var item in info.Fields)
                //...
            foreach(var item in info.Methods)
                //...
            foreach(var item in info.Interfaces)
                //...

            //...
        }
    }

Supported structures:

- _Header_ &mdash; Each class file contains the definition of a single class or interface.
- _ConstantPool_ &mdash; The constant_pool is a table of structures (§4.4) representing various string constants, class and interface names, field names, and other constants that are referred to within the ClassFile structure and its substructures.
- _ThisClass_ &mdash; The value of the this_class item must be a valid index into the constant_pool table.
- _SuperClass_ &mdash; For a class, the value of the super_class item either must be zero or must be a valid index into the constant_pool table.
- _Interfaces_ &mdash; Each value in the interfaces array must be a valid index into the constant_pool table.
- _Fields_ &mdash; Each value in the fields table must be a field_info (§4.5) structure giving a complete description of a field in this class or interface.
- _Methods_ &mdash; Each value in the methods table must be a method_info (§4.6) structure giving a complete description of a method in this class or interface.
- _Attributes_ &mdash; Each value of the attributes table must be an attribute_info (§4.7) structure
- _AttributePool_ &mdash; All attributes from all structures
