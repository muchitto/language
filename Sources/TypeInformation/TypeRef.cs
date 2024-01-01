namespace TypeInformation;

public class TypeRef
{
    private static int _id;

    public TypeRef(TypeInfo typeInfo, Scope scope)
    {
        TypeInfo = typeInfo;
        Scope = scope;

        _id++;
    }

    public int TypeId { get; } = _id;

    public TypeInfo TypeInfo { get; set; }
    public Scope Scope { get; }
}