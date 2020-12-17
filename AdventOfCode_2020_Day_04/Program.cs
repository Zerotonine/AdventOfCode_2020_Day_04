using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

/*
 * Solution for the 4th advent of code challenge 2020
 * Find out more about advent of code @ https://adventofcode.com/
 * Yes it's a weird solution but it works :D
 */
namespace AdventOfCode_2020_Day_04
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> inputList = new List<string>(
                File.ReadAllText("input.txt").Split(Environment.NewLine + Environment.NewLine)
                );

            for (int i = 0; i < inputList.Count; i++)
            {
                inputList[i] = inputList[i].Replace(Environment.NewLine, " ");
            }

            Console.WriteLine("Solution One: " + PuzzleOne(inputList));
            Console.WriteLine("Solution Two: " + PuzzleTwo(inputList));
            Console.ReadKey(true);
        }


        static int PuzzleOne(in List<string> input)
        {
            int validPassports = 0;
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                { "byr", ""},
                { "iyr", ""},
                { "eyr", ""},
                { "hgt", ""},
                { "hcl", ""},
                { "ecl", ""},
                { "pid", ""},
                //{ "cid", ""}
            };
            
            foreach(string s in input)
            {
                foreach(string key in pairs.Keys.ToList())
                {
                    pairs[key] = null;
                }

                pairs["byr"] = Regex.Match(s, @"byr:[\w#]+").Value;
                pairs["iyr"] = Regex.Match(s, @"iyr:[\w#]+").Value;
                pairs["eyr"] = Regex.Match(s, @"eyr:[\w#]+").Value;
                pairs["hgt"] = Regex.Match(s, @"hgt:[\w#]+").Value;
                pairs["hcl"] = Regex.Match(s, @"hcl:[\w#]+").Value;
                pairs["ecl"] = Regex.Match(s, @"ecl:[\w#]+").Value;
                pairs["pid"] = Regex.Match(s, @"pid:[\w#]+").Value;
                //cid doesn't matter
                if (!pairs.Values.Any(s => String.IsNullOrEmpty(s)))
                    validPassports++;
            }
            return validPassports;
        }

        static int PuzzleTwo(in List<string> input)
        {
            int validPassports = 0;
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                { "byr", ""},
                { "iyr", ""},
                { "eyr", ""},
                { "hgt", ""},
                { "hcl", ""},
                { "ecl", ""},
                { "pid", ""},
                //{ "cid", ""}
            };

            foreach (string s in input)
            {
                foreach (string key in pairs.Keys.ToList())
                {
                    pairs[key] = null;
                }

                pairs["byr"] = Regex.Match(s, @"byr:[0-9]{4}").Value?.Replace("byr:", "");
                pairs["iyr"] = Regex.Match(s, @"iyr:[0-9]{4}").Value?.Replace("iyr:", "");
                pairs["eyr"] = Regex.Match(s, @"eyr:[0-9]{4}").Value?.Replace("eyr:", "");
                pairs["hgt"] = Regex.Match(s, @"hgt:[0-9]{2,3}(cm|in)").Value?.Replace("hgt:", "");
                pairs["hcl"] = Regex.Match(s, @"hcl:#[a-f0-9]{6}").Value?.Replace("hcl:", "");
                pairs["ecl"] = Regex.Match(s, @"ecl:(amb|blu|brn|gry|grn|hzl|oth)").Value?.Replace("ecl:", "");
                pairs["pid"] = Regex.Match(s, @"pid:[0-9]+").Value?.Replace("pid:", "");
                //cid doesn't matter
                if (pairs.Values.Any(s => String.IsNullOrEmpty(s)))
                    continue;

                if (validateDataset(pairs))
                    validPassports++;
            }

            return validPassports;
        }

        static bool validateDataset(in Dictionary<string, string> pairs)
        {
            int byr = Int32.Parse(pairs["byr"]);
            if (byr < 1920 || byr > 2002)
                return false;

            int iyr = Int32.Parse(pairs["iyr"]);
            if (iyr < 2010 || iyr > 2020)
                return false;

            int eyr = Int32.Parse(pairs["eyr"]);
            if (eyr < 2020 || eyr > 2030)
                return false;

            string hgt = pairs["hgt"];
            string munit = hgt.Substring(hgt.Length - 2);
            int iHgt = Int32.Parse(hgt.Remove(hgt.Length - 2));
            
            if (munit == "cm")
            {
                if (iHgt < 150 || iHgt > 193)
                    return false;
            }
            else
            {
                if (iHgt < 59 || iHgt > 76)
                    return false;
            }

            if (pairs["pid"].Length != 9)
                return false;

            return true;
        }
    }
}
