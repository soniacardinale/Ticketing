using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ticketing.Client.Model.Configuration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            //Fluent API (in ticket utilizzo la Data Notation, ma la coomento perchè altrimenti farei 2 volte la stessa cosa)

            //Anzichè scrivere ogni volta il modelBUilder come segue:
            //modelBuilder.Entity<Ticket>()
            //    .HasKey(t => t.ID); //non è necessario perchè abbiamo rispettato la Data Notation, però meglio specificare
            //modelBuilder.Entity<Ticket>()
            //    .Property(t => t.Titolo)
            //    .HasMaxLength(100) //ho imposto la lunghezza massima del titolo di 100 caratteri

            //Definiamo:
            //var ticketModel = modelBuilder.Entity<Ticket>();

            //E le proprietà le scriviamo:
            builder
                .HasKey(t => t.ID);

            builder
                .Property(t => t.Titolo)
                .HasMaxLength(100);

            builder
             .Property(t => t.Description)
             .HasMaxLength(500);

            builder
                .Property(t => t.Priority)
                .IsRequired(); //sto richiedendo che sia obbligatoria, non può essere nulla

            builder
                .Property(t => t.Requestor)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasMany(t => t.Notes) //Relazione tra Ticket e Notes: ogni Ticket può avere più note
                .WithOne(n => n.Ticket) //Ogni nota va ad un solo ticket
                .HasForeignKey(n => n.TicketID)
                .HasConstraintName("FK_Ticket_Notes")
                .OnDelete(DeleteBehavior.Cascade);  //La relazione viene definita in un solo posto, non serve ridefinirla in Note

            //Gestione concorrenza
            builder
                .Property(t => t.RowVersion)
                .IsRowVersion();



        }
    }
}

