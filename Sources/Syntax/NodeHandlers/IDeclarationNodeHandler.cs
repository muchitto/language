using Syntax.NodeHandlers.Declarations;
using Syntax.NodeHandlers.Declarations.Enum;
using Syntax.NodeHandlers.Declarations.Function;
using Syntax.NodeHandlers.Declarations.Function.Closure;
using Syntax.NodeHandlers.Declarations.Interface;
using Syntax.NodeHandlers.Declarations.Struct;

namespace Syntax.NodeHandlers;

public interface IDeclarationNodeHandler
    : IStructDeclarationNodeHandler, IInterfaceDeclarationNodeHandler,
        IEnumDeclarationNodeHandler, IClosureDeclarationNodeHandler,
        IVariableDeclarationHandler, ITypeAliasDeclaration, IFunctionNodeHandler, IDeclarationBasicNodeHandler;