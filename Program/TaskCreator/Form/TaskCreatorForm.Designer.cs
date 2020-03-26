// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Drawing;
using System.Windows.Forms;
using Common_Library.GUI.WinForms.ToolTips;
using Derpi_Downloader.Json;
using Derpi_Downloader.Settings;
using Common_Library.Utils;
using Common_Library.Images;
using Common_Library.Utils.IO;
using Path = Common_Library.LongPath.Path;

namespace Derpi_Downloader.Forms
{
    public sealed partial class TaskCreatorForm
    {
        private void InitializeComponent()
        {
            _searchQueryLabel = new Label();
            _downloadPathLabel = new Label();
            _searchQueryTextBox = new SearchQueryTextBox();
            _downloadPathTextBox = new DerpiAdvancedPathTextBox();
            _queueAutoDownloadCheckBox = new CheckBox();
            _addTaskButton = new Button();
            _helpToolTip = new HelpToolTip();
            SuspendLayout();

            _searchQueryLabel.Location = new Point(2, 0);
            _searchQueryLabel.Size = new Size(200, 20);
            _searchQueryLabel.AutoSize = false;
            _searchQueryLabel.TextAlign = ContentAlignment.MiddleLeft;

            _downloadPathLabel.Location = new Point(2, 45);
            _downloadPathLabel.Size = new Size(200, 20);
            _downloadPathLabel.AutoSize = false;
            _downloadPathLabel.TextAlign = ContentAlignment.MiddleLeft;

            _searchQueryTextBox.Location = new Point(5, 20);
            _searchQueryTextBox.Size = new Size(390, 25);
            _searchQueryTextBox.Multiline = false;
            _searchQueryTextBox.TextAlign = HorizontalAlignment.Left;
            _searchQueryTextBox.AutoSize = false;
            _searchQueryTextBox.TextChanged += (sender, args) => OnTextChanged();

            _downloadPathTextBox.Location = new Point(3, 65);
            _downloadPathTextBox.Size = new Size(393, 25);
            _downloadPathTextBox.PathType = PathType.File;
            _downloadPathTextBox.UpdateAvailableFormatingParts(typeof(Search));
            _downloadPathTextBox.TextChanged += (sender, args) => OnTextChanged();
            _downloadPathTextBox.Text = Globals.CurrentDownloadPath;
            _downloadPathTextBox.PathBeenSelected += str => _downloadPathTextBox.Text = Path.Combine(str, Globals.CurrentDownloadFileName.GetValue());

            _queueAutoDownloadCheckBox.Location = new Point(5, 95);
            _queueAutoDownloadCheckBox.MinimumSize = new Size(0, 20);
            _queueAutoDownloadCheckBox.AutoSize = true;
            _queueAutoDownloadCheckBox.Checked = Globals.QueueAutoDownload.GetValue();

            _addTaskButton.Location = new Point(5, 115);
            _addTaskButton.Size = new Size(390, 30);
            _addTaskButton.Click += OnAddTaskButton_Click;

            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            AcceptButton = _addTaskButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            ClientSize = new Size(400, 150);
            AutoScaleMode = AutoScaleMode.Font;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = ImageUtils.IconFromImage(Images.Lineal.Download);
            Controls.Add(_searchQueryLabel);
            Controls.Add(_downloadPathLabel);
            Controls.Add(_searchQueryTextBox);
            Controls.Add(_downloadPathTextBox);
            Controls.Add(_queueAutoDownloadCheckBox);
            Controls.Add(_addTaskButton);
            ResumeLayout();
        }

        private Label _searchQueryLabel;
        private Label _downloadPathLabel;
        private SearchQueryTextBox _searchQueryTextBox;
        private DerpiAdvancedPathTextBox _downloadPathTextBox;
        private CheckBox _queueAutoDownloadCheckBox;
        private Button _addTaskButton;
        private HelpToolTip _helpToolTip;
    }
}