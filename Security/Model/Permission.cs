/**
 * Created by Marcelo Cabezas on 2019-Jun-16 11:46:42 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace Security.Model
{
    public class Permission : Entity
    {
        public static readonly Permission NullPermission = new Permission();

        public Permission()
        {
            Description = "";
        }

        public Permission(string description)
        {
            Description = description;
        }
        
        public string Description { get; set; }
    }
}