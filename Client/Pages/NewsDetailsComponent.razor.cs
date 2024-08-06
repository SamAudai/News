using Microsoft.AspNetCore.Components;
using News.Client.Services;
using News.Shared.Models;

namespace News.Client.Pages
{
    public partial class NewsDetailsComponent
    {
        NewsList newsList = new NewsList();
        private List<Comment> _comments = new List<Comment>();
        private Comment _comment = new Comment();

        [Inject]
        public IMainService<NewsList>? _mainService { get; set; }
        [Inject]
        public IMainService<Comment>? _commentService { get; set; }

        [Parameter]
        public string? id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            newsList = await _mainService!.GetData($"api/NewsLists/GetNewsById/ {id}")!;
            _comments = await _commentService!.GetAll($"api/Comments/GetAllComments/{id}")!;
        }

        private async Task AddComment()
        {
            _comment.newsListid = int.Parse(id);
            await _commentService.AddData(_comment, "api/Comments/AddComment");
            _comment = new Comment(); //reset _comment variable
            _comments = await _commentService!.GetAll($"api/Comments/GetAllComments/{id}")!;
        }
    }
}
