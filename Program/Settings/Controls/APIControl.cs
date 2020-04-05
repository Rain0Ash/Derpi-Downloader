// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Common_Library.GUI.WinForms.Controls;
using Common_Library.GUI.WinForms.Forms;
using Common_Library.Images;
using Derpi_Downloader.API;

namespace Derpi_Downloader.Settings.Forms
{
    public sealed partial class APIControl : LocalizationControl
    {
        public APIControl()
        {
            InitializeComponent();
        }

        public override String Text
        {
            get
            {
                return _apiTextBox.Text;
            }
            set
            {
                _apiTextBox.Text = value;
            }
        }

        public override void UpdateText()
        {
            _apiLabel.Text = Globals.Localization.APILabel;
            _saveAPIButton.Text = Globals.Localization.SaveAPIButton;
            _changeAPIButton.Text = Globals.Localization.ChangeAPIButton;
            _resetAPIButton.Text = Globals.Localization.ResetAPIButton;
            _toolTip.SetToolTip(_goToAPIPageButton, Globals.Localization.GoToAPIPageToolTip);
        }

        private void DisableButtons()
        {
            _apiTextBox.Enabled = false;
            _saveAPIButton.Enabled = false;
            _changeAPIButton.Enabled = false;
            _resetAPIButton.Enabled = false;
        }

        private void ResetAPIButtonCheck()
        {
            _resetAPIButton.Enabled = _apiTextBox.Text.Length > 0 || Globals.APIKey.IsValid;
        }

        private async void SaveAPIButton_Click(Object sender, EventArgs e)
        {
            if (!_apiTextBox.CheckValidFormat())
            {
                return;
            }

            if (_apiTextBox.Text != Globals.APIKey.GetValue())
            {
                String apiKey = _apiTextBox.Text;


                Boolean apiTextBoxCurrentEnabled = _apiTextBox.Enabled;
                Boolean saveAPIButtonCurrentEnabled = _saveAPIButton.Enabled;
                Boolean changeAPIButtonCurrentEnabled = _changeAPIButton.Enabled;
                Boolean resetAPIButtonCurrentEnabled = _resetAPIButton.Enabled;
                DisableButtons();

                void RestoreButtonsEnabledState()
                {
                    _apiTextBox.Enabled = apiTextBoxCurrentEnabled;
                    _saveAPIButton.Enabled = saveAPIButtonCurrentEnabled;
                    _changeAPIButton.Enabled = changeAPIButtonCurrentEnabled;
                    _resetAPIButton.Enabled = resetAPIButtonCurrentEnabled;
                }

                if (!await DerpiAPI.CheckValidAPIAsync(apiKey).ConfigureAwait(true))
                {
                    RestoreButtonsEnabledState();
                    ChangeAPI();
                    new MessageForm(Globals.Localization.EnteredAPIKeyInvalid, Globals.Localization.APIKeyInvalid, Images.Basic.Error, Images.Basic.Error)
                        .ShowDialog();
                    return;
                }

                RestoreButtonsEnabledState();
                Globals.APIKey.SetValue(apiKey);
            }

            _saveAPIButton.Enabled = false;
            _apiTextBox.Enabled = false;
        }

        private void ChangeAPIButton_Click(Object sender, EventArgs e)
        {
            ChangeAPI();
        }

        private void ResetAPIButton_Click(Object sender, EventArgs e)
        {
            ResetAPI();
        }

        public void ChangeAPI()
        {
            _apiTextBox.Enabled = true;
            _saveAPIButton.Enabled = _apiTextBox.CheckValidFormat();
            _apiTextBox.Focus();
            _apiTextBox.SelectionStart = _apiTextBox.Text.Length;
            _apiTextBox.SelectionLength = 0;
        }

        public void ResetAPI()
        {
            Globals.APIKey.RemoveValue();
            _apiTextBox.Text = String.Empty;
            ChangeAPI();
        }
    }
}