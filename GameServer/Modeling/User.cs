using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Modeling
{
    class User
    {
        public virtual string UserName{ get; set; }
        public virtual string UserPwd { get; set; }
        public virtual string UserID  { get; set; }

    }
}
