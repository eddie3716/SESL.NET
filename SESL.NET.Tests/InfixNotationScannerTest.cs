using NUnit.Framework;
using System;
using SESL.NET.InfixNotation;

namespace SESL.NET.Tests;

[TestFixture]
public class InfixNotationScannerTest
{
    /// <summary>
    ///A test for Scanner Constructor
    ///</summary>
    [Test]
    public void InfixNotationScanner_ScannerConstructorTest()
    {
        string sourceText = string.Empty;
        Assert.Throws<InvalidOperationException>(() => new InfixNotationScanner(sourceText));
    }

    /// <summary>
    ///A test for Scanner Constructor
    ///</summary>
    [Test]
    public void InfixNotationScanner_ScannerConstructorTest1()
    {
        string sourceText = null;
        Assert.Throws<InvalidOperationException>(() => new InfixNotationScanner(sourceText));
    }

    /// <summary>
    ///A test for Scanner Constructor
    ///</summary>
    [Test]
    public void InfixNotationScanner_ScannerConstructorTest2()
    {
        string sourceText = "67";
        InfixNotationScanner target = new(sourceText);
        Assert.IsTrue(target != null);
    }

    /// <summary>
    ///A test for GetCharacter
    ///</summary>
    [Test]
    public void InfixNotationScanner_GetCharacterTest()
    {
        InfixNotationScanner target = new("%");
        char expected = '%';
        char actual;
        actual = target.Get();
        Assert.AreEqual(expected, actual);

    }

    /// <summary>
    ///A test for Next
    ///</summary>
    [Test]
    public void InfixNotationScanner_NextTest()
    {
        InfixNotationScanner target = new("%");
        bool actual;
        actual = target.Next();
        Assert.IsTrue(actual);
    }

    /// <summary>
    ///A test for Next
    ///</summary>
    [Test]
    public void InfixNotationScanner_NextTest1()
    {
        InfixNotationScanner target = new("%");
        bool actual;
        target.Next();
        actual = target.Next();
        Assert.IsFalse(actual);
    }

    /// <summary>
    ///A test for Previous
    ///</summary>
    [Test]
    public void InfixNotationScanner_PreviousTest()
    {
        InfixNotationScanner target = new("6");
        bool actual;
        actual = target.Previous();
        Assert.IsFalse(actual);
    }

    /// <summary>
    ///A test for Previous
    ///</summary>
    [Test]
    public void InfixNotationScanner_PreviousTest1()
    {
        InfixNotationScanner target = new("6");
        bool actual;
        target.Next();
        actual = target.Previous();
        Assert.IsTrue(actual);
    }

    /// <summary>
    ///A test for CurrentIndex
    ///</summary>
    [Test]
    public void InfixNotationScanner_CurrentIndexTest()
    {
        InfixNotationScanner target = new("67");
        int expected = -1;
        int actual;
        actual = target.CurrentIndex;
        Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///A test for CurrentIndex
    ///</summary>
    [Test]
    public void InfixNotationScanner_CurrentIndexTest1()
    {
        InfixNotationScanner target = new("67");
        int expected = 0;
        int actual;
        target.Next();
        actual = target.CurrentIndex;
        Assert.AreEqual(expected, actual);
    }

    /// <summary>
    ///A test for SourceText
    ///</summary>
    [Test]
    public void InfixNotationScanner_SourceTextTest()
    {
        InfixNotationScanner target = new("67");
        string actual;
        actual = target.Source;
        Assert.AreEqual("67", actual);
    }
}