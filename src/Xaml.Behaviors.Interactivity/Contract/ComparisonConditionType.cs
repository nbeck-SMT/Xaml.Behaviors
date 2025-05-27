﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.
namespace Avalonia.Xaml.Interactivity;

/// <summary>
/// Represents one ternary condition.
/// </summary>
public enum ComparisonConditionType
{
    /// <summary>
    /// Specifies an equal condition.
    /// </summary>
    Equal,
    /// <summary>
    /// Specifies a not equal condition.
    /// </summary>
    NotEqual,
    /// <summary>
    /// Specifies a less than condition.
    /// </summary>
    LessThan,
    /// <summary>
    /// Specifies a less than or equal condition.
    /// </summary>
    LessThanOrEqual,
    /// <summary>
    /// Specifies a greater than condition.
    /// </summary>
    GreaterThan,
    /// <summary>
    /// Specifies a greater than or equal condition.
    /// </summary>
    GreaterThanOrEqual
}
