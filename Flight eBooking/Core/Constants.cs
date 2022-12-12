namespace Flight_eBooking.Core
{
    public class Constants
    {
        public static class Roles
        {
            public const string Administrator = "Administrator";
            public const string Agent = "Agent";
            public const string User = "User";
        }

        public static class Policies
        {
            public const string RequireAdmin = "RequireAdmin";
            public const string RequireAgent = "RequireAgent";
            public const string RequireUser = "RequireUser";
        }
    }
}
