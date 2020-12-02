using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketing.Client.Context;
using Ticketing.Client.Model;

namespace Ticketing.Client
{
    public class DataService
    {
        public List<Ticket> ListLazy()
        {
            using var ctx = new TicketContext();
            Console.WriteLine("-- TICKET LIST --");
            foreach (var t in ctx.Tickets)
            {
                Console.WriteLine($"[{t.ID}] {t.Titolo}");
                foreach (var n in t.Notes)
                {
                    Console.WriteLine("\t{0}", n.Comments);
                }
            }
            Console.WriteLine("-----------------");
            return ctx.Tickets
              //.Include(t => t.Notes)
              .ToList();

        }
        public List<Ticket> ListEager()
        {
            using var ctx = new TicketContext();

            return ctx.Tickets
                .Include(t => t.Notes)
                .ToList();
        }

        public bool AddTicket(Ticket ticket)
        {
            try
            {
                using var ctx = new TicketContext();

                if (ticket != null)
                {
                    ctx.Tickets.Add(ticket);
                    ctx.SaveChanges();
                }
                else
                    Console.WriteLine("Ticket non può essere nullo.");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore: " + ex.Message);
                return false;
            }
        }
        public bool AddNote(Note newNote)
        {
            try
            {
                using var ctx = new TicketContext();

                if (newNote != null)
                {
                    var ticket = ctx.Tickets.Find(newNote.TicketID);
                    if (ticket != null)
                    {
                        ticket.Notes.Add(newNote);
                        ctx.SaveChanges();
                    }
                }
                else
                    Console.WriteLine("Note non può essere nullo.");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore: " + ex.Message);
                return false;
            }
        }

        public Ticket GetTicketByIDViaStp(int id)
        {
            using var ctx = new TicketContext();
            SqlParameter idParam = new SqlParameter("@id", id);
            var result = ctx.Tickets.FromSqlRaw("exec stpGetTicketByID @ID", idParam).AsEnumerable();

            return result.FirstOrDefault();
        }

        public Ticket GetTicketByID(int id)
        {
            using var ctx = new TicketContext();
            if (id>0)
            {
                return ctx.Tickets.Find(id);
            }
            return null;
        }

        public bool Edit(Ticket ticket)
        {
            try
            {
                using var ctx = new TicketContext();

                if (ticket == null)
                    return false;

                Console.WriteLine("Smandrappa il ticket e poi premi enter ...");
                Console.ReadKey();

                ctx.Entry<Ticket>(ticket).State = EntityState.Modified;
                ctx.SaveChanges();

                return true;
            } catch(DbUpdateConcurrencyException ex)
            {
                //...
                Console.WriteLine("Errore: " + ex.Message);
                return false;
            }

        }

    }

}

