using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopVision
{
    public class ShopMessage
    {
        public int ID { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsImportant { get; set; }
    }
}