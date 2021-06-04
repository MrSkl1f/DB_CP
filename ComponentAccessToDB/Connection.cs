using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentAccessToDB
{
    public enum Permissions : int
    {
        Guest,
        Analytic,
        Manager,
        Moder
    }
    public static class Connection
    {
        public static string GetConnection(int perms, IConfiguration config)
        {
            if (perms == (int)Permissions.Guest)
            {
                return config["Connections:ConnectAsGuest"];
            }
            else
            {
                return config["Connections:ConnectAsModer"];
            }
        }
    }
}
