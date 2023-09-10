namespace ProjectOedipus.Common
{
    public static class Utility
    {
        public static Uri CreateUri(string url)
        {
            var uriBuilder = new UriBuilder(url);
            return uriBuilder.Uri;
        }

        public static double ConvertHoursToMs(int hrs)
        {
            int minutes = hrs * 60;
            double milliseconds = (minutes * 1000) * 60;
            return milliseconds;
        }

        public static DateTime AddTime(DateTime current, int operand)
        {
            return current.AddHours(operand);
        }
    }
}