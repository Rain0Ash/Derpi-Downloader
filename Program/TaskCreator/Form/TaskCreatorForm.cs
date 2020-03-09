// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using Common_Library.GUI.WinForms.Forms;
using Derpi_Downloader.API;
using Derpi_Downloader.Settings;

namespace Derpi_Downloader.Forms
{
    public sealed partial class TaskCreatorForm : ParentForm
    {
        public Boolean IsManualClose;
        
        public delegate void DownloadRequestHandler(DownloadRequest request);

        public event DownloadRequestHandler AddTaskControl;
        public TaskCreatorForm()
        {
            InitializeComponent();
        }

        protected override void UpdateText()
        {
            Text = Globals.Localization.DownloadForm;
            _searchQueryLabel.Text = Globals.Localization.SearchQueryLabel;
            _downloadPathLabel.Text = Globals.Localization.DownloadPathLabel;
            _downloadPathTextBox.PathTypeChangeToRelativeToolTip = Globals.Localization.PathTypeChangeButtonToRelativeToolTip;
            _downloadPathTextBox.PathTypeChangeToAbsoluteToolTip = Globals.Localization.PathTypeChangeButtonToAbsoluteToolTip;
            _downloadPathTextBox.PathFormatHelpToolTip = Globals.Localization.FormatHelpButtonToolTip;
            _downloadPathTextBox.PathDialogToolTip = Globals.Localization.FolderDialogButtonToolTip;
            _queueAutoDownloadCheckBox.Text = Globals.Localization.QueueAutoDownloadCheckBox;
            _addTaskButton.Text = Globals.Localization.AddTaskButton;
            _helpToolTip.SetToolTip(_queueAutoDownloadCheckBox, Globals.Localization.QueueAutoDownloadCheckBoxToolTip);
        }

        private void OnAddTaskButton_Click(Object sender, EventArgs e)
        {
            if (!DerpiAPI.CheckSearchRequest(_searchQueryTextBox.Text))
            {
                return;
            }
            
            AddTaskControl?.Invoke(new DownloadRequest(_searchQueryTextBox.Text, _downloadPathTextBox.Text, _queueAutoDownloadCheckBox.Checked));
            if (!IsManualClose || ModifierKeys == Keys.Control || ModifierKeys == Keys.Shift)
            {
                Close();
            }
            
            _searchQueryTextBox.Text = String.Empty;
        }

        private void OnTextChanged()
        {
            _addTaskButton.Enabled = _searchQueryTextBox.ValidSearchQuery && _downloadPathTextBox.IsValid();
        }
    }
}