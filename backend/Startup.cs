using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

//Para baixar
//Instalando o Entity Framework
//dotnet tool install --global dotnet-ef

//Baixamos o pacote SQLServer do Entity Framework
//dotnet add package Microsoft.EntityFrameworkCore.SqlServer
//Pacote especifico do banco.

//Baixamos o pacote que irá escrever nosso codigos
//dotnet add package Microsoft.EntityFrameworkCore.Design

//Testamos se ospacotes foram instalados 
//dotnet restore

//Testando a instalação do EF
//dotnet ef

//Código que criará  o nosso contexto da Base  de Dados e nossos Models
//dotnet ef dbcontext scaffold "Server=DESKTOP-D4CE51M\SQLEXPRESS; Database=Gufos; User Id=sa; Password=132" Microsoft.EntityFrameworkCore.SqlServer -o Models -d

namespace backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
