﻿using Entities.Contracts;

namespace Entities.Concretes
{
    public class Publisher : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Book>? Books { get; set; }
    }
}
