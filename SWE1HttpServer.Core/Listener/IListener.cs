﻿using Server.Core.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Listener
{
    public interface IListener
    {
        IClient AcceptClient();
        void Start();
        void Stop();
    }
}
