using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Models
{
    public class GitModel
    {
        public string Login { get; set; }
        public int Id { get; set; }

        public int Followers { get; set; }

        public int Following { get; set; }

        public int Repo { get; set; }
        public string DataCreate { get; set; }
    }
}
