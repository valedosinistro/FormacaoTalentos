using System;
using System.Net;
using System.Threading.Tasks;
using Fatec.Clinica.Dominio.Excecoes;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Fatec.Clinica.Api.Filtros
{
    public class ErroFiltro
    {
        public async Task Invoke(HttpContext context, Func<Task> next)
        {
            try
            {
                await next();
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code;

            switch (exception)
            {
                case NaoEncontradoException nfEx:
                    code = HttpStatusCode.NotFound;
                    break;
                case ConflitoException nfEx:
                    code = HttpStatusCode.Conflict;
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonConvert.SerializeObject(new { error = exception.Message, inner = exception.InnerException });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}

