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
    
    public partial class Transaktionen
    {
        public int idTransaktionen { get; set; }
        public Nullable<System.DateTime> datTransaktion { get; set; }
        public Nullable<int> Kartons { get; set; }
        public Nullable<int> FlaschenKartons { get; set; }
        public Nullable<int> GlaeserKartons { get; set; }
        public Nullable<int> KleiderKartons { get; set; }
        public int Umzuege_idUmzuege { get; set; }
        public int Umzuege_Kunden_idKunden { get; set; }
        public string Bemerkungen { get; set; }
        public string UserChanged { get; set; }
        public Nullable<System.DateTime> Erstelldatum { get; set; }
        public Nullable<int> unbenutzt { get; set; }
        public string RechnungsNr { get; set; }
        public Nullable<System.DateTime> timeTransaktion { get; set; }
        public Nullable<sbyte> final { get; set; }
    
        public virtual Umzuege Umzuege { get; set; }
    }
}
