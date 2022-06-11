﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.Models
{
    public class PokedexContext : DbContext
    {
        public PokedexContext(DbContextOptions<PokedexContext> connectionOptions):base(connectionOptions) { }

        public DbSet<Pokemones> DbPokemones { get; set; }
        public DbSet<Tipos> DbTipos { get; set; }
        public DbSet<Regiones> DbRegiones { get; set;}

        protected override void OnModelCreating(ModelBuilder model)
        {
            #region "Tables"
            model.Entity<Pokemones>().ToTable("Pokemones");
            model.Entity<Tipos>().ToTable("Tipos");
            model.Entity<Regiones>().ToTable("Regiones");
            #endregion

            #region "Primary Keys"
            model.Entity<Pokemones>().HasKey(pokemones => pokemones.ID);
            model.Entity<Tipos>().HasKey(tipos => tipos.ID);
            model.Entity<Regiones>().HasKey(regiones => regiones.ID);
            #endregion

            #region "Relationships"
            model.Entity<Tipos>()
                .HasMany<Pokemones>(tipos => tipos.TiposPokemones)
                .WithOne(pokemones => pokemones.NavigationTipoPrimario)
                .HasForeignKey(product=> product.IdTipos)
                .OnDelete(DeleteBehavior.Cascade);
            
            model.Entity<Tipos>()
                .HasMany<Pokemones>(tipos => tipos.TiposPokemonesSecundarios)
                .WithOne(pokemones => pokemones.NavigationTipoSecundario)
                .HasForeignKey(product => product.IdTipos)
                .OnDelete(DeleteBehavior.Cascade);

            model.Entity<Regiones>()
                .HasMany<Pokemones>(tipos => tipos.RegionPokemones)
                .WithOne(pokemones => pokemones.NavigationRegion)
                .HasForeignKey(product => product.IdRegion)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }

    }
}
