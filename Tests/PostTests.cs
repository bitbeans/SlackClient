using NUnit.Framework;
using SlackClient;
namespace Tests
{
	public class PostTests
    {
		[Test]
		public void SendMessage()
		{
			const string hook = "https://hooks.slack.com/services/<fill>";
			const string text = "Hello Slack";
			const string user = "tester";
			const string channel = "#general";

			var slackClient = new Client(hook);
			var r = slackClient.PostMessage(text, user, channel);
			Assert.AreEqual(true, r);
		}
    }
}
