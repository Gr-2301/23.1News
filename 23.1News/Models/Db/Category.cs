﻿namespace _23._1News.Models.Db
{
    public class Category
    {

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> CategoryUsers { get; set; }
     
    }
}
