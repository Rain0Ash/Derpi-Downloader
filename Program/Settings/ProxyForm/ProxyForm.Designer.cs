// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Drawing;
using System.Windows.Forms;
using Common_Library.GUI.WinForms.TextBoxes;
using Common_Library.GUI.WinForms.ToolTips;
using Common_Library.Images;
using Common_Library.Utils;

namespace Derpi_Downloader.Settings.Forms
{
    public partial class ProxyForm
    {
        private void InitializeComponent()
        {
            _addressLabel = new Label();
            _portLabel = new Label();
            _colonLabel = new Label();
            _loginLabel = new Label();
            _passwordLabel = new Label();
            _addressTextBox = new IPTextBox();
            _portTextBox = new PortTextBox();
            _loginTextBox = new HidenTextBox();
            _passwordTextBox = new HidenTextBox();
            _saveProxyButton = new Button();
            _resetProxyButton = new Button();
            _helpToolTip = new HelpToolTip();

            _addressLabel.Location = new Point(3, 1);
            _addressLabel.Size = new Size(100, 15);

            _addressTextBox.Location = new Point(5, 17);
            _addressTextBox.Size = new Size(100, 25);
            _addressTextBox.DefaultHost = @"127.0.0.1";
            _addressTextBox.TextChanged += (sender, args) => CheckAddressValid();

            _colonLabel.Location = new Point(101, 16);
            _colonLabel.Size = new Size(10, 25);
            _colonLabel.Font = new Font(Font.Name, Font.Size + 4);
            _colonLabel.TextAlign = ContentAlignment.TopLeft;
            _colonLabel.Text = @":";

            _portLabel.Location = new Point(107, 1);
            _portLabel.Size = new Size(100, 15);

            _portTextBox.Location = new Point(110, 17);
            _portTextBox.Size = new Size(36, 25);
            _portTextBox.DefaultPort = 3128;
            _portTextBox.TextChanged += (sender, args) => CheckAddressValid();

            _loginLabel.Location = new Point(3, 39);
            _loginLabel.Size = new Size(140, 15);

            _loginTextBox.Location = new Point(5, 54);
            _loginTextBox.Size = new Size(140, 25);

            _passwordLabel.Location = new Point(3, 75);
            _passwordLabel.Size = new Size(140, 15);

            _passwordTextBox.Location = new Point(5, 90);
            _passwordTextBox.Size = new Size(140, 25);

            _saveProxyButton.Location = new Point(3, 115);
            _saveProxyButton.Size = new Size(120, 25);
            _saveProxyButton.TextAlign = ContentAlignment.MiddleCenter;
            _saveProxyButton.Click += SaveProxyButtonClickAsync;

            _resetProxyButton.Location = new Point(123, 114);
            _resetProxyButton.Size = new Size(_saveProxyButton.Size.Height, _saveProxyButton.Size.Height);
            _resetProxyButton.Image = new Bitmap(Images.Fill.ResetGear, new Size(_resetProxyButton.Size.Height / 2, _resetProxyButton.Size.Height / 2));
            _resetProxyButton.Click += ResetProxyButtonClick;

            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            AutoScaleDimensions = new SizeF(7F, 15F);
            ClientSize = new Size(150, 145);
            AutoScaleMode = AutoScaleMode.Font;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Controls.Add(_addressLabel);
            Controls.Add(_addressTextBox);
            Controls.Add(_portLabel);
            Controls.Add(_portTextBox);
            Controls.Add(_loginLabel);
            Controls.Add(_loginTextBox);
            Controls.Add(_passwordLabel);
            Controls.Add(_passwordTextBox);
            Controls.Add(_saveProxyButton);
            Controls.Add(_resetProxyButton);
            Controls.Add(_colonLabel);
            Icon = ImageUtils.IconFromImage(Images.Others.Proxy);
        }

        private Label _addressLabel;
        private Label _colonLabel;
        private Label _portLabel;
        private Label _loginLabel;
        private Label _passwordLabel;
        private IPTextBox _addressTextBox;
        private PortTextBox _portTextBox;
        private HidenTextBox _loginTextBox;
        private HidenTextBox _passwordTextBox;
        private Button _saveProxyButton;
        private Button _resetProxyButton;
        private HelpToolTip _helpToolTip;
    }
}