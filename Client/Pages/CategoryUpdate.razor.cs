using Microsoft.AspNetCore.Components;
using News.Client.Services;
using News.Shared.Models;

namespace News.Client.Pages
{
    public partial class CategoryUpdate
    {
        Category category = new Category();

        [Inject]
        IMainService<Category>? _mainService { get; set; }

        [Parameter]
        public string id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            category = await _mainService!.GetData($"/api/Categories/GetCategoryById/{id}")!;
        }

        [Inject]
        NavigationManager? _navigationManager { get; set; }

        public async Task Update()
        {
            await _mainService!.UpdateData(category, $"/api/Categories/UpdateCategory/{id}")!;
            _navigationManager!.NavigateTo("/categories");
        }

    }
}
