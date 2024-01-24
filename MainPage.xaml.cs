using Newtonsoft.Json;
namespace MartinZumarragaDogApi
{
	public partial class MainPage : ContentPage
	{
		private const string apiUrl = "https://dog.ceo/api/breeds/list";

		public MainPage()
		{
			InitializeComponent();
		}

		private async void Button_Clicked(object sender, EventArgs e)
		{
			HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.GetAsync(apiUrl);

			if (response.IsSuccessStatusCode)
			{
				string jsonResponse = await response.Content.ReadAsStringAsync();
				List<string> breeds = ParseBreeds(jsonResponse);

				Random random = new Random();
				int randomIndex = random.Next(0, breeds.Count);
				string randomBreed = breeds[randomIndex];

				string breedImageUrl = $"https://dog.ceo/api/breed/{randomBreed}/images/random";
				HttpResponseMessage imageResponse = await client.GetAsync(breedImageUrl);

				if (imageResponse.IsSuccessStatusCode)
				{
					string imageJsonResponse = await imageResponse.Content.ReadAsStringAsync();
					dynamic imageJson = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(imageJsonResponse);
					string imageUrl = imageJson.message;


					BreedsLabel.Text = randomBreed;

					Layout.Children.Clear();


					Image dogImage = new Image { Source = imageUrl };

					Layout.Children.Add(dogImage);
				}
				else
				{
					BreedsLabel.Text = "Failed to fetch dog image";
				}
			}
			else
			{
				BreedsLabel.Text = "Failed to fetch dog breeds";
			}
		}

		private async void SearchButton_Clicked(object sender, EventArgs e)
		{
			string breedName = BreedNameEntry.Text.Trim();

			if (!string.IsNullOrEmpty(breedName))
			{
				string breedImageUrl = $"https://dog.ceo/api/breed/{breedName}/images/random";
				HttpClient client = new HttpClient();
				HttpResponseMessage imageResponse = await client.GetAsync(breedImageUrl);

				if (imageResponse.IsSuccessStatusCode)
				{
					string imageJsonResponse = await imageResponse.Content.ReadAsStringAsync();
					dynamic imageJson = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(imageJsonResponse);
					string imageUrl = imageJson.message;


					BreedsLabel.Text = breedName;


					Layout.Children.Clear();

					Image dogImage = new Image { Source = imageUrl };
					Layout.Children.Add(dogImage);
				}
				else
				{
					BreedsLabel.Text = $"Breed '{breedName}' not found";
					Layout.Children.Clear();
				}
			}
		}
		private List<string> ParseBreeds(string jsonResponse)
		{
			var json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonResponse);
			var breeds = new List<string>();

			foreach (var breed in json.message)
			{
				breeds.Add(breed.ToString());
			}

			return breeds;
		}
	}
}
