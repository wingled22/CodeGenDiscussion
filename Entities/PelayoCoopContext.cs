using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CodeGen.Entities;

public partial class PelayoCoopContext : DbContext
{
    public PelayoCoopContext(DbContextOptions<PelayoCoopContext> options)
        : base(options)
    {
    }
    public virtual DbSet<ClientInfo> ClientInfos { get; set; }
    public virtual DbSet<UserType> UserTypes { get; set; }
}
