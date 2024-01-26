using Syntax.Nodes.Declaration;

namespace Parsing.Parsers.Base;

public abstract class BaseDeclarationParser(ParsingContext context) : BaseParser(context)
{
    public DeclarationNameNode ParseDeclarationName()
    {
        var identifier = ParseSingleIdentifier();

        return new DeclarationNameNode(identifier);
    }
}