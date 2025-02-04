﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternshipApplication.Models.Domain
{
    public class ContactPersoon
    {
        public int Id { get; set; }
        public String Naam { get; set; }
        public String Voornaam { get; set; }
        public String Functie { get; set; }
        public String ContactEmail { get; set; }
        public String ContactTelNr { get; set; }
        public String GsmNummer { get; set; }

        public ContactPersoon()
        {
            
        }

        public ContactPersoon(String naam, String voornaam, String functie, String contactEmail, String contactTelNr,
            String gsmNummer)
        {
            this.Naam = naam;
            this.Voornaam = voornaam;
            this.Functie = functie;
            this.ContactEmail = contactEmail;
            this.ContactTelNr = contactTelNr;
            this.GsmNummer = GsmNummer;
        }
        

}
}