namespace SpinutechCodeTest.Constants
{
    public static class AppConstants
    {
        public static class ExerciseTitles
        {
            public const string Exercise1 = "Exercise 1 - Number to Words Converter";
            public const string Exercise2 = "Exercise 2 - Poker Hand Evaluator";
            public const string Exercise3 = "Exercise 3 - Spiral Number Generator";
            public const string Exercise4 = "Exercise 4 - Game of Life";
            public const string Exercise5 = "Exercise 5 - Template Engine";
            public const string Exercise6 = "Exercise 6 - Palindrome Checker";
        }

        public static class ExerciseDescriptions
        {
            public const string Exercise1 = "Convert numbers to their word representation in English.";
            public const string Exercise2 = "Evaluate poker hands and determine their rank and type.";
            public const string Exercise3 = "Generate a spiral pattern of numbers starting from the center.";
            public const string Exercise4 = "Evolve generations through the game of life.";
            public const string Exercise5 = "Process templates with variable substitution.";
            public const string Exercise6 = "Check if a number is a palindrome.";
        }

        public static class NumberToWords
        {
            public const string ZeroDollars = "Zero dollars";
            public const string Dollars = " dollars";
            public const string Hundred = " hundred ";
            public const string And = " and ";
            public const string Slash100 = "/100";
            public const string Space = " ";
        }

        public static class PokerHand
        {
            public const string RoyalFlush = "Royal Flush";
            public const string StraightFlush = "Straight Flush";
            public const string FourOfAKind = "Four of a Kind";
            public const string FullHouse = "Full House";
            public const string Flush = "Flush";
            public const string Straight = "Straight";
            public const string ThreeOfAKind = "Three of a Kind";
            public const string TwoPair = "Two Pair";
            public const string Pair = "Pair of";
            public const string HighCard = "High Card";
            public const string Over = "s over ";
            public const string And = "s and ";
            public const string Of = " of a Kind (";
            public const string ClosingParenthesis = ")";
            public const string Error = "Error: ";
            public const string HandMustContainExactly5Cards = "Hand must contain exactly 5 cards";
            public const string InvalidCardFormat = "Invalid card format: ";
            public const string InvalidRank = "Invalid rank: ";
            public const string InvalidSuit = "Invalid suit: ";
        }

        public static class GameOfLife
        {
            public const string EmptyBoardInput = "Empty board input";
            public const string RowHasDifferentNumberOfColumns = "Row {0} has different number of columns";
            public const string NumberOfGenerationsMustBeNonNegative = "Number of generations must be non-negative";
            public const string Error = "Error: ";
        }

        public static class TemplateEngine
        {
            public const string UnclosedVariablePlaceholder = "Unclosed variable placeholder starting at position {0}";
            public const string EmptyVariableName = "Empty variable name at position {0}";
            public const string VariableNotDefined = "Variable '{0}' is not defined";
            public const string DollarSign = "$";
            public const string OpeningBrace = "{";
            public const string ClosingBrace = "}";
        }

        public static class ControllerMessages
        {
            public const string PleaseEnterAmount = "Please enter an amount.";
            public const string InvalidAmountFormat = "Invalid amount format. Please use numbers and decimal point (e.g., 2523.04).";
            public const string AmountCannotBeNegative = "Amount cannot be negative.";
            public const string AmountTooLarge = "Amount is too large. Maximum allowed is 999,999,999.99";
            public const string TheNumberInWordsIs = "The number {0:F2} in words is: {1}";
            public const string NextGenerationBoard = "Next generation board";
            public const string TemplateProcessedSuccessfully = "Template processed successfully";
            public const string IsAPalindrome = "a palindrome";
            public const string IsNotAPalindrome = "not a palindrome";
            public const string FoundPalindromesInRange = "Found {0} palindromes in range {1}-{2}";
            public const string IsPalindromeResult = "{0} is {1}";
        }

        public static class ValidationMessages
        {
            public const string Required = "This field is required.";
            public const string InvalidFormat = "Invalid format.";
            public const string OutOfRange = "Value is out of range.";
        }

        public static class UIText
        {
            public const string Home = "Home";
            public const string Exercises = "Exercises";
            public const string Privacy = "Privacy";
            public const string BackToHome = "Back to Home";
            public const string NextExercise = "Next Exercise";
            public const string PreviousExercise = "Previous Exercise";
            public const string TryItOut = "Try It Out";
            public const string ProblemDescription = "Problem Description";
            public const string Example = "Example";
            public const string Result = "Result";
            public const string Error = "Error";
            public const string Examples = "Examples";
            public const string KeyFeatures = "Key Features";
            public const string QuickExamples = "Quick Examples";
            public const string Features = "Features";
        }

        public static class FormLabels
        {
            public const string EnterAmount = "Enter Amount:";
            public const string EnterPokerHand = "Enter Poker Hand:";
            public const string EnterMaxNumber = "Enter Maximum Number:";
            public const string EnterBoard = "Enter Board:";
            public const string EnterGenerations = "Enter Number of Generations:";
            public const string Template = "Template:";
            public const string Variables = "Variables:";
            public const string EnterNumber = "Enter Number:";
            public const string StartRange = "Start Range:";
            public const string EndRange = "End Range:";
        }

        public static class Placeholders
        {
            public const string AmountExample = "e.g., 2523.04";
            public const string PokerHandExample = "e.g., Ah As 10c 7d 6s";
            public const string MaxNumberExample = "e.g., 24";
            public const string BoardExample = "0 1 0 0 0\n1 0 0 1 1\n1 1 0 0 1\n0 1 0 0 0\n1 0 0 0 1";
            public const string GenerationsExample = "e.g., 3";
            public const string TemplateExample = "${name} has an appointment on ${day} at ${time}";
            public const string VariablesExample = "name=Billy\nday=Thursday\ntime=2:30 PM";
            public const string NumberExample = "e.g., 121";
            public const string RangeExample = "e.g., 100";
        }

        public static class HelpText
        {
            public const string EnterDecimalNumber = "Enter a decimal number to convert to words";
            public const string PokerHandFormat = "Format: [Rank][Suit] [Rank][Suit] [Rank][Suit] [Rank][Suit] [Rank][Suit]\nRanks: 2-9, T(10), J, Q, K, A | Suits: H(Hearts), D(Diamonds), C(Clubs), S(Spades)";
            public const string MaxNumberHelp = "Enter a non-negative integer to generate spiral";
            public const string BoardFormat = "Enter board as 0s and 1s, one row per line";
            public const string GenerationsHelp = "Enter number of generations to evolve";
            public const string TemplateSyntax = "Use ${variable} syntax. For escaped variables, use ${${variable}}";
            public const string VariablesFormat = "Define variables in format: variableName=value (one per line)";
            public const string NumberHelp = "Enter a positive integer to check if it's a palindrome";
            public const string RangeHelp = "Enter start and end numbers to find palindromes in range";
        }

        public static class ButtonText
        {
            public const string ConvertToWords = "Convert to Words";
            public const string EvaluateHand = "Evaluate Hand";
            public const string GenerateSpiral = "Generate Spiral";
            public const string EvolveBoard = "Evolve Board";
            public const string ProcessTemplate = "Process Template";
            public const string CheckPalindrome = "Check Palindrome";
            public const string FindPalindromes = "Find Palindromes";
            public const string ViewExercise = "View Exercise";
        }

        public static class CardColors
        {
            public const string Primary = "bg-primary";
            public const string Success = "bg-success";
            public const string Warning = "bg-warning";
            public const string Danger = "bg-danger";
            public const string Info = "bg-info";
            public const string Secondary = "bg-secondary";
        }

        public static class BootstrapClasses
        {
            public const string TextWhite = "text-white";
            public const string TextDark = "text-dark";
            public const string BtnPrimary = "btn-primary";
            public const string BtnSuccess = "btn-success";
            public const string BtnOutlineLight = "btn-outline-light";
            public const string BtnLight = "btn-light";
            public const string AlertSuccess = "alert-success";
            public const string AlertDanger = "alert-danger";
            public const string BgLight = "bg-light";
        }
    }
} 