﻿using System;

namespace ServiceRequest.Api.Data
{
    public class ServiceRequest
    {
        public Guid Id { get; set; }
        public string BuildingCode { get; set; }
        public string  Description { get; set; }
        public CurrentStatusEnum CurrentStatus { get; set; }
        public string  CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

    }

}
