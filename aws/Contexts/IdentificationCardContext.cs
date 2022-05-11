using aws.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aws.Contexts
{
    public class IdentificationCardContext : DbContext
    {
        public IdentificationCardContext(DbContextOptions options) : base(options) { }
        public DbSet<IdentificationCard> IdentificationCards { get; set; }
    }
}