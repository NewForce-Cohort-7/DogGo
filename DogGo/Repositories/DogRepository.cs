using DogGo.Models;
using DogGo.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace DogGo.Repositories
{
    public class DogRepository : IDogRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public DogRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Dog> GetAllDogs()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select d.Id, d.[Name], d.OwnerId, d.Breed, d.Notes, d.ImageUrl, o.[Name] as OwnerName
                      FROM Dog d
                      LEFT JOIN Owner o ON d.OwnerId = o.Id;";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Dog> dogs = new List<Dog>();
                    while (reader.Read())
                    {
                        Dog dog = new Dog
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                            Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            Notes = reader.IsDBNull(reader.GetOrdinal("Notes"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("Notes")),
                            ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageUrl"))
                                           ? null
                                           : reader.GetString(reader.GetOrdinal("ImageUrl")),
                            Owner = new Owner
                            {
                                Name = reader.GetString(reader.GetOrdinal("OwnerName"))
                            }
                        };
                        dogs.Add(dog);
                    }
                    reader.Close();
                    return dogs;
                }
            }
        }
        public Dog GetDogById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select d.Id, d.[Name], d.OwnerId, d.Breed, d.Notes, d.ImageUrl, o.[Name] as OwnerName
                      FROM Dog d
                      LEFT JOIN Owner o ON d.OwnerId = o.Id
                      Where d.Id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Dog dog = null;
                    while (reader.Read())
                    {
                        dog = new Dog
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                            Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            Notes = reader.IsDBNull(reader.GetOrdinal("Notes"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("Notes")),
                            ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageUrl"))
                                           ? null
                                           : reader.GetString(reader.GetOrdinal("ImageUrl")),
                            Owner = new Owner
                            {
                                Name = reader.GetString(reader.GetOrdinal("OwnerName"))
                            }
                        };
                    }
                    reader.Close();
                    return dog;
                }
            }
        }
        public Dog GetDogByOwner(int ownerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select d.Id, d.[Name], d.OwnerId, d.Breed, d.Notes, d.ImageUrl, o.[Name] as OwnerName
                      FROM Dog d
                      LEFT JOIN Owner o ON d.OwnerId = o.Id
                      Where d.OwnerId = @ownerId";
                    cmd.Parameters.AddWithValue("ownerId", ownerId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.Read())
                    {
                        Dog dog = new Dog()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                            Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            Notes = reader.IsDBNull(reader.GetOrdinal("Notes"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("Notes")),
                            ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageUrl"))
                                           ? null
                                           : reader.GetString(reader.GetOrdinal("ImageUrl")),
                            Owner = new Owner
                            {
                                Name = reader.GetString(reader.GetOrdinal("OwnerName"))
                            }
                        };
                        reader.Close();
                        return dog;
                    }
                    reader.Close();
                    return null;
                }
            }
        }
        public void AddDog(Dog dog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //Id Name OwnerId Breed Notes(nullable) ImageURL(nullable)

                    cmd.CommandText = @"
                    INSERT INTO Dog ([Name], OwnerId, Breed, Notes, ImageUrl)
                    OUTPUT INSERTED.ID
                    VALUES (@name, @ownerId, @breed, @notes, @imageUrl);
                ";

                    cmd.Parameters.AddWithValue("@name", dog.Name);
                    cmd.Parameters.AddWithValue("@ownerId", dog.OwnerId);
                    cmd.Parameters.AddWithValue("@breed", dog.Breed);
                    cmd.Parameters.AddWithValue("@notes", dog.Notes);
                    cmd.Parameters.AddWithValue("@imageUrl", dog.ImageUrl);

                    int id = (int)cmd.ExecuteScalar();

                    dog.Id = id;
                }
            }
        }
        public void UpdateDog(Dog dog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    UPDATE Dog
                    SET
                        [Name] 
                     ";
                }
            }
        }
        //public void DeleteDog(int id);
    }
}
