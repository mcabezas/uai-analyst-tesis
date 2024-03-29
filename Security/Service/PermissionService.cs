/**
 * Created by Marcelo Cabezas on 2019-May-13 7:16:56 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Layers.Service;
using Security.Dao;
using Security.Model;

namespace Security.Service
{
    public class PermissionService : GenericService<Permission, int>

    {
        public PermissionService()
        {
            Dao = new PermissionDao();
        }

   }
}