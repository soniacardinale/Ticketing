using System;
using System.Collections.Generic;
using System.Text;

namespace Ticketing.Client.Model
{
    public class Note
    {
        public int ID { get; set; }
        public string Comments { get; set; }
        public int TicketID { get; set; }  //Foreign Key => Ticket
        public Byte[] RowVersion { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
