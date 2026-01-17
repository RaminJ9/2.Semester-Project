namespace DAM_1
{
    internal class Product
    {
        public string Filename { get; set; }
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Filename: {Filename}, Tags: {Tag}";
        }
    }
}
