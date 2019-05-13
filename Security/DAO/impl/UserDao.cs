/**
 * Created by Marcelo Cabezas on 2019-May-12 11:03:36 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System.Numerics;
using Security.Model;

namespace Security.DAO.impl
{
    public class UserDao : AbstractDao<User, BigInteger>
    {
        public override User Insert(User entity)
        {
            Database.ExecuteNativeQuery("");
            throw new System.NotImplementedException();
        }

        public override User FindById(BigInteger entity)
        {
            throw new System.NotImplementedException();
        }

        public override User Update(User entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Delete(User entity)
        {
            throw new System.NotImplementedException();
        }
    }
}