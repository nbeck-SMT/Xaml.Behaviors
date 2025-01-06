using Avalonia.Input;
using Avalonia.Interactivity;

namespace Avalonia.Xaml.Interactions.Custom;

/// <summary>
/// 
/// </summary>
public class ScrollGestureEndedGestureTrigger : RoutedEventTriggerBase<ScrollGestureEndedEventArgs>
{
    /// <inheritdoc />
    protected override RoutedEvent<ScrollGestureEndedEventArgs> RoutedEvent 
        => Gestures.ScrollGestureEndedEvent;
}
