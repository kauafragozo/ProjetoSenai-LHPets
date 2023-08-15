using Projeto_Web_Lh_Pets_versão_1;

namespace Projeto_LH_Pets;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Projeto Web LH Pets - Versão 1.0!");
        app.UseStaticFiles();
        app.MapGet("/index", (HttpContext contexto) => {
            contexto.Response.Redirect("index.html", false);
            });

            Banco dba = new();
            app.MapGet("/ListaClientes", (HttpContext contexto) => {
                contexto.Response.WriteAsync(dba.GetListaString());
            });

        app.Run();
    }
}
