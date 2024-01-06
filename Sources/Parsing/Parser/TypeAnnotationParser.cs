using Lexing;
using Parsing.Nodes;
using Parsing.Nodes.Type;
using Parsing.Nodes.Type.Struct;
using Parsing.Nodes.Type.Tuple;

namespace Parsing.Parser;

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

            fields.Add(new StructTypeFieldNode(name.PosData, name.Name, type));

            if (!IsNextAndEat(TokenType.Symbol, ","))
            {
                break;
            }
        }

        return new StructTypeNode(Lexer.PeekToken().PosData, fields);
    }

    private IdentifierTypeNode ParseTypeIdentifier()
    {
        Expect(TokenType.Identifier, "expected type identifier");

        var token = Lexer.GetNextToken();

        return new IdentifierTypeNode(token.PosData, token.Value);
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

        return new TupleTypeNode(Lexer.PeekToken().PosData, types);
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