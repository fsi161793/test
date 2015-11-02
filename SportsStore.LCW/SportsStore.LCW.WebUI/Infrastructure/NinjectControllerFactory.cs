using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using SportsStore.LCW.Domain.Abstract;
using SportsStore.LCW.Domain.Concrete;
//暂时为编写
//using SportsStore.LCW.Domain.Abstract;


namespace SportsStore.LCW.WebUI.Infrastructure {
    public class NinjectControllerFactory:DefaultControllerFactory {
            private IKernel ninjectKernel;

            public NinjectControllerFactory() {
                ninjectKernel = new StandardKernel();
                AddBindings();
            }

            protected override IController GetControllerInstance(RequestContext
                requestContext, Type controllerType) {

                return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
            }

            private void AddBindings() {
                //Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
                //mock.Setup(m => m.Products).Returns(new List<Product> {
                //    new Product { Name = "Football", Price = 25 },
                //    new Product { Name = "Surf board", Price = 179 },
                //    new Product { Name = "Running shoes", Price = 95 }
                //}.AsQueryable());
                //ninjectKernel.Bind<IProductsRepository>().ToConstant(mock.Object);
                ninjectKernel.Bind<IProductsRepository>().To<EFProductRepository>();
                //put bindings here

                //绑定一个邮件发送接口

                //创建一个邮件参数
                EmailSettings emailSettings = new EmailSettings
                {
                    WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
                };
                //"settings"是(EmailOrderProcessor构造函数形参名)
                ninjectKernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                    .WithConstructorArgument("settings", emailSettings);
            }
         }
    
}