using ErrorReporting;
using Lexing;
using Syntax.Nodes;
using Syntax.Nodes.Type;
using Syntax.Nodes.Type.Function;
using Syntax.Nodes.Type.Struct;
using Syntax.Nodes.Type.Tuple;

namespace Parsing.Parsers.Type;

public class TypeAnnotationDataParser(ParsingContext context) : Parser<TypeNode>(context)
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

        return new StructTypeNode(PeekToken().PositionData, fields);
    }

    private IdentifierTypeNode ParseTypeIdentifier()
    {
        Expect(TokenType.Identifier, "expected type identifier");

        var token = GetNextToken();

        return new IdentifierTypeNode(token.PositionData, token.Value);
    }

    private TupleTypeNode ParseTupleType()
    {
        var types = ParseArgumentsWithOptionalNames()
            .Select(x => new TupleTypeFieldNode(
                x.PositionData,
                x.Name?.Name,
                x.TypeName
            )).ToList();

        return new TupleTypeNode(PeekToken().PositionData, types);
    }

    private List<ArgumentWithOptionalName> ParseArgumentsWithOptionalNames()
    {
        ExpectAndEat(TokenType.Symbol, "(", null);

        var arguments = new List<ArgumentWithOptionalName>();

        while (!IsNext(TokenType.Symbol, ")"))
        {
            var typeName = ParseTypeAnnotation();
            IdentifierNode? name = null;
            var posData = typeName.PositionData;
            if (!IsNext(TokenType.Symbol, ")") && !IsNext(TokenType.Symbol, ","))
            {
                name = (IdentifierNode)typeName;
                typeName = ParseTypeAnnotation();
                posData = name.PositionData;
            }

            arguments.Add(new ArgumentWithOptionalName
            {
                PositionData = posData,
                Name = name,
                TypeName = typeName
            });

            if (!IsNextAndEat(TokenType.Symbol, ","))
            {
                break;
            }
        }

        ExpectAndEat(TokenType.Symbol, ")", "expected an ending parenthesis for the arguments");

        return arguments;
    }


    private TypeNode ParseFunctionType()
    {
        ExpectAndEat(TokenType.Identifier, "func", "expected func");
        var arguments = ParseArgumentsWithOptionalNames()
            .Select(a => new FunctionTypeArgumentNode(
                a.Name,
                a.TypeName
            )).ToList();

        var returnType = ParseTypeAnnotation();

        return new FunctionTypeNode(PeekToken().PositionData, arguments, returnType);
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

    public override TypeNode Parse()
    {
        return ParseTypeAnnotation();
    }

    public struct ArgumentWithOptionalName
    {
        public PositionData PositionData { get; set; }
        public IdentifierNode? Name { get; set; }
        public TypeNode TypeName { get; set; }
    }
}