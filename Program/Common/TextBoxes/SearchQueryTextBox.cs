// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using Common_Library.GUI.WinForms.TextBoxes;
using Derpi_Downloader.API;

namespace Derpi_Downloader.Forms
{
    public class SearchQueryTextBox : FixedTextBox
    {
        public SearchQueryTextBox()
        {
            TextChanged += (sender, args) => OnTextChanged();
        }

        public Boolean ValidSearchQuery { get; private set; }
        
        private static readonly Dictionary<Regex, String> ReplaceDictionary = new Dictionary<Regex, String>{
            {new Regex($@"{DerpiAPI.SiteRegexPart + "tags" + @"\/?"}", RegexOptions.IgnoreCase), String.Empty},
            {new Regex(@"(-colon-|%3A)", RegexOptions.IgnoreCase), ":"},
            {new Regex(@"(%3)", RegexOptions.IgnoreCase), String.Empty},
            {new Regex(@"(%40)", RegexOptions.IgnoreCase), "@"},
            {new Regex(@"(%2C)", RegexOptions.IgnoreCase), ","},
            {new Regex(@"(-dot-)", RegexOptions.IgnoreCase), "."},
            {new Regex(@"(-slash-)", RegexOptions.IgnoreCase), "\\"},
            {new Regex(@"(-fwslash-)", RegexOptions.IgnoreCase), "/"},
            {new Regex(@"(-dash-)", RegexOptions.IgnoreCase), "-"},
            {new Regex(@"(\+)", RegexOptions.IgnoreCase), " "}
        };
        
        private void OnTextChanged()
        {
            Replace(ReplaceDictionary);
            ValidSearchQuery = DerpiAPI.CheckSearchRequest(Text);
            BackColor =
                ValidSearchQuery ||
                String.IsNullOrEmpty(Text)
                    ? Color.White
                    : Color.IndianRed;
        }
    }
}