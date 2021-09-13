using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Roslyn.Verifiers
{
    public class DefaultCompillerSetup : ISolutionTransformProvider
    {
        private readonly IPathResolver PathResolver;

        public DefaultCompillerSetup(IPathResolver pathResolver)
        {
            PathResolver = pathResolver;
            NullableWarningsFromCompiler = new Lazy<ImmutableDictionary<string, ReportDiagnostic>>(GetNullableWarningsFromCompiler);
        }

        /// <summary>
        /// By default, the compiler reports diagnostics for nullable reference types at
        /// <see cref="DiagnosticSeverity.Warning"/>, and the analyzer test framework defaults to only validating
        /// diagnostics at <see cref="DiagnosticSeverity.Error"/>. This map contains all compiler diagnostic IDs
        /// related to nullability mapped to <see cref="ReportDiagnostic.Error"/>, which is then used to enable all
        /// of these warnings for default validation during analyzer and code fix tests.
        /// </summary>
        private ImmutableDictionary<string, ReportDiagnostic> GetNullableWarningsFromCompiler()
        {
            string[] args = { "/warnaserror:nullable" };
            var currentDir = PathResolver.Resolve();
            var commandLineArguments = CSharpCommandLineParser.Default.Parse(
                args,
                baseDirectory: currentDir,
                sdkDirectory: currentDir);

            ImmutableDictionary<string, ReportDiagnostic> nullableWarnings = commandLineArguments.CompilationOptions.SpecificDiagnosticOptions;

            // Workaround for https://github.com/dotnet/roslyn/issues/41610
            nullableWarnings = nullableWarnings
                .SetItem("CS8632", ReportDiagnostic.Error)
                .SetItem("CS8669", ReportDiagnostic.Error);

            return nullableWarnings;
        }

        readonly Lazy<ImmutableDictionary<string, ReportDiagnostic>> NullableWarningsFromCompiler;

        public IEnumerable<Func<Solution, ProjectId, Solution>> SolutionTransforms
        {
            get
            {
                yield return (solution, projectId) =>
                {
                    var opts = solution.GetProject(projectId).CompilationOptions;
                    opts = opts.WithSpecificDiagnosticOptions(opts.SpecificDiagnosticOptions.SetItems(NullableWarningsFromCompiler.Value));
                    solution = solution.WithProjectCompilationOptions(projectId, opts);

                    return solution;
                };
            }
        }
    }
}
