using System;
using System.Net;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace MyRSS
{
	public class Reader
	{
		public void getRSSItems(string url)
		{
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

				// load the XML
				XElement xmlPosts = XElement.Load(streamResult);
			}

			catch (WebException ex)
			{
				//Need to invoke a message box here
				//MessageBox.Show("Unable to load RSS feed. Please check the URL is valid and correct.");
			}
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
