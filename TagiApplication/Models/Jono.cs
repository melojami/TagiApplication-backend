using System.ComponentModel.DataAnnotations.Schema;
using TagiApplication.Interfaces;

namespace TagiApplication.Models
{
    [Table("vp_jono")]
    public class Jono : ModelBase<Jono, JonoResponse, JonoRequest>, ISoftDelete
    {
        public int Id { get; set; }
        public int ResurssiId { get; set; }
        public int ResurssityyppiId { get; set; }
        public string Nimi { get; set; } = string.Empty;

        [Column("poistettuUTc")]
        public DateTime? Poistettu { get; set; }

        public override JonoResponse ToDTO(Jono jono)
        {
            return new JonoResponse()
            {
                Id = jono.Id,
                ResurssiId = jono.ResurssiId,
                ResurssityyppiId = jono.ResurssityyppiId,
                Nimi = jono.Nimi
            };
        }
    }

    public record class JonoResponse
    {
        public int Id { get; set; }
        public int ResurssiId { get; set; }
        public int ResurssityyppiId { get; set; }
        public string Nimi { get; set; } = string.Empty;
    }

    public record class JonoRequest
    {

    }
}
