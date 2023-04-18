using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using TagiApplication.Interfaces;

namespace TagiApplication.Models
{
    [Table("vp_resurssityyppi")]
    public class Tagityyppi : ModelBase<Tagityyppi, TagityyppiResponse, TagityyppiRequest>, ISoftDelete
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int Id { get; set; } // New, koska overridaa basemodelin
        public string Nimi { get; set; } = string.Empty;

        [Column("poistettuUTc")]
        public DateTime? Poistettu { get; set; }


        public override TagityyppiResponse ToDTO(Tagityyppi tagi)
        {
            return new TagityyppiResponse()
            {
                Id = tagi.Id,
                Nimi = tagi.Nimi
            };
        }

        public override Tagityyppi FromDTO(TagityyppiRequest request)
        {
            return new Tagityyppi();
            /*
            return new Tagityyppi()
            {
                Id = request.Id,
                Nimi = request.Nimi
            };
            */
        }
    }

    public record class TagityyppiResponse
    {
        public int Id { get; set; }
        public string Nimi { get; set; } = string.Empty;
    }

    public record class TagityyppiRequest
    {
        /*
        public int Id { get; set; }
        public string Nimi { get; set; } = string.Empty;
        */
    }
}

