// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Drawing;
using System.Threading.Tasks;
using Common_Library.Images;
using Common_Library.Utils;
using System.Windows.Forms;
using Common_Library.GUI.WinForms.Labels;
using Common_Library.GUI.WinForms.ListBoxes;

namespace Derpi_Downloader.Additionals.AuthorsList
{
    public partial class AuthorsListForm
    {
        private void InitializeComponent()
        {
            _includePathLabel = new Label();
            _includePathListBox = new AdvancedPathListBox();
            _excludePathLabel = new Label();
            _excludePathListBox = new AdvancedPathListBox();
            _artistsListLabel = new Label();
            _artistsRichTextBox = new RichTextBox();
            _regexLabel = new Label();
            _startButton = new Button();
            _progressBar = new ProgressBar();
            _stepLabel = new CurrentMaxValueLabel();

            _includePathLabel.Location = new Point(0, 0);
            _includePathLabel.Size = new Size(300, 15);
            _includePathLabel.TextAlign = ContentAlignment.MiddleLeft;
            _includePathLabel.AutoSize = false;
            
            _includePathListBox.Location = new Point(0, 20);
            _includePathListBox.Size = new Size(300, 60);

            _excludePathLabel.Location = new Point(300, 0);
            _excludePathLabel.Size = new Size(300, 15);
            _excludePathLabel.TextAlign = ContentAlignment.MiddleLeft;
            _excludePathLabel.AutoSize = false;
            
            _excludePathListBox.Location = new Point(300, 20);
            _excludePathListBox.Size = new Size(300, 60);
            
            _artistsListLabel.Location = new Point(0, 78);
            _artistsListLabel.Size = new Size(300, 15);
            _artistsListLabel.TextAlign = ContentAlignment.MiddleLeft;
            _artistsListLabel.AutoSize = true;

            _artistsRichTextBox.Location = new Point(0, 95);
            _artistsRichTextBox.Size = new Size(300, 60);
            _artistsRichTextBox.ReadOnly = true;

            _startButton.Location = new Point(0, 160);
            _startButton.Size = new Size(300, 30);
            _startButton.Click += (sender, args) => Task.Run(ShowArtists);
            
            _progressBar.Location = new Point(0, 190);
            _progressBar.Size = new Size(600, 10);
            _progressBar.Step = 1;
            _progressBar.Style = ProgressBarStyle.Continuous;
            
            _stepLabel.Location = new Point(_startButton.Size.Width, _startButton.Location.Y);
            _stepLabel.Size = new Size(300, 30);
            _stepLabel.AutoSize = false;
            _stepLabel.TextAlign = ContentAlignment.MiddleLeft;
            _stepLabel.Font = new Font(Font.Name, Font.Size + 3);
            _stepLabel.DisplayType = MathUtils.DisplayType.ValueAndPercent;
            _stepLabel.PercentFractionalCount = 2;
            
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            AutoScaleDimensions = new SizeF(7F, 15F);
            ClientSize = new Size(700, 230);
            AutoScaleMode = AutoScaleMode.Font;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Controls.Add(_includePathLabel);
            Controls.Add(_includePathListBox);
            Controls.Add(_excludePathLabel);
            Controls.Add(_excludePathListBox);
            Controls.Add(_artistsListLabel);
            Controls.Add(_artistsRichTextBox);
            Controls.Add(_startButton);
            Controls.Add(_progressBar);
            Controls.Add(_stepLabel);
            Icon = ImageUtils.IconFromImage(Images.Line.Tech);
        }

        private Label _includePathLabel;
        private AdvancedPathListBox _includePathListBox;
        private Label _excludePathLabel;
        private AdvancedPathListBox _excludePathListBox;
        private Label _artistsListLabel;
        private RichTextBox _artistsRichTextBox;
        private Label _regexLabel;
        private ProgressBar _progressBar;
        private CurrentMaxValueLabel _stepLabel;
        private Button _startButton;
    }
}