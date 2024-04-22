using IdeaExperiment.Models;
using RestSharp;
using RestSharp.Authenticators;
using System.Text.Json;
using System.Net;
using NUnit.Framework;

namespace IdeaExperiment
{
    public class IdeaTests
    {

        private RestClient client;
        private static string lastIdeaId;

        [OneTimeSetUp]
        public void Setup()
        {
            //get auth
            string accessToken = GetAccessToken("sharenatazlatka@example.com", "123456");
            var restOptions = new RestClientOptions("http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:84")
            {
                Authenticator = new JwtAuthenticator(accessToken),
            };

            //get client
            this.client = new RestClient(restOptions);
        }

        private string GetAccessToken(string email, string password)
        {
            var authClient = new RestClient("http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:84");

            var authRequest = new RestRequest("/api/User/Authentication", Method.Post);
            authRequest.AddJsonBody(
            new AuthenticationRequest
            {
                Email = email,
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
        public void CreateNewIdea_WithRequiredFields_ShouldSucceed()
        {
            //Arrange
            var request = new RestRequest("/api/Idea/Create", Method.Post);
            request.AddJsonBody(
            new
            {
                title = "Test Idea",
                description = "Test Description"
            });

            //Act
            var response = this.client.Execute(request);
            var content = JsonSerializer.Deserialize<ApiResponseDTO>(response.Content);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(content.Message, Is.EqualTo("Successfully created!"));
        }

        [Test, Order(2)]

        public void GetAllIdeas_WithCorrectData_ShouldSucceed()
        {
            //Arrange
            var request = new RestRequest("/api/Idea/All");

            //Act
            var response = this.client.Execute(request, Method.Get);
            var responseDataArray = JsonSerializer.Deserialize<ApiResponseDTO[]>(response.Content);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseDataArray.Length, Is.GreaterThan(0));

            lastIdeaId = responseDataArray[responseDataArray.Length - 1].IdeaId;
        }

        [Test, Order(3)] 
        public void Edit_LastCreatedIdea_ShouldSucceed()
        {
            //Arrange
            var requestData = new IdeaDTO()
            {
                Title = "EditedTestTitle",
                Description = "TestDescription with edits",
            };
            var request = new RestRequest("/api/Idea/Edit");
            request.AddQueryParameter("ideaId", lastIdeaId);
            request.AddJsonBody(requestData);

            //Act
            var response = this.client.Execute(request, Method.Put);
            var responseData = JsonSerializer.Deserialize<ApiResponseDTO>(response.Content);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseData.Message, Is.EqualTo("Edited successfully"));
        }

        [Test, Order(4)]
        public void Delete_LastCreatedIdea_ShouldSucceed()
        {
            //Arrange            
            var request = new RestRequest("/api/Idea/Delete");
            request.AddQueryParameter("ideaId", lastIdeaId);

            //Act
            var response = this.client.Execute(request, Method.Delete);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Does.Contain("The idea is deleted!"));
        }

        [Test, Order(5)]
        public void CreateIdea_WithoutRequiredFields_ShouldFail()
        {
            //Arrange
            var requestData = new IdeaDTO()
            {
                Title = "TestTitle",
            };
            var request = new RestRequest("/api/Idea/Create");
            request.AddJsonBody(requestData);

            //Act
            var response = this.client.Execute(request, Method.Post);
            var responseData = JsonSerializer.Deserialize<ApiResponseDTO>(response.Content);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test, Order(6)]
        public void Edit_NonExistingIdea_ShouldFail()
        {
            //Arrange
            var requestData = new IdeaDTO()
            {
                Title = "EditedTestTitle",
                Description = "TestDescription with edits",
            };
            var request = new RestRequest("/api/Idea/Edit");
            request.AddQueryParameter("ideaId", 998877);
            request.AddJsonBody(requestData);

            //Act
            var response = client.Execute(request, Method.Put);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(response.Content, Does.Contain("There is no such idea!"));
        }

        [Test, Order(7)]

        public void Delete_NonExistingIdea_ShouldFail()
        {
            //Arrange
            var request = new RestRequest("/api/Idea/Delete");
            request.AddQueryParameter("ideaId", 998877);

            //Act
            var response = this.client.Execute(request, Method.Delete);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(response.Content, Does.Contain("There is no such idea!"));
        }
    }

}
