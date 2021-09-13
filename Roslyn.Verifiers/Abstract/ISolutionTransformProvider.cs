using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Roslyn.Verifiers
{
    public interface ISolutionTransformProvider
    {
        IEnumerable<Func<Solution, ProjectId, Solution>> SolutionTransforms { get; }
    }
}
