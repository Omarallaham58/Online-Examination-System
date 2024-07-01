using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectV1
{
    public class Question
    {
        private int _id;
        private Course _course;
        private string _text;
        private List<Option> _options;
        private float _grade;

        public Question(int id, Course course, string text, List<Option> options, float grade)
        {
            _id = id;
            _course = course;
            _text = text;
            _options = options;
            _grade = grade;
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Course course
        {
            get { return _course; }
            set { _course = value; }
        }

        public string text
        {
            get { return _text; }
            set { _text = value; }
        }

        public List<Option> options
        {
            get { return _options; }
            set { _options = value; }
        }

        public float grade
        {
            get { return _grade; }
            set { _grade = value; }
        }
    }
}