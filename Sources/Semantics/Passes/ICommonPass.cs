using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations;
using Syntax.NodeHandlers.Declarations.Struct;

namespace Semantics.Passes;

public interface ICommonPass :
    ISymbolDeclarationNodeHandler,
    ICodeBlockNodeHandler,
    IStructChildNodeHandler,
    IVariableDeclarationHandler,
    IBasicNodeHandler,
    IDeclarationBasicNodeHandler
{
}