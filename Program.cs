var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Middleware 1: يسجل الوقت
app.Use(async (context, next) =>
{
    Console.WriteLine($"[1] Request Time: {DateTime.Now}");
    await next(); // مرر إلى الميدلوير التالي
    Console.WriteLine("[1] Response Sent");
});

// Middleware 2: يتحقق من وجود مفتاح في الهيدر
app.Use(async (context, next) =>
{
    if (!context.Request.Headers.ContainsKey("X-My-Key"))
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Missing X-My-Key header");
    }
    else
    {
        Console.WriteLine("[2] Header Check Passed");
        await next();
    }
});

// Middleware 3: معالجة النهاية الأولى
app.Map("/hello", appBuilder =>
{
    appBuilder.Run(async context =>
    {
        await context.Response.WriteAsync("Hello from /hello endpoint");
    });
});

// Middleware 4: معالجة النهاية الثانية
app.Map("/time", appBuilder =>
{
    appBuilder.Run(async context =>
    {
        await context.Response.WriteAsync($"Server time is {DateTime.Now}");
    });
});

// Middleware 5: النهاية الافتراضية
app.Run(async context =>
{
    await context.Response.WriteAsync("Welcome to the Middleware Demo App");
});

app.Run();
