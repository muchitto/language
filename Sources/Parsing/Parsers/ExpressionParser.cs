using Lexing;
using Syntax.Nodes;
using Syntax.Nodes.Expression;

namespace Parsing.Parsers;

public class ExpressionParser(ParsingContext context) : Parser<BaseNode>(context)
{
    public override BaseNode Parse()
    {
        var lhs = ParseExpressionPrimary();

        return ParseExpression(lhs, 0);
    }

    private BaseNode ParseExpressionPrimary()
    {
        var token = PeekToken();

        return token.Type switch
        {
            TokenType.StringLiteral or TokenType.NumberLiteral => new BasicLiteralParser(Context).Parse(),
            TokenType.Symbol when token.Value == "{" => new StructLiteralParser(Context).Parse(),
            TokenType.Identifier when token.Value == "if" => new IfExpressionParser(Context).Parse(),
            TokenType.Identifier when token.Value == "do" => new ClosureParser(Context).Parse(new ClosureParserData
            {
                IsExpr = true
            }),
            TokenType.Identifier => new IdentifierRelatedParser(Context).Parse(),
            _ => throw new ParseError.UnexpectedToken(
                token,
                "expected expression"
            )
        };
    }

    private BaseNode ParseExpression(BaseNode lhs, int minPrecedence)
    {
        var nextToken = PeekToken();

        while (nextToken.IsOperator() && nextToken.ToOperator() is var nextOp)
        {
            var nextPrecedence = nextOp.Precedence();

            if (nextPrecedence < minPrecedence)
            {
                break;
            }

            GetNextToken();

            var rhs = ParseExpressionPrimary();

            nextToken = PeekToken();

            while (nextToken.ToOperator() is { } peekNextOp)
            {
                var peekNextPrecedence = peekNextOp.Precedence();
                var peekNextAssoc = peekNextOp.Associativity();

                if (peekNextPrecedence > nextPrecedence ||
                    (peekNextPrecedence == nextPrecedence && peekNextAssoc == Associativity.Right))
                {
                    rhs = ParseExpression(rhs, peekNextPrecedence);
                    nextToken = PeekToken();
                }
                else
                {
                    break;
                }
            }

            var posData = lhs.PositionData;

            lhs = new BinaryOpNode(posData, lhs, rhs, nextOp);
            nextToken = PeekToken();
        }

        return lhs;
    }
}