//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebInventoryManagementSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class Stock
    {
        public long st_id { get; set; }
        public int st_proID { get; set; }
        public Nullable<int> st_proCarton { get; set; }
        public Nullable<int> st_proPieces { get; set; }
        public long st_purchaseInvID { get; set; }
    
        public virtual product product { get; set; }
        public virtual purchaseInvoice purchaseInvoice { get; set; }
    }
}
