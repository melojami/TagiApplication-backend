using System.Linq.Expressions;

namespace TagiApplication.Models
{
    public abstract class ModelBase<Model, Response, Request>
    where Model : new()
    where Response : new()
    where Request : class
    {

        public int Id { get; set; }

        public virtual Expression<Func<Model, Response>> Projection
            => x => new Response();

        public virtual Response ToDTO(Model model)
        {
            return new Response();
            { }
        }

        public virtual Model FromDTO(Request request)
        {
            return new Model();
            { }
        }
    }

    // Ei käytössä, en saanut tämän kautta ID:tä näkyviin commonrepoon
    public interface DTOBase
    {
        //public DTOBase() { }

        public int Id { get; set; }
    }
}
