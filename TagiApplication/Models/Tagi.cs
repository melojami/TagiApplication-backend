using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using TagiApplication.Interfaces;

namespace TagiApplication.Models
{
    [Table("vp_tagi")]
    public class Tagi : ModelBase<Tagi, TagiResponse, TagiRequest>, ISoftDelete
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int Id { get; set; } // New, koska overridaa basemodelin
        public int ResurssityyppiId { get; set; }
        public string? Nimi { get; set; }

        [Column("poistettuUTc")]
        public DateTime? Poistettu { get; set; }

        public override Expression<Func<Tagi, TagiResponse>> Projection
        {
            get
            {
                return x => new TagiResponse()
                {
                    Id = x.Id,
                    ResurssityyppiId = x.ResurssityyppiId,
                    Nimi = x.Nimi,
                };
            }
        }

        public override TagiResponse ToDTO(Tagi tagi)
        {
            return new TagiResponse()
            {
                Id = tagi.Id,
                ResurssityyppiId = tagi.ResurssityyppiId,
                Nimi = tagi.Nimi
            };
        }

        public override Tagi FromDTO(TagiRequest request)
        {
            return new Tagi()
            {
                Id = request.Id,
                ResurssityyppiId = request.Resurssityyppiid,
                Nimi = request.Nimi
            };
        }
    }

    public record class TagiResponse
    {
        public int Id { get; set; }
        public int ResurssityyppiId { get; set; }
        public string? Nimi { get; set; }
    }

    public record class TagiRequest
    {
        public int Id { get; set; }
        public int Resurssityyppiid { get; set; }
        public string? Nimi { get; set;}
    }
}
