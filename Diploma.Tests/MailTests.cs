using System;
using System.Diagnostics;
using System.Net.Mail;
using Diploma.EmailService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Diploma.Tests
{
  [TestClass]
  public class MailTests
  {
    [TestMethod]
    public void EmailSendThrowsExeption()
    {
      var passed = true;
      Assert.ThrowsException<SmtpException>(
        (() => MailSender.BuildEmailTemlplate("Test", "Testowanie\nUnitia Testova", "rszkorla@gmail.com")));
    }
  }
}