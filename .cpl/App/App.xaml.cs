using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;

namespace ChmlFrp_Professional_Launcher
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        // 入口
        static App()
        {
            new MainClass.Paths();
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
        }

        private static readonly Dictionary<string, Assembly> AssemblyCache = new();

        private static Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
        {
            var assemblyName = new AssemblyName(args.Name);
            var path = assemblyName.CultureInfo.Equals(CultureInfo.InvariantCulture)
                ? $"{assemblyName.Name}.dll"
                : Path.Combine(assemblyName.CultureInfo.ToString(), $"{assemblyName.Name}.dll");

            if (AssemblyCache.TryGetValue(path, out var cachedAssembly))
                return cachedAssembly;

            var executingAssembly = Assembly.GetExecutingAssembly();
            using var stream = executingAssembly.GetManifestResourceStream(path);
            if (stream == null)
                return null;

            var assemblyRawBytes = new byte[stream.Length];
            stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
            var assembly = Assembly.Load(assemblyRawBytes);

            AssemblyCache[path] = assembly;
            return assembly;
        }
    }
}
