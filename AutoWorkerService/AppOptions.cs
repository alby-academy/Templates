namespace AutoWorkerService
{
    public class AppOptions
    {
        private string _dest = @"D:\AcademyExercises\Auto";
        private string _source = @"D:\AcademyExercises\Auto\AutoName.txt";

        public string Dest { get => _dest; set => _dest = value; }
        public string Source { get => _source; set => _source = value; }

        //public string Dest { get; set; }
        //public string Source { get; set; }
    }
}