namespace HtmlEditor.Lib.Command.Implementation;

public abstract class AbstractCommand : ICommand
{
    private bool _executed = false;

    public void Execute()
    {
        if (_executed)
        {
            return;
        }

        DoExecute();
        _executed = true;
    }

    public void Undo()
    {
        if (!_executed)
        {
            return;
        }

        DoUndo();
        _executed = false;
    }

    public abstract bool Merge(ICommand other);
    public abstract void Destroy();

    protected abstract void DoExecute();
    protected abstract void DoUndo();
}