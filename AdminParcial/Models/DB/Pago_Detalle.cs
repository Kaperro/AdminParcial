//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdminParcial.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pago_Detalle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pago_Detalle()
        {
            this.Reciboes = new HashSet<Recibo>();
        }
    
        public int Id_Pago_Detalle { get; set; }
        public string Descripcion { get; set; }
        public Nullable<decimal> Monto { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recibo> Reciboes { get; set; }
    }
}