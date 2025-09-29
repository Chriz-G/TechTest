using System;
using System.Collections.Generic;
using System.Linq;

namespace UserManagement.Services.Domain.Interfaces;

/// <summary>
/// Interface for mapping objects.
/// </summary>
public interface IMappingService
{
    /// <summary>
    /// Maps a source object to a destination object.
    /// </summary>
    /// <param name="source">The source object to map.</param>
    /// <typeparam name="TSource">The type of the source object.</typeparam>
    /// <typeparam name="TDestination">The type of the destination object.</typeparam>
    /// <returns>The mapped destination object.</returns>
    TDestination Map<TSource, TDestination>(TSource source);
}
