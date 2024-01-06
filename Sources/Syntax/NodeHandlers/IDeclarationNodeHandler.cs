using Syntax.NodeHandlers.Declarations;

namespace Syntax.NodeHandlers;

public interface IDeclarationNodeHandler : IStructDeclarationNodeHandler, IInterfaceDeclarationNodeHandler,
    IEnumDeclarationNodeHandler, IFunctionDeclarationNodeHandler;