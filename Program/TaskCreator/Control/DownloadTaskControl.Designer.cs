// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;
using System.Drawing;
using Common_Library.GUI.WinForms.Labels;
using Common_Library.GUI.WinForms.RichTextBoxes;
using Common_Library.GUI.WinForms.ToolTips;
using Common_Library.Utils.IO;
using Common_Library.Utils.Math;
using Derpi_Downloader.Settings;
using Derpi_Downloader.Json;
using Path = Common_Library.LongPath.Path;

namespace Derpi_Downloader.Forms
{
    public partial class DownloadTaskControl
    {
        private void InitializeComponent()
        {
            _searchQueryLabel = new Label();
            _downloadPathLabel = new Label();
            _searchQueryTextBox = new SearchQueryTextBox();
            _downloadPathTextBox = new DerpiAdvancedPathTextBox();
            _downloadProgressBar = new ProgressBar();
            _logRichTextBox = new LoggerRichTextBox();
            _startDownloadButton = new Button();
            _removeOrRestartDownloadButton = new Button();
            _pauseResumeButton = new Button();
            _downloadValueLabel = new CurrentMaxValueLabel();
            _helpToolTip = new HelpToolTip();
            SuspendLayout();

            _searchQueryLabel.Location = new Point(2, -3);
            _searchQueryLabel.Size = new Size(200, 20);
            _searchQueryLabel.AutoSize = false;
            _searchQueryLabel.TextAlign = ContentAlignment.MiddleLeft;

            _downloadPathLabel.Location = new Point(2, 40);
            _downloadPathLabel.Size = new Size(200, 20);
            _downloadPathLabel.AutoSize = false;
            _downloadPathLabel.TextAlign = ContentAlignment.MiddleLeft;

            _searchQueryTextBox.Location = new Point(5, 18);
            _searchQueryTextBox.Size = new Size(330, 22);
            _searchQueryTextBox.Multiline = false;
            _searchQueryTextBox.TextAlign = HorizontalAlignment.Left;
            _searchQueryTextBox.AutoSize = false;
            _searchQueryTextBox.TextChanged += (sender, args) => OnTextChanged();

            _downloadPathTextBox.Location = new Point(3, 60);
            _downloadPathTextBox.Size = new Size(332, 22);
            _downloadPathTextBox.AutoSize = false;
            _downloadPathTextBox.PathType = PathType.File;
            _downloadPathTextBox.UpdateAvailableFormatingParts(typeof(Json.Image));
            _downloadPathTextBox.TextChanged += (sender, args) => OnTextChanged();
            _downloadPathTextBox.Text = Globals.CurrentDownloadPath;
            _downloadPathTextBox.PathBeenSelected += str => _downloadPathTextBox.Text = Path.Combine(str, Globals.CurrentDownloadFileName.GetValue());

            _startDownloadButton.Location = new Point(3, 90);
            _startDownloadButton.Size = new Size(302, 25);
            _startDownloadButton.TextAlign = ContentAlignment.MiddleCenter;
            _startDownloadButton.Click += OnStartDownloadButton_Click;

            _pauseResumeButton.Location = new Point(_startDownloadButton.Location.X, _startDownloadButton.Location.Y);
            _pauseResumeButton.Size = new Size(_startDownloadButton.Size.Width, _startDownloadButton.Size.Height);
            _pauseResumeButton.TextAlign = ContentAlignment.MiddleCenter;
            _pauseResumeButton.Visible = false;
            _pauseResumeButton.Enabled = false;
            _pauseResumeButton.Click += OnPauseResumeButton_Click;

            _removeOrRestartDownloadButton.Location = new Point(304, 90);
            _removeOrRestartDownloadButton.Size = new Size(30, 30);
            _removeOrRestartDownloadButton.Click += OnRemoveOrRestartDownloadButtonClick;

            _logRichTextBox.Size = new Size(200, 75);
            _logRichTextBox.Location = new Point(340, 15);
            _logRichTextBox.Font = new Font(Font.Name, Font.Size - 1);
            _logRichTextBox.BorderStyle = BorderStyle.Fixed3D;
            _logRichTextBox.Reversed = false;

            _downloadProgressBar.Size = new Size(_startDownloadButton.Size.Width - 3, 5);
            _downloadProgressBar.Location = new Point(5, 115);
            _downloadProgressBar.Style = ProgressBarStyle.Continuous;

            _downloadValueLabel.Size = new Size(_logRichTextBox.Size.Width, 30);
            _downloadValueLabel.Location = new Point(_logRichTextBox.Location.X, _logRichTextBox.Location.Y + _logRichTextBox.Size.Height);
            _downloadValueLabel.AutoSize = false;
            _downloadValueLabel.Visible = false;
            _downloadValueLabel.TextAlign = ContentAlignment.MiddleLeft;
            _downloadValueLabel.Font = new Font(Font.Name, Font.Size + 3);
            _downloadValueLabel.DisplayType = MathUtils.DisplayType.ValueAndPercent;
            _downloadValueLabel.PercentFractionalCount = 2;

            Controls.Add(_searchQueryLabel);
            Controls.Add(_downloadPathLabel);
            Controls.Add(_searchQueryTextBox);
            Controls.Add(_downloadPathTextBox);
            Controls.Add(_startDownloadButton);
            Controls.Add(_removeOrRestartDownloadButton);
            Controls.Add(_pauseResumeButton);
            Controls.Add(_logRichTextBox);
            Controls.Add(_downloadValueLabel);
            Controls.Add(_downloadProgressBar);
            AcceptButton = _startDownloadButton;
            Globals.APIKey.Changed += OnTextChanged;
            ResumeLayout();
        }


        private ProgressBar _downloadProgressBar;

        private LoggerRichTextBox _logRichTextBox;

        private LoggerRichTextBox LogRichTextBox
        {
            get
            {
                return _logRichTextBox.IsDisposed ? null : _logRichTextBox;
            }
        }

        private Button _startDownloadButton;
        private Button _removeOrRestartDownloadButton;
        private Button _pauseResumeButton;
        private CurrentMaxValueLabel _downloadValueLabel;
        private SearchQueryTextBox _searchQueryTextBox;
        private DerpiAdvancedPathTextBox _downloadPathTextBox;
        private Label _downloadPathLabel;
        private Label _searchQueryLabel;
        private HelpToolTip _helpToolTip;
    }
}