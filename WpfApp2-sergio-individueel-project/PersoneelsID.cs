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
    
    public partial class PersoneelsID
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersoneelsID()
        {
            this.Bestellings = new HashSet<Bestelling>();
        }
    
        public int PersoneelID { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Wachtwoord { get; set; }
        public int RoleID { get; set; }
        public string Username { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bestelling> Bestellings { get; set; }
    }
}