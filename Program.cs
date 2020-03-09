﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Windows.Forms;
using Common_Library.App;
using Common_Library.GUI.WinForms.Forms;
using Common_Library.Logger;
using Derpi_Downloader.Forms;
using Derpi_Downloader.Settings;

namespace Derpi_Downloader
{
    public static class Program
    {
        static Program()
        {
            App.Version = new AppVersion(Globals.Version);
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            try
            {
                Application.Run(new MainForm(args));
            }
            catch (Exception exception)
            {
                try
                {
                    if (MessageForm.GetDialogResultOnException(exception,
                            Globals.Localization.CriticalExceptionOccurredText,
                            Globals.Localization.CriticalExceptionOccurred, MessageBoxButtons.RetryCancel,
                            new[]
                            {
                                Globals.Localization.CriticalExceptionOccurredRestartButton,
                                Globals.Localization.CriticalExceptionOccurredExitButton
                            }) == DialogResult.Retry)
                    {
                        Application.Restart();
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        MessageForm.GetDialogResultOnException(exception, Globals.Localization.InitializeStageException,
                            Globals.Localization.InitializeExceptionOccured, MessageBoxButtons.OK, new[] {Globals.Localization.Exit});
                    }
                    catch (Exception)
                    {
                        MessageForm.GetDialogResultOnException(exception, "Exception on initialize stage",
                            "Initialize exception occured", MessageBoxButtons.OK, new[] {"Exit"});
                    }
                }

                Environment.Exit(exception.HResult);
            }
        }
    }
}