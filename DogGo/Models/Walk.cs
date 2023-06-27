namespace DogGo.Models
{
    public class Walk
    {
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public int WalkerId { get; set; }
        public int DogId { get; set; }
        public Walker walker { get; set; }
        public Dog dog { get; set; }
        public Owner Owner { get; set; }
    }
}
