using Mono.Cecil;
using NetArchTest.Rules;

namespace CartonCaps.Tests.Architecture.CustomRules;

/// <summary>
/// Represents a custom rule that checks if the type implements a type specific interface matching the name of the type (i.e. CalculationService implements ICalculationService).
/// </summary>
public class InterfaceImplementationRule : ICustomRule
{
    /// <summary>
    /// Determines whether the specified type implements a type specific interface matching the name of the type.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns><c>true</c> if the type implements a type specific interface matching the name of the type; otherwise, <c>false</c>.</returns>
    public bool MeetsRule(TypeDefinition type)
    {
        return type.Interfaces.Any(x => x.InterfaceType.Name == $"I{type.Name}");
    }
}

