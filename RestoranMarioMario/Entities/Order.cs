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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.OrderMenu = new HashSet<OrderMenu>();
        }
    
        public int IdOrder { get; set; }
        public int TableNumber { get; set; }
        public Nullable<decimal> OrderSum { get; set; }
        public System.DateTime Date { get; set; }
        public string NumberOrder { get; set; }

        public string CorrectTableNumber
        {
            get
            {
                return Table.TableNumber.ToString();
            }
        }
        public string CorrectDate
        {
            get
            {
                return Date.ToString("F");
            }
        }

        public virtual Table Table { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderMenu> OrderMenu { get; set; }
    }
}
