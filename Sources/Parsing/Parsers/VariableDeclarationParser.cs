using Lexing;
using Syntax.Nodes;
using Syntax.Nodes.Declaration;

namespace Parsing.Parsers;

public class VariableDeclarationParser(ParsingContext context) : Parser<VariableDeclarationNode>(context)
{
    public override VariableDeclarationNode Parse()
    {
        var token = PeekToken();
        var isLet = token.Is(TokenType.Identifier, "let");

        GetNextToken();

        var isDynamic = false;

        if (PeekToken().Is(TokenType.Symbol, "?"))
        {
            GetNextToken();

            if (isLet)
            {
                throw new ParseError.UnexpectedToken(
                    token,
                    "cannot use dynamic variables with a let statement"
                );
            }

            isDynamic = true;
        }

        var name = ParseSingleIdentifier();
        var typeNode = IsNext(TokenType.Identifier) ? new TypeAnnotationParser(Context).Parse() : null;

        BaseNode? value = null;
        if (IsNextAndEat(TokenType.Symbol, "="))
        {
            value = new ExpressionParser(Context).Parse();
        }

        if (isLet && value == null)
        {
            throw new ParseError(
                name.PositionData,
                "expected value for let statement"
            );
        }

        return new VariableDeclarationNode(
            name,
            value,
            isLet,
            typeNode,
            isDynamic
        );
    }
}