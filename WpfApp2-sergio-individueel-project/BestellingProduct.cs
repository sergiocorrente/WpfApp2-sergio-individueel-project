//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp2_sergio_individueel_project
{
    using System;
    using System.Collections.Generic;
    
    public partial class BestellingProduct
    {
        public int BestellingProductID { get; set; }
        public int BestellingID { get; set; }
        public int ProductID { get; set; }
    
        public virtual Bestelling Bestelling { get; set; }
        public virtual Bestelling Bestelling1 { get; set; }
    }
}