using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ticketing.Client.Model
{
    public class Ticket
    {
        //public Ticket()
        //{
        //    Notes = new List<Note>();
        //}
        //Data Notation:

        //[key]
        public int ID { get; set; } //posso mettere o TicketID o solo ID
        public DateTime IssueDate { get; set; }
        //[MaxLength(100)]
        public string Titolo { get; set; }
        //[MaxLength(500)]
        public string Description { get; set; }
        public string Category { get; set; }
        //[Required]
        public string  Priority { get; set; }
        public string State { get; set; }
        public  string Requestor { get; set; }

        //Gestione della concorrenza
        public Byte[] RowVersion { get; set; }
        public virtual List<Note> Notes { get; set; } //Navigation Property monodirezionale perchè ho solo quella che dal ticket mi fa andare alle note.
                                                      //Aggiungo quella da Note a Ticket

    }
}
