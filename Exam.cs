using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectV1
{
    public class Exam
    {
        private int _id;
        private Course _course;
        private DateTime _date;
        private int _duration;
        private float _grade;
        private List<Question> _questions;
        private int _nbQuestions;

        public Exam(int id, Course course, DateTime date, int duration, float grade, List<Question> questions, int nbQuestions)
        {
            _id = id;
            _course = course;
            _date = date;
            _duration = duration;
            _grade = grade;
            _questions = questions;
            _nbQuestions = nbQuestions;
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

        public DateTime date
        {
            get { return _date; }
            set { _date = value; }
        }

        public int duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public float grade
        {
            get { return _grade; }
            set { _grade = value; }
        }

        public List<Question> questions
        {
            get { return _questions; }
            set { _questions = value; }
        }

        public int nbQuestions
        {
            get { return _nbQuestions; }
            set { _nbQuestions = value; }
        }
    }
}