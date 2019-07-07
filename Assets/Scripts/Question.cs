using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Question
    {
        private int _idQ;
        private string _description;
        private bool _isImage;
        

        public Question(int idQ, string description, bool isImage)
        {
            IdQ = idQ;
            Description = description;
            IsImage = isImage;
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

        public int IdQ
        {
            get
            {
                return _idQ;
            }

            set
            {
                _idQ = value;
            }
        }

        public bool IsImage
        {
            get
            {
                return _isImage;
            }

            set
            {
                _isImage = value;
            }
        }
    }
}
