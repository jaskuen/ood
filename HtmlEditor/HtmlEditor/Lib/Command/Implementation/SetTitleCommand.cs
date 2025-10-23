namespace HtmlEditor.Lib.Command.Implementation;

public class Ref<T>
{
    public T Value;
    public Ref(T value) => Value = value;
}

public class SetTitleCommand : AbstractCommand
{
    private Ref<string> _currentTitle;
    private string _newTitle;
    private string _oldTitle;

    public SetTitleCommand(Ref<string> currentTitle, string newTitle)
    {
        _currentTitle = currentTitle;
        _newTitle = newTitle;
    }

    public override bool Merge(ICommand other)
    {
        if (other is SetTitleCommand otherCommand)
        {
            _currentTitle.Value = otherCommand._newTitle;

            return true;
        }

        return false;
    }

    public override void Destroy()
    {
    }

    protected override void DoExecute()
    {
        (_currentTitle.Value, _newTitle) = (_newTitle, _currentTitle.Value);
    }

    protected override void DoUndo()
    {
        (_currentTitle.Value, _newTitle) = (_newTitle, _currentTitle.Value);
    }
}