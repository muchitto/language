namespace TypeInformation;

public class TypeRef(Scope scope, TypeInfo typeInfo)
{
    private static int _id;

    public TypeInfo TypeInfo { get; set; } = typeInfo;

    public int TypeId { get; init; } = _id++;
    public Scope Scope { get; init; } = scope;

    public bool IsUnknown => TypeInfo is UnknownTypeInfo;

    public static TypeRef Unknown(Scope? scope)
    {
        return new TypeRef(scope, new UnknownTypeInfo());
    }

    public static TypeRef Void(Scope? scope)
    {
        return new TypeRef(scope, new VoidTypeInfo());
    }

    public static TypeRef Dynamic(Scope? scope)
    {
        return new TypeRef(scope, new DynamicTypeInfo());
    }

    public static TypeRef Bool(Scope? scope)
    {
        return new TypeRef(scope, new BoolTypeInfo());
    }

    public static TypeRef Int(Scope? scope)
    {
        return new TypeRef(scope, new IntTypeInfo(32));
    }

    public static TypeRef Float(Scope? scope)
    {
        return new TypeRef(scope, new FloatTypeInfo(64));
    }
}