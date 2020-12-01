using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ticketing.Client.Model;

namespace Ticketing.Client.Context
{
    public class TicketContext : DbContext
    {
        DbSet<Ticket> Ticketts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            var config = new ConfigurationBuilder() //legge informazioni da diverse fonti
                .SetBasePath(Directory.GetCurrentDirectory()) //forzo il configurationbuilder a cercare il file nella Current Directory
                .AddJsonFile("apsettings.json")  //vado a prendere le informazioni nel mio file appsettings
                .Build();

            string connString=config.GetConnectionString("TicketDb");
            //Oppure:
            //string connString = config.GetConnectionString("ConnectionString")["TicketDb"];
            //in questo modo la connection string sarà un file esterno 

            optionBuilder.UseSqlServer("connString"); 
        }
    }
}
