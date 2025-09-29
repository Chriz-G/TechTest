using System;
using System.Collections.Generic;
using System.Linq;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

/// <summary>
/// Implements the IMappingService interface to provide primitive mapping functionality
/// in the absence of a more robust mapping library.
/// </summary>
public class MappingService : IMappingService
{
    /// <summary>
    /// Maps a source object to a destination object.
    /// This is by no means a complete or efficient mapping implementation, Automapper or similar would be better.
    /// </summary>
    /// <param name="source">The source object to map.</param>
    /// <typeparam name="TSource">The type of the source object.</typeparam>
    /// <typeparam name="TDestination">The type of the destination object.</typeparam>
    /// <returns>The mapped destination object.</returns>
    /// <exception cref="Exception">Thrown if the source object is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the source object cannot be mapped to the destination object.</exception>
    public TDestination Map<TSource, TDestination>(TSource source)
    {
        if (source == null) throw new Exception($"Could not create instance of source type: {nameof(TSource)}");
        var sourceProperties = source.GetType().GetProperties();
        var destination = Activator.CreateInstance<TDestination>() ?? throw new Exception($"Could not create instance of destination type: {nameof(TDestination)}");
        var destinationProperties = destination.GetType().GetProperties();

        foreach (var sourceProperty in sourceProperties)
        {
            var destinationProperty = destinationProperties.FirstOrDefault(destProp => destProp.Name == sourceProperty.Name);

            if (destinationProperty != null)
            {
                var sourceValue = sourceProperty.GetValue(source);
                var destinationPropertyType = destinationProperty.PropertyType;

                if (sourceValue != null && sourceValue.GetType().IsAssignableFrom(destinationPropertyType))
                {
                    destinationProperty.SetValue(destination, sourceValue);
                }
                else if (sourceValue is string && destinationPropertyType == typeof(string))
                {
                    destinationProperty.SetValue(destination, sourceValue.ToString());
                }
                else if (sourceValue is DateTime && destinationPropertyType == typeof(DateTime))
                {
                    destinationProperty.SetValue(destination, (DateTime)sourceValue);
                }
                else if (sourceValue is bool && destinationPropertyType == typeof(bool))
                {
                    destinationProperty.SetValue(destination, (bool)sourceValue);
                }
                else if (sourceValue is int && destinationPropertyType == typeof(int))
                {
                    destinationProperty.SetValue(destination, (int)sourceValue);
                }
                else if (sourceValue is long && destinationPropertyType == typeof(long))
                {
                    destinationProperty.SetValue(destination, (long)sourceValue);
                }
                else if (sourceValue is decimal && destinationPropertyType == typeof(decimal))
                {
                    destinationProperty.SetValue(destination, (decimal)sourceValue);
                }
                else if (sourceValue is double && destinationPropertyType == typeof(double))
                {
                    destinationProperty.SetValue(destination, (double)sourceValue);
                }
                else if (sourceValue is float && destinationPropertyType == typeof(float))
                {
                    destinationProperty.SetValue(destination, (float)sourceValue);
                }
                else if (sourceValue is Guid && destinationPropertyType == typeof(Guid))
                {
                    destinationProperty.SetValue(destination, (Guid)sourceValue);
                }
                else if (sourceValue is byte[] && destinationPropertyType == typeof(byte[]))
                {
                    destinationProperty.SetValue(destination, (byte[])sourceValue);
                }
                else if (sourceValue is List<object> && destinationPropertyType == typeof(List<object>))
                {
                    destinationProperty.SetValue(destination, (List<object>)sourceValue);
                }
                else if (sourceValue is List<string> && destinationPropertyType == typeof(List<string>))
                {
                    destinationProperty.SetValue(destination, (List<string>)sourceValue);
                }
                else if (sourceValue is List<DateTime> && destinationPropertyType == typeof(List<DateTime>))
                {
                    destinationProperty.SetValue(destination, (List<DateTime>)sourceValue);
                }
                else if (sourceValue is List<bool> && destinationPropertyType == typeof(List<bool>))
                {
                    destinationProperty.SetValue(destination, (List<bool>)sourceValue);
                }
                else if (sourceValue is List<int> && destinationPropertyType == typeof(List<int>))
                {
                    destinationProperty.SetValue(destination, (List<int>)sourceValue);
                }
                else if (sourceValue is List<long> && destinationPropertyType == typeof(List<long>))
                {
                    destinationProperty.SetValue(destination, (List<long>)sourceValue);
                }
                else if (sourceValue is List<decimal> && destinationPropertyType == typeof(List<decimal>))
                {
                    destinationProperty.SetValue(destination, (List<decimal>)sourceValue);
                }
                else if (sourceValue is List<double> && destinationPropertyType == typeof(List<double>))
                {
                    destinationProperty.SetValue(destination, (List<double>)sourceValue);
                }
                else if (sourceValue is List<float> && destinationPropertyType == typeof(List<float>))
                {
                    destinationProperty.SetValue(destination, (List<float>)sourceValue);
                }
                else if (sourceValue is List<Guid> && destinationPropertyType == typeof(List<Guid>))
                {
                    destinationProperty.SetValue(destination, (List<Guid>)sourceValue);
                }
                else if (sourceValue is List<byte[]> && destinationPropertyType == typeof(List<byte[]>))
                {
                    destinationProperty.SetValue(destination, (List<byte[]>)sourceValue);
                }
                else
                {
                    throw new ArgumentException($"Property '{sourceProperty.Name}' of type '{sourceProperty.PropertyType}' cannot be mapped to property '{destinationProperty.Name}' of type '{destinationProperty.PropertyType}'.");
                }
            }
        }

        return destination;
    }

}
