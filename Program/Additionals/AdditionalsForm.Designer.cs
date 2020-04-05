// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Windows.Forms;
using Common_Library.GUI.WinForms.ToolTips;
using Common_Library.Images;
using Common_Library.Utils;
using Derpi_Downloader.Additionals.DuplicateSearch;

namespace Derpi_Downloader.Additionals.Forms
{
    using Derpi_Downloader.Additionals.AuthorsList;

    public partial class AdditionalsForm
    {
        private void InitializeComponent()
        {
            _duplicateSearchForm = new DuplicateSearchForm();
            _authorsListForm = new AuthorsListForm();

            _duplicateSearchButton = new Button();
            _makeAuthorsListButton = new Button();
            _unused2Button = new Button();
            _unused3Button = new Button();
            _unused4Button = new Button();
            _unused5Button = new Button();

            _helpToolTip = new HelpToolTip();
            SuspendLayout();

            const Int32 buttonSizeWidth = 150;
            const Int32 buttonSizeHeight = 35;

            _duplicateSearchButton.Location = new Point(0, 0);
            _duplicateSearchButton.Size = new Size(buttonSizeWidth, buttonSizeHeight);
            _duplicateSearchButton.Enabled = false;
            _duplicateSearchButton.Click += (sender, args) => { _duplicateSearchForm.ShowDialog(); };

            _makeAuthorsListButton.Location = new Point(buttonSizeWidth, 0);
            _makeAuthorsListButton.Size = new Size(buttonSizeWidth, buttonSizeHeight);
            _makeAuthorsListButton.Click += (sender, args) => { _authorsListForm.ShowDialog(); };

            _unused2Button.Location = new Point(buttonSizeWidth * 2, 0);
            _unused2Button.Size = new Size(buttonSizeWidth, buttonSizeHeight);
            _unused2Button.Enabled = false;

            _unused3Button.Location = new Point(0, buttonSizeHeight);
            _unused3Button.Size = new Size(buttonSizeWidth, buttonSizeHeight);
            _unused3Button.Enabled = false;

            _unused4Button.Location = new Point(buttonSizeWidth, buttonSizeHeight);
            _unused4Button.Size = new Size(buttonSizeWidth, buttonSizeHeight);
            _unused4Button.Enabled = false;

            _unused5Button.Location = new Point(buttonSizeWidth * 2, buttonSizeHeight);
            _unused5Button.Size = new Size(buttonSizeWidth, buttonSizeHeight);
            _unused5Button.Enabled = false;


            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            AutoScaleDimensions = new SizeF(7F, 15F);
            ClientSize = new Size(buttonSizeWidth * 3, buttonSizeHeight * 2);
            AutoScaleMode = AutoScaleMode.Font;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            Controls.Add(_duplicateSearchButton);
            Controls.Add(_makeAuthorsListButton);
            Controls.Add(_unused2Button);
            Controls.Add(_unused3Button);
            Controls.Add(_unused4Button);
            Controls.Add(_unused5Button);
            Icon = ImageUtils.IconFromImage(Images.Line.Tech);
            ResumeLayout();
        }

        private Button _duplicateSearchButton;
        private Button _makeAuthorsListButton;
        private Button _unused2Button;
        private Button _unused3Button;
        private Button _unused4Button;
        private Button _unused5Button;
        private HelpToolTip _helpToolTip;

        private DuplicateSearchForm _duplicateSearchForm;
        private AuthorsListForm _authorsListForm;
    }
}