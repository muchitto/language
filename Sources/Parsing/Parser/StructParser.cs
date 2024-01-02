using Lexing;
using Parsing.Nodes;

namespace Parsing.Parser;

public partial class Parser
{
    private StructDeclarationNode ParseStructDeclaration()
    {
        ExpectAndEat(TokenType.Identifier, "struct", "expected a struct identifier");

        var name = ParseSingleIdentifier();

        IdentifierNode? parent = null;

        if (IsNextAndEat(TokenType.Identifier, "extends"))
        {
            parent = ParseSingleIdentifier();
        }

        List<IdentifierTypeNode> interfaces = [];
        var implOnly = false;

        if (IsNextAndEat(TokenType.Identifier, "impl"))
        {
            implOnly = IsNextAndEat(TokenType.Identifier, "only");

            while (!IsNext(TokenType.Newline))
            {
                var impl = (IdentifierTypeNode)ParseSingleIdentifier();

                interfaces.Add(impl);
            }
        }

        ExpectAndEatNewline();

        var fields = new List<StructFieldNode>();

        while (!IsNextAndEat(TokenType.Identifier, "end"))
        {
            var field = ParseStructField();

            ExpectAndEatNewline();

            fields.Add(field);
        }

        return new StructDeclarationNode(name, fields, parent, interfaces, implOnly);
    }

    private StructFieldNode ParseStructField()
    {
        var token = Lexer.PeekToken();

        switch (token.Type)
        {
            case TokenType.Identifier when token.Value is "var" or "let":
            {
                var variableDeclaration = ParseVariableDeclaration();

                var name = variableDeclaration.Name.Name;

                return new StructVariableNode(token.PosData, name, variableDeclaration);
            }
            case TokenType.Identifier when token.Value == "func":
            {
                var functionDeclaration = ParseFunctionDeclaration();

                if (functionDeclaration.Name == null)
                {
                    throw new ParseError.UnexpectedToken(
                        token,
                        "expected function name"
                    );
                }

                var name = functionDeclaration.Name?.Name ?? "";

                return new StructFunctionNode(token.PosData, name, functionDeclaration);
            }
            default:
                throw new ParseError.UnexpectedToken(
                    token,
                    "expected struct field (var, let, func)"
                );
        }
    }
}