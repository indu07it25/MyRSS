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
	public partial class ViewPost : PhoneApplicationPage
	{
		public ViewPost()
		{
			InitializeComponent();
			this.Loaded += new RoutedEventHandler(DetailsView_Loaded);
		}

		void DetailsView_Loaded(object sender, RoutedEventArgs e)
		{
			string index = "";

			if (NavigationContext.QueryString.TryGetValue("item", out index))
			{
				int _index = int.Parse(index);
				wbPost.Navigate(new Uri(LatestPosts.latestPosts[_index].PostURL));
			}
		}

		private void appbar_close_Click(object sender, EventArgs e)
		{
			NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
		}
	}
}