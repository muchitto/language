using Lexing;
using Syntax.Nodes.Declaration;

namespace Parsing.Parsers;

public class TypeAliasParser(ParsingContext context) : Parser<TypeAliasDeclarationNode>(context)
{
    public override TypeAliasDeclarationNode Parse()
    {
        ExpectAndEat(TokenType.Identifier, "type", "expected type");

        var name = ParseSingleIdentifier();

        ExpectAndEat(TokenType.Symbol, "=", "expected an equals sign for the type alias");

        var type = new TypeAnnotationParser(Context).Parse();

        return new TypeAliasDeclarationNode(name, type);
    }
}