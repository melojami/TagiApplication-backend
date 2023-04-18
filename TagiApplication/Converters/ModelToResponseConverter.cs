using System.Linq.Expressions;
using System.Reflection;

namespace TagiApplication.Converters
{
    //public class ModelResponseConverter<Model, Response, Request> : IModelResponseConverter<Model, Response, Request> where Model : class where Response : class, new() where Request : class
    public class ModelResponseConverter<Model> : IModelResponseConverter
    {
        public ModelResponseConverter(IQueryable<Model> model) 
        {
            _model = model;
        }

        /** Probably not a good idea to new() objects in loop.... **/
        /*
        public Response[] ConvertModelListToResponse(Model[] model)
        {
            Response response = new Response();

            foreach (PropertyInfo prop in model.GetType().GetProperties())
            {

                var jee = 0;
            }

            return Array.Empty<Response>();
        }

        public Response ConvertModelToResponse(Model model)
        {
            Response response = new Response();

            foreach (PropertyInfo prop in model.GetType().GetProperties())
            {

                var jee = 0;
            }

            return response;
        }
        */


        public IQueryable<Response> To<Response>()
        {
            Expression<Func<Model, Response>> expr = BuildExpression<Response>();

            return _model.Select(expr);
        }

        public static Expression<Func<Model, Response>> BuildExpression<Response>()
        {
            var sourceMembers = typeof(Model).GetProperties();
            var destinationMembers = typeof(Response).GetProperties();

            var name = "src";

            var parameterExpression = Expression.Parameter(typeof(Model), name);

            return Expression.Lambda<Func<Model, Response>>(
                Expression.MemberInit(
                    Expression.New(typeof(Response)),
                    destinationMembers.Select(dest => Expression.Bind(dest,
                        Expression.Property(
                            parameterExpression,
                            sourceMembers.First(pi => pi.Name == dest.Name)
                        )
                    )).ToArray()
                    ),
                parameterExpression
                );
        }

        private readonly IQueryable<Model> _model;

    }

    //public interface IModelResponseConverter<Model, Response, Request> where Model : class where Response : class where Request : class
    public interface IModelResponseConverter
    {
        IQueryable<Response> To<Response>();
    }
}
