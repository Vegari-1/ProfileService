using ProfileService.Model;
using System;
using System.Linq;
using Xunit;
using Moq;
using AutoMapper;
using ProfileService.Service.Interface;
using Profile = ProfileService.Model.Profile;
using ProfileService.Controllers;
using ProfileService.Dto;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Service.Interface.Exceptions;
using System.Collections.Generic;
using OpenTracing;

namespace ProfileService.UnitTests.ControllerTests
{
    public class ProfileControllerTests
    {
        
        private static readonly Guid id = Guid.NewGuid();
        private static readonly bool profilePublic = true;
        private static readonly string name = "John";
        private static readonly string surname = "Smith";
        private static readonly string username = "johnsmith";
        private static readonly string email = "email@example.com";
        private static readonly string phoneNumber = "1234567890";
        private static readonly Gender gender = Gender.FEMALE;
        private static readonly DateTime dateOfBirth = DateTime.Now;
        private static readonly string biography = "bio";
        private static readonly Skill skill1 = new Skill() { Id = id, Name = "Skill1" };
        private static readonly Skill skill2 = new Skill() { Id = id, Name = "Skill2" };
        private static readonly DateTime date = DateTime.Now;
        private static readonly Education education = new Education() { Id = id, School = "School", Degree = "Degree", Field = "Field", StartDate = date, EndDate = date };
        private static readonly WorkExperience workExperience = new WorkExperience() { Id = id, Position = "Position", Company = "Company", StartDate = date, EndDate = date  };

        private static readonly SkillResponse skillResponse1 = new SkillResponse() { Id = id, Name = "Skill1" };
        private static readonly SkillResponse skillResponse2 = new SkillResponse() { Id = id, Name = "Skill2" };
        private static readonly EducationResponse educationResponse = new EducationResponse() { Id = id, School = "School", Degree = "Degree", Field = "Field", StartDate = date, EndDate = date };
        private static readonly WorkExperienceResponse workExperienceResponse = new WorkExperienceResponse() { Id = id, Position = "Position", Company = "Company", StartDate = date, EndDate = date };

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
                Public = profilePublic,
                Name = name,
                Surname = surname,
                Username = username,
                Email = email,
                PhoneNumber = phoneNumber,
                Gender = gender,
                DateOfBirth = dateOfBirth,
                Biography = biography,
                Skills = new List<Skill>{ skill1, skill2 },
                Education = new List<Education>{ education },
                WorkExperiences = new List<WorkExperience>{ workExperience }
            };

            profileResponse = new ProfileResponse()
            {
                Id = id,
                Public = profilePublic,
                Name = name,
                Surname = surname,
                Username = username,
                Email = email,
                PhoneNumber = phoneNumber,
                Gender = gender,
                DateOfBirth = dateOfBirth,
                Biography = biography,
                Skills = new List<SkillResponse>{ skillResponse1, skillResponse2 },
                Education = new List<EducationResponse>{ educationResponse },
                WorkExperiences = new List<WorkExperienceResponse>{ workExperienceResponse }
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
            mockMapper
               .Setup(x => x.Map<SkillResponse>(skill1))
               .Returns((Skill source) =>
               {
                   return skillResponse1;
               });
            mockMapper
               .Setup(x => x.Map<SkillResponse>(skill2))
               .Returns((Skill source) =>
               {
                   return skillResponse2;
               });
            mockMapper
               .Setup(x => x.Map<EducationResponse>(education))
               .Returns((Education source) =>
               {
                   return educationResponse;
               });
            mockMapper
               .Setup(x => x.Map<WorkExperienceResponse>(workExperience))
               .Returns((WorkExperience source) =>
               {
                   return workExperienceResponse;
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
            Assert.Equal(profileResponse.PhoneNumber, actionValue.PhoneNumber);
            Assert.Equal(profileResponse.Gender, actionValue.Gender);
            Assert.Equal(profileResponse.DateOfBirth, actionValue.DateOfBirth);
            Assert.Equal(profileResponse.Biography, actionValue.Biography);

            Assert.True(actionValue.Skills.Count == 2);
            var actionValueSkill1 = Assert.IsType<SkillResponse>(actionValue.Skills.First());
            Assert.Equal(skillResponse1.Id, actionValueSkill1.Id);
            Assert.Equal(skillResponse1.Name, actionValueSkill1.Name);
            var actionValueSkill2 = Assert.IsType<SkillResponse>(actionValue.Skills.Last());
            Assert.Equal(skillResponse2.Id, actionValueSkill2.Id);
            Assert.Equal(skillResponse2.Name, actionValueSkill2.Name);

            Assert.True(actionValue.Education.Count == 1);
            var actionValueEducation = Assert.IsType<EducationResponse>(actionValue.Education.First());
            Assert.Equal(educationResponse.Id, actionValueEducation.Id);
            Assert.Equal(educationResponse.School, actionValueEducation.School);
            Assert.Equal(educationResponse.Degree, actionValueEducation.Degree);
            Assert.Equal(educationResponse.Field, actionValueEducation.Field);
            Assert.Equal(educationResponse.StartDate, actionValueEducation.StartDate);
            Assert.Equal(educationResponse.EndDate, actionValueEducation.EndDate);

            Assert.True(actionValue.WorkExperiences.Count == 1);
            var actionValueWorkExperience = Assert.IsType<WorkExperienceResponse>(actionValue.WorkExperiences.First());
            Assert.Equal(workExperienceResponse.Id, actionValueWorkExperience.Id);
            Assert.Equal(workExperienceResponse.Position, actionValueWorkExperience.Position);
            Assert.Equal(workExperienceResponse.Company, actionValueWorkExperience.Company);
            Assert.Equal(workExperienceResponse.StartDate, actionValueWorkExperience.StartDate);
            Assert.Equal(workExperienceResponse.EndDate, actionValueWorkExperience.EndDate);
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
