using Microsoft.OpenApi.Models;
using Flowy.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson(x =>
 x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( swo => {
  swo.SwaggerDoc("v1", new OpenApiInfo(){
      Version = "v1",
      Title = "Flowy BPMN Api",
      Description = "ApiRest Flowy BPMN Engine."
  });
  //var filePath = Path.Combine(AppContext.BaseDirectory, "It.Flowy.Engine.xml");
  //swo.IncludeXmlComments(filePath);
});

//builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddScoped<IProcessDefinitionService, ProcessDefinitionService>();
//builder.Services.AddScoped<IDecisionDefinitionService, DecisionDefinitionService>();

builder.Services.AddFlowyConfig(builder.Configuration).AddFlowyDependencyGroup();


// configure CORS
builder.Services.AddCors(options => {
  options.AddPolicy(name: "Default", policy => {
    policy.WithOrigins("http://localhost:4200")
    .AllowAnyHeader()
    .AllowAnyMethod();
  });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("Default");
app.MapControllers();
app.Run();