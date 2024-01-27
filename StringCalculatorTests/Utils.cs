namespace StringCalculatorTests;

public class ChallengeStep
{
    public string Title { get; set; }
    public List<TestCase> TestCases { get; set; } = [];
}

public abstract class TestCase
{
    public string Input { get; set; }
    public abstract string FormatTestCaseExpectedText();
    public abstract void RunTest(StringCalculator.StringCalculator sut);
}

public class ErrorTestCase : TestCase
{
    public string Error { get; set; }

    public override string FormatTestCaseExpectedText() => $"expected error: {Error}.";
    public override void RunTest(StringCalculator.StringCalculator sut)
    {
        var ex = Assert.Throws<Exception>(() => sut.Add(Input));
        Assert.AreEqual(Error, ex.Message);
    }
}

public class ValueTestCase : TestCase
{
    public int Expected { get; set; }

    public override string FormatTestCaseExpectedText() => $"expected value: {Expected}.";
    public override void RunTest(StringCalculator.StringCalculator sut)
    {
        var result = sut.Add(Input);
        Assert.AreEqual(Expected, result);
    }
}