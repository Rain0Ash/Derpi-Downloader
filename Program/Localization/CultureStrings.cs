// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Common_Library.Localization;
using Common_Library.Utils;

namespace Derpi_Downloader.Localization
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
    public sealed class CultureStrings : CultureStringsBase
    {
        private static readonly IEnumerable<Int32> _availableLocalization = new[] {"EN", "RU", "DE"}.Select(code => GetLCID(code.ToLower()));
        
        public override IEnumerable<Int32> AvailableLocalization
        {
            get
            {
                return _availableLocalization;
            }
        }

        private static readonly Int32 RU = GetLCID("ru");
        public String ru
        {
            get
            {
                return ToString(RU);
            }
            private set
            {
                Localization[RU] = value ?? String.Empty;
            }
        }
        
        private static readonly Int32 DE = GetLCID("de");
        public String de
        {
            get
            {
                return ToString(DE);
            }
            private set
            {
                Localization[DE] = value ?? String.Empty;
            }
        }

        public CultureStrings()
            : this(null)
        {
        }
        
        public CultureStrings(String english, String russian = null, String deutch = null)
            : base(english)
        {
            ru = russian;
            de = deutch;
        }
    }
}