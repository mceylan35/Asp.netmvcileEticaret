//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eticaret.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Marka
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Marka()
        {
            this.tbl_Urun = new HashSet<tbl_Urun>();
        }
    
        public int Marka_Id { get; set; }
        public string Marka_Adi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Urun> tbl_Urun { get; set; }
    }
}