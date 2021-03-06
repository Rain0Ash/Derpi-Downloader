// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Common_Library.GUI.WinForms.Forms;
using Common_Library.Images;
using Common_Library.Localization;
using Common_Library.Utils.IO;

namespace Derpi_Downloader.Settings.Forms
{
    public sealed partial class SettingsForm : ParentForm
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            OnLostFocusPathCheck();
            OnLostFocusNameCheck();
        }

        public override void UpdateText()
        {
            Text = Globals.Localization.SettingsForm;
            _languageLabel.Text = Globals.Localization.LanguageLabel;
            _saveSettingsButton.Text = Globals.Localization.SaveSettingsButton;
            _existFileRewriteCheckBox.Text = Globals.Localization.ExistFileRewriteCheckBox;
            _queueAutoDownloadCheckBox.Text = Globals.Localization.QueueAutoDownloadCheckBox;
            _forceCloseCheckBox.Text = Globals.Localization.ForceCloseCheckBox;
            _convertSVGToPNGCheckBox.Text = Globals.Localization.ConvertSVGToPNGCheckBox;
            _notStrictAPICheckCheckBox.Text = Globals.Localization.NotStrictAPICheckCheckBox;
            _checkHashCheckBox.Text = Globals.Localization.CheckHashCheckBox;
            _downloadPathLabel.Text = Globals.Localization.DefaultDownloadPathLabel;
            _downloadNameLabel.Text = Globals.Localization.DefaultDownloadNameLabel;
            _downloadPathTextBox.PathFormatHelpToolTip = Globals.Localization.FormatHelpButtonToolTip;
            _downloadPathTextBox.PathDialogToolTip = Globals.Localization.FolderDialogButtonToolTip;
            _downloadNameTextBox.PathFormatHelpToolTip = Globals.Localization.FormatHelpButtonToolTip;
            _toolTip.SetToolTip(_existFileRewriteCheckBox, Globals.Localization.ExistFileRewriteCheckBoxToolTip);
            _toolTip.SetToolTip(_queueAutoDownloadCheckBox, Globals.Localization.QueueAutoDownloadCheckBoxToolTip);
            _toolTip.SetToolTip(_convertSVGToPNGCheckBox, Globals.Localization.ConvertSVGToPNGCheckBoxToolTip);
            _toolTip.SetToolTip(_proxyButton, Globals.Localization.ProxyButtonToolTip);
            _toolTip.SetToolTip(_resetSettings, Globals.Localization.ResetSettingsToolTip);
            _toolTip.SetToolTip(_resetAllSettings, Globals.Localization.ResetAllSettingsToolTip);
            OnCurrentPathChanged();
        }

        public void ResetAPI()
        {
            _apiControl.ResetAPI();
        }

        private void OnCurrentPathChanged()
        {
            _pathLabel.Text =
                $@"{Globals.Localization.CurrentPathLabel} {Path.Combine(Globals.CurrentDownloadFolder.GetValue() ?? String.Empty, Globals.CurrentDownloadFileName.GetValue() ?? String.Empty)}";
        }

        private void OnDownloadPath_TextChanged_PathCheck()
        {
            String trimedText = PathUtils.TrimPathEnd(_downloadPathTextBox.Text);
            if (!_downloadPathTextBox.IsValid() || String.IsNullOrEmpty(trimedText))
            {
                return;
            }

            Globals.CurrentDownloadFolder.SetValue(trimedText);
        }

        private void OnDownloadName_TextChanged_PathCheck()
        {
            String trimedText = PathUtils.TrimPath(_downloadNameTextBox.Text);
            if (!_downloadNameTextBox.IsValid() || String.IsNullOrEmpty(trimedText))
            {
                return;
            }

            Globals.CurrentDownloadFileName.SetValue(trimedText);
        }

        private void OnLostFocusPathCheck()
        {
            String trimedText = PathUtils.TrimPathEnd(_downloadPathTextBox.Text);
            if (_downloadPathTextBox.IsValid() && !String.IsNullOrEmpty(trimedText))
            {
                _downloadPathTextBox.Text = trimedText;
                return;
            }

            _downloadPathTextBox.Text = String.IsNullOrEmpty(Globals.CurrentDownloadFolder.GetValue()) ? Globals.DefaultDownloadFolder : Globals.CurrentDownloadFolder.GetValue();
        }

        private void OnLostFocusNameCheck()
        {
            String trimedText = PathUtils.TrimPath(_downloadNameTextBox.Text);
            if (_downloadNameTextBox.IsValid() && !String.IsNullOrEmpty(trimedText))
            {
                _downloadNameTextBox.Text = trimedText;
                return;
            }

            _downloadNameTextBox.Text =
                String.IsNullOrEmpty(Globals.CurrentDownloadFileName.GetValue()) ? Globals.DefaultDownloadFileName : Globals.CurrentDownloadFileName.GetValue();
        }

        private void OnExistFileRewriteCheckBox_Click(Object sender, EventArgs e)
        {
            Globals.ExistFileRewrite.SetValue(_existFileRewriteCheckBox.Checked);
        }

        private void OnQueueAutoDownloadCheckBox_Click(Object sender, EventArgs e)
        {
            Globals.QueueAutoDownload.SetValue(_queueAutoDownloadCheckBox.Checked);
        }
        
        private void OnForceCloseCheckBox_Click(Object sender, EventArgs e)
        {
            Globals.ForceClose.SetValue(_forceCloseCheckBox.Checked);
        }
        
        private void OnConvertSVGToPngCheckBox_Click(Object sender, EventArgs e)
        {
            Globals.ConvertSVGToPNG.SetValue(_convertSVGToPNGCheckBox.Checked);
        }
        
        private void OnNotStrictAPICheckCheckBox_Click(Object sender, EventArgs e)
        {
            Globals.NotStrictAPICheck.SetValue(_notStrictAPICheckCheckBox.Checked);
        }
        
        private void OnCheckHashCheckBox_Click(Object sender, EventArgs e)
        {
            Globals.NotStrictAPICheck.SetValue(_checkHashCheckBox.Checked);
        }

        private void OnResetSettingsButtonClick(out DialogResult dialogResult)
        {
            dialogResult = new MessageForm(Globals.Localization.SettingsResetYouSureQuestion,
                    Globals.Localization.YouSureQuestion,
                    Images.Basic.Warning,
                    Images.Basic.Warning,
                    MessageBoxButtons.YesNo,
                    new[] {Globals.Localization.Yes, Globals.Localization.No})
                .ShowDialog();

            if (dialogResult != DialogResult.Yes)
            {
                return;
            }

            Globals.ExistFileRewrite.ResetValue();
            Globals.QueueAutoDownload.ResetValue();
            Globals.ForceClose.ResetValue();
            Globals.ConvertSVGToPNG.ResetValue();
            Globals.NotStrictAPICheck.ResetValue();
            Globals.CheckHash.ResetValue();
            _downloadPathTextBox.Text = Globals.DefaultDownloadFolder;
            _downloadNameTextBox.Text = Globals.DefaultDownloadFileName;
            LocalizationBase.UpdateLocalization(LocalizationBase.CurrentCulture.LCID);
            Globals.Config.SaveProperties();
        }

        private void OnResetAllSettingsButtonClick()
        {
            OnResetSettingsButtonClick(out DialogResult dialogResult);
            if (dialogResult != DialogResult.Yes)
            {
                return;
            }

            _languageImagedComboBox.SelectedIndex = 0;
            _proxyForm.ResetProxy();
            _apiControl.ResetAPI();
            Globals.Config.SaveProperties();
        }

        public void Save()
        {
            Globals.CurrentDownloadFolder.SetValue(_downloadPathTextBox.Text);
            Globals.CurrentDownloadFileName.SetValue(_downloadNameTextBox.Text);
            Globals.LanguageCode.SetValue(_languageImagedComboBox.GetLanguageLCID());
            Globals.ExistFileRewrite.SetValue(_existFileRewriteCheckBox.Checked);
            Globals.QueueAutoDownload.SetValue(_queueAutoDownloadCheckBox.Checked);
            Globals.ForceClose.SetValue(_forceCloseCheckBox.Checked);
            Globals.ConvertSVGToPNG.SetValue(_convertSVGToPNGCheckBox.Checked);
            Globals.ConvertSVGToPNG.SetValue(_notStrictAPICheckCheckBox.Checked);
            Globals.Config.SaveProperties();
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _apiControl.Text = Globals.APIKey.GetValue();
            base.OnClosing(e);
        }
    }
}