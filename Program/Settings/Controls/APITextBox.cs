// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Text.RegularExpressions;
using Common_Library.GUI.WinForms.TextBoxes;
using Derpi_Downloader.API;
using Derpi_Downloader.Settings;

namespace Derpi_Downloader.Settings.Forms
{
    public class APITextBox : HidenTextBox
    {
        public APITextBox()
        {
            MaxLength = DerpiAPI.LengthAPI;
            CheckValidFormat();
        }

        protected override void CheckValidFormatColor()
        {
            if (Text.Length == 0)
            {
                BackColor = Color.White;
            }
            else if (Globals.NotStrictAPICheck.GetValue() || Regex.IsMatch(Text, $@"^{DerpiAPI.APIAllowedSymbols}{{1,{DerpiAPI.LengthAPI}}}$"))
            {
                if (Text.Length == DerpiAPI.LengthAPI)
                {
                    BackColor = Color.GreenYellow;
                    return;
                }

                BackColor = Color.Aqua;
            }
            else
            {
                BackColor = Color.Red;
            }
        }

        public override Boolean CheckValidFormat()
        {
            return DerpiAPI.CheckAPI(Text);
        }
    }
}