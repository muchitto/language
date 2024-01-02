using Parsing.Nodes;

namespace Parsing.NodeHandlers;

/// <summary>
/// </summary>
public interface IDeclarationNodeHandler
{
    /// <summary>
    ///     Function declaration related nodes
    /// </summary>
    /// <param name="functionDeclarationNode"></param>
    public void Handle(FunctionDeclarationNode functionDeclarationNode);

    public void Handle(FunctionArgumentListNode functionArgumentListNode);

    public void Handle(FunctionArgumentNode functionArgumentNode);

    public void Handle(FunctionCallNode functionCallNode);

    public void Handle(FunctionCallArgumentNode functionCallArgumentNode);

    /// <summary>
    ///     Enum declaration related nodes
    /// </summary>
    /// <param name="enumDeclarationNodeDeclaration"></param>
    public void Handle(EnumDeclarationNode enumDeclarationNodeDeclaration);

    public void Handle(EnumFunctionNode enumFunctionNode);

    public void Handle(EnumCaseAssociatedValueNode enumCaseAssociatedValueNode);

    public void Handle(EnumCaseNode enumCaseNode);

    /// <summary>
    ///     Interface declaration related nodes
    /// </summary>
    /// <param name="interfaceDeclarationNodeDeclaration"></param>
    public void Handle(InterfaceDeclarationNode interfaceDeclarationNodeDeclaration);

    /// <summary>
    ///     Struct declaration related nodes
    /// </summary>
    /// <param name="structDeclarationNode"></param>
    public void Handle(StructDeclarationNode structDeclarationNode);

    public void Handle(StructFieldNode structFieldNode);

    public void Handle(StructFunctionNode structFunctionNode);

    public void Handle(StructVariableNode structVariableNode);
}