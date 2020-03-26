﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Windows.Forms;
using Common_Library.GUI.WinForms.Forms;
using Common_Library.Images;
using Common_Library.Logger;
using Derpi_Downloader.API;
using Derpi_Downloader.Settings;

namespace Derpi_Downloader.Forms
{
    public partial class MainForm : CenterForm
    {
        private String[] _args;

        public MainForm(String[] args = null)
        {
            _args = args;
            InitializeComponent();
            FormClosing += OnForm_Closing;
        }

        protected override void UpdateText()
        {
            #if DEBUG
            Text = Globals.Localization.MainForm + @" " + @"DEBUG";
            #else
            Text = Globals.Localization.MainForm;
            #endif

            _helpToolTip.SetToolTip(_taskCreatorButton, Globals.Localization.DownloadForm);
            _helpToolTip.SetToolTip(_settingsButton, Globals.Localization.SettingsForm);
            _helpToolTip.SetToolTip(_aboutButton, Globals.Localization.AboutProgramTitle);
            _helpToolTip.SetToolTip(_additionalsButton, Globals.Localization.AdditionalFunctions);
            _queueRequestListBox.AddButtonToolTip = Globals.Localization.AddRequestToDownloadQueueToolTip;
            _queueRequestListBox.RemoveButtonToolTip = Globals.Localization.RemoveRequestToDownloadQueueToolTip;
            _logRichTextBox.UpdateLog();
        }

        private void OnForm_Shown(Object sender, EventArgs e)
        {
            CenterToScreen();
            Globals.Logger.Log(new LogMessage(Globals.Localization.ProgramSuccessfullyStarted, MessageType.Action));
            if (!Globals.APIKey.IsValid)
            {
                new MessageForm(Globals.Localization.FirstKnowText,
                    Globals.Localization.FirstKnowTitle, Images.Basic.Information,
                    Images.Basic.Information, MessageBoxButtons.OK,
                    new[] {Globals.Localization.Accept}).ShowDialog();
                _settingsForm.ShowDialog();
            }

            InitializeDownloadTaskControls(_args);

            _args = null;
        }

        private void OnForm_Closing(Object sender, FormClosingEventArgs e)
        {
            if (ContainsFocus && e.CloseReason == CloseReason.UserClosing && _downloadControl.HasDownload &&
                new MessageForm("Остались незавершенные загрузки\nВы действительно хотите закрыть программу?", "Закрыть программу?",
                    Resources.Resource.icon.ToBitmap(), Resources.Resource.icon.ToBitmap(), MessageBoxButtons.YesNo, new []{Globals.Localization.Yes, Globals.Localization.No}).Show() != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void InitializeDownloadTaskControls(String[] args = null)
        {
            if (!Globals.APIKey.IsValid)
            {
                return;
            }

            if (args?.Any() == true)
            {
                foreach (String arg in args)
                {
                    _downloadControl.AddDownloadTaskControl(new DownloadRequest(arg));
                }
            }
            else
            {
                if (Globals.APIKey.IsValid && _downloadControl.CurrentTasks <= 0)
                {
                    _downloadControl.AddDownloadTaskControl();
                }
            }
        }

        private void OpenTaskCreatorForm()
        {
            TaskCreatorForm taskCreatorForm = new TaskCreatorForm();
            taskCreatorForm.AddTaskControl += request => { _downloadControl.AddDownloadTaskControl(request); };
            taskCreatorForm.IsManualClose = ModifierKeys == Keys.Shift;
            taskCreatorForm.ShowDialog();
        }

        private void OnAPIKeyChanged()
        {
            _taskCreatorButton.Enabled = Globals.APIKey.IsValid;
        }
    }
}