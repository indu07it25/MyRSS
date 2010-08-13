using System.Collections.ObjectModel;

namespace MyRSS
{
	public class LatestPosts : ObservableCollection<Post>
	{
		/// <summary>
		/// Create a default list of cities and their latitudes and longitudes
		/// </summary>
		public void LoadDefaultData()
		{
			MainPage.latestPosts.Add(new Post("Test Post", "2010-08-14", "This is a test post"));
			MainPage.latestPosts.Add(new Post("Test Post 2", "2010-08-14", "This is a test post for 2"));
			MainPage.latestPosts.Add(new Post("Test Post 3", "2010-08-14", "This is a test post for 3"));
		}
	}
}
