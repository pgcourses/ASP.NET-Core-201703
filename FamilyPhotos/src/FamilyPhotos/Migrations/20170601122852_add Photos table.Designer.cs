using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FamilyPhotos.Data;

namespace FamilyPhotos.Migrations
{
    [DbContext(typeof(FamilyPhotosContext))]
    [Migration("20170601122852_add Photos table")]
    partial class addPhotostable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FamilyPhotos.Models.PhotoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentType");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<byte[]>("Picture");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 40);

                    b.HasKey("Id");

                    b.ToTable("Photos");
                });
        }
    }
}
