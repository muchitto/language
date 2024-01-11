namespace TypeInformation;

public class TypeRef(Scope scope, TypeInfo typeInfo)
{
    private static int _id;

    private static readonly TypeRef UnknownTypeInfo = new(null, new UnknownTypeInfo());
    private static readonly TypeRef VoidTypeInfo = new(null, new VoidTypeInfo());

    public TypeInfo TypeInfo { get; set; } = typeInfo;

    public int TypeId { get; init; } = _id++;
    public Scope Scope { get; init; } = scope;

    public bool IsUnknown => TypeInfo is UnknownTypeInfo;

    public static TypeRef Unknown(Scope? scope)
    {
        return new TypeRef(scope, new UnknownTypeInfo());
    }

    public static TypeRef Unknown()
    {
        return UnknownTypeInfo;
    }

    public static TypeRef Void()
    {
        return VoidTypeInfo;
    }
}