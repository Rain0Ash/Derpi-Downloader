// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Drawing;
using System.Windows.Forms;
using Common_Library.GUI.WinForms.ListBoxes;
using Common_Library.Images;
using Common_Library.Utils;

namespace Derpi_Downloader.Additionals.DuplicateSearch
{
    public partial class DuplicateSearchForm
    {
        private void InitializeComponent()
        {
            _pathListBox = new PathListBox();
            
            _pathListBox.Size = new Size(300, 100);
            //^(?<id>\d+)(?:__[a-zA-Zа-яА-Я0-9\-\+_]+)?(?:\.(?<ext>[a-zA-Z0-9]+))?$ derpibooru name regex

            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            AutoScaleDimensions = new SizeF(7F, 15F);
            ClientSize = new Size(450, 250);
            AutoScaleMode = AutoScaleMode.Font;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Controls.Add(_pathListBox);
            Icon = ImageUtils.IconFromImage(Images.Line.Tech);
        }

        private PathListBox _pathListBox;
    }
}