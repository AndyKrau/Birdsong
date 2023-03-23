using Birdsong.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Birdsong.Services
{
    public class JsonFileProductService
    {
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "birdscollection.json"); }
        }

        public IEnumerable<Bird> GetBirds()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Bird[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }

        public void AddRating (int birdId, int rating)
        {
            //получаем список птиц
            IEnumerable<Bird> birds = GetBirds();
            //находим по Id совпадение по birdID, если поле null добавляем рейтинг,
            //если нет конвертируем массив в список и добавляем оценку,
            // далее помещаем новый лист с оценками обратно в массив  
            var query = birds.First(x=> x.Id == birdId);

            if (query.Ratings == null)
            {
                query.Ratings = new int[] { rating };
            }
            else
            {
                var temporaryRatings = query.Ratings.ToList();
                temporaryRatings.Add(rating);
                query.Ratings = temporaryRatings.ToArray();
            }

            //Запись изменений в файл
            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Bird>>(new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    birds
                ); 
            }
        }
    }
}
