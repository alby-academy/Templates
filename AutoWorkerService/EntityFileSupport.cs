namespace AutoWorkerService
{
    public class EntityFileSupport
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return $"{Name} {Price}\n";
        }
    }
}
