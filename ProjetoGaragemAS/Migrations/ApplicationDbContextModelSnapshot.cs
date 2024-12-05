﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProjetoGaragemAS.Data;

#nullable disable

namespace ProjetoGaragemAS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProjetoGaragemAS.Models.Carro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Ano")
                        .HasColumnType("integer");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Preco")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Carros");
                });

            modelBuilder.Entity("ProjetoGaragemAS.Models.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("ProjetoGaragemAS.Models.PessoaCarroFavorito", b =>
                {
                    b.Property<int>("PessoaId")
                        .HasColumnType("integer");

                    b.Property<int>("CarroId")
                        .HasColumnType("integer");

                    b.HasKey("PessoaId", "CarroId");

                    b.HasIndex("CarroId");

                    b.ToTable("Favoritos");
                });

            modelBuilder.Entity("ProjetoGaragemAS.Models.PessoaCarroFavorito", b =>
                {
                    b.HasOne("ProjetoGaragemAS.Models.Carro", "Carro")
                        .WithMany("Favoritos")
                        .HasForeignKey("CarroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoGaragemAS.Models.Pessoa", "Pessoa")
                        .WithMany("Favoritos")
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carro");

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("ProjetoGaragemAS.Models.Carro", b =>
                {
                    b.Navigation("Favoritos");
                });

            modelBuilder.Entity("ProjetoGaragemAS.Models.Pessoa", b =>
                {
                    b.Navigation("Favoritos");
                });
#pragma warning restore 612, 618
        }
    }
}
