## Byte Code Reader
[![Auto build](https://github.com/DKorablin/ByteCodeReader/actions/workflows/release.yml/badge.svg)](https://github.com/DKorablin/ByteCodeReader/releases/latest)
[![NuGet](https://img.shields.io/nuget/v/AlphaOmega.ByteCodeReader)](https://www.nuget.org/packages/AlphaOmega.ByteCodeReader)

A managed .NET library for parsing Java Virtual Machine (JVM) class (\*.class) files. It provides strongly typed access to the full constant pool, fields, methods, interfaces and all attribute blocks defined in the Java Class File Format (JVM Spec §4).

### Target frameworks
- .NET Framework 2.0
- .NET Standard 2.0 (compatible with .NET 8 and earlier)

### Why use it?
- Zero external native dependencies
- Lazy constant pool parsing (constants are materialized as needed)
- Full fidelity access to raw attribute data
- Works with very old and modern class file versions

---
## Install
Package manager:

    Install-Package AlphaOmega.ByteCodeReader

dotnet CLI:

    dotnet add package AlphaOmega.ByteCodeReader

---
## Quick Start
```csharp
using System;
using AlphaOmega.Debug;

using(ClassFile cls = new ClassFile(StreamLoader.FromFile(@"C:\Test\MyClass.class")))
{
    if(!cls.IsValid)
        throw new InvalidOperationException("Invalid JVM class file");

    Console.WriteLine($"Class: {cls.ThisClass.Bytes}");
    Console.WriteLine($"Super: {cls.SuperClass?.Bytes ?? "<Object>"}");
    Console.WriteLine($"Version: {cls.MajorVersion}.{cls.MinorVersion}");

    // Fields
    foreach(var f in cls.Fields)
        Console.WriteLine($"Field: {f.Name.Bytes} {f.Descriptor.Bytes}");

    // Methods
    foreach(var m in cls.Methods)
        Console.WriteLine($"Method: {m.Name.Bytes}{m.Descriptor.Bytes}");

    // Constant Pool (example: list all Utf8 entries)
    foreach(var utf8 in cls.ConstantPool.Utf8)
        Console.WriteLine($"Utf8[{utf8.Index}]: {utf8.Bytes}");
}
```

---
## Core Concepts
- ClassFile: Root parser for one JVM class / interface file.
- ConstantTables: Provides typed access to each constant pool table (Class, Fieldref, Methodref, InterfaceMethodref, String, Integer, Float, Long, Double, NameAndType, Utf8, MethodHandle, MethodType, InvokeDynamic).
- AttributeTables / AttributePool: Unified access to attributes from class, fields, and methods.
- Field_Info / MethodInfo: High-level wrappers exposing metadata and per-member attributes.

---
## Supported Structures
The reader currently handles:
- Header (magic, versioning)
- ConstantPool (all spec tags including invokedynamic)
- ThisClass / SuperClass
- Interfaces
- Fields (field_info)
- Methods (method_info)
- Attributes (attribute_info) at class, field, and method level
- Aggregated AttributePool (all attributes from all structures)

Common attribute types recognized (JVM Spec §4.7):
- InnerClasses
- EnclosingMethod
- Synthetic
- Signature
- SourceFile
- SourceDebugExtension
- Deprecated
- RuntimeVisibleAnnotations
- RuntimeInvisibleAnnotations
- BootstrapMethods

Unrecognized attributes are preserved (spec requires silent ignore) so tooling can still inspect raw data.

---
## Accessing Raw Binary Data
You can access raw constant pool bytes or attribute blocks for advanced analysis or re-emission.
```csharp
Byte[] rawConstants = cls.ConstantPool.GetData();
```

---
## Error Handling
- Invalid magic number throws ArgumentException.
- Use ClassFile.IsValid for a quick check after constructing.

---
## Performance Notes
- Constant pool rows are wrapped in lightweight objects; repeated access reuses instances.
- Long / Double constants advance index per JVM spec (extra unusable slot handled internally).

---
## Limitations / Roadmap
- Does not modify or write class files (read-only).
- StackMapTable, Module, Record attributes (newer JVM versions) may need extension.
- Bytecode instruction stream decoding (Code attribute body) can be added.

Planned:
- Detailed Code attribute parser (instructions, exception table, line numbers)
- CLI tool for quick inspection
- Optional rewrite / emit API

---
## Versioning
Class file version exposed via ClassFile.Version, MajorVersion, MinorVersion (e.g. 61.0 for Java 17).

---
## Security
Only parses local byte arrays / streams. No code execution or dynamic loading.

---
## Related Specifications
- JVM Class File Format (Java SE 8+): https://docs.oracle.com/javase/specs/jvms/se8/html/jvms-4.html

---
Feedback and improvements welcome.