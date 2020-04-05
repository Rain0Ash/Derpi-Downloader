// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Common_Library.GUI.WinForms.Forms;
using Derpi_Downloader.Settings;

namespace Derpi_Downloader.Additionals.Forms
{
    public partial class AdditionalsForm : ParentForm
    {
        public AdditionalsForm()
        {
            InitializeComponent();
        }

        public override void UpdateText()
        {
            Text = Globals.Localization.AdditionalFunctions;
            _makeAuthorsListButton.Text = Globals.Localization.UnavailableFunction;
            _unused2Button.Text = Globals.Localization.UnavailableFunction;
            _unused3Button.Text = Globals.Localization.UnavailableFunction;
            _unused4Button.Text = Globals.Localization.UnavailableFunction;
            _unused5Button.Text = Globals.Localization.UnavailableFunction;
            _duplicateSearchButton.Text = Globals.Localization.DuplicateSearchButton;
            _makeAuthorsListButton.Text = Globals.Localization.MakeAuthorsListButton;
            _helpToolTip.SetToolTip(_duplicateSearchButton, Globals.Localization.DuplicateSearchButtonToolTip);
            _helpToolTip.SetToolTip(_makeAuthorsListButton, Globals.Localization.MakeAuthorsListButtonToolTip);
        }
    }
}