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

public class FunctionTypeInfo(TypeInfo? returnType, Dictionary<string, TypeInfo> parameterTypes, bool canThrow)
    : TypeInfo
{
    public TypeInfo? ReturnType { get; } = returnType;
    public Dictionary<string, TypeInfo> ParameterTypes { get; } = parameterTypes;

    public bool CanThrow { get; set; } = canThrow;
}

public class StructTypeInfo(Dictionary<string, TypeInfo> fields) : TypeInfo
{
    public Dictionary<string, TypeInfo> Fields { get; } = fields;
}