using LogiSysSvr.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogiSysSvr
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = CreateHostBuilder(args).Build();
            
            using (var scope = host.Services.CreateScope())
            {
                // �T�[�r�X�v���o�C�_�[�̎擾
                var services = scope.ServiceProvider;

                // �f�[�^�x�[�X�̎����}�C�O���[�V����
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();

                // �����̃��[�U�[�ƃ��[���̍쐬
                IdentityUserInitializer.Initialize(services).Wait();
            }

            host.Run();

            //CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
