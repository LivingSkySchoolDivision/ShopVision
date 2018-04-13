using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopVision
{
    public class UserPermissionResponse
    {
        public bool CanUserUseSystem { get; set; }
        public bool IsAdministrator { get; set; }
    }
}