using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.IO;

namespace OliveMediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<Int32, Uri> fileList = new Dictionary<Int32, Uri>(); // list of video files

        private Int32 nextVideo = 1; // current video position

        private bool loop = false; // Should the video loop? Default is no

        public MainWindow()
        {
            InitializeComponent();

            LoadFiles();    // on startup load all of the found video files
        }

        private void LoadFiles()    // loads the video files 
        {
            String videoDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "Presentation"); // On Windows 7 it would be C:\users\<username>\Video\Presentation to get the files from

            if (System.IO.Directory.Exists(videoDir))
            {
                var files = from p in System.IO.Directory.GetFiles(videoDir)
                            where Path.GetExtension(p) == ".wmv" || Path.GetExtension(p) == ".mp4" || Path.GetExtension(p) == ".m4v" || Path.GetExtension(p) == ".avi"
                            orderby p
                            select p;

                Int32 key = 1;

                fileList.Clear();

                foreach (var i in files)
                    fileList.Add(key++, new Uri(i));

                if (fileList.Count > 1)
                    PlayerMediaElement.Source = fileList[1];
            }
            else
            {
                System.IO.Directory.CreateDirectory(videoDir);
                MessageBox.Show("Please put your videos in a folder named \"Presentation\" inside your \"My Videos\" folder. Here, I'll open it for you.", "Olive Media Player");
                Process.Start(videoDir);
                CloseApplication();
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D1:    // 1 pressed: load file 1
                    if (fileList.Count >= 1)
                    {
                        loop = false;
                        nextVideo = 1;
                    }
                    break;
                case Key.D2:    // 2 pressed: load file 2
                    if (fileList.Count >= 2)
                    {
                        loop = false;
                        nextVideo = 2;
                    }
                    break;
                case Key.D3:    // 3 pressed: load file 3
                    if (fileList.Count >= 3)
                    {
                        loop = false;
                        nextVideo = 3;
                    }
                    break;
                case Key.D4:    // 4 pressed: load file 4
                    if (fileList.Count >= 4)
                    {
                        loop = false;
                        nextVideo = 4;
                    }
                    break;
                case Key.D5:    // 5 pressed: load file 5
                    if (fileList.Count >= 5)
                    {
                        loop = false;
                        nextVideo = 5;
                    }
                    break;
                case Key.D6:    // 6 pressed: load file 6
                    if (fileList.Count >= 6)
                    {
                        loop = false;
                        nextVideo = 6;
                    }
                    break;
                case Key.D7:    // 7 pressed: load file 7
                    if (fileList.Count >= 7)
                    {
                        loop = false;
                        nextVideo = 7;
                    }
                    break;
                case Key.D8:    // 8 pressed: load file 8
                    if (fileList.Count >= 8)
                    {
                        loop = false;
                        nextVideo = 8;
                    }
                    break;
                case Key.D9:    // 9 pressed: load file 9
                    if (fileList.Count >= 9)
                    {
                        loop = false;
                        nextVideo = 9;
                    }
                    break;
                case Key.D0:    // 0 pressed: load file 10
                    if (fileList.Count >= 10)
                    {
                        loop = false;
                        nextVideo = 10;
                    }
                    break;
                case Key.R:     // R pressed: reload all files
                    LoadFiles();
                    break;
                case Key.L:     // L pressed: enable single file loop
                    loop = !loop;
                    break;
                case Key.Space: // space pressed: start next element if one is selected, otherwise start or stop media
                    if (PlayerMediaElement.Source == fileList[nextVideo])
                    {
                        PlayerMediaElement.Play();
                    }
                    else
                    {
                        PlayerMediaElement.Stop();
                        PlayerMediaElement.Close();
                        PlayerMediaElement.Source = fileList[nextVideo];
                        PlayerMediaElement.Play();
                    }
                    break;
                case Key.S: // S pressed: stop media playback
                    loop = false;
                    PlayerMediaElement.Stop();
                    PlayerMediaElement.Close();
                    break;
                case Key.P: // P pressed: pause media playback
                    PlayerMediaElement.Pause();
                    break;
                case Key.Escape:        // close if escape is pressed
                    this.CloseApplication();
                    break;
                default:    // do nothing
                    break;
            }
        }

        private void PlayerMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (loop)   // loop if selected
            {
                PlayerMediaElement.Position = new TimeSpan(0, 0, 0);
                PlayerMediaElement.Play();
            }
            else        // close media when playback ended if loop is not selected
            {
                PlayerMediaElement.Close();
            }
        }

        private void CloseApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
