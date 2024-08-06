using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace News.Client.Services
{
    public class MainService<T> : IMainService<T> where T : class
    {
        public HttpClient _httpClient { get; set; }
        public NavigationManager _navigationManager { get; set; }

        public MainService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public async Task<List<T>> GetAll(string apiName)
        {
            var result = await _httpClient.GetAsync(apiName);
            if (!result.IsSuccessStatusCode)
            {
                if(result.StatusCode == HttpStatusCode.NotFound)
                {
                    _navigationManager.NavigateTo("/404");
                }
                else if(result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else if (result.StatusCode == HttpStatusCode.Forbidden)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else
                {
                    _navigationManager.NavigateTo("/500");
                }
                var msg = await result.Content.ReadAsStringAsync();
                //throw new Exception(result.StatusCode.ToString());
                throw new Exception(result.ReasonPhrase + "_" + msg);
            }
            return await _httpClient.GetFromJsonAsync<List<T>>(apiName)!;
        }

        public async Task<T> AddData(T entity, string apiName)
        {
            var result = await _httpClient.PostAsJsonAsync<T>(apiName, entity);
            //return await result.Content.ReadFromJsonAsync<T>();
            if (!result.IsSuccessStatusCode)
            {
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    _navigationManager.NavigateTo("/404");
                }
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else if (result.StatusCode == HttpStatusCode.Forbidden)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else
                {
                    _navigationManager.NavigateTo("/500");
                }
                var msg = await result.Content.ReadAsStringAsync();
                //throw new Exception(result.StatusCode.ToString());
                throw new Exception(result.ReasonPhrase + "_" + msg);
            }
            return null;
        }

        public async Task<T> GetData(string apiName)
        {
            var result = await _httpClient.GetAsync(apiName);
            if (!result.IsSuccessStatusCode)
            {
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    _navigationManager.NavigateTo("/404");
                }
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else if (result.StatusCode == HttpStatusCode.Forbidden)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else
                {
                    _navigationManager.NavigateTo("/500");
                }
                var msg = await result.Content.ReadAsStringAsync();
                //throw new Exception(result.StatusCode.ToString());
                throw new Exception(result.ReasonPhrase + "_" + msg);
            }
            return await _httpClient.GetFromJsonAsync<T>(apiName);
        }

        public async Task<T> UpdateData(T entity, string apiName)
        {
            var result = await _httpClient.PutAsJsonAsync<T>(apiName, entity);
            if (!result.IsSuccessStatusCode)
            {
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    _navigationManager.NavigateTo("/404");
                }
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else if (result.StatusCode == HttpStatusCode.Forbidden)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else
                {
                    _navigationManager.NavigateTo("/500");
                }
                var msg = await result.Content.ReadAsStringAsync();
                //throw new Exception(result.StatusCode.ToString());
                throw new Exception(result.ReasonPhrase + "_" + msg);
            }
            return null;
        }

        public async Task DeleteData(string apiName)
        {
            var result = await _httpClient.DeleteAsync(apiName);
            if (!result.IsSuccessStatusCode)
            {
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    _navigationManager.NavigateTo("/404");
                }
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else if (result.StatusCode == HttpStatusCode.Forbidden)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else
                {
                    _navigationManager.NavigateTo("/500");
                }
                var msg = await result.Content.ReadAsStringAsync();
                //throw new Exception(result.StatusCode.ToString());
                throw new Exception(result.ReasonPhrase + "_" + msg);
            }            
        }
    }
}
