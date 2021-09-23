using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticChangeNotification
{
    class Person
    {
        private String _firstName;
        public String FirstName
        {
            get { return _firstName; }
            set { _firstName = value;
                OnFirstNameChanged();
                }
        }
        public event EventHandler FirstNameChanged;
        private void OnFirstNameChanged()
        {
            if (FirstNameChanged != null)
                FirstNameChanged(this, EventArgs.Empty);
        }
    }
}
