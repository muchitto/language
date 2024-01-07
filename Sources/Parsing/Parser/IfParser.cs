using Lexing;
using Syntax.Nodes;
using Syntax.Nodes.Expression;
using Syntax.Nodes.Literal;
using Syntax.Nodes.Statement;

namespace Parsing.Parser;

public partial class Parser
{
    private IfStatementNode ParseIfStatement()
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
                nextIf = ParseIfStatement();
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
        var token = Lexer.PeekToken();
        var statements = new List<BaseNode>();

        while (!IsNextAndEat(TokenType.Identifier, "end") && !IsNext(TokenType.Identifier, "else"))
        {
            var nextToken = Lexer.PeekToken();

            if (IsEnd)
            {
                throw new ParseError.ExpectedToken(
                    new Token(TokenType.Identifier, nextToken.PositionData, "end"),
                    nextToken,
                    "expected 'end' and not end of file"
                );
            }

            var statement = ParseStatement();

            ExpectNot(TokenType.Symbol, ";", "expected a newline instead of a semicolon as semicolons are not needed");

            ExpectAndEatNewline();

            statements.Add(statement);
        }

        return new BodyContainerNode(token.PositionData, statements, false);
    }

    private IfExpressionNode ParseIfExpression()
    {
        throw new NotImplementedException();

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
                nextIf = ParseIfExpression();
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
        var token = Lexer.PeekToken();
        var statements = new List<BaseNode>();

        while (!IsNextAndEat(TokenType.Identifier, "end") && !IsNext(TokenType.Identifier, "else"))
        {
            var nextToken = Lexer.PeekToken();

            if (IsEnd)
            {
                throw new ParseError.ExpectedToken(
                    new Token(TokenType.Identifier, nextToken.PositionData, "end"),
                    nextToken,
                    "expected 'end' and not end of file"
                );
            }

            var statement = ParseExpression();

            ExpectNot(TokenType.Symbol, ";", "expected a newline instead of a semicolon as semicolons are not needed");

            ExpectAndEatNewline();

            statements.Add(statement);
        }

        return new BodyExpressionNode(token.PositionData, statements);
    }
}