//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class BarCard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BarCard()
        {
            this.BarCardIngredient = new HashSet<BarCardIngredient>();
            this.OrderMenuBarCard = new HashSet<OrderMenuBarCard>();
        }
    
        public int IdBarCard { get; set; }
        public string Name { get; set; }
        public Nullable<int> Category { get; set; }
        public decimal Sum { get; set; }
        public byte[] PhotoBarCard { get; set; }
        public int Volume { get; set; }
    
        public virtual CategoryBarCard CategoryBarCard { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BarCardIngredient> BarCardIngredient { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderMenuBarCard> OrderMenuBarCard { get; set; }
    }
}
