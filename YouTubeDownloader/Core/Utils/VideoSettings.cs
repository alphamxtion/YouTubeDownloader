using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode.Videos.Streams;

namespace YouTubeDownloader.Core.Utils
{
    public class VideoSettings
    {
        private List<MuxedStreamInfo> _muxedStreamInfo;
        private List<VideoOnlyStreamInfo> _videoOnlyStreamInfo;
        private List<AudioOnlyStreamInfo> _audioOnlyStreamInfo;
        private StreamManifest _streamManifest;

        public List<MuxedStreamInfo> MuxedStreamInfo { get { return _muxedStreamInfo; } set { _muxedStreamInfo = value; } }
        public List<VideoOnlyStreamInfo> VideoOnlyStreamInfo { get { return _videoOnlyStreamInfo; } set { _videoOnlyStreamInfo = value; } }
        public List<AudioOnlyStreamInfo> AudioOnlyStreamInfo { get { return _audioOnlyStreamInfo; } set { _audioOnlyStreamInfo = value; } }
        public StreamManifest StreamManifest { get { return _streamManifest; } set { _streamManifest = value; } }
    }

    public class DownloadSettings
    {
        public bool MuxVideoAndAudio { get; set; }

        public string DownloadPath { get; set; }

        public IStreamInfo DownloadStreamInfo { get; set; }

        public int DownloadStreamType { get; set; }

        public object ParseStream()
        {
            switch (DownloadStreamType)
            {
                case 0:
                    return (MuxedStreamInfo)DownloadStreamInfo;

                case 1:
                    return (VideoOnlyStreamInfo)DownloadStreamInfo;

                case 2:
                    return (AudioOnlyStreamInfo)DownloadStreamInfo;

                default:
                    return null;
            }
        }
    }
}
