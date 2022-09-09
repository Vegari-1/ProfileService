﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProfileService.Repository;

#nullable disable

namespace ProfileService.Repository.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220909203629_AddAutoTimestampConnection")]
    partial class AddAutoTimestampConnection
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProfileService.Model.Block", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BlockedId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BlockerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BlockedId");

                    b.HasIndex("BlockerId");

                    b.ToTable("Blocks");
                });

            modelBuilder.Entity("ProfileService.Model.Connection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Profile1")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Profile2")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Timestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("ProfileService.Model.ConnectionRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Profile1")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Profile2")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Timestamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.ToTable("ConnectionRequests");
                });

            modelBuilder.Entity("ProfileService.Model.Education", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Degree")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Field")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uuid");

                    b.Property<string>("School")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Education");
                });

            modelBuilder.Entity("ProfileService.Model.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("ProfileService.Model.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Public")
                        .HasColumnType("boolean");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("ProfileService.Model.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("ProfileService.Model.WorkExperience", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("WorkExperiences");
                });

            modelBuilder.Entity("ProfileService.Model.Block", b =>
                {
                    b.HasOne("ProfileService.Model.Profile", "Blocked")
                        .WithMany("BlockedBy")
                        .HasForeignKey("BlockedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfileService.Model.Profile", "Blocker")
                        .WithMany("Blocked")
                        .HasForeignKey("BlockerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blocked");

                    b.Navigation("Blocker");
                });

            modelBuilder.Entity("ProfileService.Model.Education", b =>
                {
                    b.HasOne("ProfileService.Model.Profile", "Profile")
                        .WithMany("Education")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("ProfileService.Model.Profile", b =>
                {
                    b.HasOne("ProfileService.Model.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("ProfileService.Model.Skill", b =>
                {
                    b.HasOne("ProfileService.Model.Profile", "Profile")
                        .WithMany("Skills")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("ProfileService.Model.WorkExperience", b =>
                {
                    b.HasOne("ProfileService.Model.Profile", "Profile")
                        .WithMany("WorkExperiences")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("ProfileService.Model.Profile", b =>
                {
                    b.Navigation("Blocked");

                    b.Navigation("BlockedBy");

                    b.Navigation("Education");

                    b.Navigation("Skills");

                    b.Navigation("WorkExperiences");
                });
#pragma warning restore 612, 618
        }
    }
}
