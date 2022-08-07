using AuthenticationWithClie.Database.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationWithClie.Database.Models
{
    public sealed class Report : Entity <Guid>
    {
        private static int IdCounter = 1;
        public User Sender { get; set; }
        public string Text { get; set; }
        public DateTime Sent { get; set; }
        public User Target { get; set; }
        
        public Report(User sender, string text,User target)
        {
            Sender = sender;
            Text = text;
            Sent = DateTime.Now;
            Target = target;
        }
    }
}
