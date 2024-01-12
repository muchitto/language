using Lexing;
using Syntax.Nodes;
using Syntax.Nodes.Declaration.Struct;
using Syntax.Nodes.Type;

namespace Parsing.Parsers;

public class StructDeclarationParser(ParsingContext context) : Parser<StructDeclarationNode>(context)
{
    public override StructDeclarationNode Parse()
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
        var token = PeekToken();

        switch (token.Type)
        {
            case TokenType.Identifier when token.Value is "var" or "let":
            {
                var variableDeclaration = new VariableDeclarationParser(Context).Parse();

                var name = variableDeclaration.Name;

                return new StructVariableNode(name, variableDeclaration);
            }
            case TokenType.Identifier when token.Value == "func":
            {
                var functionDeclaration = new FunctionDeclarationParser(Context).Parse(new FunctionDeclarationParserData
                {
                    IsMethod = true
                });

                if (functionDeclaration.Name == null)
                {
                    throw new ParseError.UnexpectedToken(
                        token,
                        "expected function name"
                    );
                }

                var name = functionDeclaration.Name;

                return new StructFunctionNode(name, functionDeclaration);
            }
            default:
                throw new ParseError.UnexpectedToken(
                    token,
                    "expected struct field (var, let, func)"
                );
        }
    }
}