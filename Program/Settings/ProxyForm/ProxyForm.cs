// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net;
using System.Threading.Tasks;
using Common_Library.GUI.WinForms.Forms;
using Common_Library.Images;
using Common_Library.Utils;
using Derpi_Downloader.RegKeys;
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
            _addressTextBox.Text = Globals.WebProxyAddress;
            _portTextBox.Text = Globals.WebProxyPort.ToString();
            _loginTextBox.Text = Globals.WebProxyLogin;
            _passwordTextBox.Text = Globals.WebProxyPassword;
        }

        private void CheckAddressValid()
        {
            _saveProxyButton.Enabled = _addressTextBox.CheckValidFormat();
        }

        private async void SaveProxyButtonClickAsync(Object sender, EventArgs e)
        {
            if (_addressTextBox.Text == Globals.WebProxyAddress &&
                NetworkUtils.ValidatePort(_portTextBox.Text) == Globals.WebProxyPort &&
                _loginTextBox.Text == Globals.WebProxyLogin &&
                _passwordTextBox.Text == Globals.WebProxyPassword)
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

            Int32 port = NetworkUtils.ValidatePort(_portTextBox.Text);
            
            if (port == 0)
            {
                new MessageForm(Globals.Localization.InvalidProxyPort, Globals.Localization.Error, Images.Basic.Warning, Images.Basic.Warning, MessageBoxButtons.OK, new []{Globals.Localization.OK}).ShowDialog();
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
                    ConfigKeys.ProxyAddress = Globals.WebProxyAddress = _addressTextBox.Text;
                    ConfigKeys.ProxyPort = Globals.WebProxyPort = NetworkUtils.ValidatePort(_portTextBox.Text);
                    ConfigKeys.ProxyLogin = Globals.WebProxyLogin = _loginTextBox.Text;
                    ConfigKeys.ProxyPassword = Globals.WebProxyPassword = _passwordTextBox.Text;
                    Close();
                    break;
                case HttpStatusCode.Forbidden:
                    new MessageForm(Globals.Localization.ProxyConnectionInvalid, Globals.Localization.Error, Images.Basic.Warning, Images.Basic.Warning, MessageBoxButtons.OK, new []{Globals.Localization.OK}).ShowDialog();
                    break;
                case HttpStatusCode.ProxyAuthenticationRequired:
                    new MessageForm(Globals.Localization.InvalidProxyCredentials, Globals.Localization.Error, Images.Basic.Warning, Images.Basic.Warning, MessageBoxButtons.OK, new []{Globals.Localization.OK}).ShowDialog();
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    new MessageForm(Globals.Localization.ProxyIsUnreachable, Globals.Localization.Error, Images.Basic.Warning, Images.Basic.Warning, MessageBoxButtons.OK, new []{Globals.Localization.OK}).ShowDialog();
                    break;
                default:
                    new MessageForm($"{Globals.Localization.UnknownError}\n{check}", Globals.Localization.Error, Images.Basic.Warning, Images.Basic.Warning, MessageBoxButtons.OK, new []{Globals.Localization.OK}).ShowDialog();
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
            _addressTextBox.Text = ConfigKeys.ProxyAddress = Globals.WebProxyAddress = _addressTextBox.DefaultHost;
            _portTextBox.Text = _portTextBox.DefaultPort.ToString();
            ConfigKeys.ProxyPort = Globals.WebProxyPort = NetworkUtils.ValidatePort(_portTextBox.Text);
            _loginTextBox.Text = ConfigKeys.ProxyLogin = Globals.WebProxyLogin = String.Empty;
            _passwordTextBox.Text = ConfigKeys.ProxyPassword = Globals.WebProxyPassword = String.Empty;
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