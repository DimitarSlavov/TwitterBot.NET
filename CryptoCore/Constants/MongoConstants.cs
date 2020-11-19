namespace CryptoCore.Constants
{
	public class MongoConstants
	{
		public static string MongoConnect { get; private set; }
		public static string JWT { get; set; }

		public static void SetMongoConnect(string mongoConnect)
		{
			MongoConnect = mongoConnect;
		}
	}
}
