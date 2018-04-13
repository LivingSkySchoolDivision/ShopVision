using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopVision
{
    public class IconRepository
    {
        List<string> _availableIcons = new List<string>()
        {
            "Amb.png",
            "Bell.png",
            "BirthdayCake.png",
            "BusWhite.png",
            "Clock.png",
            "Coffee.png",
            "Envelope.png",
            "Info.png",
            "Person.png",
            "Phone.png",
            "Snowflake.png",
            "Star.png",
            "ThumbsDown.png",
            "ThumbsUp.png",
            "Trash.png",
            "Umbrella.png",
            "Utensils.png",
            "WarningBlack.png",
            "Warningred.png",
            "WarningWhite.png",
            "Wrench.png"
        };


        public List<string> GetAll()
        {
            return _availableIcons.OrderBy(x => x).ToList();
        }

    }
}