using ErrorReporting;
using Lexing;
using Syntax.Nodes;
using Syntax.Nodes.Declaration;
using Syntax.Nodes.Declaration.Interface;
using Syntax.Nodes.Expression;
using Syntax.Nodes.Literal;
using Syntax.Nodes.Statement;
using Syntax.Nodes.Type.Function;

namespace Parsing.Parser;

public partial class Parser
{
    public Parser(Lexer lexer)
    {
        Lexer = lexer;
    }

    private Lexer Lexer { get; }

    private bool IsEnd => Lexer.IsEnd || Lexer.PeekToken().Is(TokenType.EndOfFile);

    public static ProgramContainerNode Parse(string filename, string source)
    {
        var posData = new PositionData(filename, source);
        var lexer = new Lexer(posData);
        var parser = new Parser(lexer);

        return parser.Parse();
    }

    private void Expect(TokenType token, string? message)
    {
        var peekToken = Lexer.PeekToken();

        if (peekToken.Is(token))
        {
            return;
        }

        var innerMessage = message ?? $"expected token {token}, got {peekToken}";

        throw new ParseError.ExpectedToken(
            new Token(token, peekToken.PositionData),
            peekToken,
            innerMessage
        );
    }

    private void Expect(TokenType token, string value, string? message)
    {
        var peekToken = Lexer.PeekToken();

        if (peekToken.Is(token, value))
        {
            return;
        }

        var innerMessage = message ?? $"expected token {token} with value {value}, got {peekToken}";

        throw new ParseError.ExpectedToken(
            new Token(token, peekToken.PositionData, value),
            peekToken,
            innerMessage
        );
    }

    private void ExpectIdentifier(string? message)
    {
        var peekToken = Lexer.PeekToken();

        if (peekToken.Is(TokenType.Identifier))
        {
            return;
        }

        var innerMessage = message ?? $"expected identifier, got {peekToken}";

        throw new ParseError.UnexpectedToken(peekToken, innerMessage);
    }

    private void ExpectAndEat(TokenType token, string? message)
    {
        Expect(token, message);

        Lexer.GetNextToken();
    }

    private void ExpectAndEat(TokenType token, string value, string? message)
    {
        Expect(token, value, message);

        Lexer.GetNextToken();
    }

    private void ExpectAndEatNewline()
    {
        var peekToken = Lexer.PeekToken();

        if (!peekToken.Is(TokenType.Newline))
        {
            throw new ParseError.ExpectedToken(
                new Token(TokenType.Newline, peekToken.PositionData),
                peekToken,
                "expected a newline");
        }

        Lexer.GetNextToken();
    }

    private void ExpectNot(TokenType token, string? message)
    {
        var peekToken = Lexer.PeekToken();

        if (!peekToken.Is(token))
        {
            return;
        }

        var innerMessage = message ?? $"expected token {token}, got {peekToken}";

        throw new ParseError.UnexpectedToken(
            peekToken,
            innerMessage
        );
    }

    private void ExpectNot(TokenType token, string value, string? message)
    {
        var peekToken = Lexer.PeekToken();

        if (!peekToken.Is(token, value))
        {
            return;
        }

        var innerMessage = message ?? $"expected token {token} with value {value}, got {peekToken}";

        throw new ParseError.UnexpectedToken(
            peekToken,
            innerMessage
        );
    }

    private bool IsNext(TokenType token)
    {
        return Lexer.PeekToken().Is(token);
    }

    private bool IsNext(TokenType token, string value)
    {
        return Lexer.PeekToken().Is(token, value);
    }

    private bool IsNextAndEat(TokenType token)
    {
        var peekToken = Lexer.PeekToken();

        if (!peekToken.Is(token))
        {
            return false;
        }

        Lexer.GetNextToken();

        return true;
    }

    private bool IsNextAndEat(TokenType token, string value)
    {
        var peekToken = Lexer.PeekToken();

        if (!peekToken.Is(token, value))
        {
            return false;
        }

        Lexer.GetNextToken();

        return true;
    }

    private void Optional(TokenType token)
    {
        if (Lexer.PeekToken().Is(token))
        {
            Lexer.GetNextToken();
        }
    }

    private IdentifierNode? GetIdentifierIfNext()
    {
        return IsNext(TokenType.Identifier) ? ParseSingleIdentifier() : null;
    }

    public ProgramContainerNode Parse()
    {
        var body = new List<BaseNode>();

        var token = Lexer.PeekToken();

        while (!IsEnd)
        {
            var statement = ParseStatement();

            body.Add(statement);

            if (IsNext(TokenType.EndOfFile))
            {
                break;
            }

            ExpectNot(TokenType.Symbol, ";", "expected a newline instead of a semicolon as semicolons are not needed");

            ExpectAndEatNewline();
        }

        return new ProgramContainerNode(token.PositionData, body);
    }

    private TypeAliasDeclarationNode ParseTypeAlias()
    {
        ExpectAndEat(TokenType.Identifier, "type", "expected type");

        var name = ParseSingleIdentifier();

        ExpectAndEat(TokenType.Symbol, "=", "expected an equals sign for the type alias");

        var type = ParseTypeAnnotation();

        return new TypeAliasDeclarationNode(name, type);
    }

    private AnnotationNode ParseAnnotation()
    {
        ExpectAndEat(TokenType.Symbol, "@", "expected @");

        throw new NotImplementedException();

        /*
        var name = ParseSingleIdentifier(false);

        ExpectAndEat(TokenType.Symbol, "(", "expected an opening parenthesis for the annotation");

        var annotationArguments = new List<AnnotationArgumentNode>();
        while (!IsNext(TokenType.Symbol, ")"))
        {
            var token = Lexer.PeekToken();

            if (IsEnd)
            {
                throw new ParseError.ExpectedToken(
                    new Token(TokenType.Symbol, token.PosData, ")"),
                    token,
                    "expected ')' and not end of file"
                );
            }

            var value = ParseExpressionPrimary();

            // annotationArguments.Add(new AnnotationArgumentNode(token.PosData, null, value));
        }

        ExpectAndEat(TokenType.Symbol, ")", "expected an ending parenthesis for the annotation");

        //return new AnnotationNode(name, annotationArguments, null);
        */
    }


    private InterfaceDeclarationNode ParseInterfaceDeclaration()
    {
        throw new NotImplementedException();

        /*
        ExpectAndEat(TokenType.Identifier, "interface", "expected interface");

        var name = ParseSingleIdentifier(false);

        ExpectAndEatNewline();

        var functions = new List<FunctionDeclarationNode>();

        while (!IsNextAndEat(TokenType.Identifier, "end"))
        {
            var function = ParseFunctionDeclaration();

            ExpectAndEatNewline();

            functions.Add(function);
        }

        //return new InterfaceDeclarationNode(name, functions, );
        */
    }

    private BaseNode ParseStatement()
    {
        var token = Lexer.PeekToken();

        switch (token.Type)
        {
            case TokenType.Symbol when token.Value == "@":
                return ParseAnnotation();
            case TokenType.Identifier when token.Value == "func":
                return ParseFunctionDeclaration(false);
            case TokenType.Identifier when token.Value is "let" or "var":
                return ParseVariableDeclaration();
            case TokenType.Identifier when token.Value == "struct":
                return ParseStructDeclaration();
            case TokenType.Identifier when token.Value == "if":
                return ParseIfStatement();
            case TokenType.Identifier when token.Value == "enum":
                return ParseEnum(false);
            case TokenType.Identifier when token.Value == "return":
                return ParseReturn();
            case TokenType.Identifier when token.Value == "interface":
                return ParseInterfaceDeclaration();
            case TokenType.Identifier when token.Value == "do":
                return ParseDoBlock(false);
            case TokenType.Identifier when token.Value == "type":
                return ParseTypeAlias();
            case TokenType.Identifier:
                var identifier = ParseIdentifier();

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

    private VariableDeclarationNode ParseVariableDeclaration()
    {
        var token = Lexer.PeekToken();
        var isLet = token.Is(TokenType.Identifier, "let");

        Lexer.GetNextToken();

        var isDynamic = false;

        if (Lexer.PeekToken().Is(TokenType.Symbol, "?"))
        {
            Lexer.GetNextToken();

            if (isLet)
            {
                throw new ParseError.UnexpectedToken(
                    token,
                    "cannot use dynamic variables with a let statement"
                );
            }

            isDynamic = true;
        }

        var name = ParseSingleIdentifier();
        var typeNode = IsNext(TokenType.Identifier) ? ParseTypeAnnotation() : null;

        BaseNode? value = null;
        if (IsNextAndEat(TokenType.Symbol, "="))
        {
            value = ParseExpressionPrimary();
        }

        if (!isLet && typeNode == null && value == null)
        {
            throw new ParseError(
                name.PositionData,
                "the variable declaration does not have a typename or an initial value where the type could be inferred from"
            );
        }

        if (isLet && value == null)
        {
            throw new ParseError(
                name.PositionData,
                "expected value for let statement"
            );
        }

        return new VariableDeclarationNode(
            name,
            value,
            isLet,
            typeNode,
            isDynamic
        );
    }

    private BodyContainerNode ParseBody(bool isExpr)
    {
        var token = Lexer.PeekToken();
        var statements = new List<BaseNode>();

        while (!IsNextAndEat(TokenType.Identifier, "end"))
        {
            var statement = ParseStatement();

            ExpectNot(TokenType.Symbol, ";", "expected a newline instead of a semicolon as semicolons are not needed");

            ExpectAndEatNewline();

            statements.Add(statement);
        }

        return new BodyContainerNode(token.PositionData, statements, isExpr);
    }


    private BaseNode ParseIdentifier()
    {
        var identifier = ParseIdentifierOrFieldAccess();

        if (IsNext(TokenType.Symbol, "("))
        {
            return ParseFunctionCall(identifier);
        }

        if (IsNextAndEat(TokenType.Symbol, "="))
        {
            var value = ParseExpressionPrimary();

            return new AssignmentNode(identifier, value);
        }

        if (IsNext(TokenType.Symbol, "["))
        {
            return ParseArrayAccess(identifier);
        }

        return identifier;
    }

    private ArrayAccessNode ParseArrayAccess(BaseNode left)
    {
        ExpectAndEat(TokenType.Symbol, "[", "expected an opening square bracket for the array access");

        var right = ParseExpression();

        ExpectAndEat(TokenType.Symbol, "]", "expected a closing square bracket for the array access");

        return new ArrayAccessNode(left, (ExpressionNode)right);
    }

    private IdentifierNode ParseSingleIdentifier()
    {
        Expect(TokenType.Identifier, "expected identifier");

        var name = Lexer.GetNextToken();
        var namePos = name.PositionData;

        return new IdentifierNode(namePos, name.Value);
    }

    private BaseNode ParseIdentifierOrFieldAccess()
    {
        var identifier = ParseSingleIdentifier();

        if (!IsNextAndEat(TokenType.Symbol, "."))
        {
            return identifier;
        }

        var subField = ParseIdentifier();

        return new FieldAccessNode(
            identifier,
            subField
        );
    }

    private BodyContainerNode ParseDoBlock(bool isExpr)
    {
        ExpectAndEat(TokenType.Identifier, "do", "expected do");

        var body = ParseBody(isExpr);

        ExpectAndEat(TokenType.Identifier, "end", "expected an end");

        return body;
    }

    private BaseNode ParseExpressionPrimary()
    {
        var token = Lexer.PeekToken();

        return token.Type switch
        {
            TokenType.StringLiteral => ParseStringLiteral(),
            TokenType.NumberLiteral => ParseNumberLiteral(),
            TokenType.Identifier when token.Value == "if" => ParseIfExpression(),
            TokenType.Identifier when token.Value == "do" => ParseDoBlock(true),
            TokenType.Identifier => ParseIdentifier(),
            TokenType.Symbol when token.Value == "{" => ParseStructLiteral(),
            _ => throw new ParseError.UnexpectedToken(
                token,
                "expected expression"
            )
        };
    }

    private StringLiteralNode ParseStringLiteral()
    {
        Expect(TokenType.StringLiteral, "expected string literal");

        var token = Lexer.GetNextToken();

        return new StringLiteralNode(token.PositionData, token.Value);
    }

    private NumberLiteralNode ParseNumberLiteral()
    {
        Expect(TokenType.NumberLiteral, "expected number literal");

        var token = Lexer.GetNextToken();

        return new NumberLiteralNode(token.PositionData, token.Value);
    }

    private StructLiteralNode ParseStructLiteral()
    {
        ExpectAndEat(
            TokenType.Symbol,
            "{",
            "expected an opening curly brace for the struct declaration"
        );

        var fields = new List<StructLiteralFieldNode>();

        var everyFieldHasNewline = IsNextAndEat(TokenType.Newline);
        bool? usesCommas = null;

        while (!IsNext(TokenType.Symbol, "}"))
        {
            var field = ParseStructLiteralField();

            if (usesCommas == null)
            {
                usesCommas = IsNextAndEat(TokenType.Symbol, ",");
            }
            else if (usesCommas.Value)
            {
                ExpectAndEat(
                    TokenType.Symbol,
                    ",",
                    "expected a comma"
                );
            }

            string? name = null;

            if (field is { Name: { } identifier })
            {
                name = identifier.Name;
            }

            if (fields.Any(f => f.Name.Name == name))
            {
                throw new ParseError(
                    field.PositionData,
                    $"field {name} already defined in struct literal"
                );
            }

            if (!IsNext(TokenType.Newline))
            {
                everyFieldHasNewline = false;

                if (!usesCommas.Value)
                {
                    throw new ParseError(
                        field.PositionData,
                        "if the struct literal is all on one line, it needs to use commas"
                    );
                }
            }

            if (everyFieldHasNewline)
            {
                ExpectAndEatNewline();
            }
        }

        ExpectAndEat(
            TokenType.Symbol,
            "}",
            "expected an ending curly brace for the struct declaration"
        );

        return new StructLiteralNode(Lexer.PeekToken().PositionData, fields);
    }

    private List<(PositionData PosData, IdentifierNode? Name, TypeNode TypeName)> ParseArgumentsWithOptionalNames()
    {
        ExpectAndEat(TokenType.Symbol, "(", null);

        var arguments = new List<(PositionData PosData, IdentifierNode? Name, TypeNode TypeName)>();

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

            arguments.Add((posData, name, typeName));

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
            .Select(a =>
            {
                return new FunctionTypeArgumentNode(
                    a.Name,
                    a.TypeName
                );
            }).ToList();

        var returnType = ParseTypeAnnotation();

        return new FunctionTypeNode(Lexer.PeekToken().PositionData, arguments, returnType);
    }

    private ReturnNode ParseReturn()
    {
        ExpectAndEat(TokenType.Identifier, "return", "expected return");

        BaseNode? value = null;

        if (!IsNext(TokenType.Newline))
        {
            value = ParseExpression();
        }

        return new ReturnNode(Lexer.PeekToken().PositionData, value);
    }

    private StructLiteralFieldNode ParseStructLiteralField()
    {
        var name = ParseSingleIdentifier();

        ExpectAndEat(
            TokenType.Symbol,
            "=",
            "expected an equals sign for the struct literal field"
        );

        var value = ParseExpressionPrimary();

        return new StructLiteralFieldNode(name.PositionData, name, value);
    }

    private BaseNode ParseExpression()
    {
        var lhs = ParseExpressionPrimary();

        return ParseExpression(lhs, 0);
    }

    private BaseNode ParseExpression(BaseNode lhs, int minPrecedence)
    {
        var nextToken = Lexer.PeekToken();

        while (nextToken.IsOperator() && nextToken.ToOperator() is var nextOp)
        {
            var nextPrec = nextOp.Precedence();

            if (nextPrec < minPrecedence)
            {
                break;
            }

            Lexer.GetNextToken();

            var rhs = ParseExpressionPrimary();

            nextToken = Lexer.PeekToken();

            while (nextToken.ToOperator() is { } peekNextOp)
            {
                var peekNextPrec = peekNextOp.Precedence();
                var peekNextAssoc = peekNextOp.Associativity();

                if (peekNextPrec > nextPrec || (peekNextPrec == nextPrec && peekNextAssoc == Associativity.Right))
                {
                    rhs = ParseExpression(rhs, peekNextPrec);
                    nextToken = Lexer.PeekToken();
                }
                else
                {
                    break;
                }
            }

            var posData = lhs.PositionData;

            lhs = new BinaryOpNode(posData, lhs, rhs, nextOp);
            nextToken = Lexer.PeekToken();
        }

        return lhs;
    }

    private FunctionCallNode ParseFunctionCall(BaseNode name)
    {
        var arguments = new List<FunctionCallArgumentNode>();

        ExpectAndEat(
            TokenType.Symbol,
            "(",
            "expected an opening parenthesis for the arguments"
        );

        while (!IsNext(TokenType.Symbol, ")"))
        {
            var argumentValue = ParseExpressionPrimary();
            IdentifierNode? argumentName = null;

            if (IsNextAndEat(TokenType.Symbol, ":"))
            {
                if (argumentValue is IdentifierNode argumentValueIdentifier)
                {
                    argumentName = argumentValueIdentifier;
                    argumentValue = ParseExpression();
                }
                else
                {
                    throw new ParseError(
                        argumentValue.PositionData,
                        "argument name needs to be an identifier"
                    );
                }
            }

            arguments.Add(new FunctionCallArgumentNode(argumentValue.PositionData, argumentName, argumentValue));

            if (!IsNextAndEat(TokenType.Symbol, ","))
            {
                break;
            }
        }

        ExpectAndEat(
            TokenType.Symbol,
            ")",
            "expected an ending parenthesis for the arguments"
        );

        return new FunctionCallNode(name, arguments);
    }
}