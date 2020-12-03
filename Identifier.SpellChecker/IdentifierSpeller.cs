using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using SpellChecker;

namespace Identifier.SpellChecker
{
    public class IdentifierSpeller : IIdentifierSpeller
    {
        private readonly ISpellChecker Checker;
        private readonly IEnumerable<ISpellChecker> CustomCheckers;

        private readonly ILogger<IdentifierSpeller> Logger;

        public IdentifierSpeller(ISpellChecker checker, ILogger<IdentifierSpeller> logger, IEnumerable<ISpellChecker> customCheckers)
        {
            Checker = checker;
            Logger = logger;
            CustomCheckers = customCheckers;
        }

        public IdentifierCheckResult Check(string identifier)
        {
            Logger.LogTrace($"checking: [{identifier}]");

            var parts = identifier
                .SplitCamelCase()
                .Analyze()
                .ToArray();

            var checkedParts = new List<CheckedPart>();
            foreach (var part in parts)
            {
                if (part.Type != PartType.Word)
                    continue;

                var value = part.Value;
                var checkResult = Checker.Check(value);
                if (!checkResult)
                {
                    foreach (var customChecker in CustomCheckers)
                    {

                        checkResult = customChecker.Check(value);
                        Logger.LogTrace($"  checking [{value}] with [{customChecker}] => {checkResult.IsCorrect()}");
                        if (checkResult)
                            break;
                    }
                }

                Logger.LogTrace($"check part: [{part.Value}] => {checkResult.IsCorrect()}");
                string[] suggestions = null;
                if (!checkResult)
                {
                    suggestions = Checker.Suggest(part.Value).ToArray();
                    Logger.LogTrace($"  suggestions: {string.Join(", ", suggestions)}");
                }

                checkedParts.Add(new CheckedPart(part, checkResult, suggestions));
            }

            return new IdentifierCheckResult(identifier, checkedParts.ToArray());
        }
    }
}
