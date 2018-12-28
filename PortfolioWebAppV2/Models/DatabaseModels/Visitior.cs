using System;

namespace PortfolioWebAppV2.Models.DatabaseModels
{
    public class Visitior
    {
            public Value value { get; set; }
        }

        public class Value
        {
            public DateTime start { get; set; }
            public DateTime end { get; set; }
            public UsersCount userscount { get; set; }
        }

        public class UsersCount
        {
            public int unique { get; set; }
        }

    


}