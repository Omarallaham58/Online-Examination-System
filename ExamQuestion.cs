using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectV1
{
    public class ExamQuestion : Question
    {
        private Exam _exam;

        public ExamQuestion(Question question, Exam exam) : base(question.id, question.course,
            question.text, question.options, question.grade)
        {
            _exam = exam;
        }

        //public int id
        //{
        //    get { return _id; }
        //    set { _id = value; }
        //}

        public Exam exam
        {
            get { return _exam; }
            set { _exam = value; }
        }
    }
}