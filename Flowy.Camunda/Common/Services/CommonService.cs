using System.Text;
using Flowy.Camunda.Auth.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Flowy.Camunda.Common.Services;

public class CommonService  {

  public IAuthService AuthService { get; private set;}
  public string BaseApiUrl { get; set; }

  public CommonService(IAuthService ias) {
    AuthService = ias;
    BaseApiUrl = "";
  }

  public HttpClient GetHttpClient() {
    HttpClient client = new() { };
    client.DefaultRequestHeaders.Authorization = AuthService.GetAuthenticationHeaderValue();
    return client;
  }

  public string GetCompleteUrl(string part){
    return BaseApiUrl + part;
  }

  public T? Post<T>(string url, object body) {
    HttpClient client = GetHttpClient();
    string jsonBody = JsonConvert.SerializeObject(body);
    StringContent requestContent = new (jsonBody, Encoding.UTF8, "application/json");
    HttpResponseMessage response = client.PostAsync(url, requestContent).Result;
    return DecodingResult<T>(response);
  }

  public T? Get<T>(string url) {
    HttpClient client = GetHttpClient();
    HttpResponseMessage response = client.GetAsync(url).Result;
    return DecodingResult<T>(response);
  }

  public T? Delete<T>(string url) {
    HttpClient client = GetHttpClient();
    HttpResponseMessage response = client.DeleteAsync(url).Result;
    return DecodingResult<T>(response);
  }

  public T? Patch<T>(string url, object? body = null) {
    HttpClient client = GetHttpClient();
    string jsonBody = JsonConvert.SerializeObject(body);
    StringContent requestContent = new (jsonBody, Encoding.UTF8, "application/json");
    HttpResponseMessage response = client.PatchAsync(url, requestContent).Result;
    return DecodingResult<T>(response);
  }

  private T? DecodingResult<T>(HttpResponseMessage response){
    string stringResult = response.Content.ReadAsStringAsync().Result;
    if(response.StatusCode == System.Net.HttpStatusCode.OK) {
      if(typeof(T) == typeof(string)){ return (T)Convert.ChangeType(stringResult, typeof(T)); }
      return JsonConvert.DeserializeObject<T>(stringResult);
    } else if(response.StatusCode == System.Net.HttpStatusCode.NoContent) {
      return default;
    }else if(response.StatusCode == System.Net.HttpStatusCode.NotFound) {
      return default;
    } 

    string msg = response.StatusCode.ToString();
    JObject? res = JsonConvert.DeserializeObject<JObject>(stringResult);
    if(res != null) { 
      JToken? message = res.GetValue("message");
      if(message != null) { msg += " - " + message.ToString(); }
    }
    throw new Exception(msg);
  }
}

