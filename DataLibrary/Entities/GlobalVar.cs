<<<<<<< Updated upstream
﻿namespace DataLibrary.Entities
{
    public static class GlobalVar
    {
        public const int InlcludedBreakfastExtraBillPercentage = 5;

        public const int AllInclusiveExtraBillPercentage = 10;

        public const int DefaultReservationHourStart = 12;
=======
﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Entities
{
    public static class GlobalVar
    {

        public const int InlcludedBreakfastExtraBillPercentage = 5;

        public const int AllInclusiveExtraBillPercentage = 12;

        public const int DefaultReservationHourStart = 12;

>>>>>>> Stashed changes
        public static int AmountOfElementsDisplayedPerPage { set; get; } = 10;

        public static int LoggedOnUserId { set; get; } = -1;
        public enum UserRights
        {
            Admininstrator,
            DefaultUser
        }
        public static UserRights LoggedOnUserRights { set; get; } = UserRights.DefaultUser;
<<<<<<< Updated upstream
    }
}
=======


    }
}
>>>>>>> Stashed changes
