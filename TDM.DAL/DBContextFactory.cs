using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace TDM.DAL
{
    public class DBContextFactory : IDesignTimeDbContextFactory<TDMDBContext>
    {

        public TDMDBContext CreateDbContext(string[] args = null)
        {

            var options = new DbContextOptionsBuilder<TDMDBContext>();
            options.UseMySQL("server=sharedcloud1.squidix.net;database=pbswebde_TDM;user=pbswebde_TDMAdmin;password=1!2@3#4$5%6^");

            return new TDMDBContext(options.Options);
        }
    }
}
