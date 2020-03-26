// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common_Library;
using Common_Library.Exceptions;
using Common_Library.Logger;
using Common_Library.Objects;
using Common_Library.Utils.Math;
using Derpi_Downloader.Json;
using Derpi_Downloader.Settings;
using Path = Common_Library.LongPath.Path;

// ReSharper disable ConvertToAutoProperty

namespace Derpi_Downloader.Download
{
    public partial class DownloadTask : IPauseable, IDisposable
    {
        private Boolean _isPaused;

        public Boolean IsPaused
        {
            get
            {
                return _isPaused;
            }
            set
            {
                if (_isPaused == value)
                {
                    return;
                }

                _isPaused = value;

                if (_isPaused)
                {
                    Paused?.Invoke();
                }
                else
                {
                    Resumed?.Invoke();
                }
            }
        }

        public Boolean IsCanceled
        {
            get
            {
                return _token.IsCancellationRequested;
            }
        }

        public Int32 MaximumImages { get; private set; }

        public Int32 ImagesPerPage { get; private set; }
        public Int32 Pages { get; private set; }

        private Int32 _downloadedImages;

        public Int32 DownloadedImages
        {
            get
            {
                return _downloadedImages;
            }
        }

        private Boolean _isCompleted;

        public Boolean IsCompleted
        {
            get
            {
                return _isCompleted;
            }
            private set
            {
                if (_isCompleted == value || !value)
                {
                    return;
                }

                _isCompleted = true;
                Completed?.Invoke(Images, Log);
                if (AutoDisposeOnComplete)
                {
                    RemoveTask();
                }
            }
        }

        public EventQueueList<Search> Images { get; } = new EventQueueList<Search>();
        public EventQueueList<LogMessage> Log { get; } = new EventQueueList<LogMessage>();

        public delegate void DownloadTaskHandler(EventQueueList<Search> images, EventQueueList<LogMessage> log);

        private Boolean _isInvalid;

        public Boolean IsInvalid
        {
            get
            {
                return _isInvalid;
            }

            private set
            {
                if (_isInvalid == value || !value)
                {
                    return;
                }

                _isInvalid = true;
                Canceled?.Invoke();
                _tokenSource.Cancel();
                Invalid?.Invoke(Log);
            }
        }

        public event Handlers.EmptyHandler Started;
        public event Handlers.EmptyHandler Resumed;
        public event Handlers.EmptyHandler Paused;
        public event Handlers.EmptyHandler Canceled;
        public event Handlers.EmptyHandler BeforeRemove;


        public delegate void LogHandler(EventQueueList<LogMessage> log);

        public event LogHandler Invalid;
        public event DownloadTaskHandler Completed;

        public readonly String SearchQuery;
        private readonly String _savePath;
        private readonly String _saveDirectory;

        private Boolean _saveToDisk = true;

        public Boolean SaveToDisk
        {
            get
            {
                return _saveToDisk;
            }
            set
            {
                _saveToDisk = value;
            }
        }

        private Boolean _autoDisposeOnComplete = true;

        public Boolean AutoDisposeOnComplete
        {
            get
            {
                return _autoDisposeOnComplete;
            }
            set
            {
                _autoDisposeOnComplete = value;
            }
        }

        private readonly Int32 _firstPageNumber;
        private readonly Int32 _countOfPages;

        private readonly CancellationTokenSource _tokenSource;
        private readonly CancellationToken _token;

        public DownloadTask(String searchQuery, String savePath = null, Int32 firstPageNumber = 1, Int32 countOfPages = -1)
        {
            SearchQuery = searchQuery;
            _firstPageNumber = MathUtils.ToRange(firstPageNumber, 1);
            _countOfPages = MathUtils.ToRange(countOfPages, -1);

            _savePath = savePath ?? Globals.CurrentDownloadPath;

            _saveDirectory = Path.GetDirectoryName(_savePath);

            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
        }

        public Boolean IsStarted;

        public Task StartTaskAsync()
        {
            if (IsInvalid)
            {
                Log.Add(new LogMessage(Globals.Localization.StartedInvalidTaskError, MessageType.CriticalWarning));
                return Task.CompletedTask;
            }

            if (IsCompleted)
            {
                return Task.CompletedTask;
            }

            if (!IsInitialized)
            {
                throw new NotInitializedException("Initialize first");
            }

            if (IsStarted)
            {
                throw new AlreadyInitializedException();
            }

            ImageDownloaded += search =>
            {
                /*
                    lock (Images)
                    {
                        Images.Add(search);
                    }
                */
                Interlocked.Increment(ref _downloadedImages);
            };
            IsStarted = true;
            Started?.Invoke();
            return GenerateTasksAsync();
        }

        public void Pause()
        {
            IsPaused = true;
        }

        private async Task WaitAsync(CancellationToken token)
        {
            while (IsPaused)
            {
                try
                {
                    await Task.Delay(25, token).ConfigureAwait(true);
                }
                catch (TaskCanceledException)
                {
                    return;
                }
            }
        }

        public void Resume()
        {
            IsPaused = false;
        }

        public void RemoveTask()
        {
            BeforeRemove?.Invoke();
            Dispose();
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            Images.Clear();
            Log.Clear();
            Invalid = null;
            Canceled = null;
            Completed = null;
            ImageDownloaded = null;
            ImageSaved = null;
            Started = null;
            Paused = null;
            Resumed = null;
            BeforeRemove = null;
        }

        ~DownloadTask()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}