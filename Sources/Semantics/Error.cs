namespace Semantics;

public class SemanticError : Exception
{
    public SemanticError(string message) : base(message)
    {
    }
}