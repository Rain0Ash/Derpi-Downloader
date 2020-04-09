// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace Derpi_Downloader.Settings
{
    public static class LayoutGUI
    {
        public const Int32 MainFormSizeWidth = DownloadControlSizeWidth;
        public const Int32 MainFormSizeHeight = 475;
        public const Int32 DistanceBetweenControls = 5;
        public const Int32 ButtonSizeWidth = 48;
        public const Int32 ButtonSizeHeight = 48;
        public const Int32 MainFormLoggerRichTextBoxSizeWidth = 390;
        public const Int32 MainFormLoggerRichTextBoxSizeHeight = 130;
        public const Int32 DownloadTaskControlWidth = 550;
        public const Int32 DownloadTaskControlHeight = 130;
        public const Int32 DownloadControlSizeWidth = DownloadTaskControlWidth * 2;
        public const Int32 QueueRequestListBoxWidth = 160;

        
        public const Int32 AdditionalsFormWidth = 700;
        public const Int32 AdditionalsFormHeight = 230;
        public const Int32 AdditionalsPathListViewWidth = 345;
        public const Int32 AdditionalsPathListViewHeight = 80;
    }
}