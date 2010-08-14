using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace MyRSS
{
    public partial class MainPage : PhoneApplicationPage
    {
		private Timer pgTimer;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

			if (LatestPosts.latestPosts == null)
			{
				//Start the update of progress bar
				pgCurrentStatus.Visibility = Visibility.Visible;
				TimerCallback pgTimerCallback = new TimerCallback(pgTick);
				pgTimer = new Timer(pgTimerCallback, null, 0, 10);
				
				LatestPosts.latestPosts = new LatestPosts();
				Reader rssReader = new Reader();
				rssReader.getRSSItems("http://blog.visionsource.org/feed/");
			}

			lbLatestPosts.ItemsSource = LatestPosts.latestPosts;
        }

		private void lbLatestPosts_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (lbLatestPosts.SelectedItems.Count != 0)
			{
				//MessageBox.Show(lbLatestPosts.SelectedIndex)
				NavigationService.Navigate(new Uri("/ViewPost.xaml?item=" + lbLatestPosts.SelectedIndex, UriKind.Relative));
			}
		}

		public void pgTick(Object stateInfo)
		{
			System.Windows.Deployment.Current.Dispatcher.BeginInvoke(updateProgressBar);
		}

		private void updateProgressBar()
		{
			if (Reader.isRunning == 1)
			{
				if (pgCurrentStatus.Value == 100)
				{
					pgCurrentStatus.Value = 1;
				}

				else
				{
					pgCurrentStatus.Value++;
				}
			}

			else
			{
				pgCurrentStatus.Value = 0;
				pgCurrentStatus.Visibility = Visibility.Collapsed;
				pgTimer.Dispose();
			}
		}
    }
}
