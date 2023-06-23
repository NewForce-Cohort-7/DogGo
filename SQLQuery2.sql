  SELECT Owner.Id, Owner.Email, Owner.[Name], Owner.Address, Owner.NeighborhoodId, Neighborhood.Name as NeighborhoodName, Owner.Phone, Dog.Id as DogId, Dog.Name as DogName, Dog.Breed, Dog.Notes, Dog.ImageUrl
                        FROM Owner
                        LEFT JOIN Neighborhood on Neighborhood.Id = Owner.NeighborhoodId
                        LEFT JOIN Dog on Dog.OwnerId = Owner.Id
                        Where Owner.Id = 1