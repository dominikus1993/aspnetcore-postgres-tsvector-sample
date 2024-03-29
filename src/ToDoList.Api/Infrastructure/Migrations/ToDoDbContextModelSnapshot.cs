﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;
using ToDoList.Api.Infrastructure.EntityFramework;

#nullable disable

namespace ToDoList.Api.Infrastructure.Migrations
{
    [DbContext(typeof(ToDoDbContext))]
    partial class ToDoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ToDoList.Api.Core.Model.ToDoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTimeOffset?>("CompletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("completed_at");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("boolean")
                        .HasColumnName("is_complete");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid?>("todo_list_id")
                        .HasColumnType("uuid")
                        .HasColumnName("todo_list_id");

                    b.HasKey("Id")
                        .HasName("pk_to_do_items");

                    b.HasIndex("todo_list_id")
                        .HasDatabaseName("ix_to_do_items_todo_list_id");

                    b.ToTable("to_do_items", (string)null);
                });

            modelBuilder.Entity("ToDoList.Api.Core.Model.ToDos", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<NpgsqlTsVector>("SearchVector")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("tsvector")
                        .HasColumnName("search_vector")
                        .HasComputedColumnSql("to_tsvector('english', id::text || ' ' || name)", true);

                    b.HasKey("Id")
                        .HasName("pk_to_dos");

                    b.ToTable("to_dos", (string)null);
                });

            modelBuilder.Entity("ToDoList.Api.Core.Model.ToDoItem", b =>
                {
                    b.HasOne("ToDoList.Api.Core.Model.ToDos", null)
                        .WithMany("Items")
                        .HasForeignKey("todo_list_id")
                        .HasConstraintName("fk_to_do_items_to_dos_todo_list_id");
                });

            modelBuilder.Entity("ToDoList.Api.Core.Model.ToDos", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
