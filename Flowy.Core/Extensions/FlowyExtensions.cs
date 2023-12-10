using Flowy.Camunda.Auth.Services;
using Flowy.Camunda.Operate.Services;
using Flowy.Core.Contexts;
using Flowy.Core.Services;
using Flowy.Core.Managements;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Flowy.Camunda.Zeebe.Services;
using Flowy.Camunda.Tasklist.Services;

namespace Flowy.Core.Extensions;

public static class DependencyInjections {

  public static IServiceCollection AddFlowyConfig(this IServiceCollection services, IConfiguration config)        {
    //services.Configure<PositionOptions>(config.GetSection(PositionOptions.Position));
    //services.Configure<ColorOptions>(config.GetSection(ColorOptions.Color));

    // configure database
    string? connectionString = config.GetConnectionString("Flowy");
    if (connectionString == null) { throw new Exception("No Flowy connection string in config file");}

    var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
    services.AddDbContextPool<FlowyContext>(
      options => options.UseMySql(connectionString, serverVersion)
      .LogTo(Console.WriteLine, LogLevel.Information)
      .EnableSensitiveDataLogging()
      .EnableDetailedErrors()
    );

    return services;
  }

  public static IServiceCollection AddFlowyDependencyGroup(this IServiceCollection services){
    // camunda connection
    services.AddScoped<IAuthService, AuthService>();
    services.AddScoped<IProcessDefinitionsService, ProcessDefinitionsService>();
    services.AddScoped<IProcessInstancesService, ProcessInstancesService>();
    services.AddScoped<IZeebeService, ZeebeService>();
    services.AddScoped<IVariablesService, VariablesService>();
    services.AddScoped<ITasksService, TasksService>();
    services.AddScoped<IFlowNodeInstancesService, FlowNodeInstancesService>();
    services.AddScoped<IFormsService, FormsService>();
    // flowy core
    services.AddScoped<ITenantsService, TenantsService>();
    services.AddScoped<IScopesService, ScopesService>();
    services.AddScoped<IProcessesService, ProcessesService>();
    services.AddScoped<IDraftsService, DraftsService>();
    services.AddScoped<IInstancesService, InstancesService>();
    services.AddScoped<IInteractionsService, InteractionsService>();
    // flowy management
    services.AddScoped<ITenantsManagement, TenantsManagement>();
    services.AddScoped<IScopesManagement, ScopesManagement>();
    services.AddScoped<IProcessesManagement, ProcessesManagement>();
    services.AddScoped<IDraftsManagement, DraftsManagement>();
    services.AddScoped<IProcessingManagement, ProcessingManagement>();
    services.AddScoped<IInstancesManagement, InstancesManagement>();
    services.AddScoped<IInteractionsManagement, InteractionsManagement>();

    return services;
  }
}