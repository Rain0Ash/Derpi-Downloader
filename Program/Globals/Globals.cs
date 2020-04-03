// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Reflection;
using Common_Library;
using Common_Library.Config;
using Common_Library.Crypto;
using Common_Library.Localization;
using Common_Library.Logger;
using Common_Library.LongPath;
using Common_Library.Utils.IO;
using Common_Library.Utils.Network;
using Derpi_Downloader.API;
using Derpi_Downloader.Localization;

namespace Derpi_Downloader.Settings
{
    public static class Globals
    {
        public const String Version = "1.3";
        public const String DefaultDownloadFolder = @"Download";
        public const String DefaultDownloadFileName = @"{artist?}\{name}.{ext}";

        public static readonly ProgramLocalization Localization;
        public static readonly Logger Logger;

        public static readonly Config Config;
        private const String SettingsSection = "Settings";
        private const String CurrentDownloadFolderKey = "Default download folder";
        private const String CurrentDownloadFileNameKey = "Default download file name";
        private const String LanguageCodeKey = "Language code";
        private const String APISection = "API";
        private const String APICodeKey = "API";
        private const String OptionsSection = "Options";
        private const String ExistFileRewriteKey = "Exist file rewrite";
        private const String ConvertSVGToPNGKey = "SVG to PNG";
        private const String NotStrictAPICheckKey = "Not strict API check";
        private const String QueueAutoDownloadKey = "Queue auto download";
        private const String ForceCloseKey = "Force close";
        private const String ProxySection = "Proxy";
        private const String ProxyAddressKey = "Proxy address";
        private const String ProxyPortKey = "Proxy port";
        private const String ProxyLoginKey = "Proxy login";
        private const String ProxyPasswordKey = "Proxy password";
        
        static Globals()
        {
            Config = Config.Factory(Assembly.GetCallingAssembly().GetName().Name, false);
            
            CurrentDownloadFolder = Config.GetProperty(CurrentDownloadFolderKey, DefaultDownloadFolder, 
                path => PathUtils.IsValidPath(path, PathType.All), CryptAction.Crypt, SettingsSection);
            
            CurrentDownloadFileName = Config.GetProperty(CurrentDownloadFileNameKey, DefaultDownloadFileName, 
                path => PathUtils.IsValidPath(path, PathType.LocalFile), CryptAction.Crypt, SettingsSection);

            LanguageCode = Config.GetProperty(LanguageCodeKey, LocalizationBase.BasicCulture.LCID, SettingsSection);

            ExistFileRewrite = Config.GetProperty(ExistFileRewriteKey, false, OptionsSection);
            
            ConvertSVGToPNG = Config.GetProperty(ConvertSVGToPNGKey, true, OptionsSection);
            
            NotStrictAPICheck = Config.GetProperty(NotStrictAPICheckKey, false, OptionsSection);
            
            QueueAutoDownload = Config.GetProperty(QueueAutoDownloadKey, false, OptionsSection);

            ForceClose = Config.GetProperty(ForceCloseKey, false, OptionsSection);
            
            ProxyAddress = Config.GetProperty(ProxyAddressKey, @"127.0.0.1", NetworkUtils.ValidateIPv4, ProxySection);
            
            ProxyPort = Config.GetProperty(ProxyPortKey, 3128, NetworkUtils.ValidatePort, ProxySection);

            ProxyLogin = Config.GetProperty(ProxyLoginKey, String.Empty, null, CryptAction.Crypt, ProxySection);
            
            ProxyPassword = Config.GetProperty(ProxyPasswordKey, String.Empty, null, CryptAction.Crypt, ProxySection);

            APIKey = Config.GetProperty(APICodeKey, String.Empty, DerpiAPI.CheckAPI, CryptAction.Crypt, null, false, APISection);
            
            Localization = new ProgramLocalization(LanguageCode.GetValue());
            Logger = new Logger();
            
            WebProxy = null;
            
            if (ProxyAddress.IsValid && ProxyAddress.Value != NetworkUtils.LocalhostIP && ProxyPort.IsValid)
            {
                WebProxy = ProxyUtils.CreateProxy(ProxyAddress.Value, ProxyPort.Value, ProxyLogin.Value, ProxyPassword.Value);
            }
        }
        
        public static readonly IConfigProperty<String> APIKey;
        public static readonly IConfigProperty<String> CurrentDownloadFolder;
        public static readonly IConfigProperty<String> CurrentDownloadFileName;
        public static readonly IConfigProperty<Int32> LanguageCode;
        public static readonly IConfigProperty<Boolean> ExistFileRewrite;
        public static readonly IConfigProperty<Boolean> ConvertSVGToPNG;
        public static readonly IConfigProperty<Boolean> NotStrictAPICheck;
        public static readonly IConfigProperty<Boolean> QueueAutoDownload;
        public static readonly IConfigProperty<Boolean> ForceClose;
        public static readonly IConfigProperty<String> ProxyAddress;
        public static readonly IConfigProperty<Int32> ProxyPort;
        public static readonly IConfigProperty<String> ProxyLogin;
        public static readonly IConfigProperty<String> ProxyPassword;
        
        public static event Handlers.EmptyHandler ProxyChanged;

        public static String CurrentDownloadPath
        {
            get
            {
                return Path.Combine(CurrentDownloadFolder.Value, CurrentDownloadFileName.Value);
            }
        }
        
        public static WebProxy WebProxy
        {
            get
            {
                return webProxy;
            }
            set
            {
                if (webProxy == value)
                {
                    return;
                }

                webProxy = value;
                ProxyChanged?.Invoke();
            }
        }
        
        private static WebProxy webProxy;
    }
}