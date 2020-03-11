public static class UserData
{
    private static string username, password;
    private static int sessionCount=0;

        public static string Username 
    {
        get 
        {
            return username;
        }
        set 
        {
            username = value;
        }
    }

    public static string Password 
    {
        get 
        {
            return password;
        }
        set 
        {
            password = value;
        }
    }

    public static int SessionCount 
    {
        get 
        {
            return sessionCount;
        }
        set 
        {
            sessionCount = value;
        }
    }
}