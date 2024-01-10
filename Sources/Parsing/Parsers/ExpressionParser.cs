using Lexing;
using Parsing.Parsers.Conditional;
using Parsing.Parsers.Literals;
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
            TokenType.Identifier when token.Value == "do" => new DoBlockParser(Context).Parse(new DoBlockParserData
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
            var nextPrec = nextOp.Precedence();

            if (nextPrec < minPrecedence)
            {
                break;
            }

            GetNextToken();

            var rhs = ParseExpressionPrimary();

            nextToken = PeekToken();

            while (nextToken.ToOperator() is { } peekNextOp)
            {
                var peekNextPrec = peekNextOp.Precedence();
                var peekNextAssoc = peekNextOp.Associativity();

                if (peekNextPrec > nextPrec || (peekNextPrec == nextPrec && peekNextAssoc == Associativity.Right))
                {
                    rhs = ParseExpression(rhs, peekNextPrec);
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