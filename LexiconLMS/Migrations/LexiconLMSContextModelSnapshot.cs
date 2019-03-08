﻿// <auto-generated />
using System;
using LexiconLMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LexiconLMS.Migrations
{
    [DbContext(typeof(LexiconLMSContext))]
    partial class LexiconLMSContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LexiconLMS.Models.ActivityType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("ActivityType");
                });

            modelBuilder.Entity("LexiconLMS.Models.Activityy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActivityTypeId");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("ModuleId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("ActivityTypeId");

                    b.HasIndex("ModuleId");

                    b.ToTable("Activities");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            ActivityTypeId = 1,
                            Description = "Mauris venenatis",
                            EndDate = new DateTime(2018, 11, 26, 12, 15, 0, 0, DateTimeKind.Unspecified),
                            ModuleId = -1,
                            StartDate = new DateTime(2018, 11, 26, 8, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = -2,
                            ActivityTypeId = 2,
                            Description = "Nunc tempus finibus mollis",
                            EndDate = new DateTime(2018, 11, 26, 17, 0, 0, 0, DateTimeKind.Unspecified),
                            ModuleId = -1,
                            StartDate = new DateTime(2018, 11, 26, 13, 15, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = -3,
                            ActivityTypeId = 3,
                            Description = "Mauris venenatis",
                            EndDate = new DateTime(2018, 11, 27, 17, 0, 0, 0, DateTimeKind.Unspecified),
                            ModuleId = -1,
                            StartDate = new DateTime(2018, 11, 27, 8, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = -4,
                            ActivityTypeId = 1,
                            Description = "Nunc tempus finibus mollis",
                            EndDate = new DateTime(2018, 11, 28, 17, 0, 0, 0, DateTimeKind.Unspecified),
                            ModuleId = -1,
                            StartDate = new DateTime(2018, 11, 28, 8, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = -5,
                            ActivityTypeId = 2,
                            Description = "Mauris venenatis",
                            EndDate = new DateTime(2018, 11, 29, 17, 0, 0, 0, DateTimeKind.Unspecified),
                            ModuleId = -1,
                            StartDate = new DateTime(2018, 11, 29, 8, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = -6,
                            ActivityTypeId = 2,
                            Description = "Nunc tempus finibus mollis",
                            EndDate = new DateTime(2018, 11, 30, 17, 0, 0, 0, DateTimeKind.Unspecified),
                            ModuleId = -1,
                            StartDate = new DateTime(2018, 11, 30, 8, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = -7,
                            ActivityTypeId = 1,
                            Description = "Mauris venenatis",
                            EndDate = new DateTime(2018, 12, 26, 12, 15, 0, 0, DateTimeKind.Unspecified),
                            ModuleId = -1,
                            StartDate = new DateTime(2018, 12, 1, 8, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = -8,
                            ActivityTypeId = 2,
                            Description = "Nunc tempus finibus mollis",
                            EndDate = new DateTime(2018, 12, 1, 17, 0, 0, 0, DateTimeKind.Unspecified),
                            ModuleId = -1,
                            StartDate = new DateTime(2018, 12, 1, 13, 15, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = -9,
                            ActivityTypeId = 3,
                            Description = "Mauris venenatis",
                            EndDate = new DateTime(2018, 12, 2, 17, 0, 0, 0, DateTimeKind.Unspecified),
                            ModuleId = -1,
                            StartDate = new DateTime(2018, 12, 2, 8, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = -10,
                            ActivityTypeId = 1,
                            Description = "Nunc tempus finibus mollis",
                            EndDate = new DateTime(2018, 12, 3, 15, 30, 0, 0, DateTimeKind.Unspecified),
                            ModuleId = -1,
                            StartDate = new DateTime(2018, 12, 3, 8, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = -11,
                            ActivityTypeId = 2,
                            Description = "Mauris venenatis",
                            EndDate = new DateTime(2018, 12, 4, 17, 15, 0, 0, DateTimeKind.Unspecified),
                            ModuleId = -1,
                            StartDate = new DateTime(2018, 12, 4, 8, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = -12,
                            ActivityTypeId = 3,
                            Description = "Nunc tempus finibus mollis",
                            EndDate = new DateTime(2018, 12, 5, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            ModuleId = -1,
                            StartDate = new DateTime(2018, 12, 5, 8, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("LexiconLMS.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Description = "Utbildningen mot programmerare och systemut-vecklare syftar till att skapa förutsättningar att ut-veckla kunskaper och färdigheter i programmering och att utveckla IT-system, applikationer eller delar av system. Utbildningen syftar till att inom valt språk täcka systemutveckling, frontend, backend, fullstack samt mobil applikationsutveckling.",
                            EndDate = new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Programmerare och systemutvecklare Inriktning Microsoft .NET",
                            StartDate = new DateTime(2018, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("LexiconLMS.Models.CourseDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseId");

                    b.Property<string>("Description");

                    b.Property<byte[]>("DocumentData");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UploadTime");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseDocument");
                });

            modelBuilder.Entity("LexiconLMS.Models.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseId");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Modules");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            CourseId = -1,
                            Description = "Lorem ipsum dolor sit amet",
                            EndDate = new DateTime(2018, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Programmering",
                            StartDate = new DateTime(2018, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = -2,
                            CourseId = -1,
                            Description = "Cras ut euismod enim",
                            EndDate = new DateTime(2019, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Avancerad Programmering",
                            StartDate = new DateTime(2018, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = -3,
                            CourseId = -1,
                            Description = "Ut a lobortis eros, at blandit metu",
                            EndDate = new DateTime(2019, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Databas",
                            StartDate = new DateTime(2018, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = -4,
                            CourseId = -1,
                            Description = "Vestibulum pharetra ultrices pulvinar",
                            EndDate = new DateTime(2019, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "FrontEnd",
                            StartDate = new DateTime(2019, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = -5,
                            CourseId = -1,
                            Description = "Fusce semper, tortor ac condimentum",
                            EndDate = new DateTime(2019, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "BackEnd",
                            StartDate = new DateTime(2019, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = -6,
                            CourseId = -1,
                            Description = "Vestibulum sit amet magna turpis",
                            EndDate = new DateTime(2019, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Applikationsutveckling",
                            StartDate = new DateTime(2019, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = -7,
                            CourseId = -1,
                            Description = "Nunc libero quam, varius id mattis ut",
                            EndDate = new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Testning av mjukvara",
                            StartDate = new DateTime(2019, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("LexiconLMS.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<int?>("CourseId");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("LexiconLMS.Models.Activityy", b =>
                {
                    b.HasOne("LexiconLMS.Models.ActivityType", "ActivityType")
                        .WithMany()
                        .HasForeignKey("ActivityTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LexiconLMS.Models.Module", "Module")
                        .WithMany("Activities")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LexiconLMS.Models.CourseDocument", b =>
                {
                    b.HasOne("LexiconLMS.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LexiconLMS.Models.Module", b =>
                {
                    b.HasOne("LexiconLMS.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LexiconLMS.Models.User", b =>
                {
                    b.HasOne("LexiconLMS.Models.Course", "Course")
                        .WithMany("Users")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LexiconLMS.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LexiconLMS.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LexiconLMS.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LexiconLMS.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
