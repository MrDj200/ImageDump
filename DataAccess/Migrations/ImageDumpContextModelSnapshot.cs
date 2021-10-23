﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(ImageDumpContext))]
    partial class ImageDumpContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccess.Models.DjDumpGroup", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DjImageID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("ID");

                    b.HasIndex("DjImageID");

                    b.ToTable("DumpGroups");
                });

            modelBuilder.Entity("DataAccess.Models.DjDumpUser", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Adult")
                        .HasColumnType("bit");

                    b.Property<long>("DiscordID")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("DjDumpGroupID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DjImageID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ShowNSFW")
                        .HasColumnType("bit");

                    b.Property<string>("VRCIdentityID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("DjDumpGroupID");

                    b.HasIndex("DjImageID");

                    b.HasIndex("VRCIdentityID");

                    b.ToTable("DumpUsers");
                });

            modelBuilder.Entity("DataAccess.Models.DjImage", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("NSFW")
                        .HasColumnType("bit");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid?>("UploaderID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("UploaderID");

                    b.ToTable("Images");

                    b.HasDiscriminator<string>("Discriminator").HasValue("DjImage");
                });

            modelBuilder.Entity("DataAccess.Models.DjImageTag", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DjImageID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("ID");

                    b.HasIndex("DjImageID");

                    b.ToTable("DjImageTag");
                });

            modelBuilder.Entity("DataAccess.Models.DjVRCUser", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("DjVRCImageID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UsernameID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("DjVRCImageID");

                    b.HasIndex("UsernameID");

                    b.ToTable("VRCUsers");
                });

            modelBuilder.Entity("DataAccess.Models.DjVRCUsername", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("ID");

                    b.ToTable("DjVRCUsername");
                });

            modelBuilder.Entity("DataAccess.Models.DjVRCWorld", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AuthorID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("AuthorID");

                    b.ToTable("VRCWorlds");
                });

            modelBuilder.Entity("DataAccess.Models.DjWebhook", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DjDumpUserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("DjDumpUserID");

                    b.ToTable("DjWebhook");
                });

            modelBuilder.Entity("DataAccess.Models.DjVRCImage", b =>
                {
                    b.HasBaseType("DataAccess.Models.DjImage");

                    b.Property<string>("AuthorID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("WorldID")
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("AuthorID");

                    b.HasIndex("WorldID");

                    b.HasDiscriminator().HasValue("DjVRCImage");
                });

            modelBuilder.Entity("DataAccess.Models.DjDumpGroup", b =>
                {
                    b.HasOne("DataAccess.Models.DjImage", null)
                        .WithMany("AllowedGroups")
                        .HasForeignKey("DjImageID");
                });

            modelBuilder.Entity("DataAccess.Models.DjDumpUser", b =>
                {
                    b.HasOne("DataAccess.Models.DjDumpGroup", null)
                        .WithMany("Users")
                        .HasForeignKey("DjDumpGroupID");

                    b.HasOne("DataAccess.Models.DjImage", null)
                        .WithMany("AllowedUsers")
                        .HasForeignKey("DjImageID");

                    b.HasOne("DataAccess.Models.DjVRCUser", "VRCIdentity")
                        .WithMany()
                        .HasForeignKey("VRCIdentityID");

                    b.Navigation("VRCIdentity");
                });

            modelBuilder.Entity("DataAccess.Models.DjImage", b =>
                {
                    b.HasOne("DataAccess.Models.DjDumpUser", "Uploader")
                        .WithMany()
                        .HasForeignKey("UploaderID");

                    b.Navigation("Uploader");
                });

            modelBuilder.Entity("DataAccess.Models.DjImageTag", b =>
                {
                    b.HasOne("DataAccess.Models.DjImage", null)
                        .WithMany("Tags")
                        .HasForeignKey("DjImageID");
                });

            modelBuilder.Entity("DataAccess.Models.DjVRCUser", b =>
                {
                    b.HasOne("DataAccess.Models.DjVRCImage", null)
                        .WithMany("Players")
                        .HasForeignKey("DjVRCImageID");

                    b.HasOne("DataAccess.Models.DjVRCUsername", "Username")
                        .WithMany()
                        .HasForeignKey("UsernameID");

                    b.Navigation("Username");
                });

            modelBuilder.Entity("DataAccess.Models.DjVRCUsername", b =>
                {
                    b.HasOne("DataAccess.Models.DjVRCUser", "VRCUser")
                        .WithMany("KnownUsernames")
                        .HasForeignKey("ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VRCUser");
                });

            modelBuilder.Entity("DataAccess.Models.DjVRCWorld", b =>
                {
                    b.HasOne("DataAccess.Models.DjVRCUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorID");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("DataAccess.Models.DjWebhook", b =>
                {
                    b.HasOne("DataAccess.Models.DjDumpUser", null)
                        .WithMany("Webhooks")
                        .HasForeignKey("DjDumpUserID");
                });

            modelBuilder.Entity("DataAccess.Models.DjVRCImage", b =>
                {
                    b.HasOne("DataAccess.Models.DjVRCUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorID");

                    b.HasOne("DataAccess.Models.DjVRCWorld", "World")
                        .WithMany()
                        .HasForeignKey("WorldID");

                    b.Navigation("Author");

                    b.Navigation("World");
                });

            modelBuilder.Entity("DataAccess.Models.DjDumpGroup", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("DataAccess.Models.DjDumpUser", b =>
                {
                    b.Navigation("Webhooks");
                });

            modelBuilder.Entity("DataAccess.Models.DjImage", b =>
                {
                    b.Navigation("AllowedGroups");

                    b.Navigation("AllowedUsers");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("DataAccess.Models.DjVRCUser", b =>
                {
                    b.Navigation("KnownUsernames");
                });

            modelBuilder.Entity("DataAccess.Models.DjVRCImage", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
