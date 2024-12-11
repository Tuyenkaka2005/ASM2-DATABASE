using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Shoes
{
    public static class UserSession
    {
        public static string UserRole { get; set; }

        public static bool IsAdmin()
        {
            return UserRole?.Equals("Admin", StringComparison.OrdinalIgnoreCase) ?? false;
        }

        public static bool IsStaff()
        {
            return UserRole?.Equals("Staff", StringComparison.OrdinalIgnoreCase) ?? false;
        }

        public static bool IsWarehouse()
        {
            return UserRole?.Equals("WareHouse", StringComparison.OrdinalIgnoreCase) ?? false;
        }
    }
}
