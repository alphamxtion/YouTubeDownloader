using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode.Videos.Streams;

namespace YouTubeDownloader.Forms
{
    public partial class DownloadVideo : Form
    {
        public Core.Utils.ExtendedVideo video;

        private Core.Core core;

        public DownloadVideo(string videoUrl)
        {
            InitializeComponent();

            core = new Core.Core();
            GetVideo(videoUrl);
        }

        private async void GetVideo(string videoUrl)
        {
            UpdateStatus("Loading video information, please wait ...");

            video = await Task.Run(new Func<Task<Core.Utils.ExtendedVideo>> (async () =>
            {
                return await core.GetExtendedVideo(videoUrl);
            }));

            Task.Run(new Action(() => 
            {
                while (video == null)
                {
                    UpdateStatus("Loading video information, waiting for video details to load, please wait ...");
                }           
            })).Wait();

            FillListView();
        }

        private async void FillListView()
        {           
            await Task.Run(new Action(() =>
            {
                UpdateStatus("Loading download information, please wait ...");

                foreach (VideoOnlyStreamInfo info in video.VideoSettings.VideoOnlyStreamInfo.OrderByDescending(a => a.VideoQuality.MaxHeight))
                {
                    Core.Utils.YouTubeVideoViewItem item = core.GenerateYouTubeVideoViewItem(video);

                    item.DownloadSettings.DownloadStreamInfo = info;
                    item.DownloadSettings.DownloadStreamType = 1;                    

                    item.Text = $"{info.VideoQuality.Label} ({info.VideoResolution.Width} x {info.VideoResolution.Height})";                 
                    item.SubItems.Add(info.VideoCodec);
                    item.SubItems.Add($"{Math.Round(info.Size.MegaBytes, 2)} MB");
                    item.SubItems.Add(info.Container.Name);

                    item.Group = downloadTypesListView.Groups[0];

                    Invoke(new Action(() => { downloadTypesListView.Items.Add(item); }));
                }

                foreach (AudioOnlyStreamInfo info in video.VideoSettings.AudioOnlyStreamInfo.OrderByDescending(a => a.Bitrate.KiloBitsPerSecond))
                {
                    Core.Utils.YouTubeVideoViewItem item = core.GenerateYouTubeVideoViewItem(video);

                    item.DownloadSettings.DownloadStreamInfo = info;
                    item.DownloadSettings.DownloadStreamType = 2;

                    item.Text = $"{Math.Round(info.Bitrate.KiloBitsPerSecond, 3)} kb/s"; 
                    item.SubItems.Add(info.AudioCodec);
                    item.SubItems.Add($"{Math.Round(info.Size.MegaBytes, 2)} MB");
                    item.SubItems.Add(info.Container.Name);
                    
                    item.Group = downloadTypesListView.Groups[1];

                    Invoke(new Action(() => { downloadTypesListView.Items.Add(item); }));
                }
            }));

            UpdateStatus("Loaded all information.");
        }

        private void DownloadVideo_Load(object sender, EventArgs e)
        {
            
        }

        private void downloadTypesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (downloadTypesListView.SelectedItems.Count > 0)
            {
                
            }
        }

        private string ParseText(dynamic info)
        {
            StringBuilder builder = new StringBuilder();

            if (info is IVideoStreamInfo)
            {
                builder.Append(info.VideoQuality.Label);
            }

            builder.Append(" - " + info.Container.Name);

            return builder.ToString();
        }

        private void UpdateStatus(string text)
        {
            if (this.IsHandleCreated) this.Invoke(new Action(() => statusLabel.Text = $"Status: {text}")); else statusLabel.Text = $"Status: {text}";
        }
    }
}
