using Syntax.NodeHandlers.Declarations.Enum;
using Syntax.NodeHandlers.Declarations.Function;
using Syntax.NodeHandlers.Declarations.Function.Closure;
using Syntax.NodeHandlers.Declarations.Struct;

namespace Syntax.NodeHandlers.Declarations;

public interface ISymbolDeclarationNodeHandler : IFunctionDeclarationNodeHandler, IStructDeclarationNodeHandler,
    IEnumDeclarationNodeHandler, IClosureDeclarationNodeHandler;