using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace YouTubeDownloader.Core
{
    public class Core
    {
        private YoutubeClient client;

        public Core()
        {
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(handler, true);
            handler.UseCookies = false;

            client = new YoutubeClient(httpClient);
        }

        public async Task<Video> GetVideo(string url)
        {
            return await client.Videos.GetAsync(VideoId.Parse(url));
        }

        public async Task<Utils.ExtendedVideo> GetExtendedVideo(string url)
        {
            VideoId videoId = VideoId.Parse(url);

            return new Utils.ExtendedVideo(await client.Videos.GetAsync(videoId), GetVideoSettings(videoId));    
        }

        private Utils.VideoSettings GetVideoSettings(VideoId videoId)
        {
            StreamManifest manifest = client.Videos.Streams.GetManifestAsync(videoId).Result;

            Utils.VideoSettings videoSettings = new Utils.VideoSettings()
            {
                StreamManifest = manifest,
                MuxedStreamInfo = manifest.GetMuxedStreams().ToList(),
                VideoOnlyStreamInfo = manifest.GetVideoOnlyStreams().ToList(),
                AudioOnlyStreamInfo = manifest.GetAudioOnlyStreams().ToList()
            };

            return videoSettings;
        }

        public Utils.YouTubeVideoViewItem GenerateYouTubeVideoViewItem(Utils.ExtendedVideo video)
        {
            return new Utils.YouTubeVideoViewItem()
            {
                Video = video,
                DownloadSettings = new Utils.DownloadSettings()
            };
        }

        public async Task<Utils.YouTubeVideoViewItem> GetVideoViewItem(Video video)
        {
            return null;
        }
    }
}
