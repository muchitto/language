using Lexing;
using Parsing.Parsers.Base;
using Syntax.Nodes;
using Syntax.Nodes.Expression;

namespace Parsing.Parsers;

public class ExpressionParser(ParsingContext context) : Parser<BaseNode>(context)
{
    public override BaseNode Parse()
    {
        return ParseExpression(0);
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
            TokenType.Identifier => new IdentifierRelatedParser(Context).Parse(new IdentifierRelatedParserData
            {
                IsExpression = true
            }),
            _ => throw new ParseError.UnexpectedToken(
                token,
                "expected an expression"
            )
        };
    }

    private BaseNode ParseExpressionContinue(BaseNode lhs, int minPrecedence)
    {
        var nextToken = PeekToken();

        if (!nextToken.IsOperator())
        {
            return lhs;
        }

        var op = nextToken.ToOperator();
        var precedence = op.Precedence();

        if (precedence <= minPrecedence)
        {
            return lhs;
        }

        GetNextToken();

        var rhs = ParseExpression(precedence);

        return new BinaryOpNode(
            lhs.PositionData,
            lhs,
            op,
            rhs
        );
    }

    private BaseNode ParseExpression(int minPrecedence)
    {
        var lhs = ParseExpressionPrimary();

        if (!IsNext(TokenType.Operator))
        {
            return lhs;
        }

        while (true)
        {
            var node = ParseExpressionContinue(lhs, minPrecedence);

            if (node == lhs)
            {
                break;
            }

            lhs = node;
        }

        return lhs;
    }
}