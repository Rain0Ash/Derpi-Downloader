// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Net;
using Common_Library;
using Common_Library.Logger;
using Common_Library.Utils;
using Derpi_Downloader.Localization;
using Derpi_Downloader.RegKeys;
using Path = Common_Library.LongPath.Path;

namespace Derpi_Downloader.Settings
{
    public static class Globals
    {
        public const String Version = "1.2";
        public const String DefaultDownloadFolder = @"Download";
        public const String DefaultDownloadFileName = @"{artist?}\{name}.{ext}";
        
        public static readonly ProgramLocalization Localization;
        public static readonly Logger Logger;

        static Globals()
        {
            Localization = new ProgramLocalization(ConfigKeys.LanguageCode);
            Logger = new Logger();
            APIKey = ConfigKeys.APIKey;
            CurrentDownloadFolder = ConfigKeys.CurrentDownloadFolder;
            CurrentDownloadFileName = ConfigKeys.CurrentDownloadFileName;
            ExistFileRewrite = ConfigKeys.ExistFileRewrite;
            ConvertSVGToPNG = ConfigKeys.ConvertSVGToPNG;
            QueueAutoDownload = ConfigKeys.QueueAutoDownload;
            
            WebProxy = null;
            WebProxyAddress = ConfigKeys.ProxyAddress;
            WebProxyPort = ConfigKeys.ProxyPort;
            WebProxyLogin = ConfigKeys.ProxyLogin;
            WebProxyPassword = ConfigKeys.ProxyPassword;

            if (NetworkUtils.ValidateIPv4(WebProxyAddress) && WebProxyAddress != NetworkUtils.LocalhostIP && NetworkUtils.CheckPort(WebProxyPort))
            {
                WebProxy = ProxyUtils.CreateProxy(WebProxyAddress, WebProxyPort, WebProxyLogin, WebProxyPassword);
            }
        }

        public static event Handlers.EmptyHandler CurrentDownloadFolderChanged;
        public static event Handlers.EmptyHandler CurrentDownloadFileNameChanged;
        public static event Handlers.EmptyHandler APIKeyChanged;
        public static event Handlers.EmptyHandler ExistFileRewriteChanged;
        public static event Handlers.EmptyHandler ConvertSVGToPNGChanged;
        public static event Handlers.EmptyHandler QueueAutoDownloadChanged;
        public static event Handlers.EmptyHandler ProxyChanged;
        public static String APIKey
        {
            get
            {
                return apiKey;
            }
            set
            {
                if (apiKey == value)
                {
                    return;
                }

                apiKey = value;
                APIKeyChanged?.Invoke();
            }
        }

        public static String CurrentDownloadFolder
        {
            get
            {
                return currentDownloadFolder;
            }
            set
            {
                if (currentDownloadFolder == value)
                {
                    return;
                }

                currentDownloadFolder = value;
                CurrentDownloadFolderChanged?.Invoke();
            }
        }
        
        public static String CurrentDownloadFileName
        {
            get
            {
                return currentDownloadFileName;
            }
            set
            {
                if (currentDownloadFileName == value)
                {
                    return;
                }

                currentDownloadFileName = value;
                CurrentDownloadFileNameChanged?.Invoke();
            }
        }

        public static String CurrentDownloadPath
        {
            get
            {
                return Path.Combine(CurrentDownloadFolder, CurrentDownloadFileName);
            }
        }

        public static Boolean ExistFileRewrite
        {
            get
            {
                return existFileRewrite;
            }
            set
            {
                if (existFileRewrite == value)
                {
                    return;
                }
                
                existFileRewrite = value;
                ExistFileRewriteChanged?.Invoke();
            }
        }
        
        public static Boolean ConvertSVGToPNG
        {
            get
            {
                return convertSVGToPNG;
            }
            set
            {
                if (convertSVGToPNG == value)
                {
                    return;
                }
                
                convertSVGToPNG = value;
                ConvertSVGToPNGChanged?.Invoke();
            }
        }

        public static Boolean QueueAutoDownload
        {
            get
            {
                return queueAutoDownload;
            }
            set
            {
                if (queueAutoDownload == value)
                {
                    return;
                }

                queueAutoDownload = value;
                QueueAutoDownloadChanged?.Invoke();
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

        public static String WebProxyAddress { get; set; }
        public static Int32 WebProxyPort { get; set; }
        public static String WebProxyLogin { get; set; }
        public static String WebProxyPassword { get; set; }
        
        private static String apiKey;
        private static String currentDownloadFolder;
        private static String currentDownloadFileName;
        private static Boolean existFileRewrite;
        private static Boolean convertSVGToPNG;
        private static Boolean queueAutoDownload;
        private static WebProxy webProxy;
    }
}