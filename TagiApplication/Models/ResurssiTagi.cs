using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using TagiApplication.Interfaces;

namespace TagiApplication.Models
{
    [Table("vp_resurssi_tagi")]
    public class ResurssiTagi : ModelBase<ResurssiTagi, ResurssiTagiResponse, ResurssiTagiRequest>, ISoftDelete
    {
        public int Id { get; set; }
        public int ResurssiId { get; set; }
        public int TagiId { get; set; }
        
        [Column("poistettuUTc")]
        public DateTime? Poistettu { get; set; }

        /*
        public override Expression<Func<ResurssiTagi, ResurssiTagiResponse>> Projection
        {
            get
            {
                return x => new ResurssiTagiResponse()
                {
                    Id = x.Id,
                    ResurssiId = x.ResurssiId,
                    TagiId = x.TagiId
                };
            }
        }
        */

        public override ResurssiTagiResponse ToDTO(ResurssiTagi resurssiTagi)
        {
            return new ResurssiTagiResponse()
            {
                Id = resurssiTagi.Id,
                ResurssiId = resurssiTagi.ResurssiId,
                TagiId = resurssiTagi.TagiId
            };
        }

        public override ResurssiTagi FromDTO(ResurssiTagiRequest request)
        {
            return new ResurssiTagi()
            {
                Id = request.Id,
                ResurssiId = request.ResurssiId,
                TagiId = request.TagiId
            };
        }
    }

    public record class ResurssiTagiResponse
    {
        public int Id { get; set; }
        public int ResurssiId { get; set; }
        public int TagiId { get; set; }
    }

    public record class ResurssiTagiRequest
    {
        public int Id { set; get; }
        public int ResurssiId { get; set; }
        public int TagiId { get; set; }
    }

    public record class ResurssiTagiMultipleRequest
    {
        public int[] resurssiIds { get; set; }
        public int[] tagiIds { get; set; }
    }
}
