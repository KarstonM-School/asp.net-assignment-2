var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Welcome to the Library Management System!");
app.MapControllers();

app.Run();
