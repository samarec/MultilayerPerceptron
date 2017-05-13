using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilayerPerceptron.Views
{
    public class MessageContainer : EventArgs
    {
        private string message;

        public MessageContainer(string m)
        {
            message = m;
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
