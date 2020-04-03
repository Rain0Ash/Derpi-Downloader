// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Common_Library.GUI.WinForms.ComboBoxes;
using Common_Library.GUI.WinForms.Labels;
using Common_Library.GUI.WinForms.ToolTips;
using Common_Library.Images;
using Common_Library.Utils;
using Common_Library.Utils.IO;
using Derpi_Downloader.Json;
using Derpi_Downloader.Settings;

namespace Derpi_Downloader.Forms
{
    public sealed partial class SettingsForm
    {
        private void InitializeComponent()
        {
            _saveSettingsButton = new Button();
            _resetSettings = new Button();
            _resetAllSettings = new Button();
            _apiControl = new APIControl();
            _toolTip = new HelpToolTip();
            _downloadPathLabel = new Label();
            _downloadPathTextBox = new DerpiAdvancedPathTextBox();
            _downloadNameLabel = new Label();
            _downloadNameTextBox = new DerpiAdvancedPathTextBox();
            _languageLabel = new Label();
            _languageImagedComboBox = new LocalizationComboBox(Globals.Localization);
            _proxyButton = new Button();
            _existFileRewriteCheckBox = new CheckBox();
            _queueAutoDownloadCheckBox = new CheckBox();
            _forceCloseCheckBox = new CheckBox();
            _convertSVGToPNGCheckBox = new CheckBox();
            _notStrictAPICheckCheckBox = new CheckBox();
            _pathLabel = new AdditionalsLabel();
            _proxyForm = new ProxyForm();
            SuspendLayout();

            _languageLabel.Location = new Point(5, 2);
            _languageLabel.Size = new Size(120, 15);

            _languageImagedComboBox.Location = new Point(5, 20);
            _languageImagedComboBox.Size = new Size(120, 20);

            _proxyButton.Location = new Point(3, 46);
            _proxyButton.Size = new Size(29, 29);
            _proxyButton.Image = new Bitmap(Images.Others.Proxy, new Size(24, 24));
            _proxyButton.Click += (sender, args) => _proxyForm.ShowDialog();

            _saveSettingsButton.Location = new Point(5, 225);
            _saveSettingsButton.Size = new Size(365, 40);
            _saveSettingsButton.UseVisualStyleBackColor = true;
            _saveSettingsButton.Enabled = Globals.APIKey.IsValid;
            Globals.APIKey.Changed += () => _saveSettingsButton.Enabled = Globals.APIKey.IsValid;
            _saveSettingsButton.Click += (sender, args) => Save();
            
            _resetSettings.Location = new Point(_saveSettingsButton.Location.X + _saveSettingsButton.Size.Width, _saveSettingsButton.Location.Y);
            _resetSettings.Size = new Size(40, 40);
            _resetSettings.Image = new Bitmap(Images.Line.ResetGear, new Size(24, 24));
            _resetSettings.UseVisualStyleBackColor = true;
            _resetSettings.Click += (sender, args) => OnResetSettingsButtonClick(out _);

            _resetAllSettings.Location = new Point(_resetSettings.Location.X + _resetSettings.Size.Width, _saveSettingsButton.Location.Y);
            _resetAllSettings.Size = _resetSettings.Size;
            _resetAllSettings.Image = new Bitmap(Images.Fill.ResetGear, new Size(24, 24));
            _resetAllSettings.UseVisualStyleBackColor = true;
            _resetAllSettings.Click += (sender, args) => OnResetAllSettingsButtonClick();
            
            _apiControl.Location = new Point(130, 0);
            _apiControl.Size = new Size(325, 75);

            _downloadPathLabel.Location = new Point(2, 90);
            _downloadPathLabel.Size = new Size(210, 15);

            _downloadPathTextBox.Location = new Point(5, 110);
            _downloadPathTextBox.Size = new Size(250, 25);
            _downloadPathTextBox.AutoSize = false;
            _downloadPathTextBox.Text = Globals.CurrentDownloadFolder.GetValue();
            _downloadPathTextBox.PathTypeChangeButtonEnabled = false;
            _downloadPathTextBox.TextBox.EnableUniquenessFormatingParts = false;
            _downloadPathTextBox.UpdateAvailableFormatingParts(typeof(Json.Image));
            _downloadPathTextBox.TextChanged += (sender, args) => OnDownloadPath_TextChanged_PathCheck();
            _downloadPathTextBox.TextBox.LostFocus += (sender, args) => OnLostFocusPathCheck();
            _downloadPathTextBox.PathBeenSelected += str => _downloadPathTextBox.Text = str;

            _downloadNameLabel.Location = new Point(252, 90);
            _downloadNameLabel.Size = new Size(210, 15);

            _downloadNameTextBox.Location = new Point(255, 110);
            _downloadNameTextBox.Size = new Size(195, 25);
            _downloadNameTextBox.AutoSize = false;
            _downloadNameTextBox.PathType = PathType.LocalFile;
            _downloadNameTextBox.Text = Globals.CurrentDownloadFileName.GetValue();
            _downloadNameTextBox.PathTypeChangeButtonEnabled = false;
            _downloadNameTextBox.PathDialogButtonEnabled = false;
            _downloadNameTextBox.UpdateAvailableFormatingParts(typeof(Json.Image));
            _downloadNameTextBox.TextChanged += (sender, args) => OnDownloadName_TextChanged_PathCheck();
            _downloadNameTextBox.LostFocus += (sender, args) => OnLostFocusNameCheck();

            Globals.CurrentDownloadFolder.Changed += OnCurrentPathChanged;
            Globals.CurrentDownloadFileName.Changed += OnCurrentPathChanged;

            _pathLabel.Location = new Point(2, 140);
            _pathLabel.Size = new Size(440, 20);
            _pathLabel.AutoSize = true;
            _pathLabel.TextAlign = ContentAlignment.MiddleLeft;

            _existFileRewriteCheckBox.Location = new Point(5, 160);
            _existFileRewriteCheckBox.MinimumSize = new Size(0, 20);
            _existFileRewriteCheckBox.AutoSize = true;
            _existFileRewriteCheckBox.Checked = Globals.ExistFileRewrite.GetValue();
            _existFileRewriteCheckBox.CheckedChanged += OnExistFileRewriteCheckBox_Click;

            _queueAutoDownloadCheckBox.Location = new Point(5, 180);
            _queueAutoDownloadCheckBox.MinimumSize = new Size(0, 20);
            _queueAutoDownloadCheckBox.AutoSize = true;
            _queueAutoDownloadCheckBox.Checked = Globals.QueueAutoDownload.GetValue();
            _queueAutoDownloadCheckBox.CheckedChanged += OnQueueAutoDownloadCheckBox_Click;
            
            _forceCloseCheckBox.Location = new Point(5, 200);
            _forceCloseCheckBox.MinimumSize = new Size(0, 20);
            _forceCloseCheckBox.AutoSize = true;
            _forceCloseCheckBox.Checked = Globals.ForceClose.GetValue();
            _forceCloseCheckBox.CheckedChanged += OnForceCloseCheckBox_Click;
            
            _convertSVGToPNGCheckBox.Location = new Point(260, 160);
            _convertSVGToPNGCheckBox.MinimumSize = new Size(0, 20);
            _convertSVGToPNGCheckBox.AutoSize = true;
            _convertSVGToPNGCheckBox.Checked = Globals.ConvertSVGToPNG.GetValue();
            _convertSVGToPNGCheckBox.CheckedChanged += OnConvertSVGToPngCheckBox_Click;
            _convertSVGToPNGCheckBox.Enabled = false;
            
            _notStrictAPICheckCheckBox.Location = new Point(260, 180);
            _notStrictAPICheckCheckBox.MinimumSize = new Size(0, 20);
            _notStrictAPICheckCheckBox.AutoSize = true;
            _notStrictAPICheckCheckBox.Checked = Globals.NotStrictAPICheck.GetValue();
            _notStrictAPICheckCheckBox.CheckedChanged += OnNotStrictAPICheckCheckBox_Click;

            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            AutoScaleDimensions = new SizeF(7F, 15F);
            ClientSize = new Size(450, 270);
            AutoScaleMode = AutoScaleMode.Font;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Controls.Add(_saveSettingsButton);
            Controls.Add(_resetSettings);
            Controls.Add(_resetAllSettings);
            Controls.Add(_apiControl);
            Controls.Add(_languageLabel);
            Controls.Add(_languageImagedComboBox);
            Controls.Add(_proxyButton);
            Controls.Add(_downloadPathLabel);
            Controls.Add(_downloadPathTextBox);
            Controls.Add(_downloadNameLabel);
            Controls.Add(_downloadNameTextBox);
            Controls.Add(_existFileRewriteCheckBox);
            Controls.Add(_queueAutoDownloadCheckBox);
            Controls.Add(_forceCloseCheckBox);
            Controls.Add(_convertSVGToPNGCheckBox);
            Controls.Add(_notStrictAPICheckCheckBox);
            Controls.Add(_pathLabel);
            Icon = ImageUtils.IconFromImage(Images.Line.Settings);
            ResumeLayout();
        }

        private Label _languageLabel;
        private LocalizationComboBox _languageImagedComboBox;
        private Button _proxyButton;
        private HelpToolTip _toolTip;
        private APIControl _apiControl;
        private Button _saveSettingsButton;
        private Button _resetSettings;
        private Button _resetAllSettings;
        private Label _downloadPathLabel;
        private DerpiAdvancedPathTextBox _downloadPathTextBox;
        private Label _downloadNameLabel;
        private DerpiAdvancedPathTextBox _downloadNameTextBox;
        private CheckBox _existFileRewriteCheckBox;
        private CheckBox _queueAutoDownloadCheckBox;
        private CheckBox _forceCloseCheckBox;
        private CheckBox _convertSVGToPNGCheckBox;
        private CheckBox _notStrictAPICheckCheckBox;
        private AdditionalsLabel _pathLabel;
        private ProxyForm _proxyForm;
    }
}