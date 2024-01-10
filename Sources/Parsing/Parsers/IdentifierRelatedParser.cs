using Lexing;
using Syntax.Nodes;
using Syntax.Nodes.Statement;

namespace Parsing.Parsers;

public class IdentifierRelatedParser(ParsingContext context) : Parser<BaseNode>(context)
{
    public override BaseNode Parse()
    {
        var identifier = ParseIdentifierOrFieldAccess();

        if (IsNextPossibleFunctionCall())
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

    private BaseNode ParseIdentifierOrFieldAccess()
    {
        var identifier = ParseSingleIdentifier();

        if (!IsNextAndEat(TokenType.Symbol, "."))
        {
            return identifier;
        }

        var subField = Parse();

        return new FieldAccessNode(
            identifier,
            subField
        );
    }

    private bool IsNextPossibleFunctionCall()
    {
        return IsNext(TokenType.Symbol, "(")
               || IsNext(TokenType.Identifier)
               || IsNext(TokenType.StringLiteral)
               || IsNext(TokenType.NumberLiteral)
               || IsNext(TokenType.Newline)
               || IsNext(TokenType.Symbol, "{")
               || IsEnd;
    }
}