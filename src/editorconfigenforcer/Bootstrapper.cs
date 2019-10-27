using Autofac;

namespace EditorConfigEnforcer
{
    public class Bootstrapper: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<VsLogger>()
                .SingleInstance();

            builder.RegisterType<EnforcerService>()
                .SingleInstance();

            builder.RegisterType<SolutionEventsSink>()
                .SingleInstance();

            builder.RegisterType<SolutionEventsStartup>()
                .As<IStartable>()
                .SingleInstance();
        }
    }
}