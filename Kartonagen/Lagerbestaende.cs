//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kartonagen
{
    using System;
    using System.Collections.Generic;
    
    public partial class Lagerbestaende
    {
        public int idLagerbestaende { get; set; }
        public string BuChungsdatum { get; set; }
        public Nullable<int> Kartons { get; set; }
        public Nullable<int> GlaeserKartons { get; set; }
        public Nullable<int> FlasChenKartons { get; set; }
        public Nullable<int> KleiderKartons { get; set; }
        public string UserChanged { get; set; }
        public string Bemerkung { get; set; }
    }
}
