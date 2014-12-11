using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace MindGame.Utils
{
    public class Utilities
    {
        #region Private Variable

        static string connString = "db_Blessit";

        #endregion

        #region Public Properties

        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings[connString];
            }
        }

        #endregion
    }
}
