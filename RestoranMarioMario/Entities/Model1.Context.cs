﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestoranMarioMario.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class restaurantEntities : DbContext
    {
        public restaurantEntities()
            : base("name=restaurantEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BarCard> BarCard { get; set; }
        public virtual DbSet<BarCardIngredient> BarCardIngredient { get; set; }
        public virtual DbSet<CategoryBarCard> CategoryBarCard { get; set; }
        public virtual DbSet<CategoryMenu> CategoryMenu { get; set; }
        public virtual DbSet<Check> Check { get; set; }
        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuIngredient> MenuIngredient { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderMenuBarCard> OrderMenuBarCard { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Stocks> Stocks { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Table> Table { get; set; }
        public virtual DbSet<TypeIngredient> TypeIngredient { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Waiter> Waiter { get; set; }
    }
}
