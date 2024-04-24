using RestSharp;
using RestSharp.Authenticators;
using System.Text.Json;
using System.Net;
using StorySpoilApiTests.Models;

namespace StorySpoilApiTests
{
    public class StorySpoilApiTests
    {
        private RestClient client;
        private static string StoryId;

        [OneTimeSetUp]
        public void Setup()
        {
            //get authentication
            string accessToken = GetAccessToken("lucky88", "123456");
            var restOptions = new RestClientOptions("https://d5wfqm7y6yb3q.cloudfront.net")
            {
                Authenticator = new JwtAuthenticator(accessToken),
            };

            //get client
            this.client = new RestClient(restOptions);
        }

        private string GetAccessToken(string username, string password)
        {
            var authClient = new RestClient("https://d5wfqm7y6yb3q.cloudfront.net");

            var authRequest = new RestRequest("/api/User/Authentication", Method.Post);
            authRequest.AddJsonBody(
            new AuthenticationRequest
            {
                Username = username,
                Password = password
            });

            var response = authClient.Execute(authRequest);

            if (response.IsSuccessStatusCode)
            {
                var content = JsonSerializer.Deserialize<AuthenticationResponse>(response.Content);
                var accessToken = content.AccessToken;
                return accessToken;
            }
            else
            {
                throw new InvalidOperationException("Authentication failed");
            }
        }

        [Test, Order(1)]
        public void CreateNewStory_WithRequiredFields_ShouldSucceed()
        {
            // Arrange
            var newStory = new StoryDTO
            {
                Title = "Test Story",
                Description = "Test Story Description"
            };

            // Act
            var request = new RestRequest("/api/Story/Create", Method.Post)
                .AddJsonBody(newStory);
            var response = client.Execute(request);
            var createdStory = JsonSerializer.Deserialize<ApiResponseDTO>(response.Content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.IsNotNull(createdStory.StoryId);
            Assert.That(createdStory.Msg, Is.EqualTo("Successfully created!"));

            StoryId = createdStory.StoryId; // Store the StoryId for later use
        }

        [Test, Order(2)]
        public void EditCreatedStory_WithCorrectdata_ShouldSucceed()
        {
            // Arrange
            var editedStory = new StoryDTO
            {
                Title = "Edited Test Story",
                Description = "Edited Test Story Description"
            };

            // Act
            var request = new RestRequest($"/api/Story/Edit/{StoryId}", Method.Put)
                .AddJsonBody(editedStory);
            var response = client.Execute(request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var editedStoryResponse = JsonSerializer.Deserialize<ApiResponseDTO>(response.Content);

            Assert.That(editedStoryResponse.Msg, Is.EqualTo("Successfully edited"));
        }

        [Test, Order(3)]
        public void DeleteCreatedStory_WithCorrectdata_ShouldSucceed()
        {
            // Act
            var request = new RestRequest($"/api/Story/Delete/{StoryId}", Method.Delete);
            var response = client.Execute(request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var deletedStoryResponse = JsonSerializer.Deserialize<ApiResponseDTO>(response.Content);

            Assert.That(deletedStoryResponse.Msg, Is.EqualTo("Deleted successfully!"));
        }

        [Test, Order(4)]
        public void CreateNewStory_WithoutRequiredFields_Shouldfail()
        {
            // Arrange
            var newStory = new StoryDTO
            {
                Title = "Test Story"
            };

            // Act
            var request = new RestRequest("/api/Story/Create", Method.Post)
                .AddJsonBody(newStory);
            var response = client.Execute(request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test, Order(5)]
        public void EditNonExistingStory_ShouldFail()
        {
            // Arrange
            var editedStory = new StoryDTO
            {
                Title = "Edited Test Story",
                Description = "Edited Test Story Description"
            };

            // Act
            var request = new RestRequest("/api/Story/Edit/123456", Method.Put)
                .AddJsonBody(editedStory);
            var response = client.Execute(request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

            var data = JsonSerializer.Deserialize<ApiResponseDTO>(response.Content);

            Assert.That(data.Msg, Is.EqualTo("No spoilers..."));
        }

        [Test, Order(6)]
        public void DeleteNonExistingStory_ShouldFail()
        {
            // Act
            var request = new RestRequest("/api/Story/Delete/XASSWQ", Method.Delete);
            var response = client.Execute(request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            var data = JsonSerializer.Deserialize<ApiResponseDTO>(response.Content);

            Assert.That(data.Msg, Is.EqualTo("Unable to delete this story spoiler!"));
        }

    }
}
