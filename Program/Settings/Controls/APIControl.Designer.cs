// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Diagnostics;
using System.Drawing;
using Common_Library.GUI.WinForms.ToolTips;
using Common_Library.Images;
using Common_Library.Utils.OS;
using Derpi_Downloader.API;
using Derpi_Downloader.Localization;
using Derpi_Downloader.Settings;

namespace System.Windows.Forms
{
    public sealed partial class APIControl
    {
        private void InitializeComponent()
        {
            _apiLabel = new Label();
            _apiTextBox = new APITextBox();
            _goToAPIPageButton = new Button();
            _toolTip = new HelpToolTip();
            _saveAPIButton = new Button();
            _changeAPIButton = new Button();
            _resetAPIButton = new Button();
            SuspendLayout();

            _apiLabel.AutoSize = true;
            _apiLabel.Location = new Point(0, 2);
            _apiLabel.Size = new Size(38, 15);
            
            _apiTextBox.Location = new Point(1, 18);
            _apiTextBox.Size = new Size(170, 26);
            _apiTextBox.AutoSize = false;
            _apiTextBox.Multiline = false;
            _apiTextBox.TextAlign = HorizontalAlignment.Center;
            _apiTextBox.Font = new Font(Font.Name, Font.Size + 1);
            _apiTextBox.MaxLength = DerpiAPI.LengthAPI;
            _apiTextBox.Text = Globals.APIKey;
            _apiTextBox.TextChanged += (sender, args) => _saveAPIButton.Enabled = _apiTextBox.CheckValidFormat();
            _apiTextBox.EnabledChanged += (sender, args) => _changeAPIButton.Enabled = !_apiTextBox.Enabled;
            _apiTextBox.TextChanged += (sender, args) => ResetAPIButtonCheck();
            _apiTextBox.Enabled = !_apiTextBox.CheckValidFormat() && Globals.APIKey == _apiTextBox.Text;
            
            _goToAPIPageButton.Location = new Point(172, 17);
            _goToAPIPageButton.Size = new Size(30, 29);
            _goToAPIPageButton.UseVisualStyleBackColor = true;
            _goToAPIPageButton.Image = new Bitmap(Images.Basic.Question, new Size(_goToAPIPageButton.Size.Width / 2, _goToAPIPageButton.Size.Height / 2));
            _goToAPIPageButton.Click += (sender, args) => ProcessUtils.OpenBrowser(ProgramLocalization.DerpiBooruProfileLink);

            _saveAPIButton.Location = new Point(202, 17);
            _saveAPIButton.Size = new Size(118, 29);
            _saveAPIButton.UseVisualStyleBackColor = true;
            _saveAPIButton.Enabled = false;
            _saveAPIButton.Click += SaveAPIButton_Click;
            _saveAPIButton.Enabled = _apiTextBox.CheckValidFormat() && !DerpiAPI.CheckAPI(Globals.APIKey);
            
            _changeAPIButton.Location = new Point(0, 46);
            _changeAPIButton.Size = new Size(202, 29);
            _changeAPIButton.UseVisualStyleBackColor = true;
            _changeAPIButton.Click += ChangeAPIButton_Click;
            _changeAPIButton.Enabled = !_apiTextBox.Enabled;
            
            _resetAPIButton.Location = new Point(_saveAPIButton.Location.X, 46);
            _resetAPIButton.Size = new Size(_saveAPIButton.Size.Width, _saveAPIButton.Size.Height);
            _resetAPIButton.UseVisualStyleBackColor = true;
            _resetAPIButton.Click += ResetAPIButton_Click;
            _resetAPIButton.Enabled = _apiTextBox.Text.Length > 0;

            Globals.APIKeyChanged += ResetAPIButtonCheck;
            
            Controls.Add(_apiLabel);
            Controls.Add(_apiTextBox);
            Controls.Add(_goToAPIPageButton);
            Controls.Add(_saveAPIButton);
            Controls.Add(_changeAPIButton);
            Controls.Add(_resetAPIButton);
            ResumeLayout();
        }

        private Label _apiLabel;
        private APITextBox _apiTextBox;
        private Button _goToAPIPageButton;
        private HelpToolTip _toolTip;
        private Button _saveAPIButton;
        private Button _changeAPIButton;
        private Button _resetAPIButton;
    }
}