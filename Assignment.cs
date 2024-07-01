using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectV1
{
    public class Assignment
    {
        private int _id;
        private User _student;
        private Course _course;
        private int _passed;

        public Assignment(int id, User student, Course course, int passed)
        {
            _id = id;
            _student = student;
            _course = course;
            _passed = passed;
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public User student
        {
            get { return _student; }
            set { _student = value; }
        }

        public Course course
        {
            get { return _course; }
            set { _course = value; }
        }

        public int passed
        {
            get { return _passed; }
            set { _passed = value; }
        }
    }
}