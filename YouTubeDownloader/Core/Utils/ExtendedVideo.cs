using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode.Common;
using YoutubeExplode.Videos;

namespace YouTubeDownloader.Core.Utils
{
    public class ExtendedVideo : IVideo
    {
        public VideoId Id { get; }

        public string Url => $"https://www.youtube.com/watch?v={Id}";

        public string Title { get; }

        public Author Author { get; }

        public DateTimeOffset UploadDate { get; }

        public string Description { get; }

        public TimeSpan? Duration { get; }

        public IReadOnlyList<Thumbnail> Thumbnails { get; }

        public IReadOnlyList<string> Keywords { get; }

        public Engagement Engagement { get; }

        public VideoSettings VideoSettings { get; set; }

        public ExtendedVideo(
            VideoId id,
            string title,
            Author author,
            DateTimeOffset uploadDate,
            string description,
            TimeSpan? duration,
            IReadOnlyList<Thumbnail> thumbnails,
            IReadOnlyList<string> keywords,
            Engagement engagement)
        {
            Id = id;
            Title = title;
            Author = author;
            UploadDate = uploadDate;
            Description = description;
            Duration = duration;
            Thumbnails = thumbnails;
            Keywords = keywords;
            Engagement = engagement;
        }

        public ExtendedVideo(Video video)
        {
            Id = video.Id;
            Title = video.Title;
            Author = video.Author;
            UploadDate = video.UploadDate;
            Description = video.Description;
            Duration = video.Duration;
            Thumbnails = video.Thumbnails;
            Keywords = video.Keywords;
            Engagement = video.Engagement;
        }

        public ExtendedVideo(Video video, VideoSettings videoSettings)
        {
            Id = video.Id;
            Title = video.Title;
            Author = video.Author;
            UploadDate = video.UploadDate;
            Description = video.Description;
            Duration = video.Duration;
            Thumbnails = video.Thumbnails;
            Keywords = video.Keywords;
            Engagement = video.Engagement;
            VideoSettings = videoSettings;
        }

        public override string ToString() => $"Video ({Title})";
    }
}
