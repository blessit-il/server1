namespace Football.Data
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using MindGame.Utils;


    public class Registration : DataAccessor
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
        public Registration()
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

        // <summary>
        // Executes the stored procedure.
        // </summary>
        // <returns>
        // Returns an int representing the number of records affected.
        // </returns>
        public int ExecuteNonQuery()
        {
            int recordCount;
            SqlParameter returnValueParam;
            SqlParameter[] parameters;
            SqlParameter usernameParam;
            SqlParameter passwordParam;
            SqlParameter phoneParam;
            SqlParameter contactParam;
            SqlParameter addressParam;
            SqlParameter ageParam;
            SqlParameter emailParam;
            SqlParameter genderIdParam;

            parameters = new SqlParameter[9];
            usernameParam = new SqlParameter();
            usernameParam.SqlDbType = SqlDbType.VarChar;
            usernameParam.Direction = ParameterDirection.Input;
            usernameParam.ParameterName = "@Username";
            usernameParam.Size = 250;
            usernameParam.Value = username;
            parameters[0] = usernameParam;
            passwordParam = new SqlParameter();
            passwordParam.SqlDbType = SqlDbType.VarChar;
            passwordParam.Direction = ParameterDirection.Input;
            passwordParam.ParameterName = "@Password";
            passwordParam.Size = 250;
            passwordParam.Value = password;
            parameters[1] = passwordParam;
            phoneParam = new SqlParameter();
            phoneParam.SqlDbType = SqlDbType.VarChar;
            phoneParam.Direction = ParameterDirection.Input;
            phoneParam.ParameterName = "@Phone";
            phoneParam.Size = 50;
            phoneParam.Value = phone;
            parameters[2] = phoneParam;
            contactParam = new SqlParameter();
            contactParam.SqlDbType = SqlDbType.VarChar;
            contactParam.Direction = ParameterDirection.Input;
            contactParam.ParameterName = "@Contact";
            contactParam.Size = 50;
            contactParam.Value = contact;
            parameters[3] = contactParam;
            addressParam = new SqlParameter();
            addressParam.SqlDbType = SqlDbType.VarChar;
            addressParam.Direction = ParameterDirection.Input;
            addressParam.ParameterName = "@Address";
            addressParam.Size = 50;
            addressParam.Value = address;
            parameters[4] = addressParam;
            ageParam = new SqlParameter();
            ageParam.SqlDbType = SqlDbType.Int;
            ageParam.Direction = ParameterDirection.Input;
            ageParam.ParameterName = "@Age";
            ageParam.Precision = 10;
            ageParam.Value = age;
            parameters[5] = ageParam;
            emailParam = new SqlParameter();
            emailParam.SqlDbType = SqlDbType.VarChar;
            emailParam.Direction = ParameterDirection.Input;
            emailParam.ParameterName = "@Email";
            emailParam.Size = 250;
            emailParam.Value = email;
            parameters[6] = emailParam;
            genderIdParam = new SqlParameter();
            genderIdParam.SqlDbType = SqlDbType.Bit;
            genderIdParam.Direction = ParameterDirection.Input;
            genderIdParam.ParameterName = "@GenderId";
            genderIdParam.Value = genderId;
            parameters[7] = genderIdParam;
            returnValueParam = new SqlParameter();
            returnValueParam.Direction = ParameterDirection.ReturnValue;
            returnValueParam.ParameterName = "@returnValue";
            parameters[8] = returnValueParam;
            recordCount = SqlDataHelper.ExecNonQuery(ConnectionString, "Registration", CommandType.StoredProcedure, parameters);
            returnValue = Convert.ToInt64(returnValueParam.Value);
            return recordCount;
        }

        // <summary>
        // Executes the stored procedure.
        // </summary>
        // <returns>
        // Returns an object representing the value in the first column of the first row of the result set.
        // </returns>
        public object ExecuteScalar()
        {
            object result;
            SqlParameter returnValueParam;
            SqlParameter[] parameters;
            SqlParameter usernameParam;
            SqlParameter passwordParam;
            SqlParameter phoneParam;
            SqlParameter contactParam;
            SqlParameter addressParam;
            SqlParameter ageParam;
            SqlParameter emailParam;
            SqlParameter genderIdParam;

            parameters = new SqlParameter[9];
            usernameParam = new SqlParameter();
            usernameParam.SqlDbType = SqlDbType.VarChar;
            usernameParam.Direction = ParameterDirection.Input;
            usernameParam.ParameterName = "@Username";
            usernameParam.Size = 250;
            usernameParam.Value = username;
            parameters[0] = usernameParam;
            passwordParam = new SqlParameter();
            passwordParam.SqlDbType = SqlDbType.VarChar;
            passwordParam.Direction = ParameterDirection.Input;
            passwordParam.ParameterName = "@Password";
            passwordParam.Size = 250;
            passwordParam.Value = password;
            parameters[1] = passwordParam;
            phoneParam = new SqlParameter();
            phoneParam.SqlDbType = SqlDbType.VarChar;
            phoneParam.Direction = ParameterDirection.Input;
            phoneParam.ParameterName = "@Phone";
            phoneParam.Size = 50;
            phoneParam.Value = phone;
            parameters[2] = phoneParam;
            contactParam = new SqlParameter();
            contactParam.SqlDbType = SqlDbType.VarChar;
            contactParam.Direction = ParameterDirection.Input;
            contactParam.ParameterName = "@Contact";
            contactParam.Size = 50;
            contactParam.Value = contact;
            parameters[3] = contactParam;
            addressParam = new SqlParameter();
            addressParam.SqlDbType = SqlDbType.VarChar;
            addressParam.Direction = ParameterDirection.Input;
            addressParam.ParameterName = "@Address";
            addressParam.Size = 50;
            addressParam.Value = address;
            parameters[4] = addressParam;
            ageParam = new SqlParameter();
            ageParam.SqlDbType = SqlDbType.Int;
            ageParam.Direction = ParameterDirection.Input;
            ageParam.ParameterName = "@Age";
            ageParam.Precision = 10;
            ageParam.Value = age;
            parameters[5] = ageParam;
            emailParam = new SqlParameter();
            emailParam.SqlDbType = SqlDbType.VarChar;
            emailParam.Direction = ParameterDirection.Input;
            emailParam.ParameterName = "@Email";
            emailParam.Size = 250;
            emailParam.Value = email;
            parameters[6] = emailParam;
            genderIdParam = new SqlParameter();
            genderIdParam.SqlDbType = SqlDbType.Bit;
            genderIdParam.Direction = ParameterDirection.Input;
            genderIdParam.ParameterName = "@GenderId";
            genderIdParam.Value = genderId;
            parameters[7] = genderIdParam;
            returnValueParam = new SqlParameter();
            returnValueParam.Direction = ParameterDirection.ReturnValue;
            returnValueParam.ParameterName = "@returnValue";
            parameters[8] = returnValueParam;
            result = SqlDataHelper.ExecScalar(ConnectionString, "Registration", CommandType.StoredProcedure, parameters);
            returnValue = Convert.ToInt64(returnValueParam.Value);
            return result;
        }

        // <summary>
        // Executes the stored procedure.
        // </summary>
        // <returns>
        // Returns an instance of SqlDataReader containing the result set of the stored procedure.
        // </returns>
        public SqlDataReader ExecuteReader()
        {
            SqlDataReader reader;
            SqlParameter returnValueParam;
            SqlParameter[] parameters;
            SqlParameter usernameParam;
            SqlParameter passwordParam;
            SqlParameter phoneParam;
            SqlParameter contactParam;
            SqlParameter addressParam;
            SqlParameter ageParam;
            SqlParameter emailParam;
            SqlParameter genderIdParam;

            parameters = new SqlParameter[9];
            usernameParam = new SqlParameter();
            usernameParam.SqlDbType = SqlDbType.VarChar;
            usernameParam.Direction = ParameterDirection.Input;
            usernameParam.ParameterName = "@Username";
            usernameParam.Size = 250;
            usernameParam.Value = username;
            parameters[0] = usernameParam;
            passwordParam = new SqlParameter();
            passwordParam.SqlDbType = SqlDbType.VarChar;
            passwordParam.Direction = ParameterDirection.Input;
            passwordParam.ParameterName = "@Password";
            passwordParam.Size = 250;
            passwordParam.Value = password;
            parameters[1] = passwordParam;
            phoneParam = new SqlParameter();
            phoneParam.SqlDbType = SqlDbType.VarChar;
            phoneParam.Direction = ParameterDirection.Input;
            phoneParam.ParameterName = "@Phone";
            phoneParam.Size = 50;
            phoneParam.Value = phone;
            parameters[2] = phoneParam;
            contactParam = new SqlParameter();
            contactParam.SqlDbType = SqlDbType.VarChar;
            contactParam.Direction = ParameterDirection.Input;
            contactParam.ParameterName = "@Contact";
            contactParam.Size = 50;
            contactParam.Value = contact;
            parameters[3] = contactParam;
            addressParam = new SqlParameter();
            addressParam.SqlDbType = SqlDbType.VarChar;
            addressParam.Direction = ParameterDirection.Input;
            addressParam.ParameterName = "@Address";
            addressParam.Size = 50;
            addressParam.Value = address;
            parameters[4] = addressParam;
            ageParam = new SqlParameter();
            ageParam.SqlDbType = SqlDbType.Int;
            ageParam.Direction = ParameterDirection.Input;
            ageParam.ParameterName = "@Age";
            ageParam.Precision = 10;
            ageParam.Value = age;
            parameters[5] = ageParam;
            emailParam = new SqlParameter();
            emailParam.SqlDbType = SqlDbType.VarChar;
            emailParam.Direction = ParameterDirection.Input;
            emailParam.ParameterName = "@Email";
            emailParam.Size = 250;
            emailParam.Value = email;
            parameters[6] = emailParam;
            genderIdParam = new SqlParameter();
            genderIdParam.SqlDbType = SqlDbType.Bit;
            genderIdParam.Direction = ParameterDirection.Input;
            genderIdParam.ParameterName = "@GenderId";
            genderIdParam.Value = genderId;
            parameters[7] = genderIdParam;
            returnValueParam = new SqlParameter();
            returnValueParam.Direction = ParameterDirection.ReturnValue;
            returnValueParam.ParameterName = "@returnValue";
            parameters[8] = returnValueParam;
            reader = SqlDataHelper.GetDataReader(ConnectionString, "Registration", CommandType.StoredProcedure, parameters);
            returnValue = Convert.ToInt64(returnValueParam.Value);
            return reader;
        }
    }
}
