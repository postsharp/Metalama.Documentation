using Metalama.Framework.Aspects;
using Metalama.Patterns.Contracts;
using System;
using System.Text.RegularExpressions;
namespace Doc.Contracts.RefParameter
{
  public interface IWordCounter
  {
    void CountWords(string text, [Positive(Direction = ContractDirection.Both)] ref int wordCount);
  }
  public class WordCounter : IWordCounter
  {
    public void CountWords(string text, ref int wordCount)
    {
      if (wordCount is < 0)
      {
        throw new ArgumentOutOfRangeException("wordCount", "The 'wordCount' parameter must be greater than or equal to 0.");
      }
      var regex = new Regex(@"\b\w+\b");
      wordCount += regex.Matches(text).Count;
      if (wordCount is < 0)
      {
        throw new PostconditionViolationException("The 'wordCount' parameter must be greater than or equal to 0.");
      }
    }
  }
}