using Microsoft.AspNetCore.Mvc;
using SpinutechCodeTest.Services;
using SpinutechCodeTest.Models;
using SpinutechCodeTest.Constants;

namespace SpinutechCodeTest.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly INumberToWordsService _numberToWordsService;
        private readonly IPokerHandService _pokerHandService;
        private readonly ISpiralService _spiralService;
        private readonly IGameOfLifeService _gameOfLifeService;
        private readonly ITemplateEngineService _templateEngineService;
        private readonly IPalindromeService _palindromeService;

        public ExercisesController(
            INumberToWordsService numberToWordsService,
            IPokerHandService pokerHandService,
            ISpiralService spiralService,
            IGameOfLifeService gameOfLifeService,
            ITemplateEngineService templateEngineService,
            IPalindromeService palindromeService)
        {
            _numberToWordsService = numberToWordsService;
            _pokerHandService = pokerHandService;
            _spiralService = spiralService;
            _gameOfLifeService = gameOfLifeService;
            _templateEngineService = templateEngineService;
            _palindromeService = palindromeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Exercise 1
        public IActionResult Exercise1()
        {
            var viewModel = new NumberToWordsViewModel
            {
                Title = AppConstants.ExerciseTitles.Exercise1,
                Description = AppConstants.ExerciseDescriptions.Exercise1
            };
            return View(viewModel);
        }

        // POST: Exercise 1
        [HttpPost]
        public IActionResult Exercise1(string amountInput)
        {
            var viewModel = new NumberToWordsViewModel
            {
                Title = AppConstants.ExerciseTitles.Exercise1,
                Description = AppConstants.ExerciseDescriptions.Exercise1
            };

            try
            {
                // Parse the input string to decimal
                if (string.IsNullOrWhiteSpace(amountInput))
                {
                    viewModel.ErrorMessage = AppConstants.ControllerMessages.PleaseEnterAmount;
                    return View(viewModel);
                }

                // Try to parse with different cultures
                decimal amount;
                if (!decimal.TryParse(amountInput, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out amount) &&
                    !decimal.TryParse(amountInput, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out amount))
                {
                    viewModel.ErrorMessage = AppConstants.ControllerMessages.InvalidAmountFormat;
                    return View(viewModel);
                }

                viewModel.Number = amount;

                // Validate input
                if (amount < 0)
                {
                    viewModel.ErrorMessage = AppConstants.ControllerMessages.AmountCannotBeNegative;
                    return View(viewModel);
                }

                if (amount > 999999999.99m)
                {
                    viewModel.ErrorMessage = AppConstants.ControllerMessages.AmountTooLarge;
                    return View(viewModel);
                }

                viewModel.Words = _numberToWordsService.ConvertToWords(amount);
                viewModel.Result = string.Format(AppConstants.ControllerMessages.TheNumberInWordsIs, amount, viewModel.Words);
            }
            catch (Exception ex)
            {
                viewModel.ErrorMessage = ex.Message;
            }

            return View(viewModel);
        }

        // GET: Exercise 2
        public IActionResult Exercise2()
        {
            var viewModel = new PokerHandViewModel
            {
                Title = AppConstants.ExerciseTitles.Exercise2,
                Description = AppConstants.ExerciseDescriptions.Exercise2
            };
            return View(viewModel);
        }

        // POST: Exercise 2
        [HttpPost]
        public IActionResult Exercise2(string handInput)
        {
            var viewModel = new PokerHandViewModel
            {
                Title = AppConstants.ExerciseTitles.Exercise2,
                Description = AppConstants.ExerciseDescriptions.Exercise2,
                Hand = handInput
            };

            try
            {
                var result = _pokerHandService.GetHandDescription(handInput);
                viewModel.Result = result;
                
                // Parse the result to extract hand type and rank
                var parts = result.Split(" - ");
                if (parts.Length >= 2)
                {
                    viewModel.HandType = parts[0];
                    viewModel.Description = parts[1];
                }
            }
            catch (Exception ex)
            {
                viewModel.ErrorMessage = ex.Message;
            }

            return View(viewModel);
        }

        // GET: Exercise 3
        public IActionResult Exercise3()
        {
            var viewModel = new SpiralViewModel
            {
                Title = AppConstants.ExerciseTitles.Exercise3,
                Description = AppConstants.ExerciseDescriptions.Exercise3
            };
            return View(viewModel);
        }

        // POST: Exercise 3
        [HttpPost]
        public IActionResult Exercise3(int maxNumber)
        {
            var viewModel = new SpiralViewModel
            {
                Title = AppConstants.ExerciseTitles.Exercise3,
                Description = AppConstants.ExerciseDescriptions.Exercise3,
                MaxNumber = maxNumber
            };

            try
            {
                viewModel.SpiralOutput = _spiralService.GenerateSpiral(maxNumber);
                viewModel.GridSize = (int)Math.Ceiling(Math.Sqrt(maxNumber + 1));
                viewModel.Result = $"Generated {maxNumber + 1}x{maxNumber + 1} spiral grid";
            }
            catch (Exception ex)
            {
                viewModel.ErrorMessage = ex.Message;
            }

            return View(viewModel);
        }

        // GET: Exercise 4
        public IActionResult Exercise4()
        {
            var viewModel = new GameOfLifeViewModel
            {
                Title = AppConstants.ExerciseTitles.Exercise4,
                Description = AppConstants.ExerciseDescriptions.Exercise4
            };
            return View(viewModel);
        }

        // POST: Exercise 4
        [HttpPost]
        public IActionResult Exercise4(string boardInput, int? generations = null)
        {
            var viewModel = new GameOfLifeViewModel
            {
                Title = AppConstants.ExerciseTitles.Exercise4,
                Description = AppConstants.ExerciseDescriptions.Exercise4,
                BoardInput = boardInput,
                Generations = generations
            };

            try
            {
                if (generations.HasValue && generations > 0)
                {
                    viewModel.FinalBoard = _gameOfLifeService.EvolveMultipleGenerations(boardInput, generations.Value);
                    viewModel.Result = $"Board after {generations} generations";
                }
                else
                {
                    viewModel.FinalBoard = _gameOfLifeService.NextGeneration(boardInput);
                    viewModel.Result = AppConstants.ControllerMessages.NextGenerationBoard;
                }
            }
            catch (Exception ex)
            {
                viewModel.ErrorMessage = ex.Message;
            }

            return View(viewModel);
        }

        // GET: Exercise 5
        public IActionResult Exercise5()
        {
            var viewModel = new TemplateEngineViewModel
            {
                Title = AppConstants.ExerciseTitles.Exercise5,
                Description = AppConstants.ExerciseDescriptions.Exercise5
            };
            return View(viewModel);
        }

        // POST: Exercise 5
        [HttpPost]
        public IActionResult Exercise5(string template, string variables)
        {
            var viewModel = new TemplateEngineViewModel
            {
                Title = AppConstants.ExerciseTitles.Exercise5,
                Description = AppConstants.ExerciseDescriptions.Exercise5,
                Template = template,
                Variables = variables
            };

            try
            {
                var variablesDict = ParseVariables(variables);
                viewModel.ProcessedTemplate = _templateEngineService.ProcessTemplate(template, variablesDict);
                viewModel.ExtractedVariables = _templateEngineService.ExtractVariables(template);
                viewModel.Result = AppConstants.ControllerMessages.TemplateProcessedSuccessfully;
            }
            catch (Exception ex)
            {
                viewModel.ErrorMessage = ex.Message;
            }

            return View(viewModel);
        }

        // GET: Exercise 6
        public IActionResult Exercise6()
        {
            var viewModel = new PalindromeViewModel
            {
                Title = AppConstants.ExerciseTitles.Exercise6,
                Description = AppConstants.ExerciseDescriptions.Exercise6
            };
            return View(viewModel);
        }

        // POST: Exercise 6
        [HttpPost]
        public IActionResult Exercise6(int number, int? startRange = null, int? endRange = null)
        {
            var viewModel = new PalindromeViewModel
            {
                Title = AppConstants.ExerciseTitles.Exercise6,
                Description = AppConstants.ExerciseDescriptions.Exercise6,
                Number = number,
                StartRange = startRange,
                EndRange = endRange
            };

            try
            {
                if (startRange.HasValue && endRange.HasValue)
                {
                    viewModel.PalindromesInRange = _palindromeService.GetPalindromesInRange(startRange.Value, endRange.Value);
                    viewModel.Result = string.Format(AppConstants.ControllerMessages.FoundPalindromesInRange, viewModel.PalindromesInRange.Count, startRange, endRange);
                }
                else
                {
                    viewModel.IsPalindrome = _palindromeService.IsPalindrome(number);
                    viewModel.Result = string.Format(AppConstants.ControllerMessages.IsPalindromeResult, number, viewModel.IsPalindrome.Value ? AppConstants.ControllerMessages.IsAPalindrome : AppConstants.ControllerMessages.IsNotAPalindrome);
                    
                    if (!viewModel.IsPalindrome.Value)
                    {
                        viewModel.NextPalindrome = _palindromeService.GetNextPalindrome(number);
                    }
                }
            }
            catch (Exception ex)
            {
                viewModel.ErrorMessage = ex.Message;
            }

            return View(viewModel);
        }

        private Dictionary<string, string> ParseVariables(string variables)
        {
            var result = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(variables)) return result;

            var lines = variables.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var parts = line.Split('=', 2);
                if (parts.Length == 2)
                {
                    result[parts[0].Trim()] = parts[1].Trim();
                }
            }
            return result;
        }
    }
} 