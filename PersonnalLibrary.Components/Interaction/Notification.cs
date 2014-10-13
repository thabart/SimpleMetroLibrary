using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PersonnalLibrary.Interaction
{
    public class Notification : INotification
    {
        public string Title { get; set; }

        public object Content { get; set; }

        public Notification(string title, string content)
        {
            MessageBox.Show(content, title, MessageBoxButton.OK);
        }
    }
}
