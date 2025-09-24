namespace TribonacciSequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

/// <summary>
/// <see href="https://www.freecodecamp.org/learn/daily-coding-challenge/2025-09-01"/>
/// </summary>
internal class Program
{
    static T[] TribonacciSequence<T>(T[] startSequence, int length)
        where T : INumber<T>
    {
        ArgumentNullException.ThrowIfNull(startSequence);

        if (startSequence.Length != 3)
        {
            throw new ArgumentException(paramName: nameof(startSequence),
                message: "Must have a length of 3.");
        }

        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(length),
                actualValue: length,
                message: "Must be zero or positive.");
        }

        List<T> result = [];

        for (var i = 0; i < length; i++)
        {
            if (i < 3)
            {
                result.Add(startSequence[i]);
            }
            else
            {
                checked
                {
                    result.Add(result[i - 3] + result[i - 2] + result[i - 1]);
                }
            }
        }

        return [.. result];
    }

    static void Main()
    {
        List<Failure<BigInteger>> failures = [];

        foreach ((var startSequence, var length, var expected) in Tests)
        {
            Console.Write($"Testing {ArrayToString(startSequence)} and "
                + $"{length} (expecting {ArrayToString(expected)})...");
            var actual = TribonacciSequence(startSequence, length);
            var success = expected.SequenceEqual(actual);
            Console.WriteLine($"{ArrayToString(actual)} (success: {success}).");

            if (!success)
            {
                failures.Add(new(startSequence, length));
            }
        }

        if (failures.Count > 0)
        {
            Console.WriteLine("The following inputs failed:");

            foreach ((var startSequence, var length) in failures)
            {
                Console.Write($"  {ArrayToString(startSequence)} and "
                    + $"{length}.");
            }
        }
        else
        {
            Console.WriteLine("All tests passed!");
        }
    }

    static Test<BigInteger>[] Tests =>
    [
        new([0, 0, 1], 20, [0, 0, 1, 1, 2, 4, 7, 13, 24, 44, 81, 149, 274, 504,
            927, 1_705, 3_136, 5_768, 10_609, 19_513]),
        new([21, 32, 43], 1, [21]),
        new([0, 0, 1], 0, []),
        new([10, 20, 30], 2, [10, 20]),
        new([10, 20, 30], 3, [10, 20, 30]),
        new([123, 456, 789], 8, [123, 456, 789, 1368, 2613, 4770, 8751, 16134]),
        new([0, 0, 1], 100, [0, 0, 1, 1, 2, 4, 7, 13, 24, 44, 81, 149, 274, 504,
            927, 1705, 3136, 5768, 10609, 19513, 35890, 66012, 121415, 223317,
            410744, 755476, 1389537, 2555757, 4700770, 8646064, 15902591,
            29249425, 53798080, 98950096, 181997601, 334745777, 615693474,
            1132436852, 2082876103, 3831006429, 7046319384, 12960201916,
            23837527729, 43844049029, 80641778674, 148323355432, 272809183135,
            501774317241, 922906855808, 1697490356184, 3122171529233,
            5742568741225, 10562230626642, 19426970897100, 35731770264967,
            65720971788709, 120879712950776, 222332455004452, 408933139743937,
            752145307699165, 1383410902447554, 2544489349890656,
            4680045560037375, 8607945812375585, 15832480722303616,
            29120472094716576, 53560898629395777, 98513851446415969,
            181195222170528322, 333269972246340068, 612979045863284359,
            1127444240280152749, 2073693258389777176, 3814116544533214284,
            7015254043203144209, 12903063846126135669,
            BigInteger.Parse("23732434433862494162"),
            BigInteger.Parse("43650752323191774040"),
            BigInteger.Parse("80286250603180403871"),
            BigInteger.Parse("147669437360234672073"),
            BigInteger.Parse("271606440286606849984"),
            BigInteger.Parse("499562128250021925928"),
            BigInteger.Parse("918838005896863447985"),
            BigInteger.Parse("1690006574433492223897"),
            BigInteger.Parse("3108406708580377597810"),
            BigInteger.Parse("5717251288910733269692"),
            BigInteger.Parse("10515664571924603091399"),
            BigInteger.Parse("19341322569415713958901"),
            BigInteger.Parse("35574238430251050319992"),
            BigInteger.Parse("65431225571591367370292"),
            BigInteger.Parse("120346786571258131649185"),
            BigInteger.Parse("221352250573100549339469"),
            BigInteger.Parse("407130262715950048358946"),
            BigInteger.Parse("748829299860308729347600"),
            BigInteger.Parse("1377311813149359327046015"),
            BigInteger.Parse("2533271375725618104752561"),
            BigInteger.Parse("4659412488735286161146176"),
            BigInteger.Parse("8569995677610263592944752"),
            BigInteger.Parse("15762679542071167858843489"),
            BigInteger.Parse("28992087708416717612934417")]),
    ];

    record Test<T>(T[] StartSequence, int Length, T[] Expected)
        where T : INumber<T>;

    record Failure<T>(T[] StartSequence, int Length) where T : INumber<T>;

    static string ArrayToString<T>(T[] array)
    {
        if (array is null)
        {
            return "null";
        }

        StringBuilder result = new("[");

        for (int i = 0; i < array.Length; i++)
        {
            if (i > 0)
            {
                result.Append(", ");
            }

            result.Append(array[i]?.ToString());
        }

        return result.Append(']').ToString();
    }
}
