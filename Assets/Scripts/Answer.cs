using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Answer
    {
        private int _idA;
        private string _description;
        private bool _isTrue;

        public Answer()
        {
        }

        public Answer(int idA, string description, bool isTrue)
        {
            IdA = idA;
            Description = description;
            IsTrue = isTrue;
        }

        public int IdA
        {
            get
            {
                return _idA;
            }

            set
            {
                _idA = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }

        public bool IsTrue
        {
            get
            {
                return _isTrue;
            }

            set
            {
                _isTrue = value;
            }
        }
    }
}
