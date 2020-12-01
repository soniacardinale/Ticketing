using System;
using System.Collections.Generic;
using System.Text;

namespace Ticketing.Client.Model
{
    public class Ticket
    {
        public int ID { get; set; } //posso mettere o TicketID o solo ID
        public DateTime IssueDate { get; set; }
        public string Titolo { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string  Priority { get; set; }
        public string State { get; set; }
    }
}
