using System;
using System.IO;
using System.Reflection;

namespace Identifier.SpellChecker
{
    static class Binder
    {
        public static readonly string LocalFolder = Path.GetDirectoryName(new Uri(typeof(IdentifierSpellCheckerAnalyzer).Assembly.CodeBase).LocalPath);
        static readonly string[] LocalRedirected = {
            "Microsoft.Extensions.Logging",
            "Microsoft.Extensions.Logging.Abstractions",
            "Microsoft.Extensions.DependencyInjection.Abstractions",
            "Microsoft.Extensions.Configuration.Abstractions"
        };

        public static void Bind()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (object sender, ResolveEventArgs args) =>
            {
                Fallbacklog(args.Name);
                AssemblyName name = new AssemblyName(args.Name);
                if (Array.IndexOf(LocalRedirected, name.Name) > -1)
                {
                    string fname = Path.Combine(LocalFolder, $"{name.Name}.dll");
                    Fallbacklog(fname);
                    try
                    {
                        return Assembly.LoadFrom(fname);
                    }
                    catch (Exception ex)
                    {
                        Fallbacklog(ex.Message);
                        throw;
                    }
                }
                return null;
            };
        }

        static void Fallbacklog(string s)
        {
            //File.AppendAllText(Path.Combine(LocalFolder, "bind.log"), $"{s}\n");
        }
    }
}
