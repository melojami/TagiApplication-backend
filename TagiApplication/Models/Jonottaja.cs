using System.ComponentModel.DataAnnotations.Schema;
using TagiApplication.Interfaces;

namespace TagiApplication.Models
{
    [Table("vp_jonottaja")]
    public class Jonottaja : ModelBase<Jonottaja, JonottajaResponse, JonottajaRequest>, ISoftDelete
    {
        public int Id { get; set; }
        public int JonoId { get; set; }
        public string Nimi { get; set; } = string.Empty;
        public string Otsikko { get; set; } = string.Empty;
        
        [Column("kaikkien_ehtojen_tasmattava")]
        public bool KaikkienEhtojenTasmattava { get; set; }

        [Column("poistettuUTc")]
        public DateTime? Poistettu { get; set; }

        public override JonottajaResponse ToDTO(Jonottaja jonottaja)
        {
            return new JonottajaResponse()
            {
                Id = jonottaja.Id,
                JonoId = jonottaja.JonoId,
                Nimi = jonottaja.Nimi,
                Otsikko = jonottaja.Otsikko,
                KaikkienEhtojenTasmattava = jonottaja.KaikkienEhtojenTasmattava
            };
        }
    }

    public record class JonottajaResponse
    {
        public int Id { get; set; }
        public int JonoId { get; set; }
        public string Nimi { get; set; } = string.Empty;
        public string Otsikko { get; set; } = string.Empty;
        public bool KaikkienEhtojenTasmattava { get; set; }
    }

    public record class JonottajaRequest
    {

    }
}
