using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ticketing.Client.Model;
using Ticketing.Client.Model.Configuration;
using Ticketing.Helpers;

namespace Ticketing.Client.Context
{
    sealed class TicketContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Note> Notes { get; set; }
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

            //Caricamento dati
            optionBuilder
               .UseLazyLoadingProxies()
               .UseSqlServer(connString);
        }
        

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Ticket>(new TicketConfiguration());
            modelBuilder.ApplyConfiguration<Note>(new NoteConfiguration());
        }
    }
}
