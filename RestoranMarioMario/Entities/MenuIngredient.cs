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
    
    public partial class MenuIngredient
    {
        public int IdMenuIngredient { get; set; }
        public int IdMenu { get; set; }
        public int IdIngredient { get; set; }
    
        public virtual Ingredient Ingredient1 { get; set; }
        public virtual Menu Menu1 { get; set; }
    }
}
