// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Common_Library.Images;
using Common_Library.Utils;
using System.Windows.Forms;
using Common_Library.GUI.WinForms.Labels;
using Common_Library.GUI.WinForms.ListBoxes;
using Common_Library.GUI.WinForms.ListViews;
using Common_Library.Utils.IO;
using Common_Library.Utils.Math;
using Derpi_Downloader.Settings;

namespace Derpi_Downloader.Additionals.AuthorsList
{
    public partial class AuthorsListForm
    {
        private void InitializeComponent()
        {
            _includePathLabel = new Label();
            _includePathListView = new PathListView();
            _excludePathLabel = new Label();
            _excludePathListView = new PathListView();
            _artistsListLabel = new Label();
            _artistsRichTextBox = new RichTextBox();
            _regexLabel = new Label();
            _regexListView = new EditableListView();
            _startButton = new Button();
            _progressBar = new ProgressBar();
            _stepLabel = new CurrentMaxValueLabel();

            _includePathLabel.Location = new Point(0, 0);
            _includePathLabel.Size = new Size(LayoutGUI.AdditionalsPathListViewWidth, 15);
            _includePathLabel.TextAlign = ContentAlignment.MiddleLeft;
            _includePathLabel.AutoSize = false;

            _includePathListView.Location = new Point(_includePathLabel.Location.X, _includePathLabel.Location.X + _includePathLabel.Size.Height);
            _includePathListView.Size = new Size(LayoutGUI.AdditionalsPathListViewWidth, LayoutGUI.AdditionalsPathListViewHeight);
            _includePathListView.HeaderStyle = ColumnHeaderStyle.None;
            _includePathListView.View = View.Details;
            _includePathListView.PathType = PathType.Folder;
            _includePathListView.PathStatus = PathStatus.Exist;

            _excludePathLabel.Location = new Point(_includePathListView.Size.Width + LayoutGUI.DistanceBetweenControls * 2, _includePathLabel.Location.Y);
            _excludePathLabel.Size = _includePathLabel.Size;
            _excludePathLabel.TextAlign = ContentAlignment.MiddleLeft;
            _excludePathLabel.AutoSize = false;

            _excludePathListView.Location = new Point(_excludePathLabel.Location.X, _excludePathLabel.Location.Y + _excludePathLabel.Size.Height);
            _excludePathListView.Size = _includePathListView.Size;
            _excludePathListView.HeaderStyle = ColumnHeaderStyle.None;
            _excludePathListView.View = View.Details;
            _excludePathListView.PathType = PathType.Folder;
            _excludePathListView.PathStatus = PathStatus.Exist;

            _artistsListLabel.Location = new Point(_includePathListView.Location.X, _includePathListView.Location.Y + _includePathListView.Size.Height);
            _artistsListLabel.Size = _includePathLabel.Size;
            _artistsListLabel.TextAlign = ContentAlignment.MiddleLeft;
            _artistsListLabel.AutoSize = true;

            _artistsRichTextBox.Location = new Point(_artistsListLabel.Location.X, _artistsListLabel.Location.Y + _artistsListLabel.Size.Height);
            _artistsRichTextBox.Size = _includePathListView.Size;
            _artistsRichTextBox.ReadOnly = true;
            
            _regexLabel.Location = new Point(_excludePathListView.Location.X, _excludePathListView.Location.Y + _excludePathListView.Size.Height);
            _regexLabel.Size = _excludePathLabel.Size;
            _regexLabel.Text = "regex";
            _regexLabel.TextAlign = ContentAlignment.MiddleLeft;
            _regexLabel.AutoSize = true;

            _regexListView.Location = new Point(_excludePathListView.Location.X, _regexLabel.Location.Y + _regexLabel.Size.Height);
            _regexListView.Size = _artistsRichTextBox.Size;
            _regexListView.HeaderStyle = ColumnHeaderStyle.None;
            _regexListView.View = View.Details;
            _regexListView.ValidateFunc = obj => RegexUtils.IsValidRegex(obj.ToString());

            _startButton.Location = new Point(0, _artistsRichTextBox.Location.Y + _artistsRichTextBox.Size.Height);
            _startButton.Size = new Size(LayoutGUI.AdditionalsPathListViewWidth, 30);
            _startButton.Click += (sender, args) => Task.Run(ShowArtists);

            _progressBar.Location = new Point(0, _startButton.Location.Y + _startButton.Size.Height);
            _progressBar.Size = new Size(LayoutGUI.AdditionalsFormWidth, 10);
            _progressBar.Step = 1;
            _progressBar.Style = ProgressBarStyle.Continuous;

            _stepLabel.Location = new Point(_regexListView.Location.X, _startButton.Location.Y);
            _stepLabel.Size = new Size(LayoutGUI.AdditionalsFormWidth - _regexListView.Location.X, _startButton.Size.Height);
            _stepLabel.AutoSize = false;
            _stepLabel.TextAlign = ContentAlignment.MiddleLeft;
            _stepLabel.Font = new Font(Font.Name, Font.Size + 3);
            _stepLabel.DisplayType = MathUtils.DisplayType.ValueAndPercent;
            _stepLabel.PercentFractionalCount = 2;

            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            AutoScaleDimensions = new SizeF(7F, 15F);
            ClientSize = new Size(LayoutGUI.AdditionalsFormWidth, LayoutGUI.AdditionalsFormHeight);
            AutoScaleMode = AutoScaleMode.Font;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Controls.Add(_includePathLabel);
            Controls.Add(_includePathListView);
            Controls.Add(_excludePathLabel);
            Controls.Add(_excludePathListView);
            Controls.Add(_regexLabel);
            Controls.Add(_regexListView);
            Controls.Add(_artistsListLabel);
            Controls.Add(_artistsRichTextBox);
            Controls.Add(_startButton);
            Controls.Add(_progressBar);
            Controls.Add(_stepLabel);
            Icon = ImageUtils.IconFromImage(Images.Line.Tech);
        }

        private Label _includePathLabel;
        private PathListView _includePathListView;
        private Label _excludePathLabel;
        private PathListView _excludePathListView;
        private Label _artistsListLabel;
        private RichTextBox _artistsRichTextBox;
        private Label _regexLabel;
        private EditableListView _regexListView;
        private ProgressBar _progressBar;
        private CurrentMaxValueLabel _stepLabel;
        private Button _startButton;
    }
}