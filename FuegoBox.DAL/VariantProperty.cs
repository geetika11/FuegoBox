
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
    using System.Collections.Generic;
    
public partial class VariantProperty
{

    public System.Guid ID { get; set; }

    public System.Guid VariantID { get; set; }

    public System.Guid PropertyValueID { get; set; }



    public virtual Variant Variant { get; set; }

    public virtual VariantPropertyValue VariantPropertyValue { get; set; }

}

}
