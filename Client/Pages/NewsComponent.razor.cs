using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using News.Client.Services;
using News.Shared.Models;

namespace News.Client.Pages
{
    public partial class NewsComponent
    {
        List<NewsList> _newsLists = new List<NewsList>();
        List<Category> _categories = new List<Category>();

        [Inject]
        public IMainService<NewsList>? _mainService { get; set; }

        [Inject]
        public IMainService<Category>? _categoryService { get; set; }

        [Inject]
        public IJSRuntime? js { get; set; }

        [Parameter]
        public int CatId { get; set; } 

        protected async override Task OnInitializedAsync()
        {
            _newsLists = await _mainService.GetAll("api/NewsLists/GetAllNews");
            _categories = await _categoryService.GetAll("api/Categories/GetAllCategories");
        }

        public async Task GetNewsByCategoryId(int value)
        {
            CatId = value;
            _newsLists = new List<NewsList>();
            if(CatId == 0)
            {
                _newsLists = await _mainService.GetAll("api/NewsLists/GetAllNews");
            }
            else
            {
                _newsLists = await _mainService.GetAll($"api/NewsLists/GetNewsByCategory?id={CatId}");
            }
            
        }
    }
}
