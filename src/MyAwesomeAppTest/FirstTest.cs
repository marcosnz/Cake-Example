using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyAwesomeAppTest
{
  [TestClass]
  public class FirstTest
  {
    [TestMethod]
    public void TestMethod1()
    {
      const long TheAnswer = 42;

      Assert.AreEqual(WhatIsTheMeaningOfLife(), TheAnswer);
    }

    private long WhatIsTheMeaningOfLife()
    {
      return 42;
    }
  }
}
