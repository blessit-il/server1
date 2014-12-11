using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Security;

namespace MindGame.Utils
{
    public class DataAccessor
    {
        #region Fields
        /// <summary>
        /// Stores the name of connection string provider.
        /// </summary>
        private string m_providerName;

        /// <summary>
        /// Stores the connection string.
        /// </summary>
        private string m_connectionString;
        #endregion

        #region Constructor
        /// <summary>
        /// Default
        /// </summary>
        public DataAccessor()
        {
            m_providerName = string.Empty;
            m_connectionString = string.Empty;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get/Set the provider to be used for connection string.
        /// </summary>
        public string ProviderName
        {
            get
            {
                return m_providerName;
            }
            set
            {
                m_providerName = value;
            }
        }

        /// <summary>
        /// Get the connection string from the provider.
        /// </summary>
       
        #endregion
    }
}
