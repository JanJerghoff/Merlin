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
    
    public partial class Kunden
    {
        public Kunden()
        {
            this.Umzueges = new HashSet<Umzuege>();
        }
    
        public int idKunden { get; set; }
        public string Anrede { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Telefonnummer { get; set; }
        public string Handynummer { get; set; }
        public string Email { get; set; }
        public string Straße { get; set; }
        public string Hausnummer { get; set; }
        public int PLZ { get; set; }
        public string Ort { get; set; }
        public string Land { get; set; }
        public string UserChanged { get; set; }
        public System.DateTime Erstelldatum { get; set; }
        public string Bemerkung { get; set; }
    
        public virtual ICollection<Umzuege> Umzueges { get; set; }
    }
}