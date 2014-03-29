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
        private Dictionary<System.Int32, System.Uri> fileList = new Dictionary<System.Int32, System.Uri>();

        private Int32 nextVideo = 1;

        private bool loop = false;

        public MainWindow()
        {
            InitializeComponent();

            LoadFiles();
        }

        private void LoadFiles()
        {
            String videoDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "Presentation");

            if (System.IO.Directory.Exists(videoDir))
            {
                var files = from p in System.IO.Directory.GetFiles(videoDir)
                            where Path.GetExtension(p) == ".wmv" || Path.GetExtension(p) == ".mp4" || Path.GetExtension(p) == ".m4v"
                            orderby p
                            select p;

                Int32 key = 1;

                fileList.Clear();

                foreach (var i in files)
                    fileList.Add(key++, new Uri(i));

                if (fileList.Count > 1)
                    mainMediaElement.Source = fileList[1];
            }
            else
            {
                System.IO.Directory.CreateDirectory(videoDir);
                MessageBox.Show("Please put your videos in a folder named \"Presentation\" inside your \"My Videos\" folder. Here, I'll open it for you.", "CBC Media Presenter");
                Process.Start(videoDir);
                CloseApplication();
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D1:
                    if (fileList.Count >= 1)
                    {
                        loop = false;
                        nextVideo = 1;
                    }
                    break;
                case Key.D2:
                    if (fileList.Count >= 2)
                    {
                        loop = false;
                        nextVideo = 2;
                    }
                    break;
                case Key.D3:
                    if (fileList.Count >= 3)
                    {
                        loop = false;
                        nextVideo = 3;
                    }
                    break;
                case Key.D4:
                    if (fileList.Count >= 4)
                    {
                        loop = false;
                        nextVideo = 4;
                    }
                    break;
                case Key.D5:
                    if (fileList.Count >= 5)
                    {
                        loop = false;
                        nextVideo = 5;
                    }
                    break;
                case Key.D6:
                    if (fileList.Count >= 6)
                    {
                        loop = false;
                        nextVideo = 6;
                    }
                    break;
                case Key.D7:
                    if (fileList.Count >= 7)
                    {
                        loop = false;
                        nextVideo = 7;
                    }
                    break;
                case Key.D8:
                    if (fileList.Count >= 8)
                    {
                        loop = false;
                        nextVideo = 8;
                    }
                    break;
                case Key.D9:
                    if (fileList.Count >= 9)
                    {
                        loop = false;
                        nextVideo = 9;
                    }
                    break;
                case Key.D0:
                    if (fileList.Count >= 10)
                    {
                        loop = false;
                        nextVideo = 10;
                    }
                    break;
                case Key.L:
                    LoadFiles();
                    break;
                case Key.X:
                    loop = !loop;
                    break;
                case Key.Space:
                    if (mainMediaElement.Source == fileList[nextVideo])
                    {
                        mainMediaElement.Play();
                    }
                    else
                    {
                        mainMediaElement.Stop();
                        mainMediaElement.Close();
                        mainMediaElement.Source = fileList[nextVideo];
                        mainMediaElement.Play();
                    }
                    break;
                case Key.S:
                    loop = false;
                    mainMediaElement.Stop();
                    mainMediaElement.Close();
                    break;
                case Key.P:
                    mainMediaElement.Pause();
                    break;
                case Key.Escape:        // close if escape is pressed
                    this.CloseApplication();
                    break;
                default:    // do nothing
                    break;
            }
        }

        private void mainMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (loop)
            {
                mainMediaElement.Position = new TimeSpan(0, 0, 0);
                mainMediaElement.Play();
            }
            else
            {
                mainMediaElement.Close();
            }
        }

        private void CloseApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
