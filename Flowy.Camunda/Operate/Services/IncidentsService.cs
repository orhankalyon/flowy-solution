using Flowy.Camunda.Auth.Services;
using Flowy.Camunda.Operate.Models;
using Flowy.Camunda.Operate.Models.Search;

namespace Flowy.Camunda.Operate.Services;

public interface IIncidentsService {
  Results<Incident>? GetIncidents(Quary<Incident>? quary = null);
  Incident? GetIncidentByKey(long key);
}

public class IncidentsService : OperateService, IIncidentsService {

  public IncidentsService(IAuthService ias) : base(ias) {}
  
  public Results<Incident>? GetIncidents(Quary<Incident>? quary = null){
    return Post<Results<Incident>>(
      GetCompleteUrl("/incidents/search"),
      quary != null ? quary : new {}
    );
  }

  public Incident? GetIncidentByKey(long key) {
    return Get<Incident>(GetCompleteUrl("/incidents/" + key));
  }

}
