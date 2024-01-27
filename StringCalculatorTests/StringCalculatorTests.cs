namespace StringCalculatorTests;

public class Tests
{
    [TestCaseSource(typeof(TestCases), nameof(TestCases.MapChallengeStepsToTestCaseData))]
    public int RunTest(string input, bool expectException, string expectedError)
    {
        var sut = new StringCalculator.StringCalculator();

        if (expectException)
        {
            Assert.Throws<Exception>(() => sut.Add(input), expectedError);
            return -1;
        }

        return sut.Add(input);
    }
}