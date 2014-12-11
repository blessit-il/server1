using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MindGame.Utils
{
    public class Common
    {
    }

    public enum StatusCode
    {
        REGISTRATIONFAILURE = 1000,
        REGISTRATIONSUCCESS = 1001,
        EMAILEXISTS = 1002,
        OPERATIONSUCCESS = 1003,
        OPERATIONFAILED = 1004,
        RECORDEXISTS = 1005
    }

    public class Messages
    {
        public const string CON_REGISTRATION_FAILURE_MSG = "Registration failed due to some error.";
        public const string CON_REGISTRATION_SUCCESS_MSG = "Registration successfully.";
        public const string CON_REGISTRATION_EMAIL_EXIST = "Email already exists.";
        public const string CON_OPERATION_FAILED = "Operation failed due to some error.";
        public const string CON_OPERATION_SUCCESS = "Operation done successfully.";
        public const string CON_RECORD_ALREADY_EXIST = "Record already exists.";
    }
}
