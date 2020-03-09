// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Windows.Forms;
using Common_Library.Converters;
using Common_Library.Localization;
using Common_Library.Config;
using Common_Library.Utils;
using Derpi_Downloader.API;
using Derpi_Downloader.Settings;

namespace Derpi_Downloader.RegKeys
{
    public static class ConfigKeys
    {
        private static readonly Config Config;
        private const String SettingsSection = "Settings";
        private const String CurrentDownloadFolderKey = "Default download folder";
        private const String CurrentDownloadFileNameKey = "Default download file name";
        private const String LanguageCodeKey = "Language code";
        private const String APISection = "API";
        private const String APICodeKey = "API";
        private const String OptionsSection = "Options";
        private const String ExistFileRewriteKey = "Exist file rewrite";
        private const String ConvertSVGToPNGKey = "SVG to PNG";
        private const String QueueAutoDownloadKey = "Queue auto download";
        private const String ProxySection = "Proxy";
        private const String ProxyAddressKey = "Proxy address";
        private const String ProxyPortKey = "Proxy port";
        private const String ProxyLoginKey = "Proxy login";
        private const String ProxyPasswordKey = "Proxy password";

        static ConfigKeys()
        {
            Config = Config.Factory(Assembly.GetCallingAssembly().GetName().Name, false);
        }

        public static String CurrentDownloadFolder
        {
            get
            {
                String path = Config.GetValue(CurrentDownloadFolderKey, Globals.DefaultDownloadFolder, true, SettingsSection);
                return PathUtils.IsValidPath(path) ? path : Globals.DefaultDownloadFolder;
            }
            set
            {
                Config.SetValue(CurrentDownloadFolderKey, value, true, SettingsSection);
            }
        }
        
        public static String CurrentDownloadFileName
        {
            get
            {
                String path = Config.GetValue(CurrentDownloadFileNameKey, Globals.DefaultDownloadFileName, true, SettingsSection);
                return PathUtils.IsValidPath(path, PathType.LocalFile) ? path : Globals.DefaultDownloadFileName;
            }
            set
            {
                Config.SetValue(CurrentDownloadFileNameKey, value, true, SettingsSection);
            }
        }
        
        public static Int32 LanguageCode
        {
            get
            {
                return Config.GetValue<Int32>(LanguageCodeKey, LocalizationBase.BasicCulture.Code, SettingsSection);
            }
            set
            {
                Config.SetValue(LanguageCodeKey, value, SettingsSection);
            }
        }
        
        public static String APIKey
        {
            get
            {
                String registryAPI = Config.GetValue(APICodeKey, String.Empty, true, APISection);
                return DerpiAPI.CheckAPI(registryAPI) ? registryAPI : String.Empty;
            }
            set
            {
                Config.SetValue(APICodeKey, value, true, APISection);
            }
        }

        public static Boolean ExistFileRewrite
        {
            get
            {
                return Config.GetValue(ExistFileRewriteKey, OptionsSection).ToBoolean();
            }
            set
            {
                Config.SetValue(ExistFileRewriteKey, value.ToString(), OptionsSection);
            }
        }
        
        public static Boolean ConvertSVGToPNG
        {
            get
            {
                return Config.GetValue(ConvertSVGToPNGKey, true, OptionsSection).ToBoolean();
            }
            set
            {
                Config.SetValue(ConvertSVGToPNGKey, value.ToString(), OptionsSection);
            }
        }

        public static Boolean QueueAutoDownload
        {
            get
            {
                return Config.GetValue(QueueAutoDownloadKey, false, OptionsSection).ToBoolean();
            }
            set
            {
                Config.SetValue(QueueAutoDownloadKey, value.ToString(), OptionsSection);
            }
        }

        public static String ProxyAddress
        {
            get
            {
                String proxyAddress = Config.GetValue(ProxyAddressKey, @"127.0.0.1", true, ProxySection);
                return NetworkUtils.ValidateIPv4(proxyAddress) ? proxyAddress : String.Empty;
            }
            set
            {
                Config.SetValue(ProxyAddressKey, value, ProxySection);
            }
        }

        public static Int32 ProxyPort
        {
            get
            {
                Int32 port = NetworkUtils.ValidatePort(Config.GetValue(ProxyPortKey, @"3128", true, ProxySection));
                return NetworkUtils.CheckPort(port) ? port : 3128;
            }
            set
            {
                Config.SetValue(ProxyPortKey, NetworkUtils.CheckPort(value) ? value.ToString() : null, ProxySection);
            }
        }

        public static String ProxyLogin
        {
            get
            {
                return Config.GetValue(ProxyLoginKey, String.Empty, true, ProxySection);
            }
            set
            {
                Config.SetValue(ProxyLoginKey, value, true, ProxySection);
            }
        }
        
        public static String ProxyPassword
        {
            get
            {
                return Config.GetValue(ProxyPasswordKey, String.Empty, true, ProxySection);
            }
            set
            {
                Config.SetValue(ProxyPasswordKey, value, true, ProxySection);
            }
        }
    }
}