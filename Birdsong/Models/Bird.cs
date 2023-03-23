using System.Text.Json;

namespace Birdsong.Models
{
    public class Bird
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Description { get; set; }
        public string Image { get ; set; }
        public string Audio { get; set; }

        public int[] Ratings { get; set; }

        public override string ToString() => JsonSerializer.Serialize<Bird>(this);
    }
}
