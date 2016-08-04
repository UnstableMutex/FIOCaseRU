using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FIOCaseRU;
namespace FIOCaseRU.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string ExpectedRPm = "Брюхно Владимиром Петровичем";
            FIO fm = new FIO("Брюхно", "Владимир", "Петрович");
            Assert.AreEqual(fm.Ablative.Full, ExpectedRPm);
            string expectedTPm = "Брюхно Владимиром Петровичем";
            Assert.AreEqual(fm.Ablative.Full, expectedTPm);
        }
        [TestMethod]
        public void TestMethod111()
        {
            string ExpectedRPm = "Цой Владимиру Петровичу";
            FIO fm = new FIO("Цой", "Владимир", "Петрович");
            Assert.AreEqual(fm.Dative.Full, ExpectedRPm);
         
        }
   [TestMethod]
        public void TestMethod1112342()
        {
            string ExpectedRPm = "Кузьменко Алексеем";
            FIO fm = new FIO("Кузьменко", "Алексей", Sex.Male);
            Assert.AreEqual(fm.Ablative.Full, ExpectedRPm);
         
        }

        [TestMethod]
        public void TestMethod111234234()
        {
            string ExpectedRPm = "Кузьменко Алексея";
            FIO fm = new FIO("Кузьменко", "Алексей", Sex.Male);
            Assert.AreEqual(fm.Genitive.Full, ExpectedRPm);
         
        }   [TestMethod]
        public void TestMethod1112()
        {
            string ExpectedRPm = "Цой Ольге Петровне";
            FIO fm = new FIO("Цой", "Ольга", "Петровна");
            Assert.AreEqual(fm.Dative.Full, ExpectedRPm);
         
        }
        [TestMethod]
        public void TestMethod12()
        {
            string ExpectedRPm = "Брюхно Владимира";
            FIO fm = new FIO("Брюхно", "Владимир", string.Empty);
            Assert.AreEqual(fm.Genitive.Full, ExpectedRPm);
            string expectedTPm = "Брюхно Владимиром";
            Assert.AreEqual(fm.Ablative.Full, expectedTPm);
        }
        [TestMethod]
        public void TestMethod2()
        {
            string ExpectedRPf = "Гучужно Ольгой Ивановной";
            FIO ff = new FIO("Гучужно", "Ольга", "Ивановна");
            Assert.AreEqual(ff.Ablative.Full, ExpectedRPf);
            string expectedTPf = "Гучужно Ольгой Ивановной";
            Assert.AreEqual(ff.Ablative.Full, expectedTPf);
        }
        [TestMethod]
        public void TestMethod3()
        {
            string ExpectedRPf = "Раджаи Фирдовси Гулам Оглы";
            FIO ff = new FIO("Раджаи", "Фирдовси", "ГУЛАМ оглы");
            Assert.AreEqual(ExpectedRPf, ff.Ablative.Full);
            string expectedTPf = "Раджаи Фирдовси Гулам Оглы";
            Assert.AreEqual(ff.Ablative.Full, expectedTPf);
        }
        [TestMethod]
        public void TestMethod4()
        {
            string ExpectedRPf = "Фольк Натальи Ивановны";
            FIO ff = new FIO("Фольк", "Наталья", "Ивановна");
            Assert.AreEqual(ExpectedRPf, ff.Genitive.Full);
            string expectedTPf = "Фольк Натальей Ивановной";
            Assert.AreEqual(ff.Ablative.Full, expectedTPf);
            string expectedDPf = "Фольк Наталье Ивановне";
            Assert.AreEqual(ff.Dative.Full, expectedDPf);
        }
        [TestMethod]
        public void CapitalizerTest()
        {
            CaseSettings cs = new CaseSettings();
            string expected = "Гулам Оглы";
            string source = "Гулам оглы";
            string actual = cs.Capitalizer(source);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CapitalizerTest1()
        {
            CaseSettings cs = new CaseSettings();
            string expected = "Назарова-Захарова";
            string source = "НаЗарова-захарова";
            string actual = cs.Capitalizer(source);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CapitalizerTest2()
        {
            CaseSettings cs = new CaseSettings();
            string expected = "Ли-Хан-Чин";
            string source = "Ли-хан-чин";
            string actual = cs.Capitalizer(source);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ForamtterTest()
        {
            FIO f = new FIO("Иванов", "Иван", "Иванович");
            string actual = f.ToString("1abbr");
            string expected = "Иванов И.И.";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ForamtterTest1()
        {
            FIO f = new FIO("Иванов", "Иван", "Иванович");
            string actual = f.ToString("1s&f&p");
            string expected = "ИвановИванИванович";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ForamtterTest2()
        {
            FIO f = new FIO("Иванов", "Иван", "Иванович");
            string actual = f.ToString("1s&f1&p1");
            string expected = "ИвановИИ";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ForamtterTest3()
        {
            FIO f = new FIO("Иванов", "Иван", "Иванович");
            string actual = f.ToString("1s&e&f1&d&p1&d");
            string expected = "Иванов И.И.";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ForamtterTest4()
        {
            FIO f = new FIO("Иванов", "Иван", "Иванович");
            string actual = f.ToString("1full");
            string expected = "Иванов Иван Иванович";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ForamtterTest41()
        {
            FIO f = new FIO("Иванов", "Иван", "");
            string actual = f.ToString("1full");
            string expected = "Иванов Иван";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ForamtterTest5()
        {
            FIO f = new FIO("Иванов", "Иван", "Иванович");
            string actual = f.ToString("1abbrs");
            string expected = "Иванов И. И.";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ForamtterTest51()
        {
            FIO f = new FIO("Иванов", "Иван", "");
            string actual = f.ToString("1abbrs");
            string expected = "Иванов И.";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ForamtterTest6()
        {
            FIO f = new FIO("Иванов", "Иван", "Иванович");
            string actual = f.ToString("2full");
            string expected = "Иванова Ивана Ивановича";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ForamtterTest7()
        {
            FIO f = new FIO("Иванов", "Иван", "Иванович");
            string actual = f.ToString("3full");
            string expected = "Иванову Ивану Ивановичу";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ForamtterTest8()
        {
            FIO f = new FIO("Иванов", "Иван", "Иванович");
            string actual = f.ToString("4full");
            string expected = "Иванова Ивана Ивановича";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ForamtterTest9()
        {
            FIO f = new FIO("Иванов", "Иван", "Иванович");
            string actual = f.ToString("5full");
            string expected = "Ивановым Иваном Ивановичем";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ForamtterTest10()
        {
            FIO f = new FIO("Иванов", "Иван", "Иванович");
            string actual = f.ToString("6full");
            string expected = "Иванове Иване Ивановиче";
            Assert.AreEqual(expected, actual);
        }
    }
}
