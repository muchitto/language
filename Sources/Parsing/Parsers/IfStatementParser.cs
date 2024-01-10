using Lexing;
using Syntax.Nodes;
using Syntax.Nodes.Expression;
using Syntax.Nodes.Literal;
using Syntax.Nodes.Statement;

namespace Parsing.Parsers;

public class IfStatementParser(ParsingContext context) : Parser<IfStatementNode>(context)
{
    public override IfStatementNode Parse()
    {
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

        var body = ParseIfBody();

        IfStatementNode? nextIf = null;

        if (IsNextAndEat(TokenType.Identifier, "else"))
        {
            if (IsNext(TokenType.Identifier, "if"))
            {
                nextIf = Parse();
            }
            else
            {
                var elseBody = ParseIfBody();

                nextIf = new IfStatementNode(token.PositionData, null, elseBody);
            }
        }
        else
        {
            ExpectAndEat(TokenType.Identifier, "end", "expected an end");
        }

        return new IfStatementNode(token.PositionData, condition as BinaryOpNode, body, nextIf);
    }

    private BodyContainerNode ParseIfBody()
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

            var statement = new StatementParser(Context).Parse();

            ExpectNot(TokenType.Symbol, ";", "expected a newline instead of a semicolon as semicolons are not needed");

            ExpectAndEatNewline();

            statements.Add(statement);
        }

        return new BodyContainerNode(token.PositionData, statements, false);
    }
}