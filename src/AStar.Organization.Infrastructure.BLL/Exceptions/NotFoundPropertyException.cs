﻿namespace AStar.Organization.Infrastructure.BLL.Exceptions
{
    public class NotFoundPropertyException : Exception
    {
        public NotFoundPropertyException(string entityName, string propertyName, string? foundValue)
            : base($"Entity \"{entityName}\" not found:\n" +
                   $"Entity property name: {propertyName}\n" +
                   $"Value for found: {foundValue}") { }
    }
}