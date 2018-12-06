using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZupaTechTest.Data;
using ZupaTechTest.Domain.Repositories;
using ZupaTechTest.Domain.Services;
using ZupaTechTest.Domain.Validators;

namespace ZupaTechTest
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
            services.AddMvc();
            
            services.AddDbContext<BookingContext>(options => options.UseSqlServer
                (Configuration.GetConnectionString("BookingContext")));

            services.AddScoped<IBookingRequestService, BookingRequestService>();
            services.AddScoped<IContactDetailsValidator, ContactDetailsValidator>();
            services.AddScoped<IRequestedSeatValidator, RequestedSeatValidator>();
            services.AddScoped<ISeatBookingRepository, SeatBookingRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            
        }
    }
}
