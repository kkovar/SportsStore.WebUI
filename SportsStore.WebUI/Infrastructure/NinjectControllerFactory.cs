using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Moq;
using SportsStore.Domain;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;


namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;


        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ?
                null :
                (IController)ninjectKernel.Get(controllerType);
        }


        private void AddBindings()
        {
            // mock implementation of the IProductRepository Interface
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup.Setup(m => m.Products).Returns(new List<Product> {
                new Product{ Name="Ultegra crank set", Price = 300 }
            }.AsQueryable());
            ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);

        }

    }
}