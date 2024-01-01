using Lexing;
using Parsing.Nodes;

namespace Parsing.Parser;

public partial class Parser
{
    private IfStatementNode ParseIf(bool isExpr)
    {
        var token = Lexer.PeekToken();

        ExpectAndEat(TokenType.Identifier, "if", "expected if");

        var condition = ParseExpression();

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
                condition.PosData,
                condition,
                new BooleanLiteralNode(condition.PosData, true),
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
                nextIf = ParseIf(isExpr);
            }
            else
            {
                var elseBody = ParseIfBody();

                nextIf = new IfStatementNode(token.PosData, null, elseBody);
            }
        }
        else
        {
            ExpectAndEat(TokenType.Identifier, "end", "expected an end");
        }

        return new IfStatementNode(token.PosData, condition as BinaryOpNode, body, nextIf);
    }

    private BodyContainerNode ParseIfBody()
    {
        var token = Lexer.PeekToken();
        var statements = new List<BaseNode>();

        while (!IsNextAndEat(TokenType.Identifier, "end") && !IsNext(TokenType.Identifier, "else"))
        {
            var nextToken = Lexer.PeekToken();

            if (IsEnd)
            {
                throw new ParseError.ExpectedToken(
                    new Token(TokenType.Identifier, nextToken.PosData, "end"),
                    nextToken,
                    "expected 'end' and not end of file"
                );
            }

            var statement = ParseStatement();

            ExpectNot(TokenType.Symbol, ";", "expected a newline instead of a semicolon as semicolons are not needed");

            ExpectAndEatNewline();

            statements.Add(statement);
        }

        return new BodyContainerNode(token.PosData, statements, false);
    }
}