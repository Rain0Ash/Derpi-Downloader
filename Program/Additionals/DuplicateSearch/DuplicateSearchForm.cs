// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Common_Library.GUI.WinForms.Forms;
using Common_Library.LongPath;
using Common_Library.Utils;
using Common_Library.Utils.IO;
using Common_Library.Watchers;

namespace Derpi_Downloader.Additionals.DuplicateSearch
{
    using Derpi_Downloader.Settings;

    public partial class DuplicateSearchForm : ParentForm
    {
        public DuplicateSearchForm()
        {
            InitializeComponent();
            String path = StringUtils.BeforeFormatVariables(Globals.CurrentDownloadPath);

            if (String.IsNullOrEmpty(path) || !Directory.Exists(path))
            {
                return;
            }

            FSWatcher pathObject = new FSWatcher(path, PathType.Folder, PathStatus.Exist);
            _pathListBox.Add(pathObject);
        }

        protected override void UpdateText()
        {
            Text = Globals.Localization.DuplicateSearchButtonToolTip;
        }
    }
}