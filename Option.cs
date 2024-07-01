using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectV1
{
    public class Option
    {
        private int _id;
        private int _questionId;
        private string _text;
        private int _isCorrect;

        public Option(int id, int questionId, string text, int isCorrect)
        {
            _id = id;
            _questionId = questionId;
            _text = text;
            _isCorrect = isCorrect;
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int questionId
        {
            get { return _questionId; }
            set { _questionId = value; }
        }

        public string text
        {
            get { return _text; }
            set { _text = value; }
        }

        public int isCorrect
        {
            get { return _isCorrect; }
            set { _isCorrect = value; }
        }
    }
}