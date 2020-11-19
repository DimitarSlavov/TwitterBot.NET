using System.Collections.Generic;
using Tweetinvi.Models;

namespace Twitter
{
    public interface ITwitterActions
    {
        void PublishTweet(string tweet);

        IList<string> GetTimelineStatuses();

        IEnumerable<IUser> GetFollowers();

        IEnumerable<IUser> GetFriends(int maxFriendsToRetrieve = 250);

        void FilteredStream(List<string> tracks);

        void FollowUser();

        void FollowUser(string userScreenName);

        void UnFollowUser();

        void UnFollowUser(string userScreenName);

        void UnFollowUser(long userScreenName);

        IEnumerable<ITweet> GetUserTimeline();

        bool? LikeAndRetweetLastTweet(string userScreenName);

        List<bool?> LikeAndRetweetLastTweets(string userScreenName, int tweetsCount);
    }
}
