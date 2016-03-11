using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using SlackClient.Model;

namespace SlackClient
{
	/// <summary>
	///     A simple C# class to post messages to a Slack channel.
	///     Gist by: John Gleason @jogleasonjr
	/// </summary>
	/// <see cref="https://gist.github.com/jogleasonjr/7121367" />
	public class Client
	{
		private readonly Encoding _encoding = new UTF8Encoding();
		private readonly Uri _uri;

		public Client(string urlWithAccessToken)
		{
			_uri = new Uri(urlWithAccessToken);
		}

		/// <summary>
		///     Post a message using simple strings.
		/// </summary>
		/// <param name="text">The text to send.</param>
		/// <param name="username">The username.</param>
		/// <param name="channel">The channel to post, eg: #general</param>
		/// <returns><c>true</c> on success, otherwise <c>false</c>.</returns>
		public bool PostMessage(string text, string username = null, string channel = null)
		{
			var payload = new Payload
			{
				Channel = channel,
				Username = username,
				Text = text
			};

			return PostMessage(payload);
		}

		/// <summary>
		///     Post a message using a Payload object.
		/// </summary>
		/// <param name="payload"></param>
		/// <returns><c>true</c> on success, otherwise <c>false</c>.</returns>
		public bool PostMessage(Payload payload)
		{
			var payloadJson = JsonConvert.SerializeObject(payload);

			using (var client = new WebClient())
			{
				var data = new NameValueCollection {["payload"] = payloadJson};
				var response = client.UploadValues(_uri, "POST", data);

				//The response text is usually "ok"
				var responseText = _encoding.GetString(response);
				return responseText.Equals("ok");
			}
		}
	}
}