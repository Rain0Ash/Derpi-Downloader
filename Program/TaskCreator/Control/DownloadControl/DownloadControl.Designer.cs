// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace Derpi_Downloader.Forms
{
    public partial class DownloadControl
    {
        private void InitializeComponent()
        {
            SuspendLayout();
            _taskerList.OnAdd += UpdateTaskPosition;
            _taskerList.OnRemove += UpdateTaskPosition;
            _taskerList.OnClear += UpdateTaskPosition;
            ResumeLayout();
        }
    }
}