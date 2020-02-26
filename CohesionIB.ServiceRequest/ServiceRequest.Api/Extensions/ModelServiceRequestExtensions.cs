using ServiceRequest.Api.Models;
using System;

namespace ServiceRequest.Api.Extensions
{
    public static class ModelServiceRequestExtensions
    {
        public static Data.CurrentStatusEnum TransformCurrentStatus(this Models.CurrentStatusEnum instance)
        {
            switch (instance)

            {
                case CurrentStatusEnum.NotApplicable:
                    return Data.CurrentStatusEnum.NotApplicable;
                case CurrentStatusEnum.Created:
                    return Data.CurrentStatusEnum.Created;
                case CurrentStatusEnum.InProgress:
                    return Data.CurrentStatusEnum.InProgress;
                case CurrentStatusEnum.Complete:
                    return Data.CurrentStatusEnum.Complete;
                case CurrentStatusEnum.Cancelled:
                    return Data.CurrentStatusEnum.Cancelled;
                default:
                    throw new ArgumentOutOfRangeException(nameof(instance), instance, null);
            }
        }
    }
}
