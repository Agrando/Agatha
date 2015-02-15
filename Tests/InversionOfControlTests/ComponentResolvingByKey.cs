using System;
using Agatha.Common.InversionOfControl;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Tests.ConfigurationTests;
using TestTypes;
using Xunit;

namespace Tests.InversionOfControlTests
{
    public abstract class ComponentResolvingByKey<TContainer> where TContainer : IContainer
    {
        protected ComponentResolvingByKey()
        {
            IoC.Container = InitializeContainer();
        }

        protected abstract IContainer InitializeContainer();

        [Fact]
        public void CanResolveRequestA()
        {
            Assert.Equal(typeof(RequestA), IoC.Container.Resolve<RequestA>("KeyForRequestA").GetType());
        }

        [Fact]
        public void CanResolveRequestB()
        {
            Assert.Equal(typeof(RequestB), IoC.Container.Resolve<RequestB>("KeyForRequestB").GetType());
        }
    }

    public sealed class ComponentResolvingByKeyWithCastle : ComponentResolvingByKey<Agatha.Castle.Container>
    {
        protected override IContainer InitializeContainer()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<RequestA>().Named("KeyForRequestA"));
            container.Register(Component.For<RequestB>().Named("KeyForRequestB"));

            return new Agatha.Castle.Container(container);
        }
    }
}
