using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectV1
{
    public class Grade
    {
        private int _id;
        private Exam _exam;
        private User _student;
        private float _value;

        public Grade(int id, Exam exam, User student, float value)
        {
            _id = id;
            _exam = exam;
            _student = student;
            _value = value;
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Exam exam
        {
            get { return _exam; }
            set { _exam = value; }
        }

        public User student
        {
            get { return _student; }
            set { _student = value; }
        }

        public float value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}