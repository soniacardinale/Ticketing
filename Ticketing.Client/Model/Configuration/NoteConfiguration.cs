using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ticketing.Client.Model.Configuration
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder
                .HasKey(n => n.ID);

            builder
                .Property(n => n.Comments)
                .HasMaxLength(1000)
                .IsRequired();

            //La relazione tra Ticket e Note posso metterla anche qui
          
        }
    }
}
