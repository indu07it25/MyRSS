using System.Windows;
using System.ComponentModel;

namespace MyRSS
{
	public class Post : INotifyPropertyChanged
	{
		private string postTitle;
		private string postDate;
		private string postContent;
		private string postUrl;

		public string PostTitle
		{
			get
			{
				return postTitle;
			}
			set
			{
				if (value != postTitle)
				{
					postTitle = value;

				}
			}
		}

		public string PostDate
		{
			get
			{
				return postDate;
			}
			set
			{
				if (value != postDate)
				{
					postDate = value;
				}
			}
		}

		public string PostContent
		{
			get
			{
				return postContent;
			}
			set
			{
				if (value != postContent)
				{
					postContent = value;
				}
			}
		}

		public string PostURL
		{
			get
			{
				return postUrl;
			}
			set
			{
				if (value != postUrl)
				{
					postUrl = value;
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public Post(string postTitle, string postDate, string postContent, string postUrl)
		{
			PostTitle = postTitle;
			PostDate = postDate;
			PostContent = postContent;
			PostURL = postUrl;

			Deployment.Current.Dispatcher.BeginInvoke(() => { LatestPosts.latestPosts.Add(this); }); ;
		}

		/// <summary>
		/// Raise the PropertyChanged event and pass along the property that changed
		/// </summary>
		private void NotifyPropertyChanged(string property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}
	}
}
