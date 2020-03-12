// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Derpi_Downloader.API;
using Derpi_Downloader.Settings;

namespace Derpi_Downloader.Forms
{
    public partial class DownloadControl : Control
    {
        public const Int32 MaximumTasks = 5;

        private readonly EventQueueList<DownloadTaskControl> _taskerList = new EventQueueList<DownloadTaskControl>();

        private readonly MainForm _form;
        
        public DownloadControl(MainForm form)
        {
            _form = form;
            InitializeComponent();
            _form.AddedRequest += request => 
            {
                if (CurrentTasks >= MaximumTasks)
                {
                    DownloadTaskControl completedTask = _taskerList.FirstOrDefault(task => task.Task?.IsCompleted == true && task.IsFullDownload || task.Task?.IsStarted != true && task.EmptySearchRequest && task.DownloadPathIsDefault);
                    
                    if (completedTask == null)
                    {
                        return;
                    }
                    
                    RemoveTask(completedTask);
                }

                AddDownloadTaskControl(request);
                _form.RemoveRequest(request);
            };
        }

        public Int32 CurrentTasks
        {
            get
            {
                return _taskerList.Count;
            }
        }
        
        public void AddDownloadTaskControl(DownloadRequest downloadRequest = null)
        {
            if (CurrentTasks >= MaximumTasks)
            {
                if (downloadRequest != null)
                {
                    _form.AddRequest(downloadRequest);
                }
                
                return;
            }
            
            downloadRequest ??= new DownloadRequest(String.Empty, Globals.CurrentDownloadPath, false);
            SuspendLayout();
            DownloadTaskControl downloadTaskControl = new DownloadTaskControl(downloadRequest)
            {
                Size = new Size(LayoutGUI.DownloadTaskControlWidth, LayoutGUI.DownloadTaskControlHeight)
            };
            downloadTaskControl.Completed += () =>
            {
                if (Globals.QueueAutoDownload && _form.GetRequests().Any())
                {
                    downloadTaskControl.Close();
                }
            };
            downloadTaskControl.NeedClosing += () =>
            {
                RemoveTask(downloadTaskControl);
            };
            downloadTaskControl.NeedClosing += () =>
            {
                List<Object> invalidRequests = new List<Object>();
                List<DownloadRequest> requests = new List<DownloadRequest>();
                foreach (Object obj in _form.GetRequests())
                {
                    if (!(obj is DownloadRequest request))
                    {
                        invalidRequests.Add(obj);
                        continue;
                    }

                    if (requests.Count < MaximumTasks - CurrentTasks)
                    {
                        requests.Add(request);
                    }
                }

                foreach (Object obj in invalidRequests)
                {
                    _form.RemoveRequest(obj);
                }

                foreach (DownloadRequest request in requests.TakeWhile(request => CurrentTasks < MaximumTasks))
                {
                    _form.RemoveRequest(request);
                    AddDownloadTaskControl(request);
                }
                
                if (DerpiAPI.CheckAPI(Globals.APIKey) && CurrentTasks <= 0)
                {
                    AddDownloadTaskControl();
                }
            };
            AddTask(downloadTaskControl);
            
            ResumeLayout();
        }
        
        private void AddTask(DownloadTaskControl downloadTaskControl)
        {
            _taskerList.Add(downloadTaskControl);
            Controls.Add(downloadTaskControl);
        }

        private void RemoveTask(DownloadTaskControl downloadTaskControl)
        {
            Controls.Remove(downloadTaskControl);
            _taskerList.Remove(downloadTaskControl);
            downloadTaskControl.Dispose();
            GC.Collect();
        }

        private void UpdateTaskPosition(ref DownloadTaskControl control)
        {
            UpdateTaskPosition();
        }
        
        private void UpdateTaskPosition()
        {
            SuspendLayout();
            for (Int32 id = 0; id < _taskerList.Count; id++)
            {
                DownloadTaskControl item = _taskerList[id];
                item.Location = new Point(id < 3 ? 0 : item.Size.Width, id % 3 * item.Size.Height);
            }
            ResumeLayout();
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            UpdateTaskPosition();
        }
    }
}