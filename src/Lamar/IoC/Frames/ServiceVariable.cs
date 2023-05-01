﻿using JasperFx.CodeGeneration.Frames;
using JasperFx.CodeGeneration.Model;
using Lamar.IoC.Instances;

namespace Lamar.IoC.Frames;

public enum ServiceDeclaration
{
    ImplementationType,
    ServiceType
}

public class ServiceVariable : Variable, IServiceVariable
{
    public ServiceVariable(Instance instance, Frame creator,
        ServiceDeclaration declaration = ServiceDeclaration.ImplementationType)
        : base(
            declaration == ServiceDeclaration.ImplementationType ? instance.ImplementationType : instance.ServiceType,
            instance.Name.Sanitize(), creator)
    {
        Instance = instance;
    }

    public Instance Instance { get; }
}