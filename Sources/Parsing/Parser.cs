using Lexing;
using Parsing.Nodes;

namespace Parsing;

public class ParserError(PosData posData, string message) : Exception(message)
{
    public PosData PosData { get; set; } = posData;

    public class UnexpectedToken(Token token, string message) : ParserError(token.PosData, message)
    {
        public Token Token { get; set; } = token;
    }

    public class ExpectedToken(Token expected, Token got, string message) : ParserError(got.PosData, message)
    {
        public Token Expected { get; set; } = expected;
        public Token Got { get; set; } = got;
    }
}

public class Parser
{
    public Parser(Lexer lexer)
    {
        Lexer = lexer;
    }

    private Lexer Lexer { get; }

    private bool IsEnd => Lexer.IsEnd || Lexer.PeekToken().Is(TokenType.EndOfFile);

    public static ProgramContainerNode Parse(string filename, string source)
    {
        var posData = new PosData(filename, source);
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

        throw new ParserError.ExpectedToken(
            new Token(token, peekToken.PosData),
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

        throw new ParserError.ExpectedToken(
            new Token(token, peekToken.PosData, value),
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

        throw new ParserError.UnexpectedToken(peekToken, innerMessage);
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
            throw new ParserError.ExpectedToken(
                new Token(TokenType.Newline, peekToken.PosData),
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

        throw new ParserError.UnexpectedToken(
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

        throw new ParserError.UnexpectedToken(
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

        return new ProgramContainerNode(token.PosData, body);
    }

    private TypeAliasDeclarationNode ParseTypeAlias()
    {
        ExpectAndEat(TokenType.Identifier, "type", "expected type");

        var name = ParseSingleIdentifier(false);

        ExpectAndEat(TokenType.Symbol, "=", "expected an equals sign for the type alias");

        var type = ParseTypeAnnotation();

        return new TypeAliasDeclarationNode(name, type);
    }

    private AnnotationNode ParseAnnotation()
    {
        ExpectAndEat(TokenType.Symbol, "@", "expected @");

        throw new NotImplementedException();

        var name = ParseSingleIdentifier(false);

        ExpectAndEat(TokenType.Symbol, "(", "expected an opening parenthesis for the annotation");

        var annotationArguments = new List<AnnotationArgumentNode>();
        while (!IsNext(TokenType.Symbol, ")"))
        {
            var token = Lexer.PeekToken();

            if (IsEnd)
            {
                throw new ParserError.ExpectedToken(
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
    }

    private BaseNode ParseStatement()
    {
        var token = Lexer.PeekToken();

        switch (token.Type)
        {
            case TokenType.Symbol when token.Value == "@":
                return ParseAnnotation();
            case TokenType.Identifier when token.Value == "func":
                return ParseFunctionDeclaration();
            case TokenType.Identifier when token.Value is "let" or "var":
                return ParseVariableDeclaration();
            case TokenType.Identifier when token.Value == "struct":
                return ParseStructDeclaration();
            case TokenType.Identifier when token.Value == "if":
                return ParseIf(false);
            case TokenType.Identifier when token.Value == "enum":
                return ParseEnum(false);
            case TokenType.Identifier when token.Value == "return":
                return ParseReturn();
            case TokenType.Identifier when token.Value == "do":
                return ParseDoBlock(false);
            case TokenType.Identifier when token.Value == "type":
                return ParseTypeAlias();
            case TokenType.Identifier:
                var identifier = ParseIdentifier();

                if (identifier is IdentifierNode)
                {
                    throw new ParserError.UnexpectedToken(
                        token,
                        "expected statement, got identifier"
                    );
                }

                return identifier;
            default:
                throw new ParserError.UnexpectedToken(
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
                throw new ParserError.UnexpectedToken(
                    token,
                    "cannot use dynamic variables with a let statement"
                );
            }

            isDynamic = true;
        }

        var name = ParseSingleIdentifier(false);
        var typeName = GetIdentifierIfNext(false);

        BaseNode? value = null;
        if (IsNextAndEat(TokenType.Symbol, "="))
        {
            value = ParseExpressionPrimary();
        }

        if (!isLet && typeName == null && value == null)
        {
            throw new ParserError.UnexpectedToken(
                token,
                "the variable declaration does not have a typename or an initial value where the type could be inferred from"
            );
        }

        if (isLet && value == null)
        {
            throw new ParserError.UnexpectedToken(
                token,
                "expected value for let statement"
            );
        }

        return new VariableDeclarationNode(
            name,
            value,
            isLet,
            typeName,
            isDynamic
        );
    }

    private StructDeclarationNode ParseStructDeclaration()
    {
        ExpectAndEat(TokenType.Identifier, "struct", "expected a struct identifier");

        var name = ParseSingleIdentifier(false);

        IdentifierNode? parent = null;

        if (IsNextAndEat(TokenType.Identifier, "extends"))
        {
            parent = ParseSingleIdentifier(false);
        }

        List<IdentifierNode> interfaces = [];
        var implOnly = false;

        if (IsNextAndEat(TokenType.Identifier, "impl"))
        {
            implOnly = IsNextAndEat(TokenType.Identifier, "only");

            while (!IsNext(TokenType.Newline))
            {
                var impl = ParseSingleIdentifier(false);

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
                    throw new ParserError.UnexpectedToken(
                        token,
                        "expected function name"
                    );
                }

                var name = functionDeclaration.Name?.Name ?? "";

                return new StructFunctionNode(token.PosData, name, functionDeclaration);
            }
            default:
                throw new ParserError.UnexpectedToken(
                    token,
                    "expected struct field (var, let, func)"
                );
        }
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

        return new BodyContainerNode(token.PosData, statements, isExpr);
    }

    private EnumNode ParseEnum(bool isExpr)
    {
        var startToken = Lexer.PeekToken();

        ExpectAndEat(TokenType.Identifier, "enum", "expected enum");

        var name = ParseSingleIdentifier(false);

        ExpectAndEatNewline();

        var cases = new List<EnumCaseNode>();
        var funcs = new List<EnumFunctionNode>();

        while (!IsNextAndEat(TokenType.Identifier, "end"))
        {
            var token = Lexer.PeekToken();
            if (IsNext(TokenType.Identifier, "func"))
            {
                funcs.Add(new EnumFunctionNode(token.PosData, ParseFunctionDeclaration()));

                ExpectAndEatNewline();
            }
            else if (IsNext(TokenType.Identifier))
            {
                var identifier = ParseSingleIdentifier(false);

                if (IsNextAndEat(TokenType.Newline))
                {
                    cases.Add(new EnumCaseNode(identifier.PosData, identifier, []));
                }
                else
                {
                    ExpectAndEat(TokenType.Symbol, "(", null);

                    var associatedValues = new List<EnumCaseAssociatedValueNode>();

                    while (!IsNextAndEat(TokenType.Symbol, ")"))
                    {
                        var identifierOrType = ParseSingleIdentifier(false);
                        var type = GetIdentifierIfNext(false);

                        if (type != null)
                        {
                            associatedValues.Add(
                                new EnumCaseAssociatedValueNode(
                                    identifierOrType.PosData,
                                    identifierOrType,
                                    type
                                )
                            );
                        }
                        else
                        {
                            associatedValues.Add(
                                new EnumCaseAssociatedValueNode(
                                    identifierOrType.PosData,
                                    null,
                                    identifierOrType
                                )
                            );
                        }
                    }

                    ExpectAndEatNewline();

                    cases.Add(new EnumCaseNode(identifier.PosData, identifier, associatedValues));
                }
            }
            else
            {
                throw new ParserError.UnexpectedToken(
                    token,
                    "expected enum case (func, identifier)"
                );
            }
        }

        return new EnumNode(name, cases);
    }

    private FunctionDeclarationNode ParseFunctionDeclaration()
    {
        ExpectAndEat(TokenType.Identifier, "func", "expected func");

        var name = ParseSingleIdentifier(false);

        var argumentStartToken = Lexer.PeekToken();

        ExpectAndEat(TokenType.Symbol, "(", null);

        var arguments = new List<FunctionArgumentNode>();

        while (!IsNext(TokenType.Symbol, ")"))
        {
            var identifier = ParseSingleIdentifier(false);

            TypeNode? type = null;
            var isDynamic = IsNext(TokenType.Symbol, "?");

            if (!IsNextAndEat(TokenType.Symbol, "?"))
            {
                type = ParseTypeAnnotation();
            }

            BaseNode? defaultValue = null;

            if (IsNextAndEat(TokenType.Symbol, "="))
            {
                defaultValue = ParseExpressionPrimary();
            }

            arguments.Add(new FunctionArgumentNode(identifier.PosData, identifier, type, defaultValue, isDynamic));

            if (!IsNextAndEat(TokenType.Symbol, ","))
            {
                break;
            }
        }

        ExpectAndEat(TokenType.Symbol, ")", "expected an ending parenthesis for the arguments");

        var canThrow = IsNextAndEat(TokenType.Identifier, "throws");

        var returnType = GetIdentifierIfNext(false);

        ExpectAndEatNewline();

        var body = ParseBody(true);

        return new FunctionDeclarationNode(
            name,
            new FunctionArgumentListNode(argumentStartToken.PosData, arguments),
            body,
            canThrow,
            returnType
        );
    }

    private IdentifierNode? GetIdentifierIfNext(bool acceptSubField)
    {
        return IsNext(TokenType.Identifier) ? ParseSingleIdentifier(acceptSubField) : null;
    }


    private BaseNode ParseIdentifier()
    {
        var identifier = ParseSingleIdentifier(true);

        if (IsNext(TokenType.Symbol, "("))
        {
            return ParseFunctionCall(identifier);
        }

        if (IsNextAndEat(TokenType.Symbol, "="))
        {
            var value = ParseExpressionPrimary();

            return new AssignmentNode(identifier, value);
        }

        return identifier;
    }

    private IdentifierNode ParseSingleIdentifier(bool acceptSubField)
    {
        Expect(TokenType.Identifier, "expected identifier");

        var name = Lexer.GetNextToken();
        var namePos = name.PosData;

        if (!acceptSubField || !IsNext(TokenType.Symbol, "."))
        {
            return new IdentifierNode(namePos, name.Value);
        }

        Lexer.GetNextToken();

        var subField = ParseSingleIdentifier(acceptSubField);

        return new IdentifierNode(namePos, name.Value, subField);
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
            TokenType.Identifier when token.Value == "if" => ParseIf(true),
            TokenType.Identifier when token.Value == "do" => ParseDoBlock(true),
            TokenType.Identifier => ParseIdentifier(),
            TokenType.Symbol when token.Value == "{" => ParseStructLiteral(),
            _ => throw new ParserError.UnexpectedToken(
                token,
                "expected expression"
            )
        };
    }

    private IfStatementNode ParseIf(bool isExpr)
    {
        var token = Lexer.PeekToken();

        ExpectAndEat(TokenType.Identifier, "if", "expected if");

        var condition = ParseExpression();

        if (condition is not ExpressionNode)
        {
            if (condition is not IdentifierNode or BooleanLiteralNode)
            {
                throw new ParserError.UnexpectedToken(
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
                throw new ParserError.ExpectedToken(
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

    private StringLiteralNode ParseStringLiteral()
    {
        Expect(TokenType.StringLiteral, "expected string literal");

        var token = Lexer.GetNextToken();

        return new StringLiteralNode(token.PosData, token.Value);
    }

    private NumberLiteralNode ParseNumberLiteral()
    {
        Expect(TokenType.NumberLiteral, "expected number literal");

        var token = Lexer.GetNextToken();

        return new NumberLiteralNode(token.PosData, token.Value);
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
                throw new ParserError(
                    field.PosData,
                    $"field {name} already defined in struct literal"
                );
            }

            if (!IsNext(TokenType.Newline))
            {
                everyFieldHasNewline = false;

                if (!usesCommas.Value)
                {
                    throw new ParserError(
                        field.PosData,
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

        return new StructLiteralNode(Lexer.PeekToken().PosData, fields);
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

    private StructTypeNode ParseStructType()
    {
        ExpectAndEat(TokenType.Symbol, "{", "expected an opening curly brace for the struct type");

        var fields = new List<StructTypeFieldNode>();

        while (!IsNextAndEat(TokenType.Symbol, "}"))
        {
            var name = ParseSingleIdentifier(false);

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
                    x.Item1,
                    x.Item2?.Name,
                    x.Item3
                );
            }).ToList();

        return new TupleTypeNode(Lexer.PeekToken().PosData, types);
    }

    private List<(PosData, IdentifierNode?, TypeNode)> ParseArgumentsWithOptionalNames()
    {
        ExpectAndEat(TokenType.Symbol, "(", null);

        var arguments = new List<(PosData, IdentifierNode?, TypeNode)>();

        while (!IsNext(TokenType.Symbol, ")"))
        {
            var typeName = ParseTypeAnnotation();
            IdentifierNode? name = null;
            var posData = typeName.PosData;

            if (!IsNext(TokenType.Symbol, ")") && !IsNext(TokenType.Symbol, ","))
            {
                name = (IdentifierNode)typeName;
                typeName = ParseTypeAnnotation();
                posData = name.PosData;
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
                    a.Item1,
                    a.Item2?.Name,
                    a.Item3
                );
            }).ToList();

        var returnType = ParseTypeAnnotation();

        return new FunctionTypeNode(Lexer.PeekToken().PosData, arguments, returnType);
    }

    private ReturnNode ParseReturn()
    {
        ExpectAndEat(TokenType.Identifier, "return", "expected return");

        BaseNode? value = null;

        if (!IsNext(TokenType.Newline))
        {
            value = ParseExpression();
        }

        return new ReturnNode(Lexer.PeekToken().PosData, value);
    }

    private StructLiteralFieldNode ParseStructLiteralField()
    {
        var name = ParseSingleIdentifier(false);

        ExpectAndEat(
            TokenType.Symbol,
            "=",
            "expected an equals sign for the struct literal field"
        );

        var value = ParseExpressionPrimary();

        return new StructLiteralFieldNode(name.PosData, name, value);
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

            var posData = lhs.PosData;

            lhs = new BinaryOpNode(posData, lhs, rhs, nextOp);
            nextToken = Lexer.PeekToken();
        }

        return lhs;
    }

    private FunctionCallNode ParseFunctionCall(IdentifierNode name)
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
                    throw new ParserError(
                        argumentValue.PosData,
                        "argument name needs to be an identifier"
                    );
                }
            }

            arguments.Add(new FunctionCallArgumentNode(argumentValue.PosData, argumentName, argumentValue));

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

        return new FunctionCallNode(name.PosData, name, arguments);
    }
}