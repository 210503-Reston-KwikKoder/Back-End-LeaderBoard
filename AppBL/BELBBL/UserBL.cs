﻿using BELBDL;
using BELBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BELBBL
{
    public class UserBL : IUserBL
    {
        private Repo _repo;

        public UserBL(BELBDBContext context)
        {
            _repo = new Repo(context);
        }
        public async Task<string> AddUser(User u)
        {
            return await _repo.AddUser(u);  
        }

        public async Task<User> GetUser(string Id)
        {
            return await _repo.GetUser(Id);
        }
    }
}