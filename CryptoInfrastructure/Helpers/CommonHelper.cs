using System;
using System.Text.Json;

namespace CryptoInfrastructure.Helpers
{
    public class CommonHelper
    {
        public static Func<int, int, double> RandomNumber = (int from, int to) =>
        {
            return new Random().Next(from, to);
        };

        public static TOut ModelMapper<TIn, TOut>(TIn items)
        {
            try
            {
                var serialized = JsonSerializer.Serialize(items);
                var deserialized = JsonSerializer.Deserialize<TOut>(serialized);

                return deserialized;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while model mapping", ex.InnerException);
            }
        }
    }
}
