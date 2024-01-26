using Syntax.Nodes.Declaration;

namespace Parsing.Parsers.Base;

public abstract class DeclarationParser<T>(ParsingContext context)
    : BaseDeclarationParser(context) where T : DeclarationNode
{
    public abstract T Parse();
}