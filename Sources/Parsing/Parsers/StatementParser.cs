using Lexing;
using Syntax.Nodes;
using Syntax.Nodes.Statement;

namespace Parsing.Parsers;

public class StatementParser(ParsingContext context) : Parser<BaseNode>(context)
{
    public override BaseNode Parse()
    {
        var token = PeekToken();

        switch (token.Type)
        {
            case TokenType.Symbol when token.Value == "@":
                var annotations = new AnnotationsParser(Context).Parse();

                Optional(TokenType.Newline);

                var nextNode = Parse();

                nextNode.Annotations = annotations;

                return nextNode;
            case TokenType.Identifier when token.Value == "func":
                return new FunctionDeclarationParser(Context).Parse(new FunctionDeclarationParserData
                {
                    IsMethod = false
                });
            case TokenType.Identifier when token.Value is "let" or "var":
                return new VariableDeclarationParser(Context).Parse();
            case TokenType.Identifier when token.Value == "struct":
                return new StructDeclarationParser(Context).Parse();
            case TokenType.Identifier when token.Value == "if":
                return new IfStatementParser(Context).Parse();
            case TokenType.Identifier when token.Value == "enum":
                return new EnumDeclarationParser(Context).Parse();
            case TokenType.Identifier when token.Value == "return":
                return ParseReturn();
            case TokenType.Identifier when token.Value == "interface":
                return new InterfaceDeclarationParser(Context).Parse();
            case TokenType.Identifier when token.Value == "do":
                return new ClosureParser(Context).Parse(new ClosureParserData
                {
                    IsExpr = false
                });
            case TokenType.Identifier when token.Value == "type":
                return new TypeAliasParser(Context).Parse();
            case TokenType.Identifier:
                var identifier = new IdentifierRelatedParser(Context).Parse(new IdentifierRelatedParserData
                {
                    IsExpression = false
                });

                if (identifier is IdentifierNode)
                {
                    throw new ParseError.UnexpectedToken(
                        token,
                        "expected statement, got identifier"
                    );
                }

                return identifier;
            default:
                throw new ParseError.UnexpectedToken(
                    token,
                    "expected statement"
                );
        }
    }

    private ReturnNode ParseReturn()
    {
        ExpectAndEat(TokenType.Identifier, "return", "expected return");

        BaseNode? value = null;

        if (!IsNext(TokenType.Newline))
        {
            value = new ExpressionParser(Context).Parse();
        }

        return new ReturnNode(PeekToken().PositionData, value);
    }
}