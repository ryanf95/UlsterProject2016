//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlacementHelper.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Log
    {
        public System.Guid Id { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string UserID { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
