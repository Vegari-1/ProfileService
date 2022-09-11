using ProfileService.Model;
using System;
using System.Linq;
using Xunit;
using Moq;
using Profile = ProfileService.Model.Profile;
using ProfileService.Service.Interface.Exceptions;
using System.Collections.Generic;
using ProfileService.Repository.Interface;
using ProfileService.Service.Interface;

namespace ProfileService.UnitTests.ServiceTests
{
    public class ProfileServiceTests
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
        private static readonly Skill skill1 = new Skill() { Id = id, Name = "Skill1" };
        private static readonly Skill skill2 = new Skill() { Id = id, Name = "Skill2" };
        private static readonly DateTime date = DateTime.Now;
        private static readonly Education education = new Education() { Id = id, School = "School", Degree = "Degree", Field = "Field", StartDate = date, EndDate = date };
        private static readonly WorkExperience workExperience = new WorkExperience() { Id = id, Position = "Position", Company = "Company", StartDate = date, EndDate = date };

        private static Profile savedProfile;

        private static Mock<IProfileRepository> mockProfileRepo = new Mock<IProfileRepository>();
        private static Mock<IConnectionRepository> mockConnRepo = new Mock<IConnectionRepository>();
        private static Mock<IConnectionRequestRepository> mockConnReqRepo = new Mock<IConnectionRequestRepository>();
        private static Mock<IProfileSyncService> mockProfileSyncService = new Mock<IProfileSyncService>();
        private static Mock<IBlockSyncService> mockBlockSyncService = new Mock<IBlockSyncService>();

        private static Service.ProfileService profileService = new Service.ProfileService(
            mockConnReqRepo.Object, mockConnRepo.Object, mockProfileRepo.Object, 
            mockProfileSyncService.Object, mockBlockSyncService.Object);

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
                Skills = new List<Skill> { skill1, skill2 },
                Education = new List<Education> { education },
                WorkExperiences = new List<WorkExperience> { workExperience }
            };
        }

        [Fact]
        public async void GetById_ExistingId_Profile()
        {
            SetUp();

            mockProfileRepo
                .Setup(x => x.GetByIdImage(id))
                .ReturnsAsync(savedProfile);

            var response = await profileService.GetById(id);

            Assert.IsType<Profile>(response);
            Assert.Equal(savedProfile.Id, response.Id);
            Assert.Equal(savedProfile.UserId, response.UserId);
            Assert.Equal(savedProfile.Public, response.Public);
            Assert.Equal(savedProfile.Name, response.Name);
            Assert.Equal(savedProfile.Surname, response.Surname);
            Assert.Equal(savedProfile.Username, response.Username);
            Assert.Equal(savedProfile.Email, response.Email);
            Assert.Equal(savedProfile.Phone, response.Phone);
            Assert.Equal(savedProfile.Gender, response.Gender);
            Assert.Equal(savedProfile.DateOfBirth, response.DateOfBirth);
            Assert.Equal(savedProfile.Biography, response.Biography);

            Assert.True(response.Skills.Count == 2);
            var responseSkill1 = Assert.IsType<Skill>(response.Skills.First());
            Assert.Equal(skill1.Id, responseSkill1.Id);
            Assert.Equal(skill1.Name, responseSkill1.Name);
            var responseSkill2 = Assert.IsType<Skill>(response.Skills.Last());
            Assert.Equal(skill2.Id, responseSkill2.Id);
            Assert.Equal(skill2.Name, responseSkill2.Name);

            Assert.True(response.Education.Count == 1);
            var responseEducation = Assert.IsType<Education>(response.Education.First());
            Assert.Equal(education.Id, responseEducation.Id);
            Assert.Equal(education.School, responseEducation.School);
            Assert.Equal(education.Degree, responseEducation.Degree);
            Assert.Equal(education.Field, responseEducation.Field);
            Assert.Equal(education.StartDate, responseEducation.StartDate);
            Assert.Equal(education.EndDate, responseEducation.EndDate);

            Assert.True(response.WorkExperiences.Count == 1);
            var responseWorkExperience = Assert.IsType<WorkExperience>(response.WorkExperiences.First());
            Assert.Equal(workExperience.Id, responseWorkExperience.Id);
            Assert.Equal(workExperience.Position, responseWorkExperience.Position);
            Assert.Equal(workExperience.Company, responseWorkExperience.Company);
            Assert.Equal(workExperience.StartDate, responseWorkExperience.StartDate);
            Assert.Equal(workExperience.EndDate, responseWorkExperience.EndDate);
        }

        [Fact]
        public async void GetProfile_NonExistingId_EntityNotFoundException()
        {
            SetUp();

            var exception = new EntityNotFoundException(typeof(Profile), "id");
            Guid invalidId = Guid.NewGuid();

            mockProfileRepo
               .Setup(repository => repository.GetById(invalidId))
               .ReturnsAsync(null as Profile);

            try
            {
                var response = await profileService.GetById(invalidId);
            }
            catch (Exception ex)
            {
                var thrownException = Assert.IsType<EntityNotFoundException>(ex);
            }
        }
    }
}
