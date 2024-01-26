using Lexing;
using Parsing.Parsers.Base;
using Syntax.Nodes;
using Syntax.Nodes.Expression;
using Syntax.Nodes.Literal;

namespace Parsing.Parsers;

public class IfExpressionParser(ParsingContext context) : Parser<IfExpressionNode>(context)
{
    public override IfExpressionNode Parse()
    {
        throw new NotImplementedException();

        var token = PeekToken();

        ExpectAndEat(TokenType.Identifier, "if", "expected if");

        var condition = new ExpressionParser(Context).Parse();

        if (condition is not ExpressionNode)
        {
            if (condition is not IdentifierNode or BooleanLiteralNode)
            {
                throw new ParseError.UnexpectedToken(
                    token,
                    "expected expression"
                );
            }

            condition = new BinaryOpNode(
                condition.PositionData,
                condition,
                new BooleanLiteralNode(condition.PositionData, true),
                Operator.Equal
            );
        }

        ExpectAndEatNewline();

        var body = ParseIfBodyExpression();

        IfExpressionNode? nextIf = null;

        if (IsNextAndEat(TokenType.Identifier, "else"))
        {
            if (IsNext(TokenType.Identifier, "if"))
            {
                nextIf = Parse();
            }
            else
            {
                var elseBody = ParseIfBodyExpression();

                nextIf = new IfExpressionNode(token.PositionData, null, elseBody, null);
            }
        }
        else
        {
            ExpectAndEat(TokenType.Identifier, "end", "expected an end");
        }

        return new IfExpressionNode(token.PositionData, condition as BinaryOpNode, body, nextIf);
    }

    private BodyExpressionNode ParseIfBodyExpression()
    {
        var token = PeekToken();
        var statements = new List<BaseNode>();

        while (!IsNextAndEat(TokenType.Identifier, "end") && !IsNext(TokenType.Identifier, "else"))
        {
            var nextToken = PeekToken();

            if (IsEnd)
            {
                throw new ParseError.ExpectedToken(
                    new Token(TokenType.Identifier, nextToken.PositionData, "end"),
                    nextToken,
                    "expected 'end' and not end of file"
                );
            }

            var statement = new ExpressionParser(Context).Parse();

            ExpectNot(TokenType.Symbol, ";", "expected a newline instead of a semicolon as semicolons are not needed");

            ExpectAndEatNewline();

            statements.Add(statement);
        }

        return new BodyExpressionNode(token.PositionData, statements);
    }
}