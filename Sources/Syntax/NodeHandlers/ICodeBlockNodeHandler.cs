using Syntax.Nodes;

namespace Syntax.NodeHandlers;

public interface ICodeBlockNodeHandler
{
    public void Handle(BodyContainerNode bodyContainerDeclarationNode);

    public void Handle(ProgramContainerNode programContainerNode);

    public void Handle(CodeBlockNode codeBlockNode);
}