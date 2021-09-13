using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;

namespace Roslyn.Verifiers
{
    public class AnalyzerVerifier<TAnalyzer, TTest, TVerifier>
        where TAnalyzer : DiagnosticAnalyzer
        where TTest : AnalyzerTest<TVerifier>
        where TVerifier : IVerifier, new()
    {
        protected readonly TAnalyzer Analyzer;
        private readonly Func<TTest> TestFactory;

        public AnalyzerVerifier(TAnalyzer analyzer, Func<TTest> testFactory)
        {
            Analyzer = analyzer;
            TestFactory = testFactory;
        }

        // Summary:
        //     Creates a Microsoft.CodeAnalysis.Testing.DiagnosticResult representing an expected
        //     diagnostic for the single Microsoft.CodeAnalysis.DiagnosticDescriptor supported
        //     by the analyzer.
        //
        // Returns:
        //     A Microsoft.CodeAnalysis.Testing.DiagnosticResult initialized using the single
        //     descriptor supported by the analyzer.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     If the analyzer declares support for more than one diagnostic descriptor.
        //     -or-
        //     If the analyzer does not declare support for any diagnostic descriptors.
        public DiagnosticResult Diagnostic()
        {
            try
            {
                return Diagnostic(Analyzer.SupportedDiagnostics.Single());
            }
            catch (InvalidOperationException innerException)
            {
                throw new InvalidOperationException("'Diagnostic()' can only be used when the analyzer has a single supported diagnostic. Use the 'Diagnostic(DiagnosticDescriptor)' overload to specify the descriptor from which to create the expected result.", innerException);
            }
        }

        //
        // Summary:
        //     Creates a Microsoft.CodeAnalysis.Testing.DiagnosticResult representing an expected
        //     diagnostic for the single Microsoft.CodeAnalysis.DiagnosticDescriptor with the
        //     specified ID supported by the analyzer.
        //
        // Parameters:
        //   diagnosticId:
        //     The expected diagnostic ID.
        //
        // Returns:
        //     A Microsoft.CodeAnalysis.Testing.DiagnosticResult initialized using the single
        //     descriptor with the specified ID supported by the analyzer.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     If the analyzer declares support for more than one diagnostic descriptor with
        //     the specified ID.
        //     -or-
        //     If the analyzer does not declare support for any diagnostic descriptors with
        //     the specified ID.
        public DiagnosticResult Diagnostic(string diagnosticId)
        {
            try
            {
                return Diagnostic(Analyzer.SupportedDiagnostics.Single((DiagnosticDescriptor i) => i.Id == diagnosticId));
            }
            catch (InvalidOperationException innerException)
            {
                throw new InvalidOperationException("'Diagnostic(string)' can only be used when the analyzer has a single supported diagnostic with the specified ID. Use the 'Diagnostic(DiagnosticDescriptor)' overload to specify the descriptor from which to create the expected result.", innerException);
            }
        }

        //
        // Summary:
        //     Creates a Microsoft.CodeAnalysis.Testing.DiagnosticResult representing an expected
        //     diagnostic for the specified descriptor.
        //
        // Parameters:
        //   descriptor:
        //     The diagnostic descriptor.
        //
        // Returns:
        //     A Microsoft.CodeAnalysis.Testing.DiagnosticResult initialed using the specified
        //     descriptor.
        public DiagnosticResult Diagnostic(DiagnosticDescriptor descriptor)
        {
            return new DiagnosticResult(descriptor);
        }

        //
        // Summary:
        //     Verifies the analyzer produces the specified diagnostics for the given source
        //     text.
        //
        // Parameters:
        //   source:
        //     The source text to test, which may include markup syntax.
        //
        //   expected:
        //     The expected diagnostics. These diagnostics are in addition to any diagnostics
        //     defined in markup.
        //
        // Returns:
        //     A System.Threading.Tasks.Task representing the asynchronous operation.
        public Task VerifyAnalyzerAsync(string source, params DiagnosticResult[] expected)
        {
            var test = TestFactory();
            test.TestCode = source;
            test.ExpectedDiagnostics.AddRange(expected);
            return test.RunAsync(CancellationToken.None);
        }
    }
}
