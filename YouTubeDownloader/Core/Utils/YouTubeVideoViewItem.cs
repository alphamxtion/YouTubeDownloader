using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace YouTubeDownloader.Core.Utils
{
    public class YouTubeVideoViewItem : ListViewItem
    {
        public DownloadSettings DownloadSettings { get; set; }

        public ExtendedVideo Video { get; set; }
    }
}
