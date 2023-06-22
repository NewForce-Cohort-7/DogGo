namespace DogGo.Models
{
    public class Neighborhood
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Owner Owner { get; set; }
    }
}