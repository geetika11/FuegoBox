﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace FuegoBox.DAL
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class FuegoEntities : DbContext
{
    public FuegoEntities()
        : base("name=FuegoEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Address> Address { get; set; }

    public virtual DbSet<Cart> Cart { get; set; }

    public virtual DbSet<Category> Category { get; set; }

    public virtual DbSet<Order> Order { get; set; }

    public virtual DbSet<OrderProduct> OrderProduct { get; set; }

    public virtual DbSet<Product> Product { get; set; }

    public virtual DbSet<Property> Property { get; set; }

    public virtual DbSet<Role> Role { get; set; }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<Value> Value { get; set; }

    public virtual DbSet<Variant> Variant { get; set; }

    public virtual DbSet<VariantImage> VariantImage { get; set; }

    public virtual DbSet<VariantProperty> VariantProperty { get; set; }

    public virtual DbSet<VariantPropertyValue> VariantPropertyValue { get; set; }

}

}

