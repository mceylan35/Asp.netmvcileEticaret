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
    
    public partial class tbl_Kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Kullanici()
        {
            this.Siparis = new HashSet<Siparis>();
            this.tbl_Favori = new HashSet<tbl_Favori>();
            this.tbl_Yorum = new HashSet<tbl_Yorum>();
        }
    
        public int Kullanici_Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Kullanici_Adi { get; set; }
        public string Sifre { get; set; }
        public Nullable<int> Rol_ID { get; set; }
        public string Eposta { get; set; }
        public Nullable<int> Telefon { get; set; }
        public Nullable<System.DateTime> UyeKayitTarihi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Siparis> Siparis { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Favori> tbl_Favori { get; set; }
        public virtual tbl_Rol tbl_Rol { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Yorum> tbl_Yorum { get; set; }
    }
}
