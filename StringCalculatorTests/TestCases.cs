using System.Runtime.CompilerServices;

namespace StringCalculatorTests;

public static class TestCases
{
    public static IEnumerable<ChallengeStep> GetSteps()
    {
        yield return new ChallengeStep
        {
            Title = "given an empty string should return. add(\"\") = 0",
            TestCases = [new ValueTestCase { Input = "", Expected = 0 }],
        };
        yield return new ChallengeStep
        {
            Title = "given a string with a single value should return the same value",
            TestCases =
            [
                new ValueTestCase { Input = "0", Expected = 0 },
                new ValueTestCase { Input = "5", Expected = 5 },
                new ValueTestCase { Input = "42", Expected = 42 }
            ]
        };
        yield return new ChallengeStep
        {
            Title = "given a string with two comma separated values should return the sum of them",
            TestCases =
            [
                new ValueTestCase { Input = "1,1", Expected = 2 },
                new ValueTestCase { Input = "20,22", Expected = 42 }
            ]
        };
        yield return new ChallengeStep
        {
            Title = "given a string with N comma separated values should return the sum of all of them",
            TestCases =
            [
                new ValueTestCase { Input = "1,2,3", Expected = 6 },
                new ValueTestCase { Input = "1,2,3,4,5", Expected = 15 },
                new ValueTestCase { Input = "4,6,3,7,12,1,9", Expected = 42 },
                new ValueTestCase
                {
                    Input =
                        "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1",
                    Expected = 100
                }
            ]
        };
        yield return new ChallengeStep
        {
            Title = "should accept either commas and/or new lines ('\\n') as value separators.",
            TestCases =
            [
                new ValueTestCase { Input = "1\n2,3", Expected = 6 },
                new ValueTestCase { Input = "4\n2\n7", Expected = 13 },
                new ValueTestCase { Input = "1,2\n3\n4,5", Expected = 15 },
                new ValueTestCase { Input = "4\n6\n3,7,1,1\n1,1\n8,1\n9", Expected = 42 }
            ]
        };
        yield return new ChallengeStep
        {
            Title = "should not accept negative numbers, throwing an error specifying the problematic numbers.",
            TestCases =
            [
                new ErrorTestCase { Input = "1,-2", Error = "negatives not allowed: -2" },
                new ErrorTestCase { Input = "-1\n-2,3,-4", Error = "negatives not allowed: -1,-2,-4" },
                new ErrorTestCase
                    { Input = "///\n-4/6/3/-7/1/-1/1/-1/8/1/9", Error = "negatives not allowed: -4,-7,-1,-1" },
                new ErrorTestCase { Input = "//*\n-1*-2*-10", Error = "negatives not allowed: -1,-2,-10" }
            ]
        };
        yield return new ChallengeStep
        {
            Title = "should ignore (not add) numbers greater than 1000.",
            TestCases =
            [
                new ValueTestCase { Input = "1001,2", Expected = 2 },
                new ValueTestCase { Input = "1000,2", Expected = 1002 },
                new ValueTestCase { Input = "///\n2000/6/1/1/1234/5/3000/8/1/9", Expected = 31 },
                new ValueTestCase { Input = "1\n2000,1\n10", Expected = 12 }
            ]
        };
        yield return new ChallengeStep
        {
            Title = "should accept multi-character custom delimiter using this format: '//[delimiter]\\n(numbers…)'.",
            TestCases =
            [
                new ValueTestCase { Input = "//[;;;]\n1;;;2;;;3", Expected = 6 },
                new ValueTestCase { Input = "//[-_-]\n1-_-2-_-3-_-4-_-5", Expected = 15 },
                new ValueTestCase { Input = "//[//]\n4//6//3//7//1//1//1//1//8//1//9", Expected = 42 },
                new ValueTestCase { Input = "//[&.?!]\n1&.?!1&.?!1&.?!1&.?!1&.?!1", Expected = 6 }
            ]
        };
        yield return new ChallengeStep
        {
            Title =
                "should allow multiple single character delimiters using this format: '//[delim1][delim2]...\\n(numbers…)'.",
            TestCases =
            [
                new ValueTestCase { Input = "//[;][*]\n1;2*3", Expected = 6 },
                new ValueTestCase { Input = "//[/][*]\n1/2/3**4/5", Expected = 15 },
                new ValueTestCase { Input = "//[:][_][^][-]\n4:6-3-7_1-1^1:1_8^1^9", Expected = 42 },
                new ValueTestCase { Input = "//[+][*][^]\n1^1+1^1*1*1", Expected = 6 }
            ]
        };
        yield return new ChallengeStep
        {
            Title =
                "should allow multiple multi-characters delimiters using this format: '//[delim1][delim2]...\\n(numbers…)'.",
            TestCases =
            [
                new ValueTestCase { Input = "//[**][;]\n1;2**3", Expected = 6 },
                new ValueTestCase { Input = "//[//][***]\n1//2//3***4//5", Expected = 15 },
                new ValueTestCase { Input = "//[:)][:(]\n4:)6:(3:(7:)1:)1:)1:(1:)8:", Expected = 42 },
                new ValueTestCase { Input = "//[+][*][^]\n1^1+1^1*1*1", Expected = 6 }
            ]
        };
    }

    public static IEnumerable<TestCaseData> MapChallengeStepsToTestCaseData()
    {
        return GetSteps()
            ?.SelectMany(
                challengeStep => challengeStep.TestCases
                    ?.Select(testCase =>
                        testCase switch
                        {
                            ValueTestCase valueCase =>
                                new TestCaseData(valueCase.Input, false, string.Empty)
                                    .Returns(valueCase.Expected)
                                    .SetName(
                                        $"{challengeStep.Title} => Input: '{valueCase.Input}', Expected: '{valueCase.Expected}'"),
                            ErrorTestCase errorCase =>
                                new TestCaseData(errorCase.Input, true, errorCase.Error)
                                    .Returns(-1)
                                    .SetName(
                                        $"{challengeStep.Title} => Input: '{errorCase.Input}', Expected error: '{errorCase.Error}'"),
                            _ => throw new InvalidOperationException("Unsupported TestCase type")
                        })
            ) ?? Enumerable.Empty<TestCaseData>();
    }
}