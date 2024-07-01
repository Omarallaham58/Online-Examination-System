using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectV1
{
    public class Account
    {
        private int _id;
        private string _username;
        private string _password;

        public Account(int id, string username, string password)
        {
            _id = id;
            _username = username;
            _password = password;
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}