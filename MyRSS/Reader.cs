using System;
using System.Net;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;


namespace MyRSS
{
	public class Reader
	{
		public static int isRunning = 0;

		public void getRSSItems(string url)
		{
			isRunning = 1;
			UriBuilder fullUri = new UriBuilder(url);

			HttpWebRequest rssRequest = (HttpWebRequest)WebRequest.Create(fullUri.Uri);

			// set up the state object for the async request
			RSSRequestUpdateState rssState = new RSSRequestUpdateState();
			rssState.AsyncRequest = rssRequest;

			// start the asynchronous request
			rssRequest.BeginGetResponse(new AsyncCallback(HandleRSSResponse), rssState);
		}

		/// <summary>
		/// Handle the information returned from the async request
		/// </summary>
		/// <param name="asyncResult"></param>
		private void HandleRSSResponse(IAsyncResult asyncResult)
		{
			try
			{
				// get the state information
				RSSRequestUpdateState rssState = (RSSRequestUpdateState)asyncResult.AsyncState;
				HttpWebRequest rssRequest = (HttpWebRequest)rssState.AsyncRequest;

				// end the async request
				rssState.AsyncResponse = (HttpWebResponse)rssRequest.EndGetResponse(asyncResult);

				Stream streamResult;

				streamResult = rssState.AsyncResponse.GetResponseStream();

				try
				{
					// load the XML
					XmlReader xmlReader = XmlReader.Create(streamResult);
					SyndicationFeed rssPosts = SyndicationFeed.Load(xmlReader);

					foreach (SyndicationItem sItem in rssPosts.Items)
					{
						//Is this item a post?
						if ((sItem != null) && (sItem.Summary != null) && (sItem.Title != null))
						{
							 new Post(sItem.Title.Text, sItem.PublishDate.ToString(), sItem.Summary.Text, sItem.Id);
						}
					}
				}

				catch (FormatException ex)
				{
					System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => { MessageBox.Show("Error processing RSS feed."); });
				}
			}

			catch (WebException ex)
			{
				System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => { MessageBox.Show("Unable to load RSS feed. Please check the URL is valid and correct."); });
			}

			isRunning = 0;
		}
	}

	/// <summary>
	/// State information for our BeginGetResponse async call
	/// </summary>
	public class RSSRequestUpdateState
	{
		public HttpWebRequest AsyncRequest { get; set; }
		public HttpWebResponse AsyncResponse { get; set; }
	}

}
