// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using Common_Library.GUI.WinForms.Forms;
using Common_Library.Logger;
using Common_Library.Types.Other;
using Common_Library.Utils;
using Common_Library.Utils.IO;

namespace Derpi_Downloader.Additionals.AuthorsList
{
    using Derpi_Downloader.Settings;

    public partial class AuthorsListForm : ParentForm
    {
        public AuthorsListForm()
        {
            InitializeComponent();

            String path = StringUtils.BeforeFormatVariables(Globals.CurrentDownloadPath);

            if (String.IsNullOrEmpty(path) || !Common_Library.LongPath.Directory.Exists(path))
            {
                return;
            }

            PathObject pathObject = new PathObject(path, PathType.Folder, PathStatus.Exist) {Recursive = true};
            _includePathListBox.ListBox.Add(pathObject);
        }

        protected override void UpdateText()
        {
            Text = Globals.Localization.MakeAuthorsListButtonToolTip;
            _includePathLabel.Text = Globals.Localization.IncludedPathsLabel;
            _excludePathLabel.Text = Globals.Localization.ExcludedPathsLabel;
            _artistsListLabel.Text = Globals.Localization.ArtistsListLabel;
            _startButton.Text = Globals.Localization.Perform;
            _includePathListBox.RecursiveButtonToolTip = Globals.Localization.ChangePathRecursiveToolTip;
            _includePathListBox.AddButtonToolTip = Globals.Localization.AddPathToolTip;
            _includePathListBox.RemoveButtonToolTip = Globals.Localization.RemovePathToolTip;
            _excludePathListBox.RecursiveButtonToolTip = Globals.Localization.ChangePathRecursiveToolTip;
            _excludePathListBox.AddButtonToolTip = Globals.Localization.AddPathToolTip;
            _excludePathListBox.RemoveButtonToolTip = Globals.Localization.RemovePathToolTip;
        }

        private async void ShowArtists()
        {
            _startButton.Enabled = false;
            Globals.Logger.Log(new LogMessage(Globals.Localization.AuthorListCreating, MessageType.Good));
            using AuthorsList authorsList = new AuthorsList(_includePathListBox.ListBox.Items.OfType<PathObject>(),
                _excludePathListBox.ListBox.Items.OfType<PathObject>());

            if (authorsList.FilesForAnalyzeFound <= 0)
            {
                Globals.Logger.Log(new LogMessage(Globals.Localization.AuthorListFilesNotFound, MessageType.Warning));
                _startButton.Enabled = true;
                return;
            }

            Globals.Logger.Log(new LogMessage(Globals.Localization.AuthorListFilesFound, MessageType.Good, new Object[] {authorsList.FilesForAnalyzeFound}));
            _progressBar.Minimum = 0;
            _progressBar.Value = 0;
            _progressBar.Maximum = authorsList.FilesForAnalyzeFound;
            _stepLabel.CurrentValue = 0;
            _stepLabel.MaximumValue = authorsList.FilesForAnalyzeFound;
            authorsList.FileAnalyzed += () =>
            {
                _progressBar.PerformStep();
                _stepLabel.PerformStep();
            };
            _artistsRichTextBox.Text = await authorsList.GetArtistsAsync().ConfigureAwait(true);

            Globals.Logger.Log(new LogMessage(Globals.Localization.FilesAnalyzed, MessageType.Action, new Object[] {authorsList.CurrentFilesAnalyzed}));
            Globals.Logger.Log(new LogMessage(Globals.Localization.AuthorListCompleted, MessageType.Good));

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