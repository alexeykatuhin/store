﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using WebApplication5.Abstract;
using WebApplication5.Models;

namespace WebApplication5.Concrete
{
	public class EmailSettings
	{
		public string MailToAddress = "alexeykatuhin@mail.ru";
		public string MailFromAddress = "alexeykatukhin@yandex.ru";
		public bool UseSsl = true;
		public string Username = "MySmtpUsername";
		public string Password = "head4372";
		public string ServerName = "smtp.yandex.ru";
		public int ServerPort = 587;
		public bool WriteAsFile = true;
	}

	public class EmailOrderProcessor : IOrderProcessor
	{
		private EmailSettings emailSettings;

		public EmailOrderProcessor(EmailSettings settings)
		{
			emailSettings = settings;
		}

		public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
		{
			using (var smtpClient = new SmtpClient())
			{
				smtpClient.EnableSsl = emailSettings.UseSsl;
				smtpClient.Host = emailSettings.ServerName;
				smtpClient.Port = emailSettings.ServerPort;
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Credentials
					= new NetworkCredential(emailSettings.MailFromAddress, emailSettings.Password);

			
					smtpClient.DeliveryMethod
						= SmtpDeliveryMethod.Network;
			
				StringBuilder body = new StringBuilder()
					.AppendLine("Новый заказ обработан")
					.AppendLine("---")
					.AppendLine("Товары:");

				foreach (var line in cart.Lines)
				{
					var subtotal = line.Item.Price * line.Quantity;
					body.AppendFormat("{0} x {1} (итого: {2:c}",
						line.Quantity, line.Item.Name, subtotal);
				}

				body.AppendFormat("Общая стоимость: {0:c}", cart.ComputeTotalValue())
					.AppendLine("---")
					.AppendLine("Доставка:")
					.AppendLine(shippingInfo.Name)
					.AppendLine(shippingInfo.Line1)
					.AppendLine(shippingInfo.City)
					.AppendLine(shippingInfo.Country)
					.AppendLine("---");

				MailMessage mailMessage = new MailMessage(
									   emailSettings.MailFromAddress,   // От кого
									   emailSettings.MailToAddress,     // Кому
									   "Новый заказ отправлен!",        // Тема
									   body.ToString());                // Тело письма

				if (emailSettings.WriteAsFile)
				{
					mailMessage.BodyEncoding = Encoding.UTF8;
				}

				smtpClient.Send(mailMessage);
			}
		}
	}
}