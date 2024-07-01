using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectV1
{
    public class Course
    {
        private int _id;
        private User _teacher;
        private string _code;
        private string _name;
        private int _credit;
        private int _nbStudents;

        public Course(int id, User teacher, string code, string name, int credit, int nbStudents)
        {
            _id = id;
            _teacher = teacher;
            _code = code;
            _name = name;
            _credit = credit;
            _nbStudents = nbStudents;
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public User teacher
        {
            get { return _teacher; }
            set { _teacher = value; }
        }

        public string code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int credit
        {
            get { return _credit; }
            set { _credit = value; }
        }

        public int nbStudents
        {
            get { return _nbStudents; }
            set { _nbStudents = value; }
        }
    }
}