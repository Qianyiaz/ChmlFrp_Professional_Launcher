using System.IO;

namespace ChmlFrp_Professional_Launcher
{
    internal class ClienterClass
    {
        public string directoryPath;
        public string frpPath;
        public string frpIniPath;
        public string frpExePath;
        public string setupIniPath;
        public string temp_path;
        public string temp_api_path;
        public string CPLPath;
        public string pictures_path;
        public ClienterClass()
        {
            directoryPath = Directory.GetCurrentDirectory();
            CPLPath = Path.Combine(directoryPath, "CPL");
            frpPath = Path.Combine(CPLPath, "frp");
            frpIniPath = Path.Combine(frpPath, "frpc.ini");
            frpExePath = Path.Combine(frpPath, "frpc.exe");
            setupIniPath = Path.Combine(CPLPath, "Setup.ini");
            temp_path = Path.Combine(CPLPath, "temp");
            temp_api_path = Path.Combine(temp_path, "Chmlfrp_api.json");
            pictures_path = Path.Combine(CPLPath, "pictures");
        }
    }
}
