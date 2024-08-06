using Microsoft.AspNetCore.Components;
using News.Client.Services;
using News.Shared.Models;

namespace News.Client.Pages
{
    public partial class CategoryNew
    {
        public Category category = new Category();

        [Inject]
        public IMainService<Category>? _mainService { get; set; }

        [Inject]
        NavigationManager? _navigationManager { get; set; }

        [Parameter]
        public string message { get; set; } = "";

        public async Task Create()
        {
            try
            {
                await _mainService!.AddData(category, "api/Categories/AddCategory")!;
                _navigationManager!.NavigateTo("categories");
            }
            catch(Exception ex) 
            {
                //if(ex.Message.Contains("Category name already exists")) 
                //{
                //    message = "Category name already exists!";
                //}
                message = ex.Message;
            }
        }

        public void Back()
        {
            _navigationManager!.NavigateTo("categories");
        }
    }
}
