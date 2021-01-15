using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GameLibraryProject.Models
{
    public class BadWordsValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var game = (Game)validationContext.ObjectInstance;
            string text = game.Summary;
            bool cond = true;
            var BadWords = new[] { "fudge", "poop", "bad" };

            if (text == null);
            else
            if (CheckText(text, BadWords)) cond = false;

            return cond ? ValidationResult.Success : new ValidationResult("The text contains banned words!");
        }
        private bool CheckText(string content, string[] badWords)
        {
            foreach (var badWord in badWords)
            {
                var regex = new Regex("(^|[\\?\\.,\\s])" + badWord + "([\\?\\.,\\s]|$)");
                if (regex.IsMatch(content)) return true;
            }
            return false;
        }
    }
}