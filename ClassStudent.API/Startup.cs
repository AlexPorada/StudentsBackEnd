using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using ClassStudent.DAL.AutoMapperProfiles;
using ClassStudent.DAL.Context;
using ClassStudent.DAL.Interfaces;
using ClassStudent.DAL.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace ClassStudent.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IContainer ApplicationContainer { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            RegisterDbContext(services);
            
            RegisterAutoMapperProfiles(services);
            services.AddSwaggerGen(s => s.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info() { Title = "Contacts API", Version = "v1"}));

            services.AddCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ApplicationContainer = BuildIOCContainer(services);

            return new AutofacServiceProvider(ApplicationContainer);
        }

        public IContainer BuildIOCContainer(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
            builder.RegisterAssemblyTypes(Assembly.Load("ClassStudent.DAL"))
                .Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerDependency();

            return builder.Build();
        }

        public void RegisterAutoMapperProfiles(IServiceCollection services)
        {
            Mapper.Initialize(mc => {
                     mc.AddProfile(new StudentProfile());
                     mc.AddProfile(new RoomProfile());
                     mc.AddProfile(new TeacherProfile());
                });
            }

        public void RegisterDbContext(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<StudentRoomContext>(opt => opt.UseSqlServer(connectionString).UseLazyLoadingProxies());
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(options => 
                            options.WithOrigins("*").AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader().AllowCredentials());

            app.UseSwagger();
            app.UseSwaggerUI(c => 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts API v1"));

            app.UseHttpsRedirection();


            app.UseMvc();
        }
    }
}
