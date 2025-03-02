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
            SetPath();
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
        }

        //定义路径
        public static string directoryPath;
        public static string IniPath;
        public static string frpExePath;
        public static string setupIniPath;
        public static string temp_path;
        public static string temp_api_path;
        public static string CPLPath;
        public static string pictures_path;
        public static string logfilePath;
        public static string temp_api_tunnel_path;

        private static void SetPath()
        {
            directoryPath = Directory.GetCurrentDirectory();
            //文件夹路径
            CPLPath = Path.Combine(directoryPath, "CPL");
            IniPath = Path.Combine(CPLPath, "Ini");
            temp_path = Path.Combine(CPLPath, "Temp");
            pictures_path = Path.Combine(CPLPath, "Pictures");
            //文件路径
            frpExePath = Path.Combine(CPLPath, "frpc.exe");
            logfilePath = Path.Combine(CPLPath, "Debug.logs");
            setupIniPath = Path.Combine(CPLPath, "Setup.ini");
            temp_api_path = Path.Combine(temp_path, "login_chmlfrp_api.json");
            temp_api_tunnel_path = Path.Combine(temp_path, "temp_api_tunnel.json");
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
