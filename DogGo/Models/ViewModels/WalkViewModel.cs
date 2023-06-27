using System.ComponentModel;

namespace DogGo.Models.ViewModels
{
    public class WalkViewModel
    {
        public Walk Walk { get; set; }
        public List<Walker> Walkers { get; set; }
        public List<Dog> Dogs { get; set; }

        [DisplayName("Dog(s)  (hold CTRL to select multiple)")]
        public List<int> SelectedDogIds { get; set; }
    }
}