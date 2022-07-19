using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowardAgarioStepTwo
{
    /// <summary>
    /// Simple student object to use for practice serializing and deserializing for Agario project.
    /// </summary>
    internal class Student
    {
        private float _GPA;
        private string _name;
        private int _ID;

        public Student(string name, float GPA)
        {
            _name = name;
            _GPA = GPA;
            _ID = 12345;
        }
        public float GPA
        {
            get
            {
                return _GPA;
            }
            set
            {
                _GPA = value;
            }

        }
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }

        }
        public int ID {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
    }
}
