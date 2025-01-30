using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Configurations;
using Application.Services;
using Application.Services.Auth;
using Domain.Model;
using Infra.EF.Data.Context;
using Infra.EF.Interfaces;
using Infra.EF.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SQLitePCL;

namespace Application.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection ConfigurationService(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProposalService, ProposalService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
        public static IServiceCollection ConfigurationRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProposalRepository, ProposalRepository>();
            return services;
        }

        public static void AddJwtConfigurations(this WebApplicationBuilder builder)
        {
            var JwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
            builder.Services.Configure<JwtSettings>(JwtSettingsSection);

            var jwtSettings = JwtSettingsSection.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidIssuer = jwtSettings.Issuer
                };
            });
        }

        public static void AppSeedDataBaseConstructor(this WebApplication webApplication)
        {

            using (var scope = webApplication.Services.CreateScope()) // Criar o escopo corretamente
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDataContext>();

                if (!context.Products.Any())
                {
                    var products = ProductsGenerate();

                    context.Products.AddRange(products);
                    context.SaveChanges();
                }
            }
        }

        public static List<Product> ProductsGenerate()
        {
            var random = new Random();
            var products = new List<Product>();

            var names = new[] { "Filtro de Ar", "Rodas de Alumínio", "Farol LED", "Bateria Automotiva", "Pastilhas de Freio", "Amortecedor", "Suspensão a Ar", "Banco de Couro", "Pneu Off-road", "Catalisador", "Kit de Suspensão", "Kit de Distribuição", "Radiador de Óleo", "Lâmpada Xenon", "Kit de Embreagem", "Tampa de Válvula", "Limpador de Para-brisa", "Volante Esportivo", "Teto Solar", "Chave de Roda", "Escapamento Esportivo", "Sensor de Estacionamento", "Buzina Automotiva", "Filtro de Combustível", "Luva de Câmbio", "Sistema de Navegação" };
            var descriptions = new[] { "Alta performance e durabilidade", "Design sofisticado e resistente", "Iluminação potente e eficiente", "Durabilidade garantida", "Alta qualidade e performance", "Amortecimento ideal para seu veículo", "Sistema de suspensão de última geração", "Luxo e conforto para o seu carro", "Pneus que oferecem maior aderência", "Redução de emissões e melhor desempenho", "Suspensão para performance extrema", "Peças para motor e transmissão", "Refrigeração otimizada para seu motor", "Iluminação para noites mais claras", "Peças de embreagem de alta qualidade", "Produto durável e resistente", "Peças de fácil instalação e grande eficácia", "Ajuste perfeito para seu veículo", "Conforto e estilo ao dirigir", "Facilidade de manuseio e eficiência", "Performance sonora superior", "Peças de longa durabilidade", "Alta performance e segurança", "Facilidade na instalação", "Tecnologia de ponta para seu carro" };

            for (int i = 0; i < 50; i++)
            {
                var radomName = names[random.Next(names.Length)];
                var radomPrice = Math.Round((decimal)(random.NextDouble() * 1000 + 50), 2);  // Preço entre 50 e 1050
                var radomDescription = descriptions[random.Next(descriptions.Length)];

                var product = new Product
                {
                    Name = radomName,
                    Price = radomPrice,
                    Description = radomDescription
                };

                products.Add(product);

            }


            return products;
        }
    }
}