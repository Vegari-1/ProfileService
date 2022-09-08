﻿using System;

namespace ProfileService.Service.Interface.Exceptions
{
    public class EntityExistsException : BaseException
    {
        public EntityExistsException(Type entityType, string entityFields) : base(
             String.Format("{0} with given {1} already exists", entityType.Name, entityFields))
        {
            StatusCode = 409;
        }
    }
}
