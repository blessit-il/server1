using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Football.Data;
using System.Data.SqlClient;
using MindGame.Utils;

namespace Blessit.Business
{
    [Serializable]
    [DataContract]
    public class Users
    {
        #region Private Properties
        /// <summary>
        /// The return value of the stored procedure.
        /// </summary>
        private long returnValue;

        /// <summary>
        /// Represents the parameter @Username
        /// </summary>
        private string username;

        /// <summary>
        /// Represents the parameter @Password
        /// </summary>
        private string password;

        /// <summary>
        /// Represents the parameter @Phone
        /// </summary>
        private string phone;

        /// <summary>
        /// Represents the parameter @Contact
        /// </summary>
        private string contact;

        /// <summary>
        /// Represents the parameter @Address
        /// </summary>
        private string address;

        /// <summary>
        /// Represents the parameter @Age
        /// </summary>
        private int age;

        /// <summary>
        /// Represents the parameter @Email
        /// </summary>
        private string email;

        /// <summary>
        /// Represents the parameter @GenderId
        /// </summary>
        private int genderId;
        #endregion

        #region Constructor
        // <summary>
        // Creates an instance of Registration
        // </summary>
        public Users()
        {
            returnValue = 0;
            username = string.Empty;
            password = string.Empty;
            phone = string.Empty;
            contact = string.Empty;
            address = string.Empty;
            age = 0;
            email = string.Empty;
            genderId = 0;
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the return value from the stored procedure.
        /// </summary>
        public long ReturnValue
        {
            get
            {
                return returnValue;
            }
        }

        /// <summary>
        /// Gets/Sets the value for the parameter @Username
        /// </summary>
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        /// <summary>
        /// Gets/Sets the value for the parameter @Password
        /// </summary>
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        /// <summary>
        /// Gets/Sets the value for the parameter @Phone
        /// </summary>
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
            }
        }

        /// <summary>
        /// Gets/Sets the value for the parameter @Contact
        /// </summary>
        public string Contact
        {
            get
            {
                return contact;
            }
            set
            {
                contact = value;
            }
        }

        /// <summary>
        /// Gets/Sets the value for the parameter @Address
        /// </summary>
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        /// <summary>
        /// Gets/Sets the value for the parameter @Age
        /// </summary>
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }

        /// <summary>
        /// Gets/Sets the value for the parameter @Email
        /// </summary>
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        /// <summary>
        /// Gets/Sets the value for the parameter @GenderId
        /// </summary>
        public int GenderId
        {
            get
            {
                return genderId;
            }
            set
            {
                genderId = value;
            }
        }

        public string ConnectionString
        {
            get;
            set;
        }

        #endregion

        public static Users Registration(string username, string password, string phone, string contact, string address, int age, string email, int genderId)
        {
            int userId = -1;
            Users objUser = null;

            try
            {
                Registration proc;
                proc = new Registration();
                proc.ConnectionString = Utilities.ConnectionString; ;
                proc.Username = username;
                proc.Password = password;
                proc.Phone = phone;
                proc.Contact = contact;
                proc.Address = address;
                proc.Age = age;
                proc.Email = email;
                proc.GenderId = genderId;
                object obj;

                obj = proc.ExecuteScalar();
                userId = Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                userId = -1;
                objUser = null;
            }
            return objUser;

        }

    }
}
