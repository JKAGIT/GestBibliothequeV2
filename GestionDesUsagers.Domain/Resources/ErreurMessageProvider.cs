using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesUsagers.Domain.Resources
{
    using System.Resources;
    using System.Globalization;
    using System.Reflection;

    public static class ErreurMessageProvider
    {
        private static readonly ResourceManager ResourceManager = new ResourceManager(
            "GestionDesUsagers.Domain.Resources.ErreurMessage",
            Assembly.GetExecutingAssembly());

        public static string GetMessage(string key)
        {
            return ResourceManager.GetString(key, CultureInfo.CurrentCulture) ?? key;
        }

        public static string GetMessage(string key, params object[] args)
        {
            string message = GetMessage(key);
            return string.Format(message, args);
        }
    }
}
