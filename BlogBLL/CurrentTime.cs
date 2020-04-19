using System;

namespace BlogBLL
{
    public class CurrentTime : ICurrentTime
    {
        public DateTime CurrentUTCTime()
        {
            return DateTime.Now.ToUniversalTime();
        }

    }
}
