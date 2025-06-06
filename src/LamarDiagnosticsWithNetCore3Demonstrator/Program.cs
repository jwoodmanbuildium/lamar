﻿using System.Threading.Tasks;
using JasperFx;
using Lamar;
using Lamar.CommandLine;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StructureMap.Testing.Widget;
using StructureMap.Testing.Widget3;

namespace LamarDiagnosticsWithNetCore3Demonstrator
{
    class Program
    {
        #region sample_using-lamar-diagnostics
        static Task<int> Main(string[] args)
        {
            // Start up your HostBuilder as normal
            return new HostBuilder()
                .UseLamar((context, services) =>
                {
                    // This adds a Container validation
                    // to the Oakton "check-env" command
                    services.CheckLamarConfiguration();

                    // And the rest of your application's
                    // DI registrations.
                    services.IncludeRegistry<TestClassRegistry>();

                    // This one was problematic with oddball type names,
                    // so it's in our testing
                    services.AddHttpClient();
                })

                // Call this method to start your application
                // with JasperFx handling the command line parsing
                // and delegation
                // This will be included with your reference to Lamar,
                // no other Nugets are necessary!
                .RunJasperFxCommands(args);
        }
        #endregion
    }

    public class TestClassRegistry : ServiceRegistry
    {
        public TestClassRegistry()
        {
            Scan(s =>
            {
                s.TheCallingAssembly();
                s.WithDefaultConventions();
            });

            For<IEngine>().Use<Hemi>().Named("The Hemi");

            For<IEngine>().Add<VEight>().Singleton().Named("V8");
            For<IEngine>().Add<FourFiftyFour>();
            For<IEngine>().Add<StraightSix>().Scoped();

            For<IEngine>().Add(c => new Rotary()).Named("Rotary");
            For<IEngine>().Add(c => c.GetService<PluginElectric>());

            For<IEngine>().Add(new InlineFour());

            For<IWidget>().Use<AWidget>();

            For<AWidget>().Use<AWidget>();

            ForConcreteType<DeepConstructorGuy>();

            For<EngineChoice>().Add<EngineChoice>();

            For<IService>().Use(new ColorService("red"));

            Policies.SetAllProperties(policy => { policy.TypeMatches(type => type == typeof(IService)); });

            For<IGateway>().Use<DefaultGateway>()
                .Setter<string>("Name").Is("Blue")
                .Setter<string>("Color").Is("Green");

            For(typeof(Weird<>.Thing)).Use(typeof(Weird<>.Thing));
        }
    }

    public class Weird<T>
    {
        public class Thing {}
    }

}