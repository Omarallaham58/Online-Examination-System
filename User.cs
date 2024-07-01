using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectV1
{
    public class User
    {
        private int _id;
        private Account _account;
        private string _firstName;
        private string _lastName;
        private DateTime _dob;
        private string _email;
        private string _phoneNb;
        private int _isTeacher;
        private List<Grade> _grades;
        private static int _nbUsers = 0;

        public User(int id, Account account, string firstName, string lastName,
            DateTime dob, string email, string phoneNb, int isTeacher)
        {
            _id = id;
            _account = account;
            _firstName = firstName;
            _lastName = lastName;
            _dob = dob;
            _email = email;
            _phoneNb = phoneNb;
            _isTeacher = isTeacher;
            _nbUsers++;
        }

        public User(int id, Account account, string firstName, string lastName, DateTime dob, string email, string phoneNb,
            int isTeacher, List<Grade> grades) : this(id, account, firstName, lastName, dob, email, phoneNb, isTeacher)
        {
            _grades = grades;
        }
        public static int nbUsers
        {
            get { return _nbUsers; }
            set { _nbUsers = value; }
        }

        public static int GetCurrentNumberOfUsers()
        {
            return _nbUsers;
        }
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Account account
        {
            get { return _account; }
            set { _account = value; }
        }

        public string firstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string lastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public DateTime dob
        {
            get { return _dob; }
            set { _dob = value; }
        }

        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string phoneNb
        {
            get { return _phoneNb; }
            set { _phoneNb = value; }
        }

        public int isTeacher
        {
            get { return _isTeacher; }
            set { _isTeacher = value; }
        }

        public List<Grade> grades
        {
            get { return _grades; }
            set { _grades = value; }
        }
    }
}