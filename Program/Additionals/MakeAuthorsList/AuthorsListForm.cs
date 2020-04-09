// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Common_Library.GUI.WinForms.Forms;
using Common_Library.Logger;
using Common_Library.Utils;
using Common_Library.Utils.IO;
using Common_Library.Watchers;

namespace Derpi_Downloader.Additionals.AuthorsList
{
    using Derpi_Downloader.Settings;

    public partial class AuthorsListForm : ParentForm
    {
        public AuthorsListForm()
        {
            InitializeComponent();

            String path = StringUtils.BeforeFormatVariables(Globals.CurrentDownloadPath);

            if (String.IsNullOrEmpty(path) || !Directory.Exists(path))
            {
                return;
            }

            FSWatcher pathObject = new FSWatcher(path, PathType.Folder, PathStatus.Exist) {Recursive = true};
            _includePathListView.Add(pathObject);
            _regexListView.Add(AdditionalsAPI.DefaultDerpiBooruNamePattern);
            _regexListView.Add(AdditionalsAPI.DefaultDeviantArtNamePattern);
        }

        public override void UpdateText()
        {
            Text = Globals.Localization.MakeAuthorsListButtonToolTip;
            _includePathLabel.Text = Globals.Localization.IncludedPathsLabel;
            _excludePathLabel.Text = Globals.Localization.ExcludedPathsLabel;
            _artistsListLabel.Text = Globals.Localization.ArtistsListLabel;
            _startButton.Text = Globals.Localization.Perform;
        }

        private async void ShowArtists()
        {
            _startButton.Enabled = false;
            _includePathListView.Enabled = false;
            _excludePathListView.Enabled = false;
            _regexListView.Enabled = false;
            
            Globals.Logger.Log(new LogMessage(Globals.Localization.AuthorListCreating, MessageType.Good));
            AuthorsList authorsList = new AuthorsList(
                _includePathListView.Items.OfType<ListViewItem>().Select(item => item.Tag is FSWatcher watcher ? watcher : new FSWatcher(item.Text)),
                _excludePathListView.Items.OfType<ListViewItem>().Select(item => item.Tag is FSWatcher watcher ? watcher : new FSWatcher(item.Text)),
                            _regexListView.Items.OfType<ListViewItem>().Select(item => item.Text));

            if (authorsList.FilesForAnalyzeFound <= 0)
            {
                Globals.Logger.Log(new LogMessage(Globals.Localization.AuthorListFilesNotFound, MessageType.Warning));
                _includePathListView.Enabled = true;
                _excludePathListView.Enabled = true;
                _regexListView.Enabled = true;
                _startButton.Enabled = true;
                return;
            }

            Globals.Logger.Log(new LogMessage(Globals.Localization.AuthorListFilesFound, MessageType.Good, new Object[] {authorsList.FilesForAnalyzeFound}));
            _progressBar.Minimum = 0;
            _progressBar.Value = 0;
            _progressBar.Maximum = authorsList.FilesForAnalyzeFound;
            _stepLabel.CurrentValue = 0;
            _stepLabel.MaximumValue = authorsList.FilesForAnalyzeFound;

            System.Timers.Timer timer = new System.Timers.Timer(10);

            timer.Elapsed += (sender, args) => _progressBar.Value = _stepLabel.CurrentValue = authorsList.CurrentFilesAnalyzed;
            
            timer.Start();
                
            _artistsRichTextBox.Text = await authorsList.GetArtistsAsync().ConfigureAwait(true);
            
            timer.Stop();
            timer.Dispose();
            
            _progressBar.Value = _stepLabel.CurrentValue = authorsList.CurrentFilesAnalyzed;

            Globals.Logger.Log(new LogMessage(Globals.Localization.FilesAnalyzed, MessageType.Action, new Object[] {authorsList.CurrentFilesAnalyzed}));
            Globals.Logger.Log(new LogMessage(Globals.Localization.AuthorListCompleted, MessageType.Good));

            _includePathListView.Enabled = true;
            _excludePathListView.Enabled = true;
            _regexListView.Enabled = true;
            _startButton.Enabled = true;

            if (this != ActiveForm)
            {
                return;
            }

            _artistsRichTextBox.Focus();
            _artistsRichTextBox.SelectAll();
            _artistsRichTextBox.Focus();
        }
    }
}