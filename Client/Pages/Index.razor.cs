using Microsoft.AspNetCore.Components;

namespace News.Client.Pages
{
    public partial class Index
    {
        [Inject]
        NavigationManager? NavigationManager { get; set; }

        public Dictionary<string, object> imageAttribute { get; set; } = new Dictionary<string, object>
    {
        {"src","img/flappy.png"},
        {"alt","No image found"}
    };

        public string FontColor { get; set; } = "color:green";

        public void GoToCounterPage()
        {
            NavigationManager!.NavigateTo("counter");
        }
    }
}
