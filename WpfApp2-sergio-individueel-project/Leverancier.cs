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
    
    public partial class Leverancier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Leverancier()
        {
            this.Bestellings = new HashSet<Bestelling>();
            this.Products = new HashSet<Product>();
        }
    
        public int LeverancierID { get; set; }
        public string Contactpersoon { get; set; }
        public string Telefoonnummer { get; set; }
        public string Emailadres { get; set; }
        public string Straatnaam { get; set; }
        public int Huisnummer { get; set; }
        public string Bus { get; set; }
        public int Postcode { get; set; }
        public string Gemeente { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bestelling> Bestellings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
