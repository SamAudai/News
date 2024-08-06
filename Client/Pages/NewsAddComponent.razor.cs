using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using News.Client.Services;
using News.Shared.DTOs;
using News.Shared.Models;

namespace News.Client.Pages
{
    public partial class NewsAddComponent
    {
        public NewsList_DTO _newsListDto = new NewsList_DTO();
        public List<Category> _categories = new List<Category>();

        [Inject]
        public IMainService<NewsList_DTO>? _mainService { get; set; }

        [Inject]
        public IMainService<Category>? _categoryService { get; set; }

        [Inject]
        public NavigationManager? _navigationManager { get; set; }

        IBrowserFile? _browserFile { get; set; }
        string imgUrl = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            _categories = await _categoryService!.GetAll("api/Categories/GetAllCategories");
        }

        public async Task Create()
        {
            _newsListDto.image = _browserFile!.Name;
            using(var ms = new MemoryStream())
            {
               await _browserFile.OpenReadStream().CopyToAsync(ms);
               _newsListDto.newImage = ms.ToArray();
                ms.Dispose();
            }
            await _mainService!.AddData(_newsListDto, "api/NewsLists/AddNews")!;
            _navigationManager!.NavigateTo("/");
        }

        private async Task FileLoad(InputFileChangeEventArgs e)
        {
            _browserFile = e.File;
            var buffer = new byte[_browserFile.Size];
            await _browserFile.OpenReadStream().ReadAsync(buffer, 0, buffer.Length);
            imgUrl = $"data:{_browserFile.ContentType}; Base64, {Convert.ToBase64String(buffer)}";
            this.StateHasChanged();
        }
    }
}
