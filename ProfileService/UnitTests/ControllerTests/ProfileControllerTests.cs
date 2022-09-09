using System;
using Xunit;
using Moq;
using AutoMapper;
using ProfileService.Service.Interface;
using Profile = ProfileService.Model.Profile;
using ProfileService.Controllers;
using ProfileService.Dto;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Service.Interface.Exceptions;
using OpenTracing;

namespace ProfileService.UnitTests.ControllerTests
{
    public class ProfileControllerTests
    {
        
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

        private static Profile savedProfile;
        private static ProfileResponse profileResponse;

        private static Mock<IProfileService> mockService = new Mock<IProfileService>();
        private static Mock<IMapper> mockMapper = new Mock<IMapper>();
        private static Mock<ITracer> mockTracer = new Mock<ITracer>();

        ProfileController profileController = new ProfileController(mockService.Object, mockMapper.Object, mockTracer.Object);

        private static void SetUp()
        {
            savedProfile = new Profile()
            {
                Id = id,
                UserId = userId,
                Public = profilePublic,
                Name = name,
                Surname = surname,
                Username = username,
                Email = email,
                Phone = phone,
                Gender = gender,
                DateOfBirth = dateOfBirth,
                Biography = biography,
            };

            profileResponse = new ProfileResponse()
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
                Biography = biography,
            };
        }

        [Fact]
        public async void GetProfile_ExistingId_Profile()
        {
            SetUp();

            mockService
                .Setup(x => x.GetById(id))
                .ReturnsAsync(savedProfile);

            mockMapper
               .Setup(x => x.Map<ProfileResponse>(savedProfile))
               .Returns((Profile source) =>
               {
                   return profileResponse;
               });

            var response = await profileController.GetProfile(id);

            var actionResult = Assert.IsType<OkObjectResult>(response);
            var actionValue = Assert.IsType<ProfileResponse>(actionResult.Value);
            Assert.Equal(profileResponse.Id, actionValue.Id);
            Assert.Equal(profileResponse.Public, actionValue.Public);
            Assert.Equal(profileResponse.Name, actionValue.Name);
            Assert.Equal(profileResponse.Surname, actionValue.Surname);
            Assert.Equal(profileResponse.Username, actionValue.Username);
            Assert.Equal(profileResponse.Email, actionValue.Email);
            Assert.Equal(profileResponse.Phone, actionValue.Phone);
            Assert.Equal(profileResponse.Gender, actionValue.Gender);
            Assert.Equal(profileResponse.DateOfBirth, actionValue.DateOfBirth);
            Assert.Equal(profileResponse.Biography, actionValue.Biography);
        }

        [Fact]
        public async void GetProfile_NonExistingId_EntityNotFoundException()
        {
            SetUp();

            var exception = new EntityNotFoundException(typeof(Profile), "id");
            Guid invalidId = Guid.NewGuid();

            mockService
                .Setup(service => service.GetById(invalidId))
                .ThrowsAsync(exception);

            try
            {
                var response = await profileController.GetProfile(invalidId);
            }
            catch (Exception ex)
            {
                var thrownException = Assert.IsType<EntityNotFoundException>(ex);
            }
        }
    }
}
