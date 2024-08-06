using Microsoft.AspNetCore.Components;

namespace News.Client.Components
{
    public partial class Home
    {
        [Parameter]
        public string name { get; set; } = "Audai";

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? imageAttribute { get; set; }

        [CascadingParameter]
        public string? color { get; set; }
    }
}
