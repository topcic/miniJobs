using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Helper;
using WebAPI.Models;

namespace WebAPI.Modul_TestniPodaci.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestniPodaciController : Controller
    {
        private readonly APIDbContext _db;
        public TestniPodaciController(APIDbContext dbContext)
        {
            _db = dbContext;
        }
        [HttpPost]
        public ActionResult Generisi()
        {
            var drzave = new List<Drzava>();
            var opstine = new List<Opstina>();
            var pitanje = new List<Pitanje>();
            var PosaoTipovi = new List<PosaoTip>();
            var aplikanti = new List<Aplikant>();
            var poslodavci = new List<Poslodavac>();
            var ponudjeniOdgovori = new List<PonudjeniOdgovor>();
            var apliciraniPoslovi = new List<ApliciraniPosao>();


            var poslovi = new List<Posao>();
            var posaoPitanje = new List<PosaoPitanje>();
            var pitanjaOdgovori = new List<PitanjeOdgovor>();

            pitanje.Add(new Pitanje { naziv = "Koje je radno vrijeme posla? " });
            pitanje.Add(new Pitanje { naziv = "Način plaćanja? " });
            pitanje.Add(new Pitanje { naziv = "Da li plaćanje? " });
            var rasporedi = new List<string> {
                "prva smjena",
                "druga smjena",
                "noćna smjena",
                "vikend",
                "1h-2h",
                "2h-3h",
                "4h i više",
                "po dogovoru",
                "pon-pet",
                "vikend",
                "praznici",
                "dan smjena",
             };
            var tipoviPosla = new List<string> {
            "Čišćenje",
                "Dostavljanje hrane",
                "Vrtlarenje",
                "Cijepanje drva",
                "Popravak električnih aparata",
                "Čuvanje kućnih ljubimaca",
                "Pomoć u kući",
                "Pomoć u kupovini",
                "Pomoć pri oblačenju i kupanju",
                "Priprema obroka",
                "Pomoć u održavanju higijene doma",
                "Pomoć u organizaciji lijekova",
                "Uklanjanje stabala i grmlja",
                "Keramičar",
                "Vodoinstalater",
                "Konobar",
                "Utovar/istovar",
                "Pomoćni radnik",
                "Kućna njega",
                "Radnik"
            };
            foreach (var r in rasporedi)
            {
                ponudjeniOdgovori.Add(new PonudjeniOdgovor { odgovor =r, pitanje_id = 1 });
            }
            foreach (var tp in tipoviPosla)
            {
                PosaoTipovi.Add(new PosaoTip { naziv = tp });
            }
            ponudjeniOdgovori.Add(new PonudjeniOdgovor { odgovor = "po satu", pitanje_id = 2 });
            ponudjeniOdgovori.Add(new PonudjeniOdgovor { odgovor = "dnevnica", pitanje_id = 2 });
            ponudjeniOdgovori.Add(new PonudjeniOdgovor { odgovor = "po dogovoru", pitanje_id = 2 });


            ponudjeniOdgovori.Add(new PonudjeniOdgovor { odgovor = "Prekovremene sate", pitanje_id = 3});
            ponudjeniOdgovori.Add(new PonudjeniOdgovor { odgovor = "Topli obrok", pitanje_id = 3 });




            drzave.Add(new Drzava { naziv = "BiH" });
            drzave.Add(new Drzava { naziv = "HR" });
            drzave.Add(new Drzava { naziv = "Njemacka" });
            drzave.Add(new Drzava { naziv = "Austrija" });
            drzave.Add(new Drzava { naziv = "SAD" });
            drzave.Add(new Drzava { naziv = "Malezija" });

            var lokacije = new List<string> {"Banovići", "Banja Luka", "Berkovići", "Bihać", "Bijeljina", "Bileća", "Bosanska Krupa",
                                             "Bosanski Petrovac", "Bosansko Grahovo", "Bratunac", "Brčko", "Breza", "Brod", "Bugojno",
                                             "Busovača", "Bužim", "Cazin", "Centar", "Čajniče", "Čapljina", "Čelić", "Čelinac", "Čitluk",
                                             "Derventa", "Doboj", "Doboj-Istok", "Doboj-Jug", "Dobretići", "Domaljevac-Šamac", "Donji Vakuf",
                                             "Donji Žabar", "Drvar", "Foča (FBiH)", "Fojnica", "Gacko", "Glamoč", "Goražde",
                                             "Gornji Vakuf-Uskoplje", "Gračanica", "Gradačac", "Gradiška", "Grude", "Hadžići", "Han-Pijesak",
                                             "Ilidža", "Ilijaš", "Jablanica", "Jajce", "Kakanj", "Kalesija", "Kalinovik", "Kiseljak", "Kladanj",
                                             "Ključ", "Konjic", "Kotor Varoš", "Kreševo", "Krupa na Uni", "Krupanj (RS)", "Kupres", "Laktaši",
                                             "Livno", "Lopare", "Lukavac", "Ljubinje", "Ljubuški", "Maglaj", "Milići", "Modriča", "Mostar",
                                             "Mrkonjić Grad", "Neum", "Nevesinje", "Novi Grad (Bosanski Novi)", "Novi Grad (Sarajevo)",
                                             "Novo Sarajevo", "Odžak", "Olovo", "Orašje", "Osmaci", "Oštra Luka", "Pale-Prača", "Pelagićevo",
                                             "Petrovac", "Petrovo", "Posušje", "Prozor-Rama", "Prijedor", "Prnjavor", "Ravno", "Ribnik", "Rogatica",
                                             "Rudo", "Sanski Most", "Sapna", "Sarajevo-Centar", "Sarajevo-Novi Grad", "Sarajevo-Stari Grad",
                                             "Sarajevo-Čengić Vila", "Sarajevo-Ilidža", "Sarajevo-Nahorevo", "Sarajevo-Vogošća", "Srebrenik",
                                             "Srebrenica", "Sokolac", "Srbac", "Srednji", "Stari Grad", "Šekovići", "Šipovo", "Široki Brijeg",
                                             "Štrpci", "Teočak", "Teslić", "Tešanj", "Tomislavgrad", "Travnik", "Trebinje",
                                             "Trnovo","Tuzla","Ugljevik","Usora","Vareš"
                                             ,"Velika Kladuša","Visoko","Višegrad",
                                             "Vitez","Vlasenica","Vogošća","Vukosavlje","Zavidovići",
                                              "Zenica","Zvornik","Žepče","Živinice" };

            foreach (var l in lokacije)
            {
                opstine.Add(new Opstina { description = l, drzava = drzave[0] });
            }


            poslodavci.Add(new Poslodavac { ime = "Ahmet", prezime = "Smajic", korisnickoIme = "ahmet", lozinka = "test",opstina_id=1 });
            poslodavci.Add(new Poslodavac { ime = "Almin", prezime = "Besic", korisnickoIme = "almin", lozinka = "test", opstina_id = 21 });
            poslodavci.Add(new Poslodavac { ime = "Edin", prezime = "Elez", korisnickoIme = "edin", lozinka = "test", opstina_id = 12 });
            poslodavci.Add(new Poslodavac { ime = "Nejla", prezime = "Mijic", korisnickoIme = "nejla", lozinka = "test", opstina_id = 9 });
            poslodavci.Add(new Poslodavac { ime = "Edvin", prezime = "Topic", korisnickoIme = "edvin", lozinka = "test", opstina_id = 6 });


            poslovi.Add(new Posao
            {
                poslodavac_id = 5,
                naziv = "Utovar/istovar",
                opis = "Potrebno je istovariti tri kamiona robe.",
                datum_kreiranja = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                brojAplikanata = 6,
                status = "Aktivan",
                Cijena = 8,
                opstina_id = 10,
                deadline = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                posaoTip_id = 3
            });
            poslovi.Add(new Posao
            {
                poslodavac_id = 3,
                naziv = "Konobar",
                opis = "Potrebno konobar u kaficu X.",
                datum_kreiranja = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                brojAplikanata = 2,
                Cijena = 5,
                opstina_id = 10,
                deadline = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                status = "Aktivan",
                posaoTip_id = 1
            });
            poslovi.Add(new Posao
            {
                poslodavac_id = 1,
                naziv = "Električar",
                opis = "Potrebno električar da razvede struju u kući.",
                datum_kreiranja = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                brojAplikanata = 1,
                Cijena = 10,
                opstina_id = 10,
                deadline = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                status = "Aktivan",
                posaoTip_id = 2
            });
            poslovi.Add(new Posao
            {
                poslodavac_id = 1,
                naziv = "Pomoćni radnik",
                opis = "Potreban pomoćni radnik u pekari. Zadaci: prodaja, komunikacija sa mušterijama, pomoć glavnom pekaru",
                datum_kreiranja = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                brojAplikanata = 2,
                Cijena = 6,
                opstina_id = 10,
                deadline = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                posaoTip_id = 4,
                status = "Aktivan"
            });
            poslovi.Add(new Posao
            {
                poslodavac_id = 2,
                naziv = "Kućna njega",
                opis = "Potrebna osoba za kućnu njegu oko starije osobe (80 godina). Starija osoba može da hoda ali je potrebna samo mala pomoć pri obavljanju svakodnevnih poslova",
                datum_kreiranja = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                brojAplikanata = 1,
                Cijena = 9,
                opstina_id = 10,
                deadline = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                posaoTip_id = 5,
                status = "Aktivan"
            });
            apliciraniPoslovi.Add(new ApliciraniPosao { 
            posao_id= 1,
            datum_apliciranja= DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
            status="Aktivan",
            aplikant_id=6
            });
            apliciraniPoslovi.Add(new ApliciraniPosao
            {
                posao_id = 2,
                datum_apliciranja = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                status = "Aktivan",
                aplikant_id = 7
            });
            apliciraniPoslovi.Add(new ApliciraniPosao
            {
                posao_id = 2,
                datum_apliciranja = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                status = "Aktivan",
                aplikant_id = 8
            });
            apliciraniPoslovi.Add(new ApliciraniPosao
            {
                posao_id = 2,
                datum_apliciranja = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                status = "Aktivan",
                aplikant_id = 9
            });
            apliciraniPoslovi.Add(new ApliciraniPosao
            {
                posao_id = 2,
                datum_apliciranja = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                status = "Aktivan",
                aplikant_id = 10
            });
            posaoPitanje.Add(new PosaoPitanje { pitanje_id = 1, posao_id = 2 });
            posaoPitanje.Add(new PosaoPitanje { pitanje_id = 2, posao_id = 2 });

            pitanjaOdgovori.Add(new PitanjeOdgovor { posaoPitanje_id = 1, ponudjeniOdgovor_id = 1 });
            pitanjaOdgovori.Add(new PitanjeOdgovor { posaoPitanje_id = 2, ponudjeniOdgovor_id = 13 });
            pitanjaOdgovori.Add(new PitanjeOdgovor { posaoPitanje_id = 2, ponudjeniOdgovor_id = 14 });


            aplikanti.Add(new Aplikant
            {
                datumRegistracije = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                ime = "Adil",
                prezime = "Kurić",
                korisnickoIme = "adilkuric",
                lozinka = "test",
                opstina_rodjenja_id= 1,
                
            });
            aplikanti.Add(new Aplikant
            {
                datumRegistracije = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                ime = "Almina",
                prezime = "Topić",
                korisnickoIme = "alminatopic",
                lozinka = "test",
                opstina_rodjenja_id = 2,

            });
            aplikanti.Add(new Aplikant
            {
                datumRegistracije = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                ime = "Adel",
                prezime = "Kadić",
                korisnickoIme = "adel1999",
                lozinka = "test",
                opstina_rodjenja_id = 5,

            });

            aplikanti.Add(new Aplikant
            {
                datumRegistracije = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                ime = "Amar",
                prezime = "Dizdar",
                korisnickoIme = "dizdar001",
                lozinka = "test",
                opstina_rodjenja_id = 6,

            });
            aplikanti.Add(new Aplikant
            {
                datumRegistracije = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss"),
                ime = "Emir",
                prezime = "Smajić",
                korisnickoIme = "smajke20",
                lozinka = "test",
                opstina_rodjenja_id = 6,

            });




          
           
            _db.AddRange(opstine);
            _db.AddRange(poslodavci);
            _db.AddRange(drzave);
            _db.AddRange(PosaoTipovi);
            _db.AddRange(pitanje);
            _db.SaveChanges();
            _db.AddRange(aplikanti);
            _db.AddRange(poslovi);
            _db.SaveChanges();

            _db.AddRange(posaoPitanje);
            _db.AddRange(ponudjeniOdgovori);
            _db.AddRange(apliciraniPoslovi);
            _db.SaveChanges();
            _db.AddRange(pitanjaOdgovori);




            _db.SaveChanges();

            return Count();
        }
        [HttpGet]
        public ActionResult Count()
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("Poslodavac", _db.Poslodavac.Count());
            data.Add("Aplikant", _db.Aplikant.Count());
            data.Add("KorisnickiNalog", _db.KorisnickiNalog.Count());
            data.Add("Opstina", _db.Opstina.Count());
            data.Add("PosaoTip", _db.PosaoTip.Count());
            data.Add("Pitanje", _db.Pitanje.Count());
            data.Add("Posao", _db.Posao.Count());
            data.Add("Drzava", _db.Drzava.Count());

            return Ok(data);
        }
    }
}
