using System.ComponentModel.DataAnnotations.Schema;
using TagiApplication.Interfaces;

namespace TagiApplication.Models
{
    [Table("vp_Jonottaja_tagi")]
    public class JonottajaTagi : ModelBase<JonottajaTagi, JonottajaTagiResponse, JonottajaTagiRequest>, ISoftDelete
    {
        public int Id { get; set; }
        public int JonottajaId { get; set; }
        public int TagiId { get; set; }

        [Column("poistettuUTc")]
        public DateTime? Poistettu { get; set; }

        /*
        public override Expression<Func<JonottajaTagi, JonottajaTagiResponse>> Projection
        {
            get
            {
                return x => new JonottajaTagiResponse()
                {
                    Id = x.Id,
                    JonottajaId = x.JonottajaId,
                    TagiId = x.TagiId
                };
            }
        }
        */

        public override JonottajaTagiResponse ToDTO(JonottajaTagi JonottajaTagi)
        {
            return new JonottajaTagiResponse()
            {
                Id = JonottajaTagi.Id,
                JonottajaId = JonottajaTagi.JonottajaId,
                TagiId = JonottajaTagi.TagiId
            };
        }

        public override JonottajaTagi FromDTO(JonottajaTagiRequest request)
        {
            return new JonottajaTagi()
            {
                Id = request.Id,
                JonottajaId = request.JonottajaId,
                TagiId = request.TagiId
            };
        }
    }

    public record class JonottajaTagiResponse
    {
        public int Id { get; set; }
        public int JonottajaId { get; set; }
        public int TagiId { get; set; }
    }

    public record class JonottajaTagiRequest
    {
        public int Id { set; get; }
        public int JonottajaId { get; set; }
        public int TagiId { get; set; }
    }
}

