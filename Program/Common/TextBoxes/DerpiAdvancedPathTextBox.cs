// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Windows.Forms;
using Common_Library.GUI.WinForms.Forms;
using Common_Library.GUI.WinForms.TextBoxes;
using Common_Library.Images;
using Common_Library.Localization;
using Derpi_Downloader.Localization;
using Derpi_Downloader.Settings;

namespace Derpi_Downloader.Forms
{
    public class DerpiAdvancedPathTextBox : AdvancedPathTextBox
    {
        protected override void OnFormatHelpButton_Click(Object sender, EventArgs e)
        {
            new MessageForm(
                    String.Join(
                        LocalizationBase.NewLine,
                        TextBox.AvailableFormatingPartsGroupList
                            .Select(members => (members.LinkedNames, members.Attributes))
                            .Select(str =>
                                $@"{String.Join("; ", str.LinkedNames
                                    .Where(linkedName => !String.IsNullOrEmpty(linkedName))
                                    .Select(linkedName => $"{{{linkedName}}}"))}{(str.Attributes.Length > 0 ? $"[{String.Join("; ", str.Attributes)}]" : String.Empty)}{(Globals.Localization.AboutFormatDictionary.TryGetValue(str.LinkedNames[0], out CultureStrings text) ? $" - {text}" : String.Empty)}")),
                    Globals.Localization.AboutFormatTitle,
                    Images.Basic.Question,
                    Images.Basic.Question,
                    MessageBoxButtons.OK,
                    new[] {Globals.Localization.OK})
                .ShowDialog();
        }
    }
}