// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Common_Library.Attributes;
using Common_Library.Utils;
using Common_Library.Utils.IO;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace Derpi_Downloader.Json
{
    public class Representations
    {
        [JsonProperty]
        public String thumb_tiny { get; set; }

        [JsonProperty]
        public String thumb_small { get; set; }

        [JsonProperty]
        public String thumb { get; set; }

        [JsonProperty]
        public String small { get; set; }

        [JsonProperty]
        public String medium { get; set; }

        [JsonProperty]
        public String large { get; set; }

        [JsonProperty]
        public String tall { get; set; }

        [JsonProperty]
        public String full { get; set; }
    }
    
    public class Intensities
    {
        [JsonProperty]
        public Double ne { get; set; }
        
        [JsonProperty]
        public Double nw { get; set; }
        
        [JsonProperty]
        public Double se { get; set; }
        
        [JsonProperty]
        public Double sw { get; set; }
    }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class Image
    {
        private const String UploaderNeeded = "uploader needed";
        private const String ArtistNeeded = "artist needed";

        [JsonProperty(Order = -2)]
        [FormatedField(uniqueness: true)]
        public Int32 id { get; set; }

        [JsonIgnore]
        [FormatedField]
        public String query { get; set; }
        
        [JsonProperty]
        public DateTime created_at { get; set; }

        [JsonIgnore]
        [FormatedField(new[] {"time:day:month", "data"})]
        public String created_at_date
        {
            get
            {
                return created_at.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo);
            }
        }

        [JsonProperty]
        public DateTime updated_at { get; set; }

        [JsonProperty]
        public DateTime first_seen_at { get; set; }

        [JsonIgnore]
        public String[] artists_array { get; set; }

        [JsonIgnore]
        [FormatedField]
        public String artist
        {
            get
            {
                return artists_array.FirstOr(ArtistNeeded);
            }
        }

        [JsonIgnore]
        [FormatedField(new[] {"artist?"})]
        public String artistoruploader
        {
            get
            {
                return String.IsNullOrEmpty(artist) || artist.Equals(ArtistNeeded, StringComparison.OrdinalIgnoreCase) ? uploader : artist;
            }
        }

        [JsonIgnore]
        [FormatedField]
        public String artists
        {
            get
            {
                return String.Join("_", artists_array.Any() ? artists_array : new[] {ArtistNeeded});
            }
        }

        [JsonIgnore]
        [FormatedField(new[] {"artists?"})]
        public String artistsoruploader
        {
            get
            {
                return String.IsNullOrEmpty(artists) || artists.Equals(ArtistNeeded, StringComparison.OrdinalIgnoreCase) ? uploader : artists;
            }
        }

        [JsonIgnore]
        private String[] _tags { get; set; }
        
        [JsonProperty(Order = -2)]
        [FormatedField]
        public String[] tags
        {
            get
            {
                return _tags;
            }
            set
            {
                if (_tags == value)
                {
                    return;
                }

                _tags = value;
                artists_array = _tags.Where(tag => Regex.IsMatch(tag, @"^artist:(\w|\-|\ )+$"))
                    .Select(tag => tag.Replace("artist:", String.Empty))
                    .ToArray();
            }
        }

        [JsonProperty]
        public Int32[] tag_ids { get; set; }

        [JsonProperty]
        public Int32? uploader_id { get; set; }

        [JsonProperty]
        public Int32? duplicate_of { get; set; }

        [JsonProperty]
        [FormatedField]
        public Int32 score { get; set; }

        [JsonProperty]
        public Int32 comment_count { get; set; }

        [JsonProperty]
        [FormatedField]
        public Int32 width { get; set; }

        [JsonProperty]
        [FormatedField]
        public Int32 height { get; set; }

        [JsonIgnore]
        [FormatedField(new[] {"size", "resolution"})]
        public String resolution
        {
            get
            {
                return $"{width}x{height}";
            }
        }

        [JsonProperty]
        [FormatedField(new[] {"tags_count"})]
        public Int32 tag_count { get; set; }

        [FormatedField(new []{"name", "image_name"} , true)]
        public String image_name {
            get
            {
                return Regex.Replace(view_url, "^.*\\/|\\..+$", String.Empty);
            }
        }

        [JsonProperty]
        public String name { get; set; }

        [JsonProperty]
        public String description { get; set; }

        [JsonIgnore]
        private String _uploader;
        
        [JsonProperty(Order = -2)]
        [FormatedField]
        public String uploader {
            get
            {
                return _uploader ?? UploaderNeeded;
            }
            set
            {
                _uploader = value;
            }
        }

        [JsonIgnore]
        private String _view_url;

        [JsonProperty(Order = -1)]
        public String view_url
        {
            get
            {
                return _view_url;
            }
            set
            {
                _view_url = value;
                String validName = PathUtils.RemoveIllegalChars(Regex.Replace(view_url, @"(.*\/|\..+)", String.Empty));
                name = String.IsNullOrEmpty(validName) ? id.ToString() : validName;
            }
        }

        [JsonProperty]
        public String deletion_reason { get; set; }
        
        [JsonProperty]
        public Int32 upvotes { get; set; }

        [JsonProperty]
        public Int32 downvotes { get; set; }

        [JsonProperty]
        public Int32 faves { get; set; }

        [JsonProperty]
        [FormatedField(new[] {"ratio"})]
        public Double aspect_ratio { get; set; }

        [JsonIgnore] private String _format;

        [JsonProperty]
        [FormatedField(new[] {"ext", "extension"})]
        public String format
        {
            get
            {
                return _format;
            }
            set
            {
                const String png = "png";
                const String svg = "svg";
                String val = String.IsNullOrEmpty(value) ? png : value;

                _format = (val.Equals(svg, StringComparison.OrdinalIgnoreCase) == false ? val : png).ToLower();
            }
        }
        
        [JsonProperty]
        public Boolean hidden_from_users { get; set; }

        [JsonProperty]
        public String mime_type { get; set; }

        [JsonIgnore] private String _sha512_hash;

        [JsonProperty]
        [FormatedField(new[] {"hash", "sha512"}, true)]
        public String sha512_hash
        {
            get
            {
                return _sha512_hash;
            }
            set
            {
                _sha512_hash = value;

                if (String.IsNullOrEmpty(orig_sha512_hash) && !String.IsNullOrEmpty(value))
                {
                    orig_sha512_hash = value;
                }
            }
        }

        [JsonIgnore] private String _orig_sha512_hash;

        [JsonProperty]
        public String orig_sha512_hash
        {
            get
            {
                return _orig_sha512_hash;
            }
            set
            {
                _orig_sha512_hash = value;

                if (String.IsNullOrEmpty(sha512_hash) && !String.IsNullOrEmpty(value))
                {
                    sha512_hash = value;
                }
            }
        }

        [JsonProperty]
        public String source_url { get; set; }

        [JsonProperty]
        public Intensities intensities { get; set; }
        
        [JsonProperty]
        public Representations representations { get; set; }

        [JsonProperty]
        public Boolean is_rendered { get; set; }

        [JsonProperty]
        public Boolean is_optimized { get; set; }

        public override String ToString()
        {
            return name;
        }
    }

    public class DerpiImage
    {
        [JsonProperty]
        public List<Image> images { get; set; }

        [JsonProperty]
        public List<Object> interactions { get; set; }
        
        [JsonProperty]
        public Int32 total { get; set; }
    }
}