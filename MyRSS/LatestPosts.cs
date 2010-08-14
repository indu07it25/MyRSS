using System.Collections.ObjectModel;

namespace MyRSS
{
	public class LatestPosts : ObservableCollection<Post>
	{
		public static LatestPosts latestPosts;
	}
}
