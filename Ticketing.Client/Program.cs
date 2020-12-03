using System;
using Ticketing.Client.Model;
using Ticketing.Core.Model;

namespace Ticketing.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            DataService dataService = new DataService();

            Console.WriteLine("=== Ticket Management ===");
            bool quit = false;

            do
            {
                #region Main Loop
                Console.Write("Comando: ");
                string command = Console.ReadLine();
                Console.WriteLine();

                switch (command)
                {
                    case "q":
                        quit = true;
                        break;

                    case "a":
                        // ADD
                        Ticket ticket = new Ticket();
                        ticket.Titolo = GetData("Titolo");
                        ticket.Description = GetData("Descrizione");
                        ticket.Category = GetData("Categoria");
                        ticket.Priority = GetData("Priorità");
                        ticket.Requestor = GetData("Richiedente");
                        ticket.State = "New";
                        ticket.IssueDate = DateTime.Now;
                        // codice per recuperare i dati di un ticket ...
                        var result_a = dataService.AddTicket(ticket);
                        Console.WriteLine("Operation " + (result_a ? "Completed" : "Failed!"));
                        break;

                    case "n":
                        //ADD NOTE
                        var ticketId = GetData("TicketID");
                        int.TryParse(ticketId, out int tID);
                        var comments = GetData("Commento");
                        Note note = new Note { TicketID = tID, Comments = comments };
                        var result_n = dataService.AddNote(note);
                        Console.WriteLine("Operation " + (result_n ? "Completed" : "Failed!"));
                        break;


                    case "l":
                        // LIST
                        Console.WriteLine("-- TICKET LIST (EAGER) --");
                        foreach (var t in dataService.List())
                        {
                            Console.WriteLine($"[{t.ID}] {t.Titolo}");
                            foreach (var n in t.Notes)
                                Console.WriteLine($"\t{n.Comments}");
                        }
                        Console.WriteLine("-----------------");
                        break;

                    case "e":
                        // EDIT
                        var ticketId3 = GetData("TicketID");
                        int.TryParse(ticketId3, out int tID3);
                        var ticket3 = dataService.GetTicketByID(tID3);
                        ticket3.Titolo = GetData("Titolo");
                        ticket3.Description = GetData("Descrizione");
                        ticket3.Category = GetData("Categoria");
                        ticket3.Priority = GetData("Priorità");
                        ticket3.State = GetData("Stato");
                        var editResult = dataService.Edit(ticket3);
                        Console.WriteLine("Operation " + (editResult ? "Completed" : "Failed!"));
                        break;

                    case "d":
                        // DELETE
                        break;

                    default:
                        Console.WriteLine("Comando sconosciuto.");
                        break;
                }

                #endregion
            } while (!quit);

            Console.WriteLine("=== Bye Bye ===");
        }
        private static string GetData(string message)
        {
            Console.Write(message + ": ");
            var value = Console.ReadLine();
            return value;
        }

        //Overloading
        private static string GetData(string message, string initialvalue)
        {
            Console.Write(message + " ({0}): ", initialvalue);
            var value = Console.ReadLine();
            return string.IsNullOrEmpty(value) ? initialvalue : value;
        }

    }
}
