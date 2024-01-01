namespace TypeInformation;

public abstract class TypeInfo;

public class UnknownTypeInfo : TypeInfo;

public class DynamicTypeInfo : TypeInfo;

public class NilTypeInfo : TypeInfo;

public class CharTypeInfo : TypeInfo;

public class VoidTypeInfo : TypeInfo;

public class BoolTypeInfo : TypeInfo;

public class StringTypeInfo : TypeInfo;

public class IntTypeInfo(int size) : TypeInfo
{
    public int Size { get; } = size;
}

public class FloatTypeInfo(int size) : TypeInfo
{
    public int Size { get; } = size;
}

public class FunctionTypeInfo(TypeRef? returnType, Dictionary<string, TypeRef> parameterTypes, bool canThrow)
    : TypeInfo
{
    public TypeRef? ReturnType { get; } = returnType;
    public Dictionary<string, TypeRef> ParameterTypes { get; } = parameterTypes;

    public bool CanThrow { get; set; } = canThrow;
}

public class StructTypeInfo(Dictionary<string, TypeRef> fields) : TypeInfo
{
    public Dictionary<string, TypeRef> Fields { get; } = fields;
}