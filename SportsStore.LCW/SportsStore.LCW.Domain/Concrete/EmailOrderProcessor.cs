using System;
using System.Linq;
using System.Text;
using SportsStore.LCW.Domain.Abstract;
using SportsStore.LCW.Domain.Entities;

namespace SportsStore.LCW.Domain.Concrete {

    /// <summary>
    /// 为发送邮件，创建一个存储对象
    /// </summary>
    public class EmailSettings {
        /// <summary>
        /// 目标地址 
        /// </summary>
        public string MailToAddress = "目标@example.com";
        /// <summary>
        /// 源地址
        /// </summary>
        public string MailFromAddress = "发送源@example.com";

        public bool UseSsl = true;

        /// <summary>
        /// 源地址：帐户
        /// </summary>
        public string Username = "用户";
        /// <summary>
        /// 源地址：密码
        /// </summary>
        public string Password = "密码";
        /// <summary>
        /// 服务器名字
        /// </summary>
        public string ServerName = "smtp.example.com";
        /// <summary>
        /// 服务端口
        /// </summary>
        public int ServerPort = 587;
        /// <summary>
        /// 
        /// </summary>
        public bool WriteAsFile = false;
        /// <summary>
        /// 发送日志存储
        /// </summary>
        public string FileLocation = @"c:\sports_store_emails";  
    }
    //邮件发送未编写，
    public class EmailOrderProcessor:IOrderProcessor {
        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails) {
            //流程1：创建一个stmp对象
            
            //流程2:用StringBuilder 组建购买信息发送给管理员
            //
            if (!cart.Lines.Any())
                return;
            StringBuilder sendStr = new StringBuilder();
            sendStr.AppendFormat("购买者的姓名:{0}",shippingDetails.Name);
            sendStr.AppendFormat("购买者的地址:{0},手机号码:{1}",shippingDetails.Line1,shippingDetails.Zip);
            foreach (var line in cart.Lines) {
                sendStr.AppendFormat("购买了商品:{0},数量:{1},总价{2}",
                    line.Product.Name,line.Quantity,line.Product.Price*line.Quantity);
            }
            sendStr.AppendFormat("所有商品总价为{0}",cart.ComputeTotalValue());

            //Console.Write(sendStr);
            //Console.Read();
        }
    }
}