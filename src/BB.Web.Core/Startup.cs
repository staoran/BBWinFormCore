using System.Text.Json;
using System.Text.Json.Serialization;
using BB.Core.Filter;
using FastExpressionCompiler;
using Furion;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BB.Web.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // 指定使用 FastExpressionCompiler 作为 Mapster 的表达式树编译器
        TypeAdapterConfig.GlobalSettings.Compiler = exp => exp.CompileFast();
        // 开启目标类型映射继承
        TypeAdapterConfig.GlobalSettings.AllowImplicitDestinationInheritance = true;

        // 启用jwt认证, 并启用全局授权验证
        services.AddJwt<JwtHandler>(enableGlobalAuthorize: true);

        // 跨域
        services.AddCorsAccessor();

        services.AddControllers()
            .AddAppLocalization() // 注册多语言
            .AddJsonOptions(options =>
            {
                // 驼峰命名
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                // 时间格式化
                options.JsonSerializerOptions.Converters.AddDateFormatString("yyyy-MM-dd HH:mm:ss");
                // 忽略循环引用
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            })
            // 自定义规范化返回结果
            .AddInjectWithUnifyResult<RestfulResultProvider>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) 
            app.UseDeveloperExceptionPage();

        // NGINX 反向代理获取真实IP
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            KnownNetworks = {  },
            KnownProxies = {  }
        });

        // 配置多语言，必须在 路由注册之前
        app.UseAppLocalization();

        // 添加对状态码规范化结果的处理
        app.UseUnifyResultStatusCodes();

        app.UseHttpsRedirection();

        if(env.IsDevelopment())
            app.UseSerilogRequestLogging();    // 必须在 UseStaticFiles 和 UseRouting 之间

        app.UseRouting();

        app.UseCorsAccessor();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseInject(string.Empty);

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}