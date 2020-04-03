// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using Common_Library.App;
using Common_Library.Localization;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable NotAccessedField.Global

namespace Derpi_Downloader.Localization
{
    public class ProgramLocalization : LocalizationBase
    {
        public const String Https = "https://";
        public const String Developer = "Rain0Ash";
        public const String AuthorGitHubPage = Https + "github.com/" + Developer;
        public const String ProjectGitHubPage = AuthorGitHubPage + "/Derpi-Downloader";
        public const String DerpiBooru = "derpibooru.org";
        public const String DerpiBooruLink = Https + DerpiBooru;
        public const String DerpiBooruProfileLink = DerpiBooruLink + "/registration/edit";
        public const String Licence = "Mozilla Public License 2.0";
        public const String LicenceLink = Https + "mozilla.org/en-US/MPL/2.0";

        public CultureStrings ProjectName;
        public CultureStrings CriticalExceptionOccurred;
        public CultureStrings CriticalExceptionOccurredText;
        public CultureStrings CriticalExceptionOccurredRestartButton;
        public CultureStrings CriticalExceptionOccurredExitButton;
        public CultureStrings InitializeStageException;
        public CultureStrings InitializeExceptionOccured;
        public CultureStrings MainForm;
        public CultureStrings AboutProgramTitle;
        public CultureStrings AboutProgramText;
        public CultureStrings ProgramSuccessfullyStarted;
        public CultureStrings SelectFile;
        public CultureStrings DefaultDownloadPathLabel;
        public CultureStrings DefaultDownloadNameLabel;
        public CultureStrings SelectFolder;
        public CultureStrings SettingsForm;
        public CultureStrings APILabel;
        public CultureStrings SaveAPIButton;
        public CultureStrings ChangeAPIButton;
        public CultureStrings ResetAPIButton;
        public CultureStrings CurrentPathLabel;
        public CultureStrings SaveSettingsButton;
        public CultureStrings ExistFileRewriteCheckBox;
        public CultureStrings QueueAutoDownloadCheckBox;
        public CultureStrings ConvertSVGToPNGCheckBox;
        public CultureStrings LanguageLabel;
        public CultureStrings DownloadForm;
        public CultureStrings SearchQueryLabel;
        public CultureStrings DownloadPathLabel;
        public CultureStrings AddTaskButton;
        public CultureStrings YouSureQuestion;
        public CultureStrings SettingsResetYouSureQuestion;
        public CultureStrings APIKeyInvalid;
        public CultureStrings EnteredAPIKeyInvalid;
        public CultureStrings CurrentAPIKeyInvalid;
        public CultureStrings ProxySettings;
        public CultureStrings ProxyAddressLabel;
        public CultureStrings ProxyPortLabel;
        public CultureStrings ProxyLoginLabel;
        public CultureStrings ProxyPasswordLabel;
        public CultureStrings SaveProxyButton;
        public CultureStrings ResetProxyButtonToolTip;
        public CultureStrings InvalidProxyPort;
        public CultureStrings InvalidProxyCredentials;
        public CultureStrings ProxyIsUnreachable;
        public CultureStrings ProxyConnectionInvalid;
        public CultureStrings NothingFoundOnRequest;
        public CultureStrings FirstKnowTitle;
        public CultureStrings FirstKnowText;
        public CultureStrings TasksExceeded;
        public CultureStrings TasksExceededText;
        public CultureStrings AdditionalFunctions;
        public CultureStrings DuplicateSearchButton;
        public CultureStrings DuplicateSearchButtonToolTip;
        public CultureStrings AuthorListCreating;
        public CultureStrings AuthorListFilesFound;
        public CultureStrings AuthorListFilesNotFound;
        public CultureStrings AuthorListCompleted;
        public CultureStrings FilesAnalyzed;
        public CultureStrings IncludedPathsLabel;
        public CultureStrings ExcludedPathsLabel;
        public CultureStrings ArtistsListLabel;
        public CultureStrings ChangePathRecursiveToolTip;
        public CultureStrings AddPathToolTip;
        public CultureStrings RemovePathToolTip;
        public CultureStrings AddRequestToDownloadQueueToolTip;
        public CultureStrings RemoveRequestToDownloadQueueToolTip;
        public CultureStrings MakeAuthorsListButton;
        public CultureStrings MakeAuthorsListButtonToolTip;
        public CultureStrings UnavailableFunction;
        public CultureStrings GoToAPIPageToolTip;
        public CultureStrings FileDialogButtonToolTip;
        public CultureStrings FolderDialogButtonToolTip;
        public CultureStrings PathTypeChangeButtonToRelativeToolTip;
        public CultureStrings PathTypeChangeButtonToAbsoluteToolTip;
        public CultureStrings FormatHelpButtonToolTip;
        public CultureStrings ProxyButtonToolTip;
        public CultureStrings ResetSettingsToolTip;
        public CultureStrings ResetAllSettingsToolTip;
        public CultureStrings CloseDownloadTaskControlToolTip;
        public CultureStrings ReuseDownloadTaskControlToolTip;
        public CultureStrings ExistFileRewriteCheckBoxToolTip;
        public CultureStrings QueueAutoDownloadCheckBoxToolTip;
        public CultureStrings ConvertSVGToPNGCheckBoxToolTip;
        public CultureStrings Exit;
        public CultureStrings OK;
        public CultureStrings Yes;
        public CultureStrings No;
        public CultureStrings Cancel;
        public CultureStrings Retry;
        public CultureStrings Ignore;
        public CultureStrings Accept;
        public CultureStrings Close;
        public CultureStrings Select;
        public CultureStrings Perform;
        public CultureStrings Resume;
        public CultureStrings Pause;
        public CultureStrings Debug;
        public CultureStrings Action;
        public CultureStrings Good;
        public CultureStrings Warning;
        public CultureStrings CriticalWarning;
        public CultureStrings Error;
        public CultureStrings CriticalError;
        public CultureStrings FatalError;
        public CultureStrings UnknownError;
        public CultureStrings GetImageError;
        public CultureStrings GetPageError;
        public CultureStrings NoImagesFound;
        public CultureStrings CantFoundDuplicateImage;
        public CultureStrings ZeroDownloadPagesArgument;
        public CultureStrings InvalidImageHashError;
        public CultureStrings FormatFileNameError;
        public CultureStrings FormatDirectoryError;
        public CultureStrings CreateDirectoryError;
        public CultureStrings WriteAccessDeniedError;
        public CultureStrings InitializedInvalidTaskError;
        public CultureStrings StartedInvalidTaskError;
        public CultureStrings ImageWaitToLongRetry;
        public CultureStrings PageWaitToLongRetry;
        public CultureStrings Created;
        public CultureStrings ClickForResume;
        public CultureStrings ClickForPause;
        public CultureStrings DownloadStarted;
        public CultureStrings DownloadCompleted;
        private CultureStrings _downloadForSearch;
        public CultureStrings DownloadForSearchStarted;
        public CultureStrings DownloadForSearchCompleted;
        public CultureStrings DownloadForSearchInvalid;
        public CultureStrings DownloadTaskIsCanceled;

        public CultureStrings AboutFormatTitle;

        public Dictionary<String, CultureStrings> AboutFormatDictionary { get; } = new Dictionary<String, CultureStrings>();

        public ProgramLocalization(Int32 lcid)
            :
            base(lcid, new CultureStrings())
        {
        }

        protected override void InitializeLanguage()
        {
            String appVersion = App.Version.GetVersion();

            ProjectName = new CultureStrings(
                "Derpi Downloader",
                "Derpi Загрузчик",
                "Derpi Herunterladen");

            CriticalExceptionOccurred = new CultureStrings(
                "Crititical exception occurred!",
                "Произошло критическое исключение!",
                "Kritische Ausnahme aufgetreten!");

            CriticalExceptionOccurredText = new CultureStrings(
                $"Crititical exception occurred!{NewLine}Information about this exception:",
                $"Произошло критическое исключение!{NewLine}Информация об этом исключении:",
                $"Kritische Ausnahme ist aufgetreten!{NewLine}Informationen zu dieser Ausnahme:");

            CriticalExceptionOccurredRestartButton = new CultureStrings(
                "Restart",
                "Перезапустить",
                "Neustart");

            CriticalExceptionOccurredExitButton = new CultureStrings(
                "Exit",
                "Выйти",
                "Ausgang");

            InitializeStageException = new CultureStrings(
                "Exception on initialize stage",
                "Ошибка на стадии инициализации");

            InitializeExceptionOccured = new CultureStrings(
                "Initialize exception occured",
                "Вызвана ошибка при инициализации");

            MainForm = new CultureStrings(
                $"{ProjectName.en} {appVersion}",
                $"{ProjectName.ru} {appVersion}",
                $"{ProjectName.de} {appVersion}");

            AboutProgramTitle = new CultureStrings(
                "About program",
                "О программе",
                "Über das Programm");

            AboutProgramText = new CultureStrings(
                $"<link>{ProjectGitHubPage}|{ProjectName.en}</link>{NewLine}Program version: {appVersion}{NewLine}Developed by <link>{AuthorGitHubPage}|{Developer}</link>{NewLine}Licence: <link>{LicenceLink}|{Licence}</link>",
                $"<link>{ProjectGitHubPage}|{ProjectName.ru}</link>{NewLine}Версия программы: {appVersion}{NewLine}Разработано <link>{AuthorGitHubPage}|{Developer}</link>{NewLine}Лицензия: <link>{LicenceLink}|{Licence}</link>",
                $"<link>{ProjectGitHubPage}|{ProjectName.de}</link>{NewLine}Ausführung {appVersion}{NewLine}Entwickelt von <link>{AuthorGitHubPage}|{Developer}</link>{NewLine}Licence: <link>{LicenceLink}|{Licence}</link>");

            ProgramSuccessfullyStarted = new CultureStrings(
                "Program successfully started",
                "Программа успешно запущена",
                "Programm erfolgreich gestartet");

            SelectFile = new CultureStrings(
                "Select file",
                "Выберите файл",
                "Datei aussuchen");

            SelectFolder = new CultureStrings(
                "Select folder",
                "Выберите папку",
                "Ordner auswählen");

            SettingsForm = new CultureStrings(
                "Settings",
                "Настройки",
                "Einstellungen");

            APILabel = new CultureStrings(
                "API Key",
                "API Ключ",
                "API-Schlüssel");

            SaveAPIButton = new CultureStrings(
                "Save API",
                "Сохранить API",
                "Speichern API");

            ChangeAPIButton = new CultureStrings(
                "Change API",
                "Изменить API",
                "Ändern API");

            ResetAPIButton = new CultureStrings(
                "Reset API",
                "Сбросить API",
                "Zurücksetzen API");

            CurrentPathLabel = new CultureStrings(
                "Current path:",
                "Текущий путь:",
                "Aktueller pfad:");

            DefaultDownloadPathLabel = new CultureStrings(
                "Default download folder",
                "Стандартная папка загрузки",
                "Standard download ordner");

            DefaultDownloadNameLabel = new CultureStrings(
                "Default download filename",
                "Стандартное имя файла",
                "Standard download datei name");

            SaveSettingsButton = new CultureStrings(
                "Save settings",
                "Сохранить настройки",
                "Einstellungen speichern");

            ExistFileRewriteCheckBox = new CultureStrings(
                "Overwrite files",
                "Перезаписывать файлы",
                "Dateien überschreiben");

            QueueAutoDownloadCheckBox = new CultureStrings(
                "Auto download from queue",
                "Автоскачивание из очереди");

            ExistFileRewriteCheckBoxToolTip = new CultureStrings(
                "Pass/overwrite files with matching names",
                "Пропускать/перезаписывать файлы при совпадении имен",
                "Dateien mit übereinstimmenden Namen übergeben / überschreiben");

            QueueAutoDownloadCheckBoxToolTip = new CultureStrings(
                "Auto start download request from queue",
                "Автоматически начинать скачивания запроса из очереди");

            ConvertSVGToPNGCheckBox = new CultureStrings(
                "Convert SVG to PNG",
                "Конвертировать SVG в PNG",
                "Konvertieren Sie SVG in PNG");

            ConvertSVGToPNGCheckBoxToolTip = new CultureStrings(
                "Convert SVG image to PNG and save both",
                "Конвертировать SVG изображение в PNG и сохранять две версии",
                "Konvertieren Sie das SVG-Bild in PNG und speichern Sie beide");

            LanguageLabel = new CultureStrings(
                "Language",
                "Язык",
                "Sprache");

            DownloadForm = new CultureStrings(
                "New download",
                "Новое скачивание",
                "Neuer download");

            SearchQueryLabel = new CultureStrings(
                "Image query",
                "Поисковый запрос",
                "Suchanfrage");

            DownloadPathLabel = new CultureStrings(
                "Download path",
                "Путь загрузки",
                "Pfad herunterladen");

            AddTaskButton = new CultureStrings(
                "Add download",
                "Добавить загрузку",
                "Download hinzufügen");

            YouSureQuestion = new CultureStrings(
                "Are you sure?",
                "Вы уверены?",
                "Bist du sicher?");

            SettingsResetYouSureQuestion = new CultureStrings(
                "Do you want to reset the settings?",
                "Вы точно хотите сбросить настройки?",
                "Möchten Sie die Einstellungen zurücksetzen?");

            APIKeyInvalid = new CultureStrings(
                "API key is not valid",
                "API ключ недействителен",
                "Der API-Schlüssel ist ungültig");

            CurrentAPIKeyInvalid = new CultureStrings(
                "Current API key is not valid",
                "Текущий API ключ недействителен",
                "Der aktuelle API-Schlüssel ist ungültig");

            ProxySettings = new CultureStrings(
                "Proxy",
                "Прокси",
                "Proxy");

            ProxyAddressLabel = new CultureStrings(
                "Proxy address",
                "Адрес прокси",
                "Proxy-Adresse");

            ProxyPortLabel = new CultureStrings(
                "Port",
                "Порт",
                "Port");

            ProxyLoginLabel = new CultureStrings(
                "Authorization name",
                "Имя учетной записи",
                "Autorisierungsname");

            ProxyPasswordLabel = new CultureStrings(
                "Password",
                "Пароль",
                "Passwort");

            SaveProxyButton = new CultureStrings(
                "Save proxy",
                "Сохранить прокси",
                "Proxy speichern");

            ResetProxyButtonToolTip = new CultureStrings(
                "Reset proxy settings",
                "Сбросить настройки прокси",
                "Proxy-Einstellungen zurücksetzen");

            InvalidProxyPort = new CultureStrings(
                "Invalid proxy port",
                "Неверный порт прокси",
                "Ungültiger Proxy-Port");

            InvalidProxyCredentials = new CultureStrings(
                "Invalid proxy credentials",
                "Неверные авторизационные данные",
                "Ungültige Proxy-Anmeldeinformationen");

            ProxyIsUnreachable = new CultureStrings(
                "Proxy is unreachable",
                "Прокси не отвечает",
                "Proxy ist nicht erreichbar");

            ProxyConnectionInvalid = new CultureStrings(
                "Proxy connection invalid",
                "Ошибка подключения к прокси");

            NothingFoundOnRequest = new CultureStrings(
                "Nothing found for request",
                "По запросу ничего не найдено",
                "Nichts gefunden für Anfrage");

            EnteredAPIKeyInvalid = new CultureStrings(
                "Entered API key is not valid",
                "Введенный API ключ недействителен",
                "Der eingegebene API-Schlüssel ist ungültig");

            FirstKnowTitle = new CultureStrings(
                "Getting to know the program",
                "Знакомство с программой",
                "Das Programm kennenlernen");

            FirstKnowText = new CultureStrings(
                $"Welcome!{NewLine}{NewLine}" +
                $"This program allows you to mass download images from <link>{DerpiBooruLink}</link>{NewLine}" +
                $"Please, after closing this window, enter and save your API key,{NewLine}" +
                $"which can be obtained on the page of your profile at the link: <link>{DerpiBooruProfileLink}</link>{NewLine}" +
                $"{NewLine}{AboutProgramText.en}",
                $"Приветствую!{NewLine}{NewLine}" +
                $"Данная программа позволяет массово скачивать изображения с сайта <link>{DerpiBooruLink}</link>{NewLine}" +
                $"Пожалуйста, после закрытия этого окна, введите и сохраните ваш API ключ,{NewLine}" +
                $"который может быть получен на странице вашего профиля по ссылке: <link>{DerpiBooruProfileLink}</link>{NewLine}" +
                $"{NewLine}{AboutProgramText.ru}",
                $"Willkommen!{NewLine}{NewLine}" +
                $"Mit diesem Programm können Sie Bilder von <link>{DerpiBooruLink}</link>{NewLine}in großen Mengen herunterladen." +
                $"Bitte geben Sie nach dem Schließen dieses Fensters Ihren API-Schlüssel{NewLine}ein und speichern Sie ihn." +
                $"API-Schlüssel erhalten Sie auf der Seite Ihres Profils unter dem Link: <link>{DerpiBooruProfileLink}</link>{NewLine}" +
                $"{NewLine}{AboutProgramText.de}");

            TasksExceeded = new CultureStrings(
                "Current count of tasks is maximum",
                "Текущее количество задач является максимальным",
                "Die aktuelle Anzahl der Aufgaben ist maximal");

            TasksExceededText = new CultureStrings(
                $"Current count of tasks {{0}}/{{1}}.{NewLine}Please complete or remove any task, before creating new.",
                $"Текущее количество задач {{0}}/{{1}}.{NewLine}Пожалуйста, завершите или удалите любое задание, перед созданием нового.",
                $"Aktuelle Anzahl der Aufgaben {{0}}/{{1}}.{NewLine}Bitte beenden oder entfernen Sie eine Aufgabe, bevor Sie eine neue erstellen.");

            UnavailableFunction = new CultureStrings(
                "Unavailable function",
                "Недоступная функция");

            AdditionalFunctions = new CultureStrings(
                "Additional functions",
                "Дополнительные функции");

            DuplicateSearchButton = new CultureStrings(
                "Duplicate images",
                "Поиск повторений");

            MakeAuthorsListButton = new CultureStrings(
                "List of authors",
                "Список авторов");

            MakeAuthorsListButtonToolTip = new CultureStrings(
                "Make list of authors of downloaded images",
                "Создание списка авторов из скачанных изображений");

            DuplicateSearchButtonToolTip = new CultureStrings(
                "Image duplicate files in folder by mask",
                "Поиск дублирующихся файлов по маске");

            AuthorListCreating = new CultureStrings(
                "Started author list creating procedure/",
                "Начато создание списка авторов");

            AuthorListFilesFound = new CultureStrings(
                "Found {0} files for analyzing",
                "Найдено {0} файлов для анализа");

            AuthorListFilesNotFound = new CultureStrings(
                "Author list files not found!",
                "Файлы для создания списка не найдены!");

            AuthorListCompleted = new CultureStrings(
                "Author list creating completed",
                "Завершено создание списка авторов");

            FilesAnalyzed = new CultureStrings(
                "Files analyzed: {0}",
                "Файлов проанализировано: {0}");

            IncludedPathsLabel = new CultureStrings(
                "Included paths",
                "Включенные пути");

            ExcludedPathsLabel = new CultureStrings(
                "Excluded paths",
                "Исключенные пути");

            ArtistsListLabel = new CultureStrings(
                "Artist list",
                "Список авторов");

            ChangePathRecursiveToolTip = new CultureStrings(
                "Switch path recursive status",
                "Переключить рекурсивный статус пути");

            AddPathToolTip = new CultureStrings(
                "Add path",
                "Добавить путь");

            RemovePathToolTip = new CultureStrings(
                "Remove path",
                "Удалить путь");

            AddRequestToDownloadQueueToolTip = new CultureStrings(
                "Add request to download queue",
                "Добавить запрос в очередь скачивания");

            RemoveRequestToDownloadQueueToolTip = new CultureStrings(
                "Remove request from download queue",
                "Удалить запрос из очереди скачивания");

            GoToAPIPageToolTip = new CultureStrings(
                "Open page with API",
                "Открыть страницу с API",
                "Seite mit API öffnen");

            FileDialogButtonToolTip = new CultureStrings(
                "Select file path",
                "Выбрать путь к файлу",
                "Dateipfad auswählen");

            FolderDialogButtonToolTip = new CultureStrings(
                "Select folder path",
                "Выбрать путь к папке",
                "Ordnerpfad auswählen");

            PathTypeChangeButtonToRelativeToolTip = new CultureStrings(
                "Transform to relative path",
                "Преобразовать в относительный путь",
                "In relativen Pfad umwandeln");

            PathTypeChangeButtonToAbsoluteToolTip = new CultureStrings(
                "Transform to absolute path",
                "Преобразовать в абсолютный путь",
                "In absoluten Pfad transformieren");

            FormatHelpButtonToolTip = new CultureStrings(
                "Show available variables for replace",
                "Показать доступные переменные для замены",
                "Verfügbare Variablen zum Ersetzen anzeigen");

            ProxyButtonToolTip = new CultureStrings(
                "Open proxy settings",
                "Открыть настройки прокси",
                "Proxy-Einstellungen öffnen");

            ResetSettingsToolTip = new CultureStrings(
                "Reset settings",
                "Сбросить настройки",
                "Einstellungen zurücksetzen");

            ResetAllSettingsToolTip = new CultureStrings(
                "Reset all settings",
                "Сбросить все настройки",
                "Alle Einstellungen zurücksetzen");

            CloseDownloadTaskControlToolTip = new CultureStrings(
                "Cancel task",
                "Отменить задание",
                "Aufgabe abbrechen");

            ReuseDownloadTaskControlToolTip = new CultureStrings(
                "Reuse control",
                "Использовать форму снова",
                "Kontrolle wiederverwenden");

            Exit = new CultureStrings(
                "Exit",
                "Выход");

            OK = new CultureStrings(
                "OK",
                "Понятно",
                "Ich verstehe");

            Yes = new CultureStrings(
                "Yes",
                "Да",
                "Ja");

            No = new CultureStrings(
                "No",
                "Нет",
                "Nein");

            Cancel = new CultureStrings(
                "Cancel",
                "Отменить",
                "Stornieren");

            Retry = new CultureStrings(
                "Retry",
                "Повторить",
                "Wiederholen");

            Ignore = new CultureStrings(
                "Ignore",
                "Игнорировать",
                "Ignorieren");

            Accept = new CultureStrings(
                "Accept",
                "Принять",
                "Akzeptieren");

            Close = new CultureStrings(
                "Close",
                "Закрыть",
                "Schließen");

            Select = new CultureStrings(
                "Select",
                "Выбрать");

            Perform = new CultureStrings(
                "Perform",
                "Выполнить",
                "Ausführen");

            Resume = new CultureStrings(
                "Resume",
                "Возобновить",
                "Fortsetzen");

            Pause = new CultureStrings(
                "Pause",
                "Приостановить",
                "Pause");

            Debug = new CultureStrings(
                "Debug",
                "Отладка",
                "Debuggen");

            Action = new CultureStrings(
                "Action",
                "Действие",
                "Aktion");

            Good = new CultureStrings(
                "Good",
                "Хорошо",
                "Gut");

            Warning = new CultureStrings(
                "Warning",
                "Предупреждение",
                "Warnung");

            CriticalWarning = new CultureStrings(
                "Critical warning",
                "Критическое предупреждение",
                "Kritische warnung");

            Error = new CultureStrings(
                "Error",
                "Ошибка",
                "Fehler");

            CriticalError = new CultureStrings(
                "Critical error",
                "Критическая ошибка",
                "Kritischer fehler");

            FatalError = new CultureStrings(
                "Fatal error",
                "Фатальная ошибка",
                "Fataler fehler");

            UnknownError = new CultureStrings(
                "Unknown error",
                "Неизвестная ошибка",
                "Unbekannter fehler");

            GetPageError = new CultureStrings(
                "Error on getting page {0}",
                "Ошибка при получении страницы {0}",
                "Fehler beim Abrufen der Seite {0}");

            NoImagesFound = new CultureStrings(
                "No images found",
                "Не найдены изображения",
                "Keine Bilder gefunden");

            //TODO:
            CantFoundDuplicateImage = new CultureStrings(
                "Can't found duplicate image",
                "Не найдено дублирующее изображения",
                "Keine Bilder gefunden");

            ZeroDownloadPagesArgument = new CultureStrings(
                "Count of pages argument is 0",
                "Аргумент количества страниц равен 0",
                "Argument für Seitenzahl ist 0");

            GetImageError = new CultureStrings(
                "Error getting image {0}",
                "Ошибка получения изображения {0}",
                "Fehler beim Abrufen von Bild {0}");

            InvalidImageHashError = new CultureStrings(
                $"Image: {{0}}{NewLine} has invalid hash{NewLine}Correct hash{NewLine}{{1}}{NewLine}Image hash{NewLine}{{2}}{NewLine}",
                $"Изображение: {{0}}{NewLine}имеет неверный хэш{NewLine}Правильный хэш{NewLine}{{1}}{NewLine}Хэш изображения{NewLine}{{2}}{NewLine}",
                $"Bild: {{0}} {NewLine} hat ungültigen Hash {NewLine} Korrekter Hash {NewLine} {{1}} {NewLine} Bild-Hash {NewLine} {{2}} {NewLine}");

            FormatFileNameError = new CultureStrings(
                "Image: {{0}} format filename error",
                "Изображение: {{0}} ошибка при подстановке значений имени файла");

            FormatDirectoryError = new CultureStrings(
                "Format directory error",
                "Ошибка при сборке имени директории");

            CreateDirectoryError = new CultureStrings(
                "Error on create directory",
                "Ошибка при создании директории",
                "Fehler beim Erstellen des Verzeichnisses");

            WriteAccessDeniedError = new CultureStrings(
                "No write access on this path",
                "Отсутствует доступ на запись по данному пути",
                "Kein Schreibzugriff auf diesen Pfad");

            InitializedInvalidTaskError = new CultureStrings(
                "An attempt was made to initialize an invalid task",
                "Была предпринята попытка инициализировать неверную задачу",
                "Es wurde versucht, eine ungültige Aufgabe zu initialisieren");

            StartedInvalidTaskError = new CultureStrings(
                "An attempt was made to start an invalid task",
                "Была предпринята попытка запустить неверную задачу",
                "Es wurde versucht, eine ungültige Aufgabe zu starten");

            ImageWaitToLongRetry = new CultureStrings(
                "Image {0} waiting too long. Retrying.",
                "Слишком долгое ожидание изображения id:{0}. Перезапуск.",
                "Bild {0} wartet zu lange. Wiederholen.");

            PageWaitToLongRetry = new CultureStrings(
                "Page {0} waiting too long. Retrying.",
                "Слишком долгое ожидание страницы №:{0}. Перезапуск.",
                "Seite {0} wartet zu lange. Wiederholen.");

            Created = new CultureStrings(
                "Created",
                "Создано",
                "Erstellt");

            ClickForResume = new CultureStrings(
                "Click for resume",
                "Нажмите чтобы возобновить",
                "Klicken Sie hier, um fortzufahren");

            ClickForPause = new CultureStrings(
                "Click for pause",
                "Нажмите чтобы приостановить",
                "Klicken Sie für eine einhalten");

            DownloadStarted = new CultureStrings(
                "Download started",
                "Загрузка начата",
                "Download gestartet");

            DownloadCompleted = new CultureStrings(
                "Download completed",
                "Загрузка завершена",
                "Download abgeschlossen");

            _downloadForSearch = new CultureStrings(
                "Download for images: {0}",
                "Загрузка для поискового запроса {0}",
                "Download zur Suche: {0}");

            DownloadForSearchStarted = new CultureStrings(
                $"{_downloadForSearch.en} started",
                $"{_downloadForSearch.ru} начата",
                $"{_downloadForSearch.de} gestartet");

            DownloadForSearchCompleted = new CultureStrings(
                $"{_downloadForSearch.en} completed",
                $"{_downloadForSearch.ru} завершена",
                $"{_downloadForSearch.de} abgeschlossen");

            DownloadForSearchInvalid = new CultureStrings(
                $"{_downloadForSearch.en} invalid",
                $"{_downloadForSearch.ru} завершена неверно",
                $"{_downloadForSearch.de} ungültig");

            DownloadTaskIsCanceled = new CultureStrings(
                "Download task {0} is canceled",
                "Задание загрузки {0} отменено",
                "Die Download-Aufgabe {0} wurde abgebrochen");

            AboutFormatTitle = new CultureStrings(
                "Variables for replace",
                "Переменные для замены",
                "Variablen zum Ersetzen");

            AboutFormatDictionary["id"] = new CultureStrings(
                "ID of image",
                "ID изображения",
                "ID des Bildes");

            AboutFormatDictionary["artist"] = new CultureStrings(
                "First artist of image",
                "Первый автор изображения",
                "Erster Künstler des Bildes");

            AboutFormatDictionary["artist?"] = new CultureStrings(
                "First artist of image or uploader",
                "Первый автор изображения или имя выложившего изображение");

            AboutFormatDictionary["artists"] = new CultureStrings(
                "All artists of image",
                "Все авторы изображения");

            AboutFormatDictionary["artists?"] = new CultureStrings(
                "All artists of image or uploader",
                "Все авторы изображения или имя выложившего изображение");

            AboutFormatDictionary["tags"] = new CultureStrings(
                "Tags of image",
                "Теги изображения",
                "Tags des Bildes");

            AboutFormatDictionary["score"] = new CultureStrings(
                "Score of image",
                "Количество очков изображения",
                "Partitur des Bildes");

            AboutFormatDictionary["width"] = new CultureStrings(
                "Width of image",
                "Ширина изображения",
                "Breite des Bildes");

            AboutFormatDictionary["height"] = new CultureStrings(
                "Height of image",
                "Высота изображения",
                "Bildhöhe");

            AboutFormatDictionary["size"] = new CultureStrings(
                "Size of image ({Width}x{Height})",
                "Размер изображения ({Ширина}x{Высота})");

            AboutFormatDictionary["tags_count"] = new CultureStrings(
                "Tags count of image",
                "Количество тегов изображения",
                "Anzahl der Tags des Bildes");

            AboutFormatDictionary["name"] = new CultureStrings(
                "Name of image (ID + tags)",
                "Имя изображения (ID + теги)",
                "Name des Bildes (ID + Tags)");

            AboutFormatDictionary["uploader"] = new CultureStrings(
                "Uploader of image",
                "Имя выложившего изображение",
                "Uploader des Bildes");

            AboutFormatDictionary["ratio"] = new CultureStrings(
                "Ratio of image",
                "Коэффициент изображения",
                "Bildverhältnis");

            AboutFormatDictionary["ext"] = new CultureStrings(
                "Extension of image",
                "Расширение изображения",
                "Erweiterung des Bildes");

            AboutFormatDictionary["hash"] = new CultureStrings(
                "Hash of image",
                "Хэш изображения");
        }
    }
}