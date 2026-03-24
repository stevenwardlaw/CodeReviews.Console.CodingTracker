namespace codingTracker.stevenwardlaw
{
    internal static class DataValidation
    {
        public static bool ValidateDate(string date)
        {
            return DateTime.TryParseExact(date, "yyyy-MM-dd HH:mm", null, 0, out DateTime result);
        }

        public static bool ValidateNumber(string num)
        {
            return Int16.TryParse(num, out short result);
        }

        public static bool IsEndDateAfter(string _startTime, string _endTime)
        {
            DateTime startTime = DateTime.ParseExact(_startTime, "yyyy-MM-dd HH:mm", null);
            DateTime endTime = DateTime.ParseExact(_endTime, "yyyy-MM-dd HH:mm", null);
            if (endTime > startTime) return true;
            else return false;
        }
    }
}
