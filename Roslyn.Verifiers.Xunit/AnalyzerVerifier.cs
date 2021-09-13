using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using System;

namespace Roslyn.Verifiers.Xunit
{
    public abstract class AnalyzerVerifier<TAnalyzer, TTest> : AnalyzerVerifier<TAnalyzer, TTest, XUnitVerifier>
        where TAnalyzer : DiagnosticAnalyzer
        where TTest : AnalyzerTest<XUnitVerifier>
    {
        protected AnalyzerVerifier(TAnalyzer analyzer, Func<TTest> test) : base(analyzer, test)
        {
        }
    }
}
