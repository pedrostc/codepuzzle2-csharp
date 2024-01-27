export function FormatTestCaseName(stepNbr: number, testCaseNbr: number, testCase: TestCase): string {
    return `Test Case ${stepNbr}.${testCaseNbr} - input: "${escapeNewLines(testCase.Input)}"; ${formatTestCaseExpectedText(testCase)}`;
}

function escapeNewLines(text: string): string {
    return text.replace(/\n/gi, '\\n');
}

function formatTestCaseExpectedText(testCase: TestCase): string {
    // duplicating the conditional here to make the transpiler happy.
    // it will fail to transpile and run the tests if we perform the conditional in a separate variable or function.
    const whatsExpected = "Error" in testCase ? "error" : "value";
    const expectedResult = "Error" in testCase ? testCase.Error : testCase.Expected.toString();

    return `expected ${whatsExpected}: ${expectedResult}.`;
}

export interface IChallengeStep {
    Title: string;
    TestCases: TestCase[];
}

export type TestCase = IErrorTestCase | IValueTestCase;

interface IBaseTestCase {
    Input: string;
}

export interface IErrorTestCase extends IBaseTestCase {
    Error: string;
}

export interface IValueTestCase extends IBaseTestCase {
    Expected: number;
}