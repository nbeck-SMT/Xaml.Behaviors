// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.
using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.LogicalTree;
using Avalonia.Threading;

namespace Avalonia.Xaml.Interactions.Core;

/// <summary>
/// An action that will set the data object to the clipboard.
/// </summary>
public class SetClipboardDataObjectAction : Interactivity.StyledElementAction
{
    /// <summary>
    /// Identifies the <seealso cref="Clipboard"/> avalonia property.
    /// </summary>
    public static readonly StyledProperty<IClipboard?> ClipboardProperty =
        AvaloniaProperty.Register<SetClipboardDataObjectAction, IClipboard?>(nameof(Clipboard));

    /// <summary>
    /// Gets or sets the clipboard to use. This is an avalonia property.
    /// </summary>
    public IClipboard? Clipboard
    {
        get => GetValue(ClipboardProperty);
        set => SetValue(ClipboardProperty, value);
    }
    
    /// <summary>
    /// Identifies the <seealso cref="IDataObject"/> avalonia property.
    /// </summary>
    public static readonly StyledProperty<IDataObject?> DataObjectProperty =
        AvaloniaProperty.Register<SetClipboardDataObjectAction, IDataObject?>(nameof(DataObject));

    /// <summary>
    /// Gets or sets the text to set to the clipboard. This is an avalonia property.
    /// </summary>
    public IDataObject? DataObject
    {
        get => GetValue(DataObjectProperty);
        set => SetValue(DataObjectProperty, value);
    }

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

        Dispatcher.UIThread.InvokeAsync(async () => await SetClipboardDataObjectAsync(visual));

        return true;
    }

    private async Task SetClipboardDataObjectAsync(Visual visual)
    {
        if (IsEnabled != true || DataObject is null)
        {
            return;
        }

        try
        {
            var clipboard = Clipboard ?? (visual.GetSelfAndLogicalAncestors().LastOrDefault() as TopLevel)?.Clipboard;
            if (clipboard is null)
            {
                return;
            }

            await clipboard.SetDataObjectAsync(DataObject);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
