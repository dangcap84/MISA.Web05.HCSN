using MISA.Web05.Interfaces.Repository;
using MISA.Web05.Interfaces.Service;
using MISA.Web05.Service;
using MISA.Web05.Services;
using MISA.Web05.Infrastructure.Repository;
using MISA.Web05.Core.Interfaces.Repository;
using MISA.Web05.Core.Interfaces.Service;
using MISA.Web05.Core.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Thêm cấu hình Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .WithOrigins("http://localhost:8080") //Note:  The URL must be specified without a trailing slash (/).
            //Cho phép tất cả phương thức.
            .AllowAnyMethod()
            //Cho phép tất cả header.
            .AllowAnyHeader()
            //đánh giá xem xuất xứ có được cho phép hay không.
            .SetIsOriginAllowed(_ => true)
            //máy chủ cho phép các cookie (hoặc thông tin đăng nhập của người dùng khác)
            //được đưa vào các yêu cầu nguồn gốc chéo.
            .AllowCredentials();
        });


});

// Thêm Authentication service
// gọi hàm AddCookie() để có thể dùng được chứng thực bằng Cookie
// nó sẽ thêm Authentication Scheme vào cấu hình cho ứng dụng của chúng ta
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie = new CookieBuilder
        {
            HttpOnly = true,
            Name = ".aspNetCoreDemo.Security.Cookie",
            Path = "/",
            //Cookie sẽ được gửi với các yêu cầu "cùng một trang web" và với điều hướng cấp cao nhất "trên nhiều trang web".
            SameSite = SameSiteMode.Lax,
            //Nếu URI cung cấp cookie là HTTPS, thì cookie sẽ chỉ được trả lại máy chủ trong các yêu cầu HTTPS tiếp theo.
            //Ngược lại, nếu URI cung cấp cookie là HTTP, thì cookie sẽ được trả về máy chủ trên tất cả các yêu cầu HTTP và HTTPS.
            //Giá trị này đảm bảo HTTPS cho tất cả các yêu cầu được xác thực trên các máy chủ được triển khai và cũng hỗ trợ HTTP
            //để phát triển máy chủ cục bộ và cho các máy chủ không có hỗ trợ HTTPS.
            SecurePolicy = CookieSecurePolicy.SameAsRequest
        };

        // Thời gian tồn tại 1 ngày
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        // Gia hạn cookie
        // Nếu đã chạy hết 1/2 thời gian sống
        options.SlidingExpiration = true;
    });

///Tiêm interface
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IFixedAssetRepository, FixedAssetRepository>();
builder.Services.AddScoped<IFixedAssetService, FixedAssetService>();
builder.Services.AddScoped<IFixedAssetCategoryRepository, FixedAssetCategoryRepository>();
builder.Services.AddScoped<IFixedAssetCategoryService, FixedAssetCategoryService>();
builder.Services.AddScoped<ILicenseService, LicenseService>();
builder.Services.AddScoped<ILicenseRepository, LicenseRepository>();
builder.Services.AddScoped<ILicenseDetailService, LicenseDetailService>();
builder.Services.AddScoped<ILicenseDetailRepository, LicenseDetailRepository>();
builder.Services.AddScoped<ILicenseAddService, LicenseAddService>();
builder.Services.AddScoped<ILicenseAddRepository, LicenseAddRepository>();
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Cấu hình lại với AllowAll
app.UseCors("AllowAll");
//Xác thực
app.UseAuthentication();
//Cấp quyền
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapControllers();

app.Run();
