using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Common_Library.GUI.WinForms.Controls;
using Common_Library.GUI.WinForms.Forms;
using Common_Library.GUI.WinForms.ListBoxes;
using Common_Library.GUI.WinForms.RichTextBoxes;
using Common_Library.GUI.WinForms.ToolTips;
using Derpi_Downloader.Settings;
using Derpi_Downloader.Json;
using Common_Library.Images;
using Derpi_Downloader.Additionals.Forms;
using Derpi_Downloader.Resources;
using Derpi_Downloader.Settings.Forms;
using Derpi_Downloader.TaskCreator.Forms;

namespace Derpi_Downloader.Forms
{
    public partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        public event TaskCreatorForm.DownloadRequestHandler AddedRequest;

        public void AddRequest(Object request)
        {
            _queueRequestListBox.ListBox.Add(request);
        }

        public void RemoveRequest(Object request)
        {
            _queueRequestListBox.ListBox.Remove(request);
        }

        public IEnumerable<Object> GetRequests()
        {
            return _queueRequestListBox.ListBox.Items.OfType<Object>();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            _aboutButton = new Button();
            _taskCreatorButton = new Button();
            _settingsButton = new Button();
            _additionalsButton = new Button();
            _downloadControl = new DownloadControl(this);
            _logRichTextBox = new LoggerRichTextBox();
            _queueRequestListBox = new AdvancedListBox();
            _helpToolTip = new HelpToolTip();
            _pageControl = new PageControl<Control>();
            SuspendLayout();

            _pageControl.Size = new Size(LayoutGUI.DownloadControlSizeWidth,
                LayoutGUI.MainFormSizeHeight - LayoutGUI.ButtonSizeHeight - LayoutGUI.DistanceBetweenControls);
            _pageControl.Location = new Point(0, LayoutGUI.ButtonSizeHeight + 1);
            _pageControl.Font = new Font(Font.Name, Font.Size + 2);
            _pageControl.Position = TabAlignment.Bottom;
            _pageControl.Alignment = ContentAlignment.BottomCenter;
            _pageControl.NextPageButton.Enabled = false;
            _pageControl.PreviousPageButton.Enabled = false;
            _pageControl.PageValueLabel.Enabled = false;
            _pageControl.NextPageButton.Visible = false;
            _pageControl.PreviousPageButton.Visible = false;
            _pageControl.PageValueLabel.Visible = false;
            _pageControl.Add(_downloadControl);
            //
            //DownloadControl
            //
            _downloadControl.Location = new Point(0, 0);
            _downloadControl.Size = new Size(_pageControl.Size.Width, _pageControl.Size.Height - _pageControl.ButtonHeight);
            _downloadControl.VisibleChanged += (sender, args) => UpdateText();
            // 
            // taskCreatorButton
            // 
            _taskCreatorButton.Location = new Point(0, 0);
            _taskCreatorButton.Size = new Size(LayoutGUI.ButtonSizeWidth, LayoutGUI.ButtonSizeHeight);
            _taskCreatorButton.Image = new Bitmap(Images.Lineal.Plus, new Size(LayoutGUI.ButtonSizeWidth / 2, LayoutGUI.ButtonSizeHeight / 2));
            _taskCreatorButton.UseVisualStyleBackColor = true;
            _taskCreatorButton.Enabled = Globals.APIKey.IsValid;
            _taskCreatorButton.Click += (sender, args) =>
            {
                if (_downloadControl.CurrentTasks < DownloadControl.MaximumTasks &&
                    (ModifierKeys == Keys.Control || ModifierKeys == (Keys.Shift | Keys.Control)))
                {
                    _downloadControl.AddDownloadTaskControl();
                    return;
                }

                OpenTaskCreatorForm();
            };
            Globals.APIKey.Changed += () => { _taskCreatorButton.Enabled = Globals.APIKey.IsValid; };
            Globals.APIKey.Changed += () =>
            {
                if (Globals.APIKey.IsValid && _downloadControl.CurrentTasks <= 0)
                {
                    _downloadControl.AddDownloadTaskControl();
                }
            };
            // 
            // settingsButton
            // 
            _settingsButton.Size = new Size(LayoutGUI.ButtonSizeWidth, LayoutGUI.ButtonSizeHeight);
            _settingsButton.Location = new Point(LayoutGUI.MainFormSizeWidth - _settingsButton.Size.Width, 0);
            _settingsButton.Image = new Bitmap(Images.Line.Settings, new Size(LayoutGUI.ButtonSizeWidth / 2, LayoutGUI.ButtonSizeHeight / 2));
            _settingsButton.UseVisualStyleBackColor = true;
            _settingsButton.Click += (sender, args) => { _settingsForm.ShowDialog(); };
            //
            // additionalsButton
            //
            _additionalsButton.Size = new Size(LayoutGUI.ButtonSizeWidth, LayoutGUI.ButtonSizeHeight);
            _additionalsButton.Location = new Point(LayoutGUI.MainFormSizeWidth - _settingsButton.Size.Width - _additionalsButton.Size.Width, 0);
            _additionalsButton.Image = new Bitmap(Images.Line.Tech, new Size(LayoutGUI.ButtonSizeWidth / 2, LayoutGUI.ButtonSizeHeight / 2));
            _additionalsButton.UseVisualStyleBackColor = true;
            _additionalsButton.Click += (sender, args) => { _additionalsForm.ShowDialog(); };
            // 
            // aboutButton
            // 
            _aboutButton.Size = new Size(LayoutGUI.ButtonSizeWidth, LayoutGUI.ButtonSizeHeight);
            _aboutButton.Location =
                new Point(LayoutGUI.MainFormSizeWidth - _settingsButton.Size.Width - _additionalsButton.Size.Width - _aboutButton.Size.Width, 0);
            _aboutButton.UseVisualStyleBackColor = true;
            _aboutButton.Image = new Bitmap(Images.Basic.Question, new Size(LayoutGUI.ButtonSizeWidth / 2, LayoutGUI.ButtonSizeHeight / 2));
            _aboutButton.Click += (sender, args) =>
            {
                new MessageForm(ModifierKeys == Keys.Shift ? Globals.Localization.FirstKnowText : Globals.Localization.AboutProgramText,
                    Globals.Localization.AboutProgramTitle, Images.Basic.Information, Resource.icon.ToBitmap(), MessageBoxButtons.OK,
                    new[] {Globals.Localization.Close}).ShowDialog();
            };

            _logRichTextBox.Size = new Size(LayoutGUI.MainFormLoggerRichTextBoxSizeWidth - 2, LayoutGUI.MainFormLoggerRichTextBoxSizeHeight);
            _logRichTextBox.Location = new Point(LayoutGUI.MainFormSizeWidth - _logRichTextBox.Size.Width - LayoutGUI.QueueRequestListBoxWidth,
                LayoutGUI.MainFormSizeHeight - _logRichTextBox.Size.Height - _pageControl.ButtonHeight - LayoutGUI.DistanceBetweenControls + 1);
            _logRichTextBox.BorderStyle = BorderStyle.None;
            _logRichTextBox.ScrollBars = RichTextBoxScrollBars.None;
            _logRichTextBox.Reversed = false;
            _logRichTextBox.BorderStyle = BorderStyle.Fixed3D;
            Globals.Logger.Logged += _logRichTextBox.Log;

            _queueRequestListBox.Size = new Size(LayoutGUI.QueueRequestListBoxWidth, _logRichTextBox.Size.Height);
            _queueRequestListBox.Location = new Point(_logRichTextBox.Location.X + _logRichTextBox.Size.Width, _logRichTextBox.Location.Y);
            _queueRequestListBox.AddButton.Click += (sender, args) => { OpenTaskCreatorForm(); };
            _queueRequestListBox.ListBox.ItemAdded += (obj, index) =>
            {
                if (obj is DownloadRequest request)
                {
                    AddedRequest?.Invoke(request);
                }
            };

            //
            // MainForm
            //  
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(LayoutGUI.MainFormSizeWidth, LayoutGUI.MainFormSizeHeight);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Controls.Add(_logRichTextBox);
            Controls.Add(_queueRequestListBox);
            Controls.Add(_aboutButton);
            Controls.Add(_additionalsButton);
            Controls.Add(_taskCreatorButton);
            Controls.Add(_settingsButton);
            Controls.Add(_pageControl);
            Icon = Resource.icon;
            Shown += OnForm_Shown;
            Globals.APIKey.Changed += OnAPIKeyChanged;
            OnAPIKeyChanged();
            JsonAPI.OnExceptionResponce += code =>
            {
                switch (code)
                {
                    case HttpStatusCode.Forbidden:
                    case HttpStatusCode.Unauthorized:
                        new MessageForm(Globals.Localization.CurrentAPIKeyInvalid, Globals.Localization.APIKeyInvalid, Images.Basic.Error, Images.Basic.Error)
                            .ShowDialog();
                        _settingsForm.ResetAPI();
                        _settingsForm.ShowDialog();
                        break;
                    default:
                        new MessageForm($"{Globals.Localization.UnknownError}: {code}", Globals.Localization.UnknownError, Images.Basic.Error,
                            Images.Basic.Error).ShowDialog();
                        break;
                }
            };
            ResumeLayout();
        }

        #endregion

        private Button _aboutButton;
        private Button _taskCreatorButton;
        private Button _settingsButton;
        private Button _additionalsButton;
        private DownloadControl _downloadControl;
        private LoggerRichTextBox _logRichTextBox;
        private AdvancedListBox _queueRequestListBox;
        private HelpToolTip _helpToolTip;
        private PageControl<Control> _pageControl;

        private readonly SettingsForm _settingsForm = new SettingsForm();
        private readonly AdditionalsForm _additionalsForm = new AdditionalsForm();
    }
}