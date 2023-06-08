using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webAPI.Models.ViewModels;
using WebAPI.Data;
using WebAPI.Models;

namespace webAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OcjenaController : Controller
    {
        private readonly APIDbContext _db;

        public OcjenaController(APIDbContext db)
        {
            _db = db;

        }
        [HttpPost]
        public ActionResult Add(OcjenaAddVM ocjena)
        {

            var ocjenjujeKorisnik = _db.KorisnickiNalog.FirstOrDefault(k => k.korisnickoIme == ocjena.ocjenjujeUsername);
            if (ocjenjujeKorisnik == null)
                return BadRequest();

            var ocjenjeniKorisnik = _db.KorisnickiNalog.FirstOrDefault(k => k.korisnickoIme == ocjena.ocjenjeniUsername);
            if (ocjenjeniKorisnik == null)
                return BadRequest();
            if ((bool)ocjenjujeKorisnik.isPoslodavac)
            {
                // kada poslodavac ocjenjuje

                var ocjenaObj = new Ocjena()
                {
                    ocjenjuje_id = ocjenjujeKorisnik.id,
                    ocjenjen_id = ocjenjeniKorisnik.id,
                    vrijednost = ocjena.ocjena,
                    komentar = ocjena.komentar,
                    datum_kreiranja = DateTime.Now,
                    apliciraniPosao_id = ocjena.posao_id
                    
                };
                _db.Ocjena.Add(ocjenaObj);
                _db.SaveChanges();
                var obavljeniPosao = _db.Posao.FirstOrDefault(p => p.id == ocjena.posao_id);
                if (obavljeniPosao.brojAplikanata == 1)
                    obavljeniPosao.status = "Završen";
                else
                {
                    var ocjenjeniAplikanti = _db.Ocjena.Where(o => o.ocjenjuje_id == ocjenjujeKorisnik.id && o.apliciraniPosao_id == ocjena.posao_id).ToList().Count();
                    if (obavljeniPosao.brojAplikanata == ocjenjeniAplikanti)
                        obavljeniPosao.status = "Završen";
                }

                _db.SaveChanges();

            }
            //Ovo je za tebe adi
            /*   else
               {
                   //aplikanat ocjenjuje
                   var ocjenaObj = new Ocjena()
                   {
                       ocjenjuje_id = ocjenjujeKorisnik.id,
                       ocjenjen_id = ocjenjeniKorisnik.id,
                       vrijednost = ocjena.ocjena,
                       komentar = ocjena.komentar,
                       datum_kreiranja = DateTime.Now,
                       apliciraniPosao_id = ocjena.posao_id,
                       status = "validna"
                   };
                   _db.Ocjena.Add(ocjenaObj);
                
                   _db.SaveChanges();
               }*/

            return Ok();
        }
        [HttpGet]
        public ActionResult GetOcjenjeneAplikanteByPosaoId(int posao_id)
        {
            var posao = _db.Posao.FirstOrDefault(p => p.id == posao_id);
            if (posao == null)
                return BadRequest();
            var ocjenjeniAplikanti = (from o in _db.Ocjena
                                      join k in _db.KorisnickiNalog on o.ocjenjen_id equals k.id
                                      where o.apliciraniPosao_id == posao_id && o.ocjenjuje_id == posao.poslodavac_id
                                      select k.korisnickoIme)
                           .ToList();

            var result = ocjenjeniAplikanti.Select(username => new { username }).ToList();

            return Ok(result);
        }
    }
}
