﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DEMO.BLL.Repository;

namespace Company.DEMO.DAL.Entities
{
    public class Department :BaseEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
