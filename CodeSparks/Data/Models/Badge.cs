﻿namespace CodeSparks.Data.Models
{
    public class Badge
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public long UserId { get; set; }
    }

}