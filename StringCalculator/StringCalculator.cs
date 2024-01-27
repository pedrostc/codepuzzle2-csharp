namespace StringCalculator;

public class StringCalculator
{
    public int Add(string input)
    {

        var inputs = input.Split(new[] { ',', '\n' }).Select(int.Parse);
        var negatives = inputs.Where(i => i < 0);
        if (negatives.Any())
        {
            throw new Exception($"negatives not allowed: {string.Join(",", negatives)}" );
        }
        return 0;
    }
}