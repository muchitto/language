namespace TypeInformation;

public class TypeRef(Scope scope, TypeInfo typeInfo)
{
    private static int _id;

    public TypeInfo TypeInfo { get; set; } = typeInfo;

    public int TypeId { get; init; } = _id++;
    public Scope Scope { get; init; } = scope;

    public static TypeRef Unknown(Scope? scope = null)
    {
        return new TypeRef(scope, new UnknownTypeInfo());
    }
}