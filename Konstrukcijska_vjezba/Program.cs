/*
|-------------------------------|
|                               |
| Autor: Vatroslav Krpan        |
| Projekt: HNL                  |
| Predmet: Osnove Programiranja |
| Ustanova: VŠMTI               |
| Godina: 2021.                 |
|                               |
|-------------------------------|
*/
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Konstrukcijska_vjezba
{
    class Program
    {
        public struct Igraci        //struktura za opis igrača
        {
            public int id_kluba;
            public int oib;
            public string ime_igraca;
            public string prezime_igraca;
            public string ime_kluba;

            public Igraci(int id,int oib_igr, string ime, string prezime, string klub)
            {
                id_kluba = id;
                oib = oib_igr;
                ime_igraca = ime;
                prezime_igraca = prezime;
                ime_kluba = klub;
            }
        }
        public struct Klubovi   //struktura za opis klubova
        {
            public int id_kluba;
            public string naziv_kluba;
            public string ime_trenera;
            public string grad_kluba;
            public string ime_stadiona;
            public string dat_osnivanja;

            public Klubovi(int id, string klub, string trener, string grad, string stadion, string osnovan)
            {
                id_kluba = id;
                naziv_kluba = klub;
                ime_trenera = trener;
                grad_kluba = grad;
                ime_stadiona = stadion;
                dat_osnivanja = osnovan;
            }
        }
        public struct Rezultati
        {
            public int rbr;
            public string ime_kluba;
            public int broj_utakmica;
            public int broj_bodova;
            public int pobjeda;
            public int nerijeseno;
            public int razlika;
            public int pogodci;

            public Rezultati(int redni, string klub, int br_utakmica, int bodovi, int win, int draw, int gol_razlika, int zabijeno_golova)
            {
                rbr = redni;
                ime_kluba = klub;
                broj_utakmica = br_utakmica;
                broj_bodova = bodovi;
                pobjeda = win;
                nerijeseno = draw;
                razlika = gol_razlika;
                pogodci = zabijeno_golova;
            }
        }
        public struct Utakmica
        {
            public int idDomaci;
            public int idGosti;
            public int goloviDomaci;
            public int goloviGosti;

            public Utakmica(int id_domaci, int id_gosti, int golovi_domaci, int golovi_gosti)
            {
                idDomaci = id_domaci;
                idGosti = id_gosti;
                goloviDomaci = golovi_domaci;
                goloviGosti = golovi_gosti;
            }
        }
        static void Main(string[] args)
        {
            LogAkcije("Korisnik je pokrenuo program.");
            string path_klubovi = "klubovi.xml";
            string path_igraci = "igraci.xml";
            string path_rezultati = "rezultati.xml";
            List<Igraci> listaIgraca = new List<Igraci>();
            List<Klubovi> listaKlubova = new List<Klubovi>();
            List<Rezultati> listaRezultata = new List<Rezultati>();
            List<Utakmica> listaUtakmica = new List<Utakmica>();
            if (File.Exists(path_rezultati) && File.Exists(path_igraci) && File.Exists(path_klubovi))
            {
                dohvatiIzbornik(listaIgraca, listaKlubova, listaRezultata, listaUtakmica);
            }
        }
        static void dohvatiIzbornik(List<Igraci> listaIgraca, List<Klubovi> listaKlubova, List<Rezultati> listaRezultata, List<Utakmica> listaUtakmica)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija za dohvacanje glavnog izbornka
            * 
            * -----------------------------------------------------
            */
            LogAkcije("Pokretanje glavnog izbornika");
            Console.WriteLine("Odaberite akciju!");
            Console.WriteLine("1 - Ažuriraj klubove\n2 - Ažuriraj igrače\n3 - Prijelaz\n4 - Prikaz klubova s igračima\n5 - Odigraj utakmice\n6 - Prikaži rang listu\n7 - Izlaz iz programa");
            int odabir = provjeraBroja();
            switch (odabir)
            {
                case 1:                                                           // Azuriraj klubove
                    LogAkcije("Pokretanje funkcije za azuriranje klubova");
                    dohvatiIgrace(listaIgraca);
                    dohvatiKlubove(listaKlubova);
                    Console.WriteLine("\n");
                    azurirajKlubove(listaKlubova, listaIgraca, listaRezultata, listaUtakmica);
                    break;
                case 2:                                                           // Azuriraj igrace
                    LogAkcije("Pokretanje fukcije za azuriranje igraca");
                    dohvatiIgrace(listaIgraca);
                    dohvatiKlubove(listaKlubova);
                    Console.WriteLine("\n");
                    azurirajIgrace(listaIgraca, listaKlubova, listaRezultata, listaUtakmica);
                    break;
                case 3:                                                            // Prijelaz
                    LogAkcije("Pokretanje fukcije za prijelaz igraca");
                    dohvatiIgrace(listaIgraca);
                    dohvatiKlubove(listaKlubova);
                    Console.WriteLine("\n");
                    prijelazIgraca(listaIgraca, listaKlubova, listaRezultata, listaUtakmica);
                    break;
                case 4:                                                           // Prikaz klubova s igračima
                    LogAkcije("Pokretanje fukcije za prikaz klubova sa trenerima i igracima");
                    dohvatiIgrace(listaIgraca);
                    dohvatiKlubove(listaKlubova);
                    Console.WriteLine("\n");
                    prikazKlubovaSIgracima(listaIgraca, listaKlubova, listaRezultata, listaUtakmica);
                    break;
                case 5:                                                              // Odigraj utakmice
                    LogAkcije("Pokretanje fukcije za nasumicno odigravanje utakmica");
                    dohvatiIgrace(listaIgraca);
                    dohvatiKlubove(listaKlubova);
                    odigraj(listaIgraca, listaKlubova, listaRezultata, listaUtakmica);
                    break;
                case 6:                                                                // Prikazi rang listu
                    LogAkcije("Pokretanje fukcije za prikaz ljestvice");
                    dohvatiIgrace(listaIgraca);
                    dohvatiKlubove(listaKlubova);
                    dohvatiUtakmice(listaUtakmica);
                    izracunaj(listaUtakmica, listaRezultata, listaKlubova);
                    prikaziRangListu(listaIgraca, listaKlubova,listaRezultata, listaUtakmica);
                    break;
                case 7:
                    LogAkcije("Izlaz iz programa");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Niste odabrali valjanu opciju sa izbornika! Odaberite ponovo.");
                    dohvatiIzbornik(listaIgraca, listaKlubova, listaRezultata, listaUtakmica);
                    break;
            }
        }
        static void PonovniPrikazIzbornikaIliIzlaz(List<Igraci> igraciS, List<Klubovi> kluboviS, List<Rezultati> rezultatiS, List<Utakmica> utakmicaS)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja nakon svake zavrsene akcije u programu
            * nudi korisniku ponovni prikaz izbornika ili
            * izlaz iz programa
            * 
            * -----------------------------------------------------
            */
            Console.WriteLine("\nPritisnite tipku < Enter > za ponovni prikaz izbornika, a tipku < ESC > za izlaz iz programa");
            bool krajUnosa = false;
            while (!krajUnosa)
            {
                ConsoleKeyInfo sadrzajUnosa = Console.ReadKey(true);
                if(sadrzajUnosa.Key == ConsoleKey.Enter)
                {
                    krajUnosa = true;
                    ocistiMemoriju(kluboviS, igraciS, rezultatiS, utakmicaS);          //čišćenje lista nakon svake akcije
                    dohvatiIzbornik(igraciS, kluboviS, rezultatiS, utakmicaS);
                }
                else if(sadrzajUnosa.Key == ConsoleKey.Escape)
                {
                    LogAkcije("Izlaz iz programa");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Niste pritisnuli niti jednu od ponudenih tipki!");
                    PonovniPrikazIzbornikaIliIzlaz(igraciS, kluboviS, rezultatiS, utakmicaS);
                }
            }
        }
        static List<Klubovi> dohvatiKlubove(List<Klubovi> kluboviS)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja ce sve klubove iz klubovi.xml spremiti
            * u listu tipa Klubovi
            * 
            * -----------------------------------------------------
            */
            string sadrzaj_xml = "";
            StreamReader osr = new StreamReader("klubovi.xml");
            using (osr)
            {
                sadrzaj_xml = osr.ReadToEnd();
            }
            XmlDocument xml_datoteka = new XmlDocument();
            xml_datoteka.LoadXml(sadrzaj_xml);
            XmlNodeList atributi = xml_datoteka.SelectNodes("//data/klub");
            foreach (XmlNode sadrzaj in atributi)
            {
                kluboviS.Add(new Klubovi(Convert.ToInt32(sadrzaj.Attributes["id"].Value), sadrzaj.Attributes["naziv"].Value, sadrzaj.Attributes["trener"].Value, sadrzaj.Attributes["grad"].Value, sadrzaj.Attributes["stadion"].Value, sadrzaj.Attributes["osnovan"].Value));
            }
            osr.Close();
            return kluboviS;
        }
        static List<Igraci> dohvatiIgrace(List<Igraci> igraciS)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja ce sve igrace iz datoteke igraci.xml 
            * spremiti u listu tipa Igraci
            * 
            * -----------------------------------------------------
            */
            string sadrzaj_xml = "";
            StreamReader osr = new StreamReader("igraci.xml");
            using (osr)
            {
                sadrzaj_xml = osr.ReadToEnd();
            }
            XmlDocument xml_datoteka = new XmlDocument();
            xml_datoteka.LoadXml(sadrzaj_xml);
            var atributi = xml_datoteka.SelectNodes("//data/igrac");
            foreach (XmlElement sadrzaj in atributi)
            {
                igraciS.Add(new Igraci(Convert.ToInt32(sadrzaj.Attributes["id"].Value), Convert.ToInt32(sadrzaj.Attributes["oib"].Value), sadrzaj.Attributes["ime"].Value, sadrzaj.Attributes["prezime"].Value, sadrzaj.Attributes["klub"].Value));
            }
            osr.Close();
            return igraciS;
        }
        static List<Rezultati> dohvatiRezultate(List<Rezultati> rezultatiS)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja će sve zapise rezultata iz rezultati.xml
            * spremiti u listu tipa Rezultati
            * 
            * -----------------------------------------------------
            */
            string sadrzaj_xml = "";
            StreamReader osr = new StreamReader("rezultati.xml");
            using (osr)
            {
                sadrzaj_xml = osr.ReadToEnd();
            }
            XmlDocument xml_datoteka = new XmlDocument();
            xml_datoteka.LoadXml(sadrzaj_xml);
            var atributi = xml_datoteka.SelectNodes("//data/rezultat");
            foreach (XmlElement sadrzaj in atributi)
            {
                rezultatiS.Add(new Rezultati(Convert.ToInt32(sadrzaj.Attributes["rbr"].Value), sadrzaj.Attributes["klub"].Value, Convert.ToInt32(sadrzaj.Attributes["brojUtakmica"].Value), Convert.ToInt32(sadrzaj.Attributes["bodovi"].Value), Convert.ToInt32(sadrzaj.Attributes["pobjeda"].Value), Convert.ToInt32(sadrzaj.Attributes["nerijeseno"].Value), Convert.ToInt32(sadrzaj.Attributes["golRazlika"].Value), Convert.ToInt32(sadrzaj.Attributes["zabijenihGolova"].Value)));
                
            }
            osr.Close();
            return rezultatiS;
        }
        static void azurirajKlubove(List<Klubovi> kluboviS, List<Igraci> igraciS, List<Rezultati> rezultatiS, List<Utakmica> utakmicaS)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija za dodavanje i brisanje klubova
            * 
            * -----------------------------------------------------
            */
            int odabir;
            Console.WriteLine("1. - Dodavanje kluba");
            Console.WriteLine("2. - Brisanje kluba");
            odabir = provjeraBroja();
            if (odabir == 1)
            {
                XDocument dat = XDocument.Load("klubovi.xml");
                int count = dat.Descendants("klub").Count();
                //Console.WriteLine("{0} ", count);
                if (count < 10)
                {
                    Console.WriteLine("Unesite id kluba: ");
                    int id = provjeraBroja();
                    Console.WriteLine("Unesite ime kluba: ");
                    string ime_kluba = provjeraPraznogStringa();
                    Console.WriteLine("Unesite ime grada: ");
                    string grad = provjeraPraznogStringa();
                    Console.WriteLine("Unesite ime trenera: ");
                    string trener = provjeraPraznogStringa();
                    Console.WriteLine("Unesite ime stadiona: ");
                    string stadion = provjeraPraznogStringa();
                    Console.WriteLine("Unesite godinu osnivanja kluba: ");
                    string god_osn = provjeraPraznogStringa();
                    var x = new Klubovi(id, ime_kluba, trener, grad, stadion, god_osn);
                    kluboviS.Add(x);
                    Console.WriteLine("Klub {0} je uspjesno dodan u bazu!", ime_kluba);
                    LogAkcije("Korisnik je dodao novoi klub");
                    KluboviUXml(kluboviS);
                    PonovniPrikazIzbornikaIliIzlaz(igraciS, kluboviS, rezultatiS, utakmicaS);
                }
                else if (count >= 10)
                {
                    Console.WriteLine("U bazu je vec unesen maksimalan broj od 10 klubova");
                    PonovniPrikazIzbornikaIliIzlaz(igraciS, kluboviS, rezultatiS, utakmicaS);
                }
            }
            else if (odabir == 2)
            {
                Console.WriteLine("Unesi id kluba kojeg želiš izbrisati: ");
                foreach (Klubovi klub in kluboviS)
                {
                    Console.WriteLine("{0} - {1} ", klub.id_kluba, klub.naziv_kluba);
                }
                int id = provjeraBroja();
                foreach (Klubovi klub in kluboviS)
                {
                    if (id == klub.id_kluba)
                    {
                        kluboviS.Remove(klub);
                        Console.WriteLine("Uspješno ste obrisali {0}", klub.naziv_kluba);
                        break;
                    }
                }
                List<Igraci> obrisani = new List<Igraci>();
                foreach (Igraci bris_igr in igraciS)
                {
                    if (bris_igr.id_kluba == id)
                    {
                        obrisani.Add(modifyPlayer(bris_igr));
                    }
                }
                igraciS = igraciS.Where(x => x.id_kluba != id).ToList();
                foreach (Igraci igrac in obrisani)
                {
                    igraciS.Add(igrac);
                }
                Console.WriteLine("Svi igraci obrisanog kluba sada ne pripadaju niti jednom klubu!\n ID njihovog kluba sada iznosi 0");
                igraciUXml(igraciS);
                KluboviUXml(kluboviS);
                LogAkcije("Korisnik je izbrisao klub");
                PonovniPrikazIzbornikaIliIzlaz(igraciS, kluboviS, rezultatiS, utakmicaS);
            }
            else
            {
                Console.WriteLine("Niste odabrali valjanu opciju!");
                azurirajKlubove(kluboviS, igraciS, rezultatiS, utakmicaS);
            }
        }
        static Igraci modifyPlayer(Igraci igr)
        {
            /*
            * -------------------------------------------------------
            * 
            * Funkcija sluzi za mijenjanje atributa pojedinog
            * igrača u slučaju da je njegov klub obrisan
            * 
            * -------------------------------------------------------
            */
            igr.id_kluba = 0;
            igr.ime_kluba = "";
            return igr;
        }
        static void KluboviUXml(List<Klubovi> kluboviS)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja sprema podatke iz liste klubova
            *  u datoteku klubovi.xml
            * 
            * -----------------------------------------------------
            */
            var doc = XDocument.Load("klubovi.xml");
            doc.Root.RemoveNodes();
            foreach (Klubovi klub in kluboviS)
            {
                var newElement = new XElement("klub",
                    new XAttribute("id", klub.id_kluba),
                    new XAttribute("naziv", klub.naziv_kluba),
                    new XAttribute("trener", klub.ime_trenera),
                    new XAttribute("grad", klub.grad_kluba),
                    new XAttribute("stadion", klub.ime_stadiona),
                    new XAttribute("osnovan", klub.dat_osnivanja));
                doc.Element("data").Add(newElement);
                doc.Save("klubovi.xml");
            }
            doc.Save("klubovi.xml");
        }

        static void igraciUXml(List<Igraci> igraciS)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja sprema podatke iz liste igraca
            *  u datoteku igraci.xml
            * 
            * -----------------------------------------------------
            */
            var doc = XDocument.Load("igraci.xml");
            doc.Root.RemoveNodes();
            foreach (Igraci igrac in igraciS)
            {
                var newElement = new XElement("igrac",
                    new XAttribute("id", igrac.id_kluba),
                    new XAttribute("oib", igrac.oib),
                    new XAttribute("ime", igrac.ime_igraca),
                    new XAttribute("prezime", igrac.prezime_igraca),
                    new XAttribute("klub", igrac.ime_kluba));
                doc.Element("data").Add(newElement);
                doc.Save("igraci.xml");
            }
            doc.Save("igraci.xml");
        }
        static void azurirajIgrace(List<Igraci> igraciS, List<Klubovi> kluboviS, List<Rezultati> rezultatiS, List<Utakmica> utakmicaS)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja sluzi za dodavanje/brisanje igraca
            * 
            * -----------------------------------------------------
            */
            Console.WriteLine("1-Dodati igraca");
            Console.WriteLine("2-Izbrisati igraca");
            int izbor = provjeraBroja();
            if (izbor == 1)
            {
                Console.WriteLine("Unesite ime i prezime igraca, te njegov oib");
                string ime = provjeraPraznogStringa();
                string prezime = provjeraPraznogStringa();
                int oib = provjeraBroja();
                Console.WriteLine("Odaberite id kluba igrača");
                foreach(Klubovi obj in kluboviS)
                {
                    Console.WriteLine("{0} - {1}", obj.id_kluba, obj.naziv_kluba);
                }
                int id_kl = provjeraBroja();
                string klub = "";
                foreach(Klubovi obj in kluboviS)
                {
                    if(id_kl == obj.id_kluba)
                    {
                        klub = obj.naziv_kluba;           // dohvaćanje imena odabranog kluba preko id-a
                        break;
                    }
                }
                var x = new Igraci(id_kl, oib, ime, prezime, klub);
                igraciS.Add(x);
                Console.WriteLine("Igrac {0} {1} je uspjesno dodan u bazu!", x.ime_igraca, x.prezime_igraca);
                LogAkcije("Korisnik je dodao novog igraca");
                igraciUXml(igraciS);
                PonovniPrikazIzbornikaIliIzlaz(igraciS, kluboviS, rezultatiS, utakmicaS);
            }
            else if (izbor == 2)
            {
            ponovo:
                Console.WriteLine("Odaberite id kluba iz kojeg zelite obrisati igraca");
                foreach (Klubovi klub in kluboviS)
                {
                    Console.WriteLine("{0} - {1} ", klub.id_kluba, klub.naziv_kluba);
                }
                int id_kl = provjeraBroja();
                bool pronasao = false;
                foreach(Klubovi klub in kluboviS)
                {
                    if (id_kl == klub.id_kluba)
                    {
                        pronasao = true;
                    }
                }
                if(!pronasao)
                {
                    Console.WriteLine("Krivi ID!");
                    goto ponovo;
                }
                Console.WriteLine("Unesite ime, prezime i oib igraca kojeg zelite obrisati");
                int rbr = 1;
                foreach(Klubovi klub in kluboviS)
                {
                    if(id_kl == klub.id_kluba)   //pronalazi klub iz kojeg se igrač briše
                    {
                        foreach(Igraci igrac in igraciS)     
                        {
                            if (igrac.id_kluba == id_kl)  // ispisuje sve igrače koji igraju za taj klub
                            {
                                Console.WriteLine("{0}. - {1} {2} OIB: {3}", rbr, igrac.ime_igraca, igrac.prezime_igraca, igrac.oib);
                                rbr++;
                            }
                        }
                        break;
                    }
                    
                }
                string ime = provjeraPraznogStringa();
                string prezime = provjeraPraznogStringa();
                int oib = provjeraBroja();
                bool naden = false;     
                foreach (Igraci item in igraciS)
                {
                    if (item.id_kluba == id_kl && item.oib == oib && item.ime_igraca.ToLower() == ime.ToLower() && item.prezime_igraca.ToLower() == prezime.ToLower())
                    {
                        igraciS.Remove(item);
                        Console.WriteLine("Igrac {0} {1} s oibom {2} je uspjesno izbrisan iz baze! ", item.ime_igraca, item.prezime_igraca, item.oib);
                        naden = true;
                        break;
                    }
                }
                if (!naden) // služi za vraćanje korisnika na ponvni unos u slučaju da igrač nije pronađen
                {
                    Console.WriteLine("Igrac kojeg trazite nije pronaden u bazi! Pokušajte ponovo!");
                    goto ponovo;
                }
                LogAkcije("Korisnik je obrisao igraca");
            }
            else
            {
                Console.WriteLine("Niste odabrali valjanu opciju! Pokušajte ponovo");
                azurirajIgrace(igraciS, kluboviS, rezultatiS, utakmicaS);
            }
            igraciUXml(igraciS);
            PonovniPrikazIzbornikaIliIzlaz(igraciS, kluboviS, rezultatiS, utakmicaS);
        }
        static void prijelazIgraca(List<Igraci> igraciS, List<Klubovi> kluboviS, List<Rezultati> rezultatiS, List<Utakmica> utakmicaS)
        {
           /*
           * -----------------------------------------------------
           * 
           * Funkcija koja sluzi za prebacivanje igraca iz
           * jednog kluba u drugi klub
           * 
           * -----------------------------------------------------
           */
            Console.WriteLine("Odaberite iz kojeg kuba zelite prebaciti igraca");
            foreach (Klubovi klub in kluboviS)
            {
                Console.WriteLine("{0} - {1} ", klub.id_kluba, klub.naziv_kluba);
            }
            int id_starog = provjeraBroja();
            Console.WriteLine("Unesite ime, prezime i oib igraca kojeg zelite prebaciti: ");
            int rbr = 1;
            foreach (Klubovi klub in kluboviS)
            {
                if (id_starog == klub.id_kluba)
                {
                    foreach (Igraci igrac in igraciS)
                    {
                        if (igrac.id_kluba == id_starog) //ispisuje sve igrače koji igraju za odabrani klub
                        {
                            Console.WriteLine("{0}. - {1} {2} OIB: {3}", rbr, igrac.ime_igraca, igrac.prezime_igraca, igrac.oib);
                            rbr++;
                        }
                    }
                    break;
                }
            }
            string ime = provjeraPraznogStringa();
            string prezime = provjeraPraznogStringa();
            int oib = provjeraBroja();
            Console.WriteLine("Odaberite id kluba u koji se igrac prebacuje");
            foreach (Klubovi klub in kluboviS)
            {
                Console.WriteLine("{0} - {1} ", klub.id_kluba, klub.naziv_kluba);

            }
            int id_novog = provjeraBroja();
            List<Igraci> promjena = new List<Igraci>();        // lista u koju se sprema prebačeni igrač sa novim vrijednostima
            bool naden = false;
            foreach (Igraci igrac in igraciS)
            {
                if (igrac.id_kluba == id_starog && igrac.oib==oib && igrac.ime_igraca.ToLower() == ime.ToLower() && igrac.prezime_igraca.ToLower() == prezime.ToLower())
                {                                                                 // u listi pronalazi igrača sa unesenim parametrima
                    if(id_novog == id_starog)
                    {
                        Console.WriteLine("Ne možete prebaciti igrača u klub u kojem već igra!");
                        prijelazIgraca(igraciS, kluboviS, rezultatiS, utakmicaS);
                    }
                    foreach(Klubovi klub in kluboviS)
                    {
                        if(klub.id_kluba == id_novog)        // pronalazi klub u koji se igrač prebacuje
                        {
                            promjena.Add(prijelaz(igrac, id_novog, klub.naziv_kluba));
                            Console.WriteLine("Igrac {0} {1} uspjesno je prebacen iz {2} u {3}", igrac.ime_igraca, igrac.prezime_igraca, igrac.ime_kluba, klub.naziv_kluba);
                            naden = true;
                            break;
                        }
                    }
                    break;
                }
            }
            if(naden == false)
            {
                Console.WriteLine("Igrac nije pronaden u bazi, pokusajte ponovo!");
                prijelazIgraca(igraciS, kluboviS, rezultatiS, utakmicaS);
            }
            //List<Igraci> temp = new List<Igraci>();
            //temp = igraciS.Where(x => x.oib != oib).ToList();
            foreach(Igraci igr in igraciS)
            {
                if(igr.oib == oib)
                {
                    igraciS.Remove(igr);  // brisanje igrača koij se prebacuje sa starim vrijednostima iz liste 
                    break;
                }
            }
            foreach (Igraci igrac in promjena)
            {
                igraciS.Add(igrac);       // dodavanje igrača prebačenog u novi klub u listu koja se zatim sprema u xml
            }
            LogAkcije("Prijelaz igraca u drugi klub");
            igraciUXml(igraciS);
            PonovniPrikazIzbornikaIliIzlaz(igraciS, kluboviS, rezultatiS, utakmicaS);
        }
        static Igraci prijelaz(Igraci igr, int new_id, string new_klub)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja sluzi za dodjeljivanje novih vrijednosti
            *  igracu koji se prebacuje u drugi klub
            * 
            * -----------------------------------------------------
            */
            igr.id_kluba = new_id;
            igr.ime_kluba = new_klub;
            return igr;               // vraća igrača sa izmjenjenim vrijednostima 
        }

        static void prikazKlubovaSIgracima(List<Igraci> igraciS, List<Klubovi> kluboviS, List<Rezultati> rezultatiS, List<Utakmica> utakmicaS)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija prikazuje sve klubove sa njihovim trenerima
            *  i igracima koji igraju za taj klub
            * 
            * -----------------------------------------------------
            */
            foreach (Klubovi klub in kluboviS)
            {
                int cntr = 1;
                Console.WriteLine("Klub: {0}", klub.naziv_kluba);
                Console.WriteLine("Trener: {0}\n", klub.ime_trenera);
                foreach (Igraci igrac in igraciS)
                {

                    if (klub.id_kluba == igrac.id_kluba)
                    {

                        Console.WriteLine("{0} {1} {2}", cntr, igrac.ime_igraca, igrac.prezime_igraca);
                        cntr++;
                    }
                }
                if (cntr < 12)
                {
                    Console.WriteLine("Klub nema dovoljan broj igraca!");
                }
                Console.WriteLine("\n");
            }
            PonovniPrikazIzbornikaIliIzlaz(igraciS, kluboviS, rezultatiS, utakmicaS);
        }


        static bool countIgracaKlubova(List<Igraci> igraciS, List<Klubovi> kluboviS)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija provjerava postoji li 10 klubova te ima li
            * svaki od njih minimalno 11 igraca kako bi se 
            * utakmice mogle odigrati
            * 
            * -----------------------------------------------------
            */
            int br_kl = 0;
            int br_igr = 0;
            foreach (Klubovi klub in kluboviS)
            {
                foreach (Igraci igrac in igraciS)
                {
                    if (klub.id_kluba == igrac.id_kluba)
                    {
                        br_igr++;
                    }
                }
                br_kl++;
            }
            if (br_kl == 10 && br_igr > 11)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void odigraj(List<Igraci> igraciS, List<Klubovi> kluboviS, List<Rezultati> rezultatiS, List<Utakmica> utakmicaS)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja koristeći klasu Random nasumično
            * dodjeljuje vrijednosti zabijenih i primljenih golova
            * u utakmici. Princip je takav da se igraju dvije 
            * polusezone, a u svakoj od njih svaki klub će
            * igrati kod kuće i u gostima.
            * 45 * 2 + 45 * 2 = 180
            * 
            * -----------------------------------------------------
            */
            Random rnd = new Random();
            int zabijeni, primljeni;
            if(countIgracaKlubova(igraciS, kluboviS))
            {
                for (int i = 1; i < 3; i++)
                {
                    foreach (Klubovi klub in kluboviS)
                    {
                        foreach (Klubovi klub_vs in kluboviS)
                        {
                            if (klub.naziv_kluba != klub_vs.naziv_kluba)
                            {
                                zabijeni = rnd.Next(0, 3);
                                primljeni = rnd.Next(0, 3);
                                Utakmica zapis = new Utakmica(klub.id_kluba, klub_vs.id_kluba, zabijeni, primljeni);
                                utakmicaS.Add(zapis);
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("\nMora postojati tocno 10 klubova i minimalno 11 igraca u svakom");
            }
            if (utakmicaS.Count == 180)
            {
                Console.WriteLine("\nSve utakmice uspjesno su odigrane!");
            }
            UtakmiceUXml(utakmicaS);
            PonovniPrikazIzbornikaIliIzlaz(igraciS, kluboviS, rezultatiS, utakmicaS);
        }
        static void izracunaj(List<Utakmica> utakmicaS, List<Rezultati> rezultatiS, List<Klubovi> kluboviS)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja sluzi za racunanje svih parametara 
            * potrebnih za prikaz rang liste za svih 10 klubova
            * 
            * -----------------------------------------------------
            */
            int bodovi = 0;
            int pobjedaDomaci = 0;
            int nerjesenDomaci = 0;
            int gubitakDomaci = 0;
            int gol_razlika = 0;
            int gol_dao = 0;
            int gol_primio = 0;
            int broj_utakmica = 0;
            for (int j = 1; j <= 10; j++)
            {
                foreach (Utakmica zapis in utakmicaS)
                {
                    if (zapis.idDomaci == j)            //klub čiji se rezultati zbrajaju, na domacem terenu 
                    {
                        gol_dao += zapis.goloviDomaci;
                        gol_primio += zapis.goloviGosti;
                        if (zapis.goloviDomaci > zapis.goloviGosti)
                        {
                            pobjedaDomaci += 1;
                            bodovi += 3;
                        }
                        else if (zapis.goloviDomaci == zapis.goloviGosti)
                        {
                            nerjesenDomaci += 1;
                            bodovi += 1;
                        }
                        else if (zapis.goloviDomaci < zapis.goloviGosti)
                        {
                            gubitakDomaci += 1;
                        }
                        broj_utakmica++;
                    }
                    else if (zapis.idGosti == j)            //klub čiji se rezultati zbrajaju, na gostujucem terenu 
                    {
                        gol_dao += zapis.goloviGosti;
                        gol_primio += zapis.goloviDomaci;
                        if (zapis.goloviDomaci < zapis.goloviGosti)
                        {
                           pobjedaDomaci += 1;
                            bodovi += 3;
                        }
                        else if (zapis.goloviDomaci == zapis.goloviGosti)
                        {
                            nerjesenDomaci += 1;
                            bodovi += 1;
                        }
                        else if (zapis.goloviDomaci > zapis.goloviGosti)
                        {
                            gubitakDomaci += 1;
                        }
                        broj_utakmica++;
                    }
                }
                //bodovi = Convert.ToInt32(pobjedaDomaci * 3 + nerjesenDomaci);
                gol_razlika = gol_dao - gol_primio;
                foreach (Klubovi klub in kluboviS)                                 // dodavanje rezultata za pojedini klub u listu
                { 
                    if (klub.id_kluba == j)
                    {
                        rezultatiS.Add(new Rezultati(j, klub.naziv_kluba, broj_utakmica, bodovi, pobjedaDomaci, nerjesenDomaci, gol_razlika, gol_dao));
                        bodovi = 0;
                        gol_razlika = 0;
                    }
                }
                pobjedaDomaci = 0;
                nerjesenDomaci = 0;
                gubitakDomaci = 0;
                broj_utakmica = 0;
            }
            rezultatiUXml(rezultatiS);
        }
        static List<Utakmica> dohvatiUtakmice(List<Utakmica> utakmiceS)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja ce dohvatiit sve zapise iz datoteke
            * zapisiUtakmica.xml i spremiti ih u listu tipa Utakmica
            * 
            * -----------------------------------------------------
            */
            string sadrzaj_xml = "";
            StreamReader osr = new StreamReader("zapisiUtakmica.xml");
            using (osr)
            {
                sadrzaj_xml = osr.ReadToEnd();
            }
            XmlDocument xml_datoteka = new XmlDocument();
            xml_datoteka.LoadXml(sadrzaj_xml);
            XmlNodeList atributi = xml_datoteka.SelectNodes("//data/utakmica");
            foreach (XmlNode sadrzaj in atributi)
            {
                utakmiceS.Add(new Utakmica(Convert.ToInt32(sadrzaj.Attributes["idDomaci"].Value), Convert.ToInt32(sadrzaj.Attributes["idGosti"].Value), Convert.ToInt32(sadrzaj.Attributes["goloviDomaci"].Value), Convert.ToInt32(sadrzaj.Attributes["goloviGosti"].Value)));
            }
            osr.Close();
            return utakmiceS;
        }
        static void UtakmiceUXml(List<Utakmica> utakmicaS)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja sprema podatke iz liste odigranih
            * utakmica u datoteku zapisUtakmica.xml
            * 
            * -----------------------------------------------------
            */
            var doc = XDocument.Load("zapisiUtakmica.xml");
            doc.Root.RemoveNodes();
            foreach (Utakmica zapis in utakmicaS)
            {
                var newElement = new XElement("utakmica",
                    new XAttribute("idDomaci", zapis.idDomaci),
                    new XAttribute("idGosti", zapis.idGosti),
                    new XAttribute("goloviDomaci", zapis.goloviDomaci),
                    new XAttribute("goloviGosti", zapis.goloviGosti));
                doc.Element("data").Add(newElement);
                doc.Save("zapisiUtakmica.xml");
            }
            doc.Save("zapisiUtakmica.xml");
        }
        static void rezultatiUXml(List<Rezultati> rezultatiS)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja sprema podatke iz liste rezultata
            * rang liste u datoteku rezultati.xml
            * 
            * -----------------------------------------------------
            */
            var doc = XDocument.Load("rezultati.xml");
            doc.Root.RemoveNodes();
            foreach (Rezultati rez in rezultatiS)
            {
                var newElement = new XElement("rezultat",
                    new XAttribute("rbr", rez.rbr),
                    new XAttribute("klub", rez.ime_kluba),
                    new XAttribute("brojUtakmica", rez.broj_utakmica),
                    new XAttribute("bodovi", rez.broj_bodova),
                    new XAttribute("pobjeda", rez.pobjeda),
                    new XAttribute("nerijeseno", rez.nerijeseno),
                    new XAttribute("golRazlika", rez.razlika),
                    new XAttribute("zabijenihGolova", rez.pogodci));
                doc.Element("data").Add(newElement);
                doc.Save("rezultati.xml");
            }
            doc.Save("rezultati.xml");
        }

        static void prikaziRangListu(List<Igraci> igraciS,List<Klubovi> kluboviS,List<Rezultati> rezultatiS, List<Utakmica> utakmicaS)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija služi za prikazivanje rang liste klubova
            * u tablici pomoću naredbe ConsoleTable.
            * Klubovi se rangiraju silazno po broju bodova, 
            * a ako dva ili više klubova imaju isti broj bodova 
            * gleda se koji ima veću gol razliku. 
            * 
            * -----------------------------------------------------
            */
            var table = new ConsoleTable("Redni broj", "Klub", "Broj Utakmica", "Bodovi", "Pobjeda", "Neriješeno", "Gol razlika", "Postignuti pogodci");
            List<Rezultati> Sorted = rezultatiS.OrderByDescending(o => o.broj_bodova).ThenByDescending(o => o.razlika).ThenByDescending(o => o.pogodci).ToList();
            int counter = 1;
            foreach (Rezultati zapis in Sorted)
            {
                table.AddRow(counter+".", zapis.ime_kluba, zapis.broj_utakmica, zapis.broj_bodova, zapis.pobjeda, zapis.nerijeseno, zapis.razlika, zapis.pogodci);
                counter++;
            }
            table.Write();
            PonovniPrikazIzbornikaIliIzlaz(igraciS, kluboviS, rezultatiS, utakmicaS);
        }
        static int provjeraBroja()
        {
            /*
            * ------------------------------------------------------
            * 
            * Funkcija provjerava je li korisnik unjeo cjelobrojnu
            * vrijednost, a ako nije vraća ga na ponovni unos
            * 
            * ------------------------------------------------------
            */
            int broj;
            bool num = false;
            while (!num)
            {
                if (int.TryParse(Console.ReadLine(), out broj))
                {
                    num = true;
                    return broj;
                }
                else
                {
                    Console.WriteLine("Niste unijeli cijelobrojnu vrijednost, pokusajte ponoovo!");
                }
            }
            return 0;
        }
        static string provjeraPraznogStringa()
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija koja nam osigurava da korisnik ne može
            * ostaviti polje za unos podatka praznim
            * 
            * -----------------------------------------------------
            */
            string unos = "";
            while (unos == "")
            {
                unos = Console.ReadLine();
                if (unos == "")
                {
                    Console.WriteLine("Ovo polje ne možete ostaviti praznim!");
                }
            }
            return unos;
        }
        static void LogAkcije(string akcija)
        {
            /*
            * -----------------------------------------------------
            * 
            * Funkcija upisuje akciju koju je korisnik napravio
            * u datoteku logs.log
            * 
            * -----------------------------------------------------
            */
            DateTime now = DateTime.Now;
            StreamWriter log = new StreamWriter("logs.log", true);
            log.WriteLine("{0} - {1}", now.ToString("F"), akcija);
            log.Flush();
            log.Close();
        }
        static void ocistiMemoriju(List<Klubovi> kluboviS, List<Igraci> igraciS, List<Rezultati> rezultatiS, List<Utakmica> utakmicaS)
        {
            LogAkcije("Čišćenje lista/memorije.");
            kluboviS.Clear();
            igraciS.Clear();
            rezultatiS.Clear();
            utakmicaS.Clear();
        }
    }
}