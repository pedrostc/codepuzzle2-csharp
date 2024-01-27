import { IChallengeStep } from "./utils";

export const steps: IChallengeStep[] = [
    {
        Title: "given an empty string should return. add(\"\") = 0",
        TestCases: [{ Input: "", Expected: 0 }]
    },
    {
        Title: "given a string with a single value should return the same value",
        TestCases: [
            { Input: "0", Expected: 0 },
            { Input: "5", Expected: 5 },
            { Input: "42", Expected: 42 }
        ]
    },
    {
        Title: "given a string with two comma separated values should return the sum of them",
        TestCases: [
            { Input: "1,1", Expected: 2 },
            { Input: "20,22", Expected: 42 },
        ]
    },
    {
        Title: "given a string with N comma separated values should return the sum of all of them",
        TestCases: [
            { Input: "1,2,3", Expected: 6 },
            { Input: "1,2,3,4,5", Expected: 15 },
            { Input: "4,6,3,7,12,1,9", Expected: 42 },
            { Input: "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1", Expected: 100 },
        ]
    },
    {
        Title: "should accept either commas and/or new lines ('\\n') as value separators.",
        TestCases: [
            { Input: "1\n2,3", Expected: 6 },
            { Input: "4\n2\n7", Expected: 13 },
            { Input: "1,2\n3\n4,5", Expected: 15 },
            { Input: "4\n6\n3,7,1,1\n1,1\n8,1\n9", Expected: 42 },
        ]
    },
    {
        Title: "should also support any 1 char user defined symbol delimiter using this format: '//(delimiter)\\n(numbers…)'.",
        TestCases: [
            { Input: "//;\n1;2;3", Expected: 6 },
            { Input: "//-\n1-2-3-4-5", Expected: 15 },
            { Input: "///\n4/6/3/7/1/1/1/1/8/1/9", Expected: 42 },
            { Input: "//&\n1&1&1&1&1&1", Expected: 6 },
        ]
    },
    {
        Title: "should not accept negative numbers, throwing an error specifying the problematic numbers.",
        TestCases: [
            { Input: "1,-2", Error: "negatives not allowed: -2" },
            { Input: "-1\n-2,3,-4", Error: "negatives not allowed: -1,-2,-4" },
            { Input: "///\n-4/6/3/-7/1/-1/1/-1/8/1/9", Error: "negatives not allowed: -4,-7,-1,-1" },
            { Input: "//*\n-1*-2*-10", Error: "negatives not allowed: -1,-2,-10" },
        ]
    },
    {
        Title: "should ignore (not add) numbers greater than 1000.",
        TestCases: [
            { Input: "1001,2", Expected: 2 },
            { Input: "1000,2", Expected: 1002 },
            { Input: "///\n2000/6/1/1/1234/5/3000/8/1/9", Expected: 31 },
            { Input: "1\n2000,1\n10", Expected: 12 },
        ]
    },
    {
        Title: "should accept multi-character custom delimiter using this format: '//[delimiter]\\n(numbers…)'.",
        TestCases: [
            { Input: "//[;;;]\n1;;;2;;;3", Expected: 6 },
            { Input: "//[-_-]\n1-_-2-_-3-_-4-_-5", Expected: 15 },
            { Input: "//[//]\n4//6//3//7//1//1//1//1//8//1//9", Expected: 42 },
            { Input: "//[&.?!]\n1&.?!1&.?!1&.?!1&.?!1&.?!1", Expected: 6 },
        ]
    },
    {
        Title: "should allow multiple single character delimiters using this format: '//[delim1][delim2]...\\n(numbers…)'.",
        TestCases: [
            { Input: "//[;][*]\n1;2*3", Expected: 6 },
            { Input: "//[/][*]\n1/2/3**4/5", Expected: 15 },
            { Input: "//[:][_][^][-]\n4:6-3-7_1-1^1:1_8^1^9", Expected: 42 },
            { Input: "//[+][*][^]\n1^1+1^1*1*1", Expected: 6 },
        ]
    },
    {
        Title: "should allow multiple multi-characters delimiters using this format: '//[delim1][delim2]...\\n(numbers…)'.",
        TestCases: [
            { Input: "//[**][;]\n1;2**3", Expected: 6 },
            { Input: "//[//][***]\n1//2//3***4//5", Expected: 15 },
            { Input: "//[:)][:(]\n4:)6:(3:(7:)1:)1:)1:(1:)8:)1:)9", Expected: 42 },
        ]
    },
];
