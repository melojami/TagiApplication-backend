using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace TagiApplication.Models
{
    [Table("vp_resurssi")]
    public class Resurssi : ModelBase<Resurssi, ResurssiResponse, ResurssiRequest>
    {
        public int Id { get; set; }
        public int ResurssityyppiId { get; set; }
        public string Tarkenne { get; set; } = string.Empty;
        public int? RootId { get; set; }
        public string Nimi { get; set; } = string.Empty;
        public string Sijainti { get; set; } = string.Empty;

        /*
        public override Expression<Func<Resurssi, ResurssiResponse>> Projection
        {
            get
            {
                return x => new ResurssiResponse()
                {
                    Id = x.Id,
                    ResurssityyppiId = x.ResurssityyppiId,
                    RootId = x.RootId,
                    Nimi = x.Nimi,
                    Sijainti = x.Sijainti
                };
            }
        }
        */

        public override ResurssiResponse ToDTO(Resurssi resurssi)
        {
            return new ResurssiResponse()
            {
                Id = resurssi.Id,
                ResurssityyppiId = resurssi.ResurssityyppiId,
                Tarkenne = resurssi.Tarkenne,
                RootId = resurssi.RootId,
                Nimi = resurssi.Nimi,
                Sijainti = resurssi.Sijainti
            };
        }
    }

    public record class ResurssiResponse
    {
        public int Id { get; set; }
        public int ResurssityyppiId { get; set; }
        public string Tarkenne { get; set; }
        public int? RootId { get; set; }
        public string Nimi { get; set; } = string.Empty;
        public string Sijainti { get; set; } = string.Empty;
    }

    public record class ResurssiRequest
    {

    }
}
