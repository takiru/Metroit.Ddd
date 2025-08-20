using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MyDbContext() : base()
        {

        }

        public MyDbContext(DbContextOptions options) : base(options)
        {

        }
    }

    [Table("M_Password")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public string UserId { get; set; }

        [Column("ent_date")]
        public DateTime CreatedAt { get; set; }
    }
}
