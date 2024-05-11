using System;
using System.Collections.Generic;
using GameFinder.Common;
using JetBrains.Annotations;
using TransparentValueObjects;

namespace GameFinder.StoreHandlers.Xbox;

/// <summary>
/// Represents an ID for games installed with the Xbox App.
/// </summary>
[PublicAPI]
[ValueObject<string>]
public readonly partial struct XboxGameId : IId<XboxGameId>, IAugmentWith<DefaultEqualityComparerAugment>
{
    /// <inheritdoc/>
    public static IEqualityComparer<string> InnerValueDefaultEqualityComparer { get; } = StringComparer.OrdinalIgnoreCase;
}