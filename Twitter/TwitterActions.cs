using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Streaming;
using TweetinviUser = Tweetinvi.User;

namespace Twitter
{
    internal class TwitterActions : ITwitterActions
    {
        public TwitterActions()
        {
            this.User = new UserSettings
            (
                "",
                "",
                "",
                ""
            );

            this.User.SetAdditionalParams(0);

            User.Authenticate();

            RateLimit.RateLimitTrackerMode = RateLimitTrackerMode.TrackAndAwait;
        }

        public TwitterActions(
            string consumerKey,
            string consumerSecret,
            string userAccessToken,
            string userAccessSecret)
        {
            this.User = new UserSettings
            (
                consumerKey,
                consumerSecret,
                userAccessToken,
                userAccessSecret
            );

            User.Authenticate();
        }

        public TwitterActions(
            string consumerKey,
            string consumerSecret,
            string userAccessToken,
            string userAccessSecret,
            long accountId)
        {
            this.User = new UserSettings
            (
                consumerKey,
                consumerSecret,
                userAccessToken,
                userAccessSecret
            );

            this.User.SetAdditionalParams(accountId);

            User.Authenticate();
        }

        private UserSettings User { get; set; }

        private async Task<bool?> LikeAndRetweetIfNotSo(ITweet tweet)
        {
            if (tweet != null)
            {
                if (tweet.Favorited && tweet.Retweeted)
                {
                    return false;
                }

                var delay = new Random().Next(1200, 2800);

                await Task.Delay(delay);

                if (!tweet.Favorited)
                {
                    tweet.Favorite();
                }

                delay = new Random().Next(1200, 2800);

                await Task.Delay(delay);

                if (!tweet.Retweeted)
                {
                    tweet.PublishRetweet();
                }

                return true;
            }

            return null;
        }

        private void ValidateNumberOfTweets(int tweetsCount)
        {
            if (tweetsCount > 40)
            {
                throw new Exception("Maximum number of tweets must be below 40!");
            }
        }

        public void PublishTweet(string tweet)
        {
            ExceptionHandler.SwallowWebExceptions = false;

            try
            {
                Tweet.PublishTweet(tweet);
            }
            catch (Exception e)
            {
                Helpers.ErrMsg("Couldn't send status", e.Message);
            }
        }

        public IList<string> GetTimelineStatuses()
        {
            IList<string> list = new List<string>();

            try
            {
                IEnumerable<ITweet> statuses = GetUserTimeline();

                statuses.ToList().ForEach(s => list.Add(s.Text));
            }
            catch (Exception e)
            {
                Helpers.ErrMsg("Couldn't get timeline statuses", e.Message);
            }

            return list;
        }

        public IEnumerable<IUser> GetFollowers()
        {
            IEnumerable<IUser> list = default;

            try
            {
                list = TweetinviUser.GetFollowers(UserSettings.AccountId);
            }
            catch (Exception e)
            {
                Helpers.ErrMsg("Couldn't get followers", e.Message);
                return default;
            }

            return list;
        }

        public IEnumerable<IUser> GetFriends(int maxFriendsToRetrieve = 250)
        {
            IEnumerable<IUser> list = default;

            try
            {
                list = TweetinviUser.GetFriends(UserSettings.AccountId, maxFriendsToRetrieve);
            }
            catch (Exception e)
            {
                Helpers.ErrMsg("Couldn't get followers", e.Message);
                return default;
            }

            return list;
        }

        public void FilteredStream(List<string> tracks)
        {
            IFilteredStream stream = Stream.CreateFilteredStream();

            tracks.ForEach(t => stream.AddTrack(t));

            try
            {
                stream.MatchingTweetReceived += (sender, args) =>
                {
                    Console.WriteLine($"\n{args.Tweet}\n");
                };

                stream.StartStreamMatchingAllConditions();
            }
            catch (Exception e)
            {
                Helpers.ErrMsg("Couldn't get Filtered Streams", e.Message);
            }
        }

        public void FollowUser()
        {
            var followersList = GetFollowers().ToList();

            if (followersList.Count != default)
            {
                bool followed = default;

                try
                {
                    while (!followed)
                    {
                        var randomFollower = new Random().Next(default, followersList.Count());

                        var randomFollowerId = followersList.ElementAt(randomFollower).UserIdentifier.Id;

                        var randomFollowerFriends = TweetinviUser.GetFollowers(randomFollowerId);

                        foreach (var randomFollowersFollower in randomFollowerFriends)
                        {
                            if (randomFollowersFollower.Description.ToLower().Contains("crypto"))
                            {
                                FollowUser(randomFollowersFollower.ScreenName);
                                followed = true;
                                break;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    //implement logging
                }
            }
        }

        public void FollowUser(string userScreenName)
        {
            try
            {
                TweetinviUser.FollowUser(userScreenName);
            }
            catch (Exception e)
            {
                Helpers.ErrMsg("Couldn't follow user", e.Message);
            }
        }

        public void UnFollowUser()
        {
            var friendsList = GetFriends().ToList();

            int randomFriend = default;

            IUser friendsScreenName = default;

            var followersList = GetFollowers();

            bool unfollow = default;

            try
            {
                while (!unfollow)
                {

                    randomFriend = new Random().Next(default, friendsList.Count());

                    if (randomFriend != default)
                    {
                        friendsScreenName = friendsList.ElementAt(randomFriend);
                        friendsList.RemoveAt(randomFriend);
                    }
                    else
                    {
                        break;
                    }

                    if (followersList.Contains(friendsScreenName) ||
                        friendsList.Count == default)
                    {
                        unfollow = true;

                        UnFollowUser(friendsScreenName.ScreenName);
                    }

                }
            }
            catch (Exception)
            {
                //implement logging
            }
        }

        public void UnFollowUsers()
        {
            var friendsList = GetFriends().ToList();

            for (int i = 0; i < friendsList.Count; i++)
            {
                try
                {
                    UnFollowUser(friendsList[i].ScreenName);
                }
                catch (Exception)
                {
                    //implement logging
                }
            }
        }

        public void UnFollowUser(string userScreenName)
        {
            try
            {
                TweetinviUser.UnFollowUser(userScreenName);
            }
            catch (Exception e)
            {
                Helpers.ErrMsg("Couldn't unfollow user", e.Message);
            }
        }

        public void UnFollowUser(long userScreenName)
        {
            try
            {
                TweetinviUser.UnFollowUser(userScreenName);
            }
            catch (Exception e)
            {
                Helpers.ErrMsg("Couldn't unfollow user", e.Message);
            }
        }

        public IEnumerable<ITweet> GetUserTimeline()
        {
            return Timeline.GetUserTimeline(UserSettings.AccountId);
        }

        public bool? LikeAndRetweetLastTweet(string userScreenName)
        {
            try
            {
                var userTimeline = Timeline.GetUserTimeline(userScreenName, 1);

                if (userTimeline == null)
                {
                    return false;
                }

                var lastTweet = userTimeline.First();

                return LikeAndRetweetIfNotSo(lastTweet).GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while liking and retweeting.", ex.InnerException);
            }
        }

        public List<bool?> LikeAndRetweetLastTweets(string userScreenName, int tweetsCount)
        {
            ValidateNumberOfTweets(tweetsCount);

            try
            {
                var userTimeline = Timeline.GetUserTimeline(userScreenName, tweetsCount);

                var result = new List<bool?>();

                foreach (var tweet in userTimeline)
                {
                    result.Add(LikeAndRetweetIfNotSo(tweet).GetAwaiter().GetResult());
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while liking and retweeting.", ex.InnerException);
            }
        }

        public async Task DeleteTweetsAsync()
        {
            while (true)
            {
                var tweets = Timeline.TimelineController.GetUserTimeline(UserSettings.AccountId);

                while (tweets != null && tweets.Any())
                {
                    foreach (var tweet in tweets)
                    {
                        Console.WriteLine(tweet.Text);

                        if (tweet.IsTweetDestroyed == false)
                        {
                            var response = Tweet.DestroyTweet(tweet);
                            Console.WriteLine($"Tweet Destroyed. {DateTime.Now}");
                        }
                    }

                    await Task.Delay(TimeSpan.FromMinutes(1));

                    tweets = Timeline.TimelineController.GetUserTimeline(UserSettings.AccountId);
                }

                await Task.Delay(TimeSpan.FromMinutes(3));
            }
        }
    }
}