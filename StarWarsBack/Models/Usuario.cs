﻿namespace StarWarsBack.Models
{
    public class Usuario
    {

        public int Id { get; set; }

        public string? nombre { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime? createdAt { get; set; }


    }
}
