using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
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
		public static LatestPosts latestPosts;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

			if (latestPosts == null)
			{
				latestPosts = new LatestPosts();
				latestPosts.LoadDefaultData();
			}

			lbLatestPosts.ItemsSource = latestPosts;

			Reader rssReader = new Reader();
			rssReader.getRSSItems("http://blog.visionsource.org/feed/");
        }
    }
}
