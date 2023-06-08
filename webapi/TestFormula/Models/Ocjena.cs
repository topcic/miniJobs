using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Ocjena
    {
        [Key]
        public int id { get; set; }
        public int vrijednost { get; set; }
       public string  komentar { get; set; }

        public int ocjenjen_id { get; set; }
        public KorisnickiNalog ocjenjen { get; set; }

        //  [ForeignKey(nameof(primalac))]
        public int ocjenjuje_id { get; set; }
        public KorisnickiNalog ocjenjuje { get; set; }

        public int apliciraniPosao_id { get; set; }
        public ApliciraniPosao apliciraniPosao { get; set; }

        public DateTime datum_kreiranja { get; set; }

    }
}
