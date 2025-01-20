﻿using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Reactive;
using Avalonia.Xaml.Interactivity;

namespace Avalonia.Xaml.Interactions.Core;

public class CustomStyledElement : StyledElement
{
    public static readonly StyledProperty<object?> ValueProperty =
        AvaloniaProperty.Register<CustomStyledElement, object?>(nameof(Value));

    public object? Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == ValueProperty)
        {
            Console.WriteLine($"Value={Value}");
        }
    }
}

/// <summary>
/// A behavior that performs actions when the bound data meets a specified condition.
/// </summary>
[RequiresUnreferencedCode("This functionality is not compatible with trimming.")]
public class DataTriggerBehavior : StyledElementTrigger
{
    /// <summary>
    /// Identifies the <seealso cref="Binding"/> avalonia property.
    /// </summary>
    public static readonly StyledProperty<object?> BindingProperty =
        AvaloniaProperty.Register<DataTriggerBehavior, object?>(nameof(Binding));

    /// <summary>
    /// Identifies the <seealso cref="ComparisonCondition"/> avalonia property.
    /// </summary>
    public static readonly StyledProperty<ComparisonConditionType> ComparisonConditionProperty =
        AvaloniaProperty.Register<DataTriggerBehavior, ComparisonConditionType>(nameof(ComparisonCondition));

    /// <summary>
    /// Identifies the <seealso cref="Value"/> avalonia property.
    /// </summary>
    public static readonly StyledProperty<object?> ValueProperty =
        AvaloniaProperty.Register<DataTriggerBehavior, object?>(nameof(Value));

    /// <summary>
    /// Gets or sets the bound object that the <see cref="DataTriggerBehavior"/> will listen to. This is an avalonia property.
    /// </summary>
    public object? Binding
    {
        get => GetValue(BindingProperty);
        set => SetValue(BindingProperty, value);
    }

    /// <summary>
    /// Gets or sets the type of comparison to be performed between <see cref="DataTriggerBehavior.Binding"/> and <see cref="DataTriggerBehavior.Value"/>. This is an avalonia property.
    /// </summary>
    public ComparisonConditionType ComparisonCondition
    {
        get => GetValue(ComparisonConditionProperty);
        set => SetValue(ComparisonConditionProperty, value);
    }

    /// <summary>
    /// Gets or sets the value to be compared with the value of <see cref="DataTriggerBehavior.Binding"/>. This is an avalonia property.
    /// </summary>
    public object? Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    static DataTriggerBehavior()
    {
        //BindingProperty.Changed.Subscribe(
        //    new AnonymousObserver<AvaloniaPropertyChangedEventArgs<object?>>(OnValueChanged));

        //ComparisonConditionProperty.Changed.Subscribe(
        //    new AnonymousObserver<AvaloniaPropertyChangedEventArgs<ComparisonConditionType>>(OnValueChanged));

        //ValueProperty.Changed.Subscribe(
        //    new AnonymousObserver<AvaloniaPropertyChangedEventArgs<object?>>(OnValueChanged));
    }


    /// <inheritdoc />
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
                
        if (change.Property == BindingProperty)
        {
            OnValueChanged(change);
        }

        if (change.Property == ComparisonConditionProperty)
        {
            OnValueChanged(change);
        }

        if (change.Property == ValueProperty)
        {
            OnValueChanged(change);
        }
    }

    /// <inheritdoc />
    protected override void OnInitializedEvent()
    {
        base.OnInitializedEvent();

        Execute(parameter: null);
    }

    private void OnValueChanged(AvaloniaPropertyChangedEventArgs args)
    {
        if (args.Sender is not DataTriggerBehavior behavior)
        {
            return;
        }

        behavior.Execute(parameter: args);
    }

    private void Execute(object? parameter)
    {        
        if (AssociatedObject is null)
        {
            return;
        }

        if (!IsEnabled)
        {
            return;
        }

        // NOTE: In UWP version binding null check is not present but Avalonia throws exception as Bindings are null when first initialized.
        var binding = Binding;
        if (binding is not null)
        {
            // Some value has changed--either the binding value, reference value, or the comparison condition. Re-evaluate the equation.
            if (ComparisonConditionTypeHelper.Compare(Binding, ComparisonCondition, Value))
            {
                Interaction.ExecuteActions(AssociatedObject, Actions, parameter);
            }
        }
    }
}
