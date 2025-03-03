using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using Avalonia.VisualTree;

namespace Avalonia.Xaml.Interactions.Core;

/// <summary>
/// An action that will clear the clipboard.
/// </summary>
public class ClearClipboardAction : Interactivity.StyledElementAction
{
    /// <summary>
    /// Executes the action.
    /// </summary>
    /// <param name="sender">The <see cref="object"/> that is passed to the action by the behavior. Generally this is <seealso cref="Avalonia.Xaml.Interactivity.IBehavior.AssociatedObject"/> or a target object.</param>
    /// <param name="parameter">The value of this parameter is determined by the caller.</param>
    /// <returns>True if the command is successfully executed; else false.</returns>
    public override object Execute(object? sender, object? parameter)
    {
        if (sender is not Visual visual)
        {
            return false;
        }

        Dispatcher.UIThread.InvokeAsync(async () => await ClearClipboardAsync(visual));

        return true;
    }

    private async Task ClearClipboardAsync(Visual visual)
    {
        if (IsEnabled != true)
        {
            return;
        }

        try
        {
            var clipboard = (visual.GetVisualRoot() as TopLevel)?.Clipboard;
            if (clipboard is null)
            {
                return;
            }

            await clipboard.ClearAsync();
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
