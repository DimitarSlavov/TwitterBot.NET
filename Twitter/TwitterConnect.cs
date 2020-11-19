using System;
using System.Collections.Generic;
using System.Linq;
using Tweetinvi.Models;

namespace Twitter
{
	internal class TwitterConnect
	{
		public static void Main(string[] args)
		{
			var twitterActions = new TwitterActions();
			string repeat = string.Empty;

			do
			{
				Console.WriteLine("Choose action:\n");

				List<string> actions = new List<string>();

				actions.Add("1. Send tweet status.");
				actions.Add("2. Get timeline statuses.");
				actions.Add("3. Get Followers list.");
				actions.Add("4. Get Friends list.");
				actions.Add("5. Follow User.");
				actions.Add("6. Follow random User.");
				actions.Add("7. Unfollow User.");
				actions.Add("8. Unfollow random User.");
				actions.Add("9. Get Filtered Stream.");
				actions.Add("10. Like and Retweet user last tweet.");
				actions.Add("11. Delete tweets.");

				actions.ForEach(a => Console.WriteLine(a));

				byte action = byte.Parse(Console.ReadLine());

				if (action > default(byte) && action <= actions.Count)
					switch (action)
					{
						case 1:
							Console.WriteLine("Send tweet status:\n");

							twitterActions.PublishTweet(Console.ReadLine());

							Console.WriteLine("Status sent successfully!\n");
							break;
						case 2:
							IList<string> statuses = twitterActions.GetTimelineStatuses();

							if (!statuses.Any())
								Console.WriteLine("You don't have any statuses? Something went wrong.\n");
							else
								statuses.ToList().ForEach(i => Console.WriteLine($"\n{i}\n"));
							break;
						case 3:
							IEnumerable<IUser> followers = twitterActions.GetFollowers();

							if (!followers.Any())
								Console.WriteLine("You don't have any subscribers? Something went wrong.\n");
							else
								followers.ToList().ForEach(i => Console.WriteLine($"\n{i}\n"));
							break;
						case 4:
							IEnumerable<IUser> friends = twitterActions.GetFriends();

							if (!friends.Any())
								Console.WriteLine("You don't have any friends? Something went wrong.\n");
							else
								friends.ToList().ForEach(i => Console.WriteLine($"\n{i}\n"));
							break;
						case 5:
							Console.WriteLine("Write user's name you'd like to follow:\n");

							twitterActions.FollowUser(Console.ReadLine());
							break;
						case 6:
							twitterActions.FollowUser();
							break;
						case 7:
							Console.WriteLine("Write user's name you'd like to unfollow:\n");

							twitterActions.UnFollowUser(Console.ReadLine());
							break;
						case 8:
							twitterActions.UnFollowUser();
							break;
						case 9:
							Console.WriteLine("Write few keywords separated with whitespace:\n");

							List<string> tracks = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

							twitterActions.FilteredStream(tracks);
							break;
						case 10:
							Console.WriteLine("Enter User Screen Name:\n");

							var user = Console.ReadLine();

							twitterActions.LikeAndRetweetLastTweet(user);
							break;
						case 11:
							twitterActions.DeleteTweetsAsync().GetAwaiter().GetResult();
							break;
					}
				else
					Console.WriteLine("This action is not available.\n");

				Console.WriteLine("Continue? (anything but \"yes\")\n");
				repeat = Console.ReadLine();
			}
			while (repeat.Equals("yes"));
		}
	}
}