// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using Common_Library;
using Common_Library.GUI.WinForms.Controls;
using Common_Library.Images;
using Common_Library.Localization;
using Common_Library.Logger;
using Common_Library.Utils.IO;
using Derpi_Downloader.Download;
using Derpi_Downloader.Json;
using Derpi_Downloader.Settings;

namespace Derpi_Downloader.Forms
{
    public partial class DownloadTaskControl : LocalizationControl
    {
        public new String Text
        {
            get
            {
                return _searchQueryTextBox.Text;
            }
        }

        public Boolean EmptySearchRequest
        {
            get
            {
                return String.IsNullOrEmpty(Text);
            }
        }

        public Boolean DownloadPathIsDefault
        {
            get
            {
                return Globals.CurrentDownloadPath.Equals(_downloadPathTextBox.Text);
            }
        }

        public DownloadTaskControl(DownloadRequest request)
            : this(request.SearchQuery, request.DownloadPath, request.AutoDownload, request.ImageType)
        {
        }

        public DownloadTaskControl(String search, String path, RequestImageType imageType)
            : this(search, path, Globals.QueueAutoDownload.GetValue(), imageType)
        {
        }

        public DownloadTaskControl(String search, String path, Boolean autoStartDownload, RequestImageType imageType = RequestImageType.Image)
        {
            InitializeComponent();
            _searchQueryTextBox.Text = search;
            _downloadPathTextBox.Text = path;

            Log(new LogMessage(Globals.Localization.Created, MessageType.Action));

            if (autoStartDownload && Globals.APIKey.IsValid)
            {
                OnStartDownloadButton_Click(null, EventArgs.Empty);
            }
        }


        private void OnTextChanged()
        {
            _startDownloadButton.Enabled = Task?.IsStarted != true && Globals.APIKey.IsValid &&
                                           _searchQueryTextBox.ValidSearchQuery &&
                                           _downloadPathTextBox.IsValid();
        }

        protected override void UpdateText()
        {
            if (Disposing || IsDisposed)
            {
                return;
            }

            _searchQueryLabel.Text = Globals.Localization.SearchQueryLabel;
            _downloadPathLabel.Text = Globals.Localization.DownloadPathLabel;
            _startDownloadButton.Text = Task?.IsCompleted != true && Task?.IsInvalid != true ? Globals.Localization.AddTaskButton : Globals.Localization.Close;
            _pauseResumeButton.Text = Task == null ? String.Empty : Task.IsPaused ? Globals.Localization.ClickForResume : Globals.Localization.ClickForPause;
            _downloadPathTextBox.PathTypeChangeToRelativeToolTip = Globals.Localization.PathTypeChangeButtonToRelativeToolTip;
            _downloadPathTextBox.PathTypeChangeToAbsoluteToolTip = Globals.Localization.PathTypeChangeButtonToAbsoluteToolTip;
            _downloadPathTextBox.PathFormatHelpToolTip = Globals.Localization.FormatHelpButtonToolTip;
            _downloadPathTextBox.PathDialogToolTip = Globals.Localization.FolderDialogButtonToolTip;
            _helpToolTip.SetToolTip(_removeOrRestartDownloadButton,
                Task?.IsCompleted != true && Task?.IsInvalid != true
                    ? Globals.Localization.CloseDownloadTaskControlToolTip
                    : Globals.Localization.ReuseDownloadTaskControlToolTip);
            LogRichTextBox?.UpdateLog();
            _removeOrRestartDownloadButton.Image =
                new Bitmap(Task?.IsCompleted != true && Task?.IsInvalid != true ? Images.Others.XButton : Images.Lineal.Reuse,
                    new Size(_removeOrRestartDownloadButton.Size.Width / 2, _removeOrRestartDownloadButton.Size.Height / 2));
        }

        public DownloadTask Task { get; private set; }

        public Int32 CurrentDownloadedImages
        {
            get
            {
                return _downloadValueLabel.CurrentValue;
            }
        }

        public Int32 CountOfImages
        {
            get
            {
                return _downloadValueLabel.MaximumValue;
            }
        }

        public Boolean IsFullDownload
        {
            get
            {
                return CurrentDownloadedImages == CountOfImages;
            }
        }

        public event Handlers.EmptyHandler Completed;
        public event Handlers.EmptyHandler NeedClosing;

        public void Start()
        {
            if (Task?.IsCompleted != true && Task?.IsInvalid != true)
            {
                StartDownloadAsync(_searchQueryTextBox.Text);
            }
        }

        private void OnStartDownloadButton_Click(Object sender, EventArgs e)
        {
            if (Task?.IsCompleted != true && Task?.IsInvalid != true)
            {
                Start();
                return;
            }

            Close();
        }

        private void OnPauseResumeButton_Click(Object sender, EventArgs e)
        {
            if (Task == null)
            {
                return;
            }

            if (Task.IsPaused)
            {
                Task.Resume();
                return;
            }

            Task.Pause();
        }

        private void BeforeTaskStarted()
        {
            _startDownloadButton.Enabled = false;
            _searchQueryTextBox.ReadOnly = true;
            _downloadPathTextBox.Enabled = false;
            _downloadProgressBar.Step = 1;
            _downloadProgressBar.Value = 0;
        }

        private void OnTaskStarted()
        {
            _startDownloadButton.Visible = false;
            _pauseResumeButton.Enabled = true;
            _pauseResumeButton.Visible = true;
            Globals.Logger.Log(new LogMessage(Globals.Localization.DownloadForSearchStarted, MessageType.Good, new[] {Task.SearchQuery}));
            Log(new LogMessage(Globals.Localization.DownloadStarted, MessageType.Good));
            UpdateText();
        }

        private void OnTaskCompleted(EventQueueList<Json.Image> images, EventQueueList<LogMessage> log)
        {
            Globals.Logger.Log(new LogMessage(Globals.Localization.DownloadForSearchCompleted, MessageType.Good, new[] {Task.SearchQuery}));
            Log(new LogMessage(Globals.Localization.DownloadCompleted, MessageType.Good));
            _pauseResumeButton.Visible = false;
            _pauseResumeButton.Enabled = false;
            _startDownloadButton.ForeColor = IsFullDownload ? _startDownloadButton.ForeColor : Color.Red;
            _startDownloadButton.Enabled = true;
            _startDownloadButton.Visible = true;
            _downloadValueLabel.ForeColor = IsFullDownload ? _downloadValueLabel.ForeColor : Color.Red;
            UpdateText();
            Completed?.Invoke();
        }

        private void OnTaskInvalid(EventQueueList<LogMessage> log)
        {
            Globals.Logger.Log(new LogMessage(Globals.Localization.DownloadForSearchInvalid, MessageType.Error, new[] {Task?.SearchQuery ?? String.Empty}));
            _pauseResumeButton.Visible = false;
            _pauseResumeButton.Enabled = false;
            _startDownloadButton.Enabled = true;
            _startDownloadButton.Visible = true;
            UpdateText();
        }

        private void OnTaskInitialized(EventQueueList<LogMessage> log)
        {
            _downloadProgressBar.Maximum = Task.MaximumImages;
            _downloadValueLabel.CurrentValue = 0;
            _downloadValueLabel.MaximumValue = Task.MaximumImages;
            _downloadValueLabel.Visible = true;
            Task.ImageSaved += OnImageSaved;
        }

        private void AfterTaskCreated()
        {
            _startDownloadButton.Enabled = false;
            Task.Log.OnAdd += Log;
            Task.Started += OnTaskStarted;
            Task.Completed += OnTaskCompleted;
            Task.Invalid += OnTaskInvalid;
            Task.Initialized += OnTaskInitialized;
            Task.Paused += OnTaskPaused;
            Task.Resumed += OnTaskResumed;
        }

        private void OnDownloadFinally()
        {
            try
            {
                Task.ImageSaved -= OnImageSaved;
                Task.Log.OnAdd -= Log;
                Task.Started -= OnTaskStarted;
                Task.Completed -= OnTaskCompleted;
                Task.Invalid -= OnTaskInvalid;
                Task.Initialized -= OnTaskInitialized;
                Task.Paused -= OnTaskPaused;
                Task.Resumed -= OnTaskResumed;
            }
            catch (Exception)
            {
                //ignored
            }

            UpdateText();
        }

        private void Log(ref LogMessage message)
        {
            Log(message);
        }

        private void Log(LogMessage message)
        {
            LogRichTextBox?.Log(message);
        }

        private void OnTaskPaused()
        {
            UpdateText();
        }

        private void OnTaskResumed()
        {
            UpdateText();
        }

        private void OnImageSaved(Json.Image image)
        {
            _downloadProgressBar.PerformStep();
            _downloadValueLabel.PerformStep();
        }

        private void OnRemoveOrRestartDownloadButtonClick(Object sender, EventArgs e)
        {
            if (Task == null)
            {
                Close();
                return;
            }

            if (!Task.IsInvalid && !Task.IsCompleted)
            {
                Globals.Logger.Log(new LogMessage(Globals.Localization.DownloadTaskIsCanceled, MessageType.Warning, new[] {Task.SearchQuery ?? String.Empty}));
                Task.RemoveTask();
                Close();
                return;
            }

            Task.RemoveTask();
            Task = null;
            _searchQueryTextBox.ReadOnly = false;
            _downloadPathTextBox.Enabled = true;
            _pauseResumeButton.Visible = false;
            _pauseResumeButton.Enabled = false;
            _startDownloadButton.ResetForeColor();
            _startDownloadButton.Enabled = true;
            _startDownloadButton.Visible = true;
            _downloadProgressBar.Value = 0;
            _downloadValueLabel.CurrentValue = 0;
            _downloadValueLabel.MaximumValue = 0;
            _downloadValueLabel.ResetForeColor();
            _downloadValueLabel.Visible = false;
            UpdateText();
        }

        private async void StartDownloadAsync(String searchRequest)
        {
            BeforeTaskStarted();

            Task = new DownloadTask(searchRequest, PathUtils.IsValidFilePath(_downloadPathTextBox.Text) ? _downloadPathTextBox.Text : null);
            try
            {
                AfterTaskCreated();

                if (!Task.IsInvalid)
                {
                    await Task.InitializeTaskAsync().ConfigureAwait(true);
                }

                if (!Task.IsInvalid)
                {
                    await Task.StartTaskAsync().ConfigureAwait(true);
                }
            }
            catch (Exception e)
            {
                Log(new LogMessage($"{e.Message}{LocalizationBase.NewLine}{e.StackTrace}", MessageType.CriticalError));
                OnTaskInvalid(Task.Log);
            }

            OnDownloadFinally();
        }

        public void Close()
        {
            NeedClosing?.Invoke();
            Dispose();
        }

        protected override void Dispose(Boolean disposing)
        {
            Task?.Dispose();
            Task = null;
            NeedClosing = null;
            Completed = null;
            base.Dispose(disposing);
        }
    }
}