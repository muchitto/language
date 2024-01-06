using Parsing.NodeHandlers.Declarations;

namespace Parsing.NodeHandlers;

public interface IDeclarationNodeHandler : IStructDeclarationNodeHandler, IInterfaceDeclarationNodeHandler,
    IEnumDeclarationNodeHandler, IFunctionDeclarationNodeHandler;