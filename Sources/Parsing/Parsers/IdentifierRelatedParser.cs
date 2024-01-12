using Lexing;
using Syntax.Nodes;
using Syntax.Nodes.Statement;

namespace Parsing.Parsers;

public record struct IdentifierRelatedParserData
{
    public bool IsExpression;
}

public class IdentifierRelatedParser(ParsingContext context)
    : ParserWithData<BaseNode, IdentifierRelatedParserData>(context)
{
    public override BaseNode Parse(IdentifierRelatedParserData data)
    {
        var identifier = ParseIdentifierOrFieldAccess(data);

        if (IsNextPossibleFunctionCall(data))
        {
            return new FunctionCallParser(Context).Parse(new FunctionCallParserData
            {
                Name = identifier
            });
        }

        if (IsNextAndEat(TokenType.Symbol, "="))
        {
            var value = new ExpressionParser(Context).Parse();

            return new AssignmentNode(identifier, value);
        }

        if (IsNext(TokenType.Symbol, "["))
        {
            return new ArrayAccessParser(Context).Parse(new ArrayAccessParserData
            {
                Left = identifier
            });
        }

        return identifier;
    }

    private BaseNode ParseIdentifierOrFieldAccess(IdentifierRelatedParserData data)
    {
        var identifier = ParseSingleIdentifier();

        if (!IsNextAndEat(TokenType.Symbol, "."))
        {
            return identifier;
        }

        var subField = Parse(data);

        return new FieldAccessNode(
            identifier,
            subField
        );
    }

    private bool IsNextPossibleFunctionCall(IdentifierRelatedParserData data)
    {
        return !data.IsExpression
               && (IsNext(TokenType.Symbol, "(")
                   || IsNext(TokenType.Identifier)
                   || IsNext(TokenType.StringLiteral)
                   || IsNext(TokenType.NumberLiteral)
                   || IsNext(TokenType.Newline)
                   || IsNext(TokenType.Symbol, "{")
                   || IsEnd);
    }
}