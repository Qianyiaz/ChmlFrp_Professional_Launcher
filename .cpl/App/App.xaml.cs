using System;
using System.Globalization;
using System.Reflection;
using System.Windows;

namespace ChmlFrp_Professional_Launcher
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
        }

        private static Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var assemblyName = new AssemblyName(args.Name);

            var path = assemblyName.Name + ".dll";
            if (assemblyName.CultureInfo.Equals(CultureInfo.InvariantCulture) == false)
                path = $@"{assemblyName.CultureInfo}\{path}";
            using var stream = executingAssembly.GetManifestResourceStream(path);
            if (stream == null)
                return null;
            var assemblyRawBytes = new byte[stream.Length];
            stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
            return Assembly.Load(assemblyRawBytes);
        }
    }
}
