﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coupons.Models
{
    public class Admin : User
    {


        public Admin(int id, String username, String mail, String phone) : base(id, username, mail, phone) { }

    }
}
