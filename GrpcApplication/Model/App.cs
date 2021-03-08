namespace GrpcApplication.Model
{
    public class App
    {
        public App(){
        }

        public App(string appUrl)
        {
            Url = appUrl;
        }

        public string Id { get; set; }

        public string Url
        {
            get => Id != null ? "https://play.google.com/store/apps/details?id=" + Id : null;
            init => Id = value.Split("=").Length == 2 ? value.Split("=")[1] : value;
        }

        public string Name { get; set; }
        public long InstallsCount { get; set; }
    }
}