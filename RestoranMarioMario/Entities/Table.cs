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
    
    public partial class Table
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Table()
        {
            this.Order = new HashSet<Order>();
        }
    
        public int IdTable { get; set; }
        public int TableNumber { get; set; }
        public string TablePassword { get; set; }
        public Nullable<int> TableWaiter { get; set; }
        public string CorrectWaiter
        {
            get
            {
                return Waiter.Surname.ToString();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
        public virtual Waiter Waiter { get; set; }
    }
}
