using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ticketing.Client.Model;
using Ticketing.Helpers;

namespace Ticketing.Client.Context
{
    sealed class TicketContext : DbContext
    {
        DbSet<Ticket> Tickets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            //anzichè fare tutto questo ogni volta, mi creo una classe di supporto che utilizzerò ogni volta -> Lo metto in Ticketing.Helpers
            //var config = new ConfigurationBuilder() //legge informazioni da diverse fonti
            //    .SetBasePath(Directory.GetCurrentDirectory()) //forzo il configurationbuilder a cercare il file nella Current Directory
            //    .AddJsonFile("apsettings.json")  //vado a prendere le informazioni nel mio file appsettings
            //    .Build();
            //string connString=config.GetConnectionString("TicketDb");

            //Oppure:
            //string connString = config.GetConnectionString("ConnectionString")["TicketDb"];
            //in questo modo la connection string sarà un file esterno 

            //Dopo aver costruito la classe Config, posso semplicemente scrivere:
            string connString = Config.GetConnectionString("TicketDb");

            //Oppure:
            //string connString = Config.GetSection("ConnString")["TicketDb"];

            optionBuilder.UseSqlServer(connString); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API (in ticket utilizzo la Data Notation, ma la coomento perchè altrimenti farei 2 volte la stessa cosa)

            //Anzichè scrivere ogni volta il modelBUilder come segue:
            //modelBuilder.Entity<Ticket>()
            //    .HasKey(t => t.ID); //non è necessario perchè abbiamo rispettato la Data Notation, però meglio specificare
            //modelBuilder.Entity<Ticket>()
            //    .Property(t => t.Titolo)
            //    .HasMaxLength(100) //ho imposto la lunghezza massima del titolo di 100 caratteri

            //Definiamo:
            var ticketModel = modelBuilder.Entity<Ticket>(); 

            //E le proprietà le scriviamo:
            ticketModel
                .HasKey(t => t.ID);

            ticketModel
                .Property(t => t.Titolo)
                .HasMaxLength(100);

            ticketModel
             .Property(t => t.Description)
             .HasMaxLength(500);

            ticketModel
                .Property(t => t.Priority)
                .IsRequired(); //sto richiedendo che sia obbligatoria, non può essere nulla

            ticketModel
                .Property(t => t.Requestor)
                .HasMaxLength(50)
                .IsRequired();






        }
    }
}
