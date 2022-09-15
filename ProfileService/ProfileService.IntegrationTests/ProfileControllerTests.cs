using Newtonsoft.Json;
using ProfileService.Dto;
using ProfileService.Model;
using ProfileService.Repository;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProfileService.IntegrationTests
{
    public class ProfileControllerTests : IClassFixture<IntegrationWebApplicationFactory<Program, AppDbContext>>
    {
        private readonly IntegrationWebApplicationFactory<Program, AppDbContext> _factory;
        private readonly HttpClient _client;

        public ProfileControllerTests(IntegrationWebApplicationFactory<Program, AppDbContext> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        private static readonly string schemaName = "profile";
        private static readonly string tableName = "Profiles";
        private static readonly Guid id = Guid.NewGuid();
        private static readonly Guid userId = Guid.NewGuid();
        private static readonly bool profilePublic = true;
        private static readonly string name = "John";
        private static readonly string surname = "Smith";
        private static readonly string username = "johnsmith";
        private static readonly string email = "email@example.com";
        private static readonly string phone = "1234567890";
        private static readonly string gender = "Female";
        private static readonly DateTime dateOfBirth = DateTime.Now;
        private static readonly string biography = "bio";
        private static readonly string picture = "base64";
        
        private static readonly string query = "Joh";

        [Fact]
        public async Task Create_CorrectData_ProfileResponse()
        {
            // Given
            ProfileRequest profileRequest = new ProfileRequest()
            {
                Public = profilePublic,
                Name = name,
                Surname = surname,
                Username = username,
                Email = email,
                Phone = phone,
                Gender = gender,
                DateOfBirth = dateOfBirth,
                Biography = biography,
                Picture = picture
            };
            var requestContent = new StringContent(JsonConvert.SerializeObject(profileRequest), Encoding.UTF8, "application/json");

            // When
            var response = await _client.PostAsync("/api/profile", requestContent);

            // Then
            response.EnsureSuccessStatusCode();
            var responseContentString = await response.Content.ReadAsStringAsync();
            var responseContentObject = JsonConvert.DeserializeObject<ProfileResponse>(responseContentString);
            Assert.NotNull(responseContentObject);
            Assert.Equal(profilePublic, responseContentObject.Public);
            Assert.Equal(name, responseContentObject.Name);
            Assert.Equal(surname, responseContentObject.Surname);
            Assert.Equal(username, responseContentObject.Username);
            Assert.Equal(email, responseContentObject.Email);
            Assert.Equal(phone, responseContentObject.Phone);
            Assert.Equal(gender, responseContentObject.Gender);
            Assert.Equal(dateOfBirth, responseContentObject.DateOfBirth);
            Assert.Equal(biography, responseContentObject.Biography);
            Assert.Equal(1L, _factory.CountTableRows(schemaName, tableName));

            // Rollback
            _factory.DeleteById(schemaName, tableName, responseContentObject.Id);
        }

        [Fact]
        public async void GetByPublic_isPublic_IEnumerableProfileResponseSimple()
        {
            // Given
            Profile profile = new Profile()
            {
                Id = id,
                Public = profilePublic,
                Name = name,
                Surname = surname,
                Username = username,
                Email = email,
                Phone = phone,
                Gender = gender,
                DateOfBirth = dateOfBirth,
                Biography = biography
            };
            _factory.Insert(schemaName, tableName, profile);

            // When
            var response = await _client.GetAsync("/api/profile?isPublic=true");

            // Then
            response.EnsureSuccessStatusCode();
            var responseContentString = await response.Content.ReadAsStringAsync();
            var responseContentObject = JsonConvert.DeserializeObject<List<ProfileSimpleResponse>>(responseContentString);
            Assert.NotNull(responseContentObject);
            Assert.Single(responseContentObject);
            var responseObject = responseContentObject[0];
            Assert.Equal(id, responseObject.Id);
            Assert.Equal(name, responseObject.Name);
            Assert.Equal(surname, responseObject.Surname);
            Assert.Equal(username, responseObject.Username);
            Assert.Null(responseObject.Picture);

            // Rollback
            _factory.DeleteById(schemaName, tableName, id);
        }

        [Fact]
        public async void SearchProfiles_IsPublicAndNameContains_IEnumerableProfileResponseSimple()
        {
            // Given
            Profile profile = new Profile()
            {
                Id = id,
                Public = profilePublic,
                Name = name,
                Surname = surname,
                Username = username,
                Email = email,
                Phone = phone,
                Gender = gender,
                DateOfBirth = dateOfBirth,
                Biography = biography
            };
            _factory.Insert(schemaName, tableName, profile);

            // When

            var response = await _client.GetAsync("/api/profile?isPublic=true&query=" + query);

            // Then
            response.EnsureSuccessStatusCode();
            var responseContentString = await response.Content.ReadAsStringAsync();
            var responseContentObject = JsonConvert.DeserializeObject<List<ProfileSimpleResponse>>(responseContentString);
            Assert.NotNull(responseContentObject);
            Assert.Single(responseContentObject);
            var responseObject = responseContentObject[0];
            Assert.Equal(id, responseObject.Id);
            Assert.Equal(name, responseObject.Name);
            Assert.Equal(surname, responseObject.Surname);
            Assert.Equal(username, responseObject.Username);
            Assert.Null(responseObject.Picture);
            Assert.Contains(query, responseObject.Name);

            // Rollback
            _factory.DeleteById(schemaName, tableName, id);
        }
    }
}
