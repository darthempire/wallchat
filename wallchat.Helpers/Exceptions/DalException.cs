﻿using System;

namespace wallchat.Helpers.Exceptions
{
    public class DalException : Exception
    {
        public DalException(string message)
            : base(message)
        { }
    }
}