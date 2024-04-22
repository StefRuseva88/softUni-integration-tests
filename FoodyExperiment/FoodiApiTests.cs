using FoodyExperiment.Models;
using RestSharp;
using System.Text.Json;
using RestSharp.Authenticators;
using System.Net;

namespace FoodyExperiment
{
    public class FoodiApiTests
    {

        private RestClient client;
        private static string foodId;

        [OneTimeSetUp]
        public void Setup()
        {
            //get auth
            string accessToken = GetAccessToken("sharenatazlatka", "123456");
            var restOptions = new RestClientOptions("http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:86")
            {
                Authenticator = new JwtAuthenticator(accessToken),
            };

            //get client
            this.client = new RestClient(restOptions);
        }

        private string GetAccessToken(string username, string password)
        {
            var authClient = new RestClient("http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:86");

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
        public void CreateNewFood_WithRequiredFields_ShouldSucceed()
        {
            // Arrange
            var newFood = new FoodDto
            {
                Name = "Test Food",
                Description = "Test Food Description"
            };

            // Act
            var request = new RestRequest("/api/Food/Create", Method.Post)
                .AddJsonBody(newFood);
            var response = client.Execute(request);
            var createdFood = JsonSerializer.Deserialize<ApiResponseDto>(response.Content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created)); //Status code cretaed is 201
            Assert.IsNotNull(createdFood.FoodId);

            foodId = createdFood.FoodId; // Store the foodId for later use
        }

        [Test, Order(2)]
        public void EditCreatedFood_WithCorrectData_ShouldSucceed()
        {
            // Arrange
            var editedFood = new FoodDto
            {
                Name = "Edited Test Food",
                Description = "Edited Test Food Description"
            };

            // Act
            var request = new RestRequest($"/api/Food/Edit/{foodId}", Method.Patch)
            .AddJsonBody(new[]
            { new 

                {   path = "/name",
                    op = "replace",
                    value = "edited food"
                }
            });
            var response = client.Execute(request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var data = JsonSerializer.Deserialize<ApiResponseDto>(response.Content);

            Assert.That(data.Message, Is.EqualTo("Successfully edited"));
        }

        [Test, Order(3)]
        public void GetAllFoods_WithCorrectdata_ShouldSucceed()
        {
            // Arrange
            var request = new RestRequest("/api/Food/All", Method.Get);

            // Act
            var response = client.Execute(request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var data = JsonSerializer.Deserialize<List<ApiResponseDto>>(response.Content);

            Assert.That(response.Content, Is.Not.Empty);

        }

        [Test, Order(4)]
        public void DeleteCreatedFood_WithCorrectData_ShouldSucceed()
        {

            // Arrange
            var request = new RestRequest($"/api/Food/Delete/{foodId}", Method.Delete);

            // Act
            var response = client.Execute(request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var data = JsonSerializer.Deserialize<ApiResponseDto>(response.Content);

            Assert.That(data.Message, Is.EqualTo("Deleted successfully!"));
        }

        [Test, Order(5)]
        public void CreateFood_WithoutRequiredFields_ShouldFail()
        {
            // Arrange: 
            var newFood = new FoodDto();

            // Act
            var request = new RestRequest("/api/Food/Create", Method.Post)
                .AddJsonBody(newFood);
            var response = client.Execute(request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

        }

        [Test, Order(6)]
        public void Edit_NonExistingFood_ShouldFail()
        {
            // Arrange
            var nonExistingFoodId = "non-existing-food-id";

            // Act
            var request = new RestRequest($"/api/Food/Edit/{nonExistingFoodId}", Method.Patch)
            .AddJsonBody(new[]
            {
                new
                {
                    path = "/name",
                    op = "replace",
                    value = "EditedFood",
                },

            });
            var response = client.Execute(request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

            var data = JsonSerializer.Deserialize<ApiResponseDto>(response.Content);

            Assert.That(data.Message, Is.EqualTo("No food revues..."));
        }

        [Test, Order(7)]
        public void Delete_NonExistingFood_ShouldFail()
        {
            // Arrange
            var request = new RestRequest("/api/Food/Delete/XASDAXAS", Method.Delete);

            // Act
            var response = this.client.Execute(request);

            // Assert

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

            var content = JsonSerializer.Deserialize<ApiResponseDto>(response.Content);

            Assert.That(content.Message, Is.EqualTo("No food revues..."));

        }
    }
}