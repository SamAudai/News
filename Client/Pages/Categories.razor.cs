using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using News.Client.Services;
using News.Shared.Models;

namespace News.Client.Pages
{
    public partial class Categories
    {
        [Inject]
        public IMainService<Category>? _categoryService { get; set; }

        List<Category>? _categories = null;

        [Inject]
        public IJSRuntime? js { get; set; }

        [Parameter]
        public string message { get; set; } = "";

        protected override async Task OnInitializedAsync()
        {
            //_categories = new List<Category>();
            //_categories = await _categoryService!.GetAll("api/Categories/GetAllCategories");
            await Task.Run(GetAllData);
        }

        private async Task GetAllData()
        {
            try
            {
                //System.Threading.Thread.Sleep(5000);
                _categories = new List<Category>();
                _categories = await _categoryService!.GetAll("api/Categories/GetAllCategories");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task GetErrors()
        {
            try
            {
                _categories = new List<Category>();
                _categories = await _categoryService!.GetAll("api/Categories/GetCategoriesError");
            }
            catch(Exception ex) 
            { 
                message = ex.Message;
            }
        }

        private async Task GetUnauthorizedErrors()
        {
            try
            {
                _categories = new List<Category>();
                _categories = await _categoryService!.GetAll("api/Categories/GetCategoriesUnauthorized");
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
        }

        public async Task Delete(int id)
        {
            var cat = _categories!.FirstOrDefault(c => c.id == id)!;
            var confirmed = await js!.InvokeAsync<bool>("confirm", "Delete Category?");
            if (confirmed)
            {
                await _categoryService!.DeleteData($"api/Categories/DeleteCategory/{id}");
                _categories = await _categoryService!.GetAll("api/Categories/GetAllCategories");
            }

        }
    }
}
