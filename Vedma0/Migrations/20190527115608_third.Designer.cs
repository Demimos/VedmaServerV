﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vedma0.Data;

namespace Vedma0.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190527115608_third")]
    partial class third
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

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

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Vedma0.Models.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<DateTime>("EndTime");

                    b.Property<bool>("IncludeGeo");

                    b.Property<bool>("IncludeGeoFence");

                    b.Property<bool>("IncludeNews");

                    b.Property<bool>("IncludeNewsComments");

                    b.Property<bool>("IncludeNewsPublishing");

                    b.Property<bool>("IncludeNewsRate");

                    b.Property<bool>("IncludeVR");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("OwnerId");

                    b.Property<DateTime>("StartTime");

                    b.Property<string>("_MasterIds")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Vedma0.Models.GameEntities.GameEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<Guid>("GameId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("QRTag");

                    b.Property<string>("Tag");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("GameEntities");

                    b.HasDiscriminator<string>("Discriminator").HasValue("GameEntity");
                });

            modelBuilder.Entity("Vedma0.Models.Logging.DiaryPage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CharacterId");

                    b.Property<DateTime>("DateTime");

                    b.Property<Guid>("GameId");

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("GameId");

                    b.ToTable("Diary");
                });

            modelBuilder.Entity("Vedma0.Models.Logging.Log", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<Guid>("GameId");

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Logs");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Log");
                });

            modelBuilder.Entity("Vedma0.Models.ManyToMany.EntityPreset", b =>
                {
                    b.Property<long>("GameEntityId");

                    b.Property<long>("PresetId");

                    b.HasKey("GameEntityId", "PresetId");

                    b.HasIndex("PresetId");

                    b.ToTable("EntityPreset");
                });

            modelBuilder.Entity("Vedma0.Models.ManyToMany.GameUser", b =>
                {
                    b.Property<Guid>("GameId");

                    b.Property<string>("VedmaUserId");

                    b.HasKey("GameId", "VedmaUserId");

                    b.HasIndex("VedmaUserId");

                    b.ToTable("GameUsers");
                });

            modelBuilder.Entity("Vedma0.Models.Preset", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<Guid>("GameId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<bool>("SelfInsight");

                    b.Property<long>("SortValue");

                    b.Property<string>("Title");

                    b.Property<string>("_Abilities");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Presets");
                });

            modelBuilder.Entity("Vedma0.Models.Properties.BaseProperty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<Guid>("GameId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<long?>("PresetId");

                    b.Property<int>("SortValue");

                    b.Property<bool>("Visible");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("PresetId");

                    b.ToTable("BaseProperties");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseProperty");
                });

            modelBuilder.Entity("Vedma0.Models.Properties.EntityProperty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("BasePropertyId");

                    b.Property<string>("Description");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<long>("GameEntityId");

                    b.Property<Guid>("GameId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<long?>("PresetId");

                    b.Property<int>("SortValue");

                    b.Property<bool>("Visible");

                    b.HasKey("Id");

                    b.HasIndex("BasePropertyId");

                    b.HasIndex("GameEntityId");

                    b.HasIndex("GameId");

                    b.HasIndex("PresetId");

                    b.ToTable("Properties");

                    b.HasDiscriminator<string>("Discriminator").HasValue("EntityProperty");
                });

            modelBuilder.Entity("Vedma0.Models.VedmaUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<Guid?>("CurrentGame");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("EmailSignal");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PushToken");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Vedma0.Models.GameEntities.Character", b =>
                {
                    b.HasBaseType("Vedma0.Models.GameEntities.GameEntity");

                    b.Property<bool>("Active");

                    b.Property<bool>("HasSuspendedSignal");

                    b.Property<string>("InActiveMessage");

                    b.Property<string>("UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Character");

                    b.HasDiscriminator().HasValue("Character");
                });

            modelBuilder.Entity("Vedma0.Models.Logging.DebugItem", b =>
                {
                    b.HasBaseType("Vedma0.Models.Logging.Log");


                    b.ToTable("DebugItem");

                    b.HasDiscriminator().HasValue("DebugItem");
                });

            modelBuilder.Entity("Vedma0.Models.Logging.ErrorItem", b =>
                {
                    b.HasBaseType("Vedma0.Models.Logging.Log");


                    b.ToTable("ErrorItem");

                    b.HasDiscriminator().HasValue("ErrorItem");
                });

            modelBuilder.Entity("Vedma0.Models.Logging.VerboseItem", b =>
                {
                    b.HasBaseType("Vedma0.Models.Logging.Log");


                    b.ToTable("VerboseItem");

                    b.HasDiscriminator().HasValue("VerboseItem");
                });

            modelBuilder.Entity("Vedma0.Models.Properties.BaseNumericProperty", b =>
                {
                    b.HasBaseType("Vedma0.Models.Properties.BaseProperty");

                    b.Property<double>("DefaultValue");

                    b.ToTable("BaseNumericProperty");

                    b.HasDiscriminator().HasValue("BaseNumericProperty");
                });

            modelBuilder.Entity("Vedma0.Models.Properties.BaseTextArrayProperty", b =>
                {
                    b.HasBaseType("Vedma0.Models.Properties.BaseProperty");


                    b.ToTable("BaseTextArrayProperty");

                    b.HasDiscriminator().HasValue("BaseTextArrayProperty");
                });

            modelBuilder.Entity("Vedma0.Models.Properties.BaseTextProperty", b =>
                {
                    b.HasBaseType("Vedma0.Models.Properties.BaseProperty");

                    b.Property<string>("DefaultValue")
                        .IsRequired()
                        .HasColumnName("BaseTextProperty_DefaultValue");

                    b.ToTable("BaseTextProperty");

                    b.HasDiscriminator().HasValue("BaseTextProperty");
                });

            modelBuilder.Entity("Vedma0.Models.Properties.NumericProperty", b =>
                {
                    b.HasBaseType("Vedma0.Models.Properties.EntityProperty");

                    b.Property<double>("Value");

                    b.ToTable("NumericProperty");

                    b.HasDiscriminator().HasValue("NumericProperty");
                });

            modelBuilder.Entity("Vedma0.Models.Properties.TextProperty", b =>
                {
                    b.HasBaseType("Vedma0.Models.Properties.EntityProperty");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnName("TextProperty_Value");

                    b.ToTable("TextProperty");

                    b.HasDiscriminator().HasValue("TextProperty");
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
                    b.HasOne("Vedma0.Models.VedmaUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Vedma0.Models.VedmaUser")
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

                    b.HasOne("Vedma0.Models.VedmaUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Vedma0.Models.VedmaUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Vedma0.Models.GameEntities.GameEntity", b =>
                {
                    b.HasOne("Vedma0.Models.Game", "Game")
                        .WithMany("GameEntities")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Vedma0.Models.Logging.DiaryPage", b =>
                {
                    b.HasOne("Vedma0.Models.GameEntities.Character", "Character")
                        .WithMany("Diary")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Vedma0.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Vedma0.Models.Logging.Log", b =>
                {
                    b.HasOne("Vedma0.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Vedma0.Models.ManyToMany.EntityPreset", b =>
                {
                    b.HasOne("Vedma0.Models.GameEntities.GameEntity", "GameEntity")
                        .WithMany("EntityPresets")
                        .HasForeignKey("GameEntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Vedma0.Models.Preset", "Preset")
                        .WithMany("EntityPresets")
                        .HasForeignKey("PresetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Vedma0.Models.ManyToMany.GameUser", b =>
                {
                    b.HasOne("Vedma0.Models.Game", "Game")
                        .WithMany("GameUsers")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Vedma0.Models.VedmaUser", "VedmaUser")
                        .WithMany("GameUsers")
                        .HasForeignKey("VedmaUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Vedma0.Models.Preset", b =>
                {
                    b.HasOne("Vedma0.Models.Game", "Game")
                        .WithMany("Presets")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Vedma0.Models.Properties.BaseProperty", b =>
                {
                    b.HasOne("Vedma0.Models.Game", "Game")
                        .WithMany("BaseProperties")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Vedma0.Models.Preset", "Preset")
                        .WithMany("BaseProperties")
                        .HasForeignKey("PresetId");
                });

            modelBuilder.Entity("Vedma0.Models.Properties.EntityProperty", b =>
                {
                    b.HasOne("Vedma0.Models.Properties.BaseProperty", "BaseProperty")
                        .WithMany("Properties")
                        .HasForeignKey("BasePropertyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Vedma0.Models.GameEntities.GameEntity", "GameEntity")
                        .WithMany("Properties")
                        .HasForeignKey("GameEntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Vedma0.Models.Game", "Game")
                        .WithMany("EntityProperties")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Vedma0.Models.Preset", "Preset")
                        .WithMany("EntityProperties")
                        .HasForeignKey("PresetId");
                });

            modelBuilder.Entity("Vedma0.Models.GameEntities.Character", b =>
                {
                    b.HasOne("Vedma0.Models.VedmaUser", "User")
                        .WithMany("Characters")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
