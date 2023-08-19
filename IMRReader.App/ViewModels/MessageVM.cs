namespace IMRReader.Application.ViewModels
{
    public record MessageVM
    {
        public required string Content { get; set; }

        public int MinimumDisplayTime { get; set; }

        public int MaximumDisplayTime { get; set; }

        public bool ShouldStay { get; set; }

        public MessageVM()
        {
            MinimumDisplayTime = 200;
            MaximumDisplayTime = 1000;
            ShouldStay = true;
        }
    }
}
