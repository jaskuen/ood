namespace Figures.Model.Core.Events;

/// <summary>
/// Реализует паттерн Издатель/Подписчик для обмена событиями между компонентами.
/// </summary>
public class EventEmitter
{
    private readonly Dictionary<string, List<Action<object?>>> _listeners = new();

    /// <summary>
    /// Регистрирует обработчик для указанного события.
    /// </summary>
    public void On(EventType eventType, Action<object?> handler)
    {
        var eventName = eventType.ToString();
        if (!_listeners.ContainsKey(eventName))
        {
            _listeners[eventName] = new List<Action<object?>>();
        }

        _listeners[eventName].Add(handler);
    }

    /// <summary>
    /// Инициирует событие, вызывая все зарегистрированные обработчики.
    /// </summary>
    protected void Emit(EventType eventType, object? payload = null)
    {
        var eventName = eventType.ToString();
        if (!_listeners.TryGetValue(eventName, out var listener))
        {
            return;
        }

        foreach (var handler in listener)
        {
            handler(payload);
        }
    }
}

