using Lexing;
using Syntax.Nodes;
using Syntax.Nodes.Type;
using Syntax.Nodes.Type.Struct;
using Syntax.Nodes.Type.Tuple;

namespace Parsing;

public partial class Parser
{
    private StructTypeNode ParseStructType()
    {
        ExpectAndEat(TokenType.Symbol, "{", "expected an opening curly brace for the struct type");

        var fields = new List<StructTypeFieldNode>();

        while (!IsNextAndEat(TokenType.Symbol, "}"))
        {
            var name = ParseSingleIdentifier();

            ExpectAndEat(TokenType.Symbol, ":", "expected a colon for the struct type");

            var type = ParseTypeAnnotation();

            fields.Add(new StructTypeFieldNode(name.PositionData, name.Name, type));

            if (!IsNextAndEat(TokenType.Symbol, ","))
            {
                break;
            }
        }

        return new StructTypeNode(Lexer.PeekToken().PositionData, fields);
    }

    private IdentifierTypeNode ParseTypeIdentifier()
    {
        Expect(TokenType.Identifier, "expected type identifier");

        var token = Lexer.GetNextToken();

        return new IdentifierTypeNode(token.PositionData, token.Value);
    }

    private TupleTypeNode ParseTupleType()
    {
        var types = ParseArgumentsWithOptionalNames()
            .Select(x =>
            {
                return new TupleTypeFieldNode(
                    x.PosData,
                    x.Name?.Name,
                    x.TypeName
                );
            }).ToList();

        return new TupleTypeNode(Lexer.PeekToken().PositionData, types);
    }

    private TypeNode ParseTypeAnnotation()
    {
        if (IsNext(TokenType.Identifier, "func"))
        {
            return ParseFunctionType();
        }

        if (IsNext(TokenType.Symbol, "{"))
        {
            return ParseStructType();
        }

        if (IsNext(TokenType.Symbol, "("))
        {
            return ParseTupleType();
        }

        ExpectIdentifier("expected type annotation");

        return ParseTypeIdentifier();
    }
}