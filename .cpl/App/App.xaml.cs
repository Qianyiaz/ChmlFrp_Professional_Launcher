using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;

namespace ChmlFrp_Professional_Launcher
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
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

        static App()
        {
            SetPath();
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
        }

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
