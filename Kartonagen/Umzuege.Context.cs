﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kartonagen
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UmzuegeEntities : DbContext
    {
        public UmzuegeEntities()
            : base("name=UmzuegeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Kunden> Kundens { get; set; }
        public DbSet<Lagerbestaende> Lagerbestaendes { get; set; }
        public DbSet<Transaktionen> Transaktionens { get; set; }
        public DbSet<Umzuege> Umzueges { get; set; }
        public DbSet<Umzugsfortschritt> Umzugsfortschritts { get; set; }
    }
}