namespace HtmlEditor.Lib.Command;

public interface ICommand
{
    public void Execute();
    public void Undo();
    public bool Merge(ICommand other);
    public void Destroy();
}