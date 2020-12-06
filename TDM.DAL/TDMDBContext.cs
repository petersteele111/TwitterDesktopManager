using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TDM.Domain.Models;

namespace TDM.DAL
{
    public class TDMDBContext : DbContext
    {
        public TDMDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tweet> Tweets { get; set; }

    }
}
