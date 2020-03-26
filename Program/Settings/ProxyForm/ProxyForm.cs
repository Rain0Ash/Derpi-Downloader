// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net;
using System.Threading.Tasks;
using Common_Library.GUI.WinForms.Forms;
using Common_Library.Images;
using Common_Library.Utils.Network;
using Derpi_Downloader.Settings;

namespace System.Windows.Forms
{
    public partial class ProxyForm : ParentForm
    {
        public ProxyForm()
        {
            InitializeComponent();
        }

        protected override void UpdateText()
        {
            Text = Globals.Localization.ProxySettings;
            _addressLabel.Text = Globals.Localization.ProxyAddressLabel;
            _portLabel.Text = Globals.Localization.ProxyPortLabel;
            _loginLabel.Text = Globals.Localization.ProxyLoginLabel;
            _passwordLabel.Text = Globals.Localization.ProxyPasswordLabel;
            _saveProxyButton.Text = Globals.Localization.SaveProxyButton;
            _helpToolTip.SetToolTip(_resetProxyButton, Globals.Localization.ResetProxyButtonToolTip);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _addressTextBox.Text = Globals.ProxyAddress.GetValue();
            _portTextBox.Text = Globals.ProxyPort.GetValue().ToString();
            _loginTextBox.Text = Globals.ProxyLogin.GetValue();
            _passwordTextBox.Text = Globals.ProxyPassword.GetValue();
        }

        private void CheckAddressValid()
        {
            _saveProxyButton.Enabled = _addressTextBox.CheckValidFormat();
        }

        private async void SaveProxyButtonClickAsync(Object sender, EventArgs e)
        {
            if (_addressTextBox.Text == Globals.ProxyAddress.GetValue() &&
                _portTextBox.Text == Globals.ProxyPort.GetValue().ToString() &&
                _loginTextBox.Text == Globals.ProxyLogin.GetValue() &&
                _passwordTextBox.Text == Globals.ProxyPassword.GetValue())
            {
                Close();
                return;
            }

            if (_addressTextBox.Text == @"127.0.0.1" && String.IsNullOrEmpty(_loginTextBox.Text) && String.IsNullOrEmpty(_passwordTextBox.Text))
            {
                ResetProxy();
                Close();
                return;
            }


            Int32.TryParse(_portTextBox.Text, out Int32 port);

            if (!NetworkUtils.ValidatePort(port))
            {
                new MessageForm(Globals.Localization.InvalidProxyPort, Globals.Localization.Error, Images.Basic.Warning, Images.Basic.Warning,
                    MessageBoxButtons.OK, new[] {Globals.Localization.OK}).ShowDialog();
                return;
            }

            WebProxy proxy = ProxyUtils.CreateProxy(_addressTextBox.Text, port, _loginTextBox.Text, _passwordTextBox.Text);
            Task<HttpStatusCode> checkTask = ProxyUtils.CheckProxyAsync(proxy);

            _addressTextBox.Enabled = false;
            _portTextBox.Enabled = false;
            _loginTextBox.Enabled = false;
            _passwordTextBox.Enabled = false;
            _resetProxyButton.Enabled = false;
            _saveProxyButton.Enabled = false;

            HttpStatusCode check = HttpStatusCode.ServiceUnavailable;
            if (await Task.WhenAny(checkTask, Task.Delay(3000)).ConfigureAwait(true) == checkTask)
            {
                check = await checkTask.ConfigureAwait(true);
            }

            switch (check)
            {
                case HttpStatusCode.OK:
                    Globals.WebProxy = proxy;
                    Globals.ProxyAddress.SetValue(_addressTextBox.Text);
                    Globals.ProxyPort.SetValue(port);
                    Globals.ProxyLogin.SetValue(_loginTextBox.Text);
                    Globals.ProxyPassword.SetValue(_passwordTextBox.Text);
                    Close();
                    break;
                case HttpStatusCode.Forbidden:
                    new MessageForm(Globals.Localization.ProxyConnectionInvalid, Globals.Localization.Error, Images.Basic.Warning, Images.Basic.Warning,
                        MessageBoxButtons.OK, new[] {Globals.Localization.OK}).ShowDialog();
                    break;
                case HttpStatusCode.ProxyAuthenticationRequired:
                    new MessageForm(Globals.Localization.InvalidProxyCredentials, Globals.Localization.Error, Images.Basic.Warning, Images.Basic.Warning,
                        MessageBoxButtons.OK, new[] {Globals.Localization.OK}).ShowDialog();
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    new MessageForm(Globals.Localization.ProxyIsUnreachable, Globals.Localization.Error, Images.Basic.Warning, Images.Basic.Warning,
                        MessageBoxButtons.OK, new[] {Globals.Localization.OK}).ShowDialog();
                    break;
                default:
                    new MessageForm($"{Globals.Localization.UnknownError}\n{check}", Globals.Localization.Error, Images.Basic.Warning, Images.Basic.Warning,
                        MessageBoxButtons.OK, new[] {Globals.Localization.OK}).ShowDialog();
                    break;
            }

            _addressTextBox.Enabled = true;
            _portTextBox.Enabled = true;
            _loginTextBox.Enabled = true;
            _passwordTextBox.Enabled = true;
            _resetProxyButton.Enabled = true;
            CheckAddressValid();
        }

        public void ResetProxy()
        {
            Globals.WebProxy = null;
            _addressTextBox.Text = _addressTextBox.DefaultHost;
            Globals.ProxyAddress.SetValue(_addressTextBox.DefaultHost);
            _portTextBox.Text = _portTextBox.DefaultPort.ToString();

            Int32.TryParse(_portTextBox.Text, out Int32 port);
            
            Globals.ProxyPort.SetValue(port);
            _loginTextBox.Text = String.Empty;
            Globals.ProxyLogin.ResetValue();
            _passwordTextBox.Text = String.Empty;
            Globals.ProxyPassword.ResetValue();
        }

        private void ResetProxyButtonClick(Object sender, EventArgs e)
        {
            if (new MessageForm(Globals.Localization.SettingsResetYouSureQuestion, Globals.Localization.YouSureQuestion,
                    Images.Basic.Warning, Images.Basic.Warning, MessageBoxButtons.YesNo,
                    new[] {Globals.Localization.Yes, Globals.Localization.No}).ShowDialog() ==
                DialogResult.Yes)
            {
                ResetProxy();
            }
        }
    }
}