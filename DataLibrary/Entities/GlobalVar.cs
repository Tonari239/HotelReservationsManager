using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Entities
{
    public static class GlobalVar
    {

        public const int InlcludedBreakfastExtraBillPercentage = 5;

        public const int AllInclusiveExtraBillPercentage = 12;

        public const int DefaultReservationHourStart = 12;

        public const decimal PriceForSingleBed = 10.5m;
        public const decimal PriceForDoubleBed = 13.5m;
        public const decimal PriceForApartment = 15;
        public const decimal PriceForMaisonette = 20;
        public const decimal PriceForPenthouse = 30;
        public static int AmountOfElementsDisplayedPerPage { set; get; } = 10;

        public static int LoggedOnUserId { set; get; } = -1; 
        public enum UserRights
        {
            Admininstrator,
            DefaultUser
        }
        public static UserRights LoggedOnUserRights { set; get; } = UserRights.DefaultUser;


    }
}
