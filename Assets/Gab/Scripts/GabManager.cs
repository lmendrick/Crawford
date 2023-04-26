using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace Gab.Scripts
{
    /// <summary>
    /// ConversateManager
    /// usage:
    /// ConversateManager.StartNew(conversation);
    /// ConversateManager.SelectNext(); //SelectPrevious()
    /// ConversateManager.Advance();
    /// ConversateManager.SetValue("coins", myCoinAmount)
    /// int myCoinsCopy = Conversate.GetValue("coins");
    /// To subscribe to events (event Actions)
    /// void OnEnable() { ConversateManager.OnVariableChanged += myMethod; }
    /// void OnDisable() { ConversateManager.OnVariableChanged -= myMethod; }
    /// void myMethod(string variableName, int newValue) { }
    /// </summary>
    [IncludeInSettings(true)]
    [SuppressMessage("ReSharper", "StringIndexOfIsCultureSpecific.1")]
    [SuppressMessage("ReSharper", "StringIndexOfIsCultureSpecific.2")]
    public class GabManager : SingletonPersistent<GabManager>
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _panel;
        [SerializeField] private GabVariableSetSo _initialVariableSet;

        public static bool IsShowingConversation => _isShowingConversation;
        private static bool _isShowingConversation = false;
        
        //C# event Actions (as alternative to Unity Events which show in inspector)
        public static event Action<string, int> OnVariableChanged; //string: name of variable changed, int: new value
        public static event Action<bool> OnConversationEnded; //bool: variable changes were made
        public static event Action<string> OnConversationStarted; //string: name of conversation
        
        ////Unity events (as alternative to C# event Actions which are only subscribable in code)
        //[SerializeField] private UnityEvent<string> OnVariableChanged; //string: name of variable changed
        //[SerializeField] private UnityEvent<bool> OnConversationEnded; //bool: variable changes were made
        //[SerializeField] private UnityEvent<string> OnConversationStarted; //string: name of conversation
        
        [Header("Optional Start Conversation")] [SerializeField]
        private GabConversationSo _conversation;

        private const string PassageStylePrefix = "<color=#fff><alpha=#ff>";
        private const string PassageStyleSuffix = "</color><alpha=#ff>";
        private const string OptionStylePrefix = "<alpha=#00>> <color=#fff><alpha=#aa>";
        private const string OptionStyleSuffix = "</color><alpha=#ff>";
        private const string SelectedOptionStylePrefix = "<color=#fff><alpha=#ff>> </color><color=#fff>";
        private const string SelectedOptionStyleSuffix = "</color><alpha=#ff>";

        private string _passageCurrent;
        private int _linkSelected = -1;
        private string _linkedPassageName = "";

        private List<GabLink> _links = new();
        private bool _variableChangesMade;

        [HideInInspector]
        public NumberDictionary NumberVariables = new();




        //static methods for Visual Scripting access
        
        /// <summary>
        /// Starts a new conversation, including interpreting the Start passage and showing the panel
        /// </summary>
        /// <param name="conversation"></param>
        public static void StartNew(GabConversationSo conversation)
        {
            GabManager.Instance.StartNewImpl(conversation);
        }

        /// <summary>
        /// Ends a conversation immediately
        /// </summary>
        public static void End()
        {
            GabManager.Instance.EndImpl();
        }

        /// <summary>
        /// Advances to the next passage using the selected option (or ends conversation)
        /// </summary>
        public static void Advance()
        {
            GabManager.Instance.AdvanceImpl();
        }

        /// <summary>
        /// Selects the next link option, wrapping around to lowest if at highest
        /// </summary>
        public static void SelectNext()
        {
            GabManager.Instance.SelectNextImpl();
        }

        /// <summary>
        /// Selects the previous link option, wrapping around to highest if at lowest
        /// </summary>
        public static void SelectPrevious()
        {
            GabManager.Instance.SelectPreviousImpl();
        }

        /// <summary>
        ///  Gets the integer value of a variable using its name as a key
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns>value of variable and/or 0 if not found</returns>
        public static int GetValue(string variableName)
        {
            return GabManager.Instance.GetValueImpl(variableName);
        }

        /// <summary>
        /// Sets a variable to a new value (creates a variable if it does not already exist)
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="newValue"></param>
        public static void SetValue(string variableName, int newValue)
        {
            GabManager.Instance.SetValueImpl(variableName, newValue);
        }

        /// <summary>
        /// Changes the value of a variable by delta (use negative numbers to subtract)
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="delta"></param>
        public static void ChangeValue(string variableName, int delta)
        {
            GabManager.Instance.ChangeValueImpl(variableName, delta);
        }

        void Start()
        {

            //copy the starting variables into the NumberVariables dictionary
            if (_initialVariableSet != null)
            {
                foreach (KeyValuePair<string, int> kvp in _initialVariableSet.numberDictionary)
                {
                    if (!NumberVariables.TryAdd(kvp.Key, kvp.Value))
                        print("The starting variable set tried to add a variable that already exists (ConversateManager)");
                }                
            }
            
            if (_text == null) print("No text object is referenced");
            
            if (_panel == null) print("No panel object is referenced");
            else _panel.SetActive(false); //start hidden unless a conversation is referenced

            //if there is a conversation scriptable object referenced, start things up
            if (_conversation != null)
                StartNewImpl(_conversation);
        }

        private void ChangeValueImpl(string variableName, int delta)
        {
            SetValueImpl(variableName, GetValueImpl(variableName) + delta);
        }

        private void SetValueImpl(string variableName, int newValue)
        {
            //if it doesn't exist already, create it and set it
            if (NumberVariables.TryAdd(variableName, newValue)) return;
            
            //it exists, so get rid of it and replace it with same name, new value
            NumberVariables.Remove(variableName);
            if (NumberVariables.TryAdd(variableName, newValue)) return;
            
            //if we get here, I don't know how, but we should find out!
            print($"Unable to set value with variable name: {variableName}");
        }
        
        private int GetValueImpl(string variableName)
        {
            if (NumberVariables.TryGetValue(variableName, out var value))
                return value;
            print($"Requested variable, {variableName} not found, returned zero (ConversateManager.GetValue())");
            return 0;
        }

        private void SelectNextImpl()
        {
            if (_links.Count <= 0) return;
            _linkSelected = (_linkSelected + 1) % _links.Count;
            _linkedPassageName = _links[_linkSelected].name;
            _text.text = GetFormattedPassage(GetPassageText(), false);
        }

        private void SelectPreviousImpl()
        {
            if (_links.Count <= 0) return;
            _linkSelected = (_linkSelected + _links.Count - 1) % _links.Count;
            _linkedPassageName = _links[_linkSelected].name;
            _text.text = GetFormattedPassage(GetPassageText(), false);
        }

        private void AdvanceImpl()
        {
            if (_conversation == null) return;

            if (_links.Count == 0 || _linkedPassageName == "")
            {
                //no options means we are done with conversation
                EndImpl();
                return;
            }

            _passageCurrent = _linkedPassageName;
            _linkSelected = -1;

            if (!SetupNewPassage())
            {
                Debug.Log("Had to close panel due to error (ConversateManager)");
                EndImpl();
            }
        }

        private void StartNewImpl(GabConversationSo newConversation)
        {
            _conversation = newConversation;
            _passageCurrent = "Start";
            _linkedPassageName = "";
            _variableChangesMade = false;
            
            if (SetupNewPassage())
            {
                _panel.SetActive(true);
                _isShowingConversation = true;
                EventBus.Trigger(GabEventNames.ConversationStarted, newConversation.name); //visual scripting event
                OnConversationStarted?.Invoke(newConversation.name);
            }
            else
            {
                Debug.Log("ConversateManager.StartNew(): First passage must have name \"Start\"");
                EndImpl();
            }
        }

        private bool SetupNewPassage()
        {
            string encodedPassageText = GetPassageText();
            if (encodedPassageText == "")
            {
                return false;
            }

            // _links = GetLinks(encodedPassageText);
            
            //reset links because this is a new passage
            _links.Clear(); 
            
            _text.text = GetFormattedPassage(encodedPassageText, true);
            
            if (_links.Count > 0)
            {
                _linkSelected = 0;
                _linkedPassageName = _links[_linkSelected].name;
            }
            return true;
        }

        private string GetPassageText()
        {
            if (_conversation.passageDictionary.TryGetValue(_passageCurrent, out var newText))
            {
                return newText;
            }

            Debug.Log($"ConversateManager.GetPassageText(): Name, {_passageCurrent}, not found in this conversation");
            return "";
        }

        private void EndImpl()
        {
            _panel.SetActive(false);
            _isShowingConversation = false;
            EventBus.Trigger(GabEventNames.ConversationEnded, _variableChangesMade); //visual scripting event
            OnConversationEnded?.Invoke(_variableChangesMade);
        }
        
        private static string[] TokenizePassageShallow(string encodedPassage)
        {
            //This regex splits only the first level of
            //  single newlines
            //  regular text,
            //  $variables,
            //  outer parenthesis (macros),
            //  and outer square brackets (blocks/hooks)
            //  Also creates null/empty strings and series of spaces between ) and [
            //https://regex101.com/r/ryMb5p/1
            //with help from: https://stackoverflow.com/questions/546433/regular-expression-to-match-balanced-parentheses
            //with example var outerParenthesisOnly = new Regex(@"\((?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!))\)");
            var re = new Regex(@"(\n)|(\((?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!))\))|(\[(?>\[(?<c>)|[^\[\]]+|\](?<-c>))*(?(c)(?!))\])|(\$\w*\b)|([^[)($\]\n]*)");
            // foreach (var s in re.Split(encodedPassage).Where(x => !string.IsNullOrEmpty(x)).ToArray())
            // {
            //     print($"token: {s}");
            // }
            return re.Split(encodedPassage)
                .Where(x => !string.IsNullOrEmpty(x) && !Regex.IsMatch(x, @"^[ ]+$"))
                .ToArray();
        }
        
        private string GetFormattedPassage(string encodedPassageText, bool resetVarsAndAddLinks)
        {
            List<string> visibleParts = new();
            var showNextBlock = true;
            var ifElseStructureComplete = true;
            var previousTokenWantsNextNewline = false;
            //var tokenIndex = 1;
            foreach (var token in TokenizePassageShallow(encodedPassageText))
            {
                //print($"token ${tokenIndex++} is '{token.Replace(" ", "_")}'");
                
                //if token is just a newline
                if (token is "\n")
                {
                    if (previousTokenWantsNextNewline)
                    {
                        previousTokenWantsNextNewline = false; //reset
                        visibleParts.Add("\n");
                    }
                    continue;
                }
                
                //if token is just spaces (and not part of a paragraph) skip
                //These come from leftover spaces between (if: ) and the next [, for instance.
                //(https://regex101.com/r/vV7ZAU/1)
                if (Regex.IsMatch(token, @"^[ ]+$"))
                {
                    //don't count as a token
                    //don't change status of previousTokenWantsNextNewline
                    continue;
                }

                //macro: starts with (
                if (token.StartsWith('('))
                {
                    previousTokenWantsNextNewline = false;

                    var colonIndex = token.IndexOf(':');
                    if (colonIndex < 1) continue; //just skip adding this whole token

                    string expressionString = token[(colonIndex+1)..^1]; //after ':' to just before ')' (could be ':')
                    
                    string macroType = token[1..colonIndex].Trim().ToLower(); //between ( and :
                    switch (macroType)
                    {
                        case "if":
                            ifElseStructureComplete = false; //starting a new if/else structure
                            showNextBlock = EvaluateBooleanExpression(expressionString);
                            continue;
                        case "elseif":
                            showNextBlock = EvaluateBooleanExpression(expressionString);
                            continue;
                        case "else":
                            if (!ifElseStructureComplete) showNextBlock = true; //nothing else was done,so set this to true, so last block executes
                            continue;
                        case "set":
                            if (!resetVarsAndAddLinks) continue;
                            if (!ProcessSetMacro(expressionString))
                            {
                                print($"ConversateManager.GetFormattedPassage(): Set macro failed, {token}");
                            }
                            continue;
                        default:
                            continue; //no implemented macro for the type
                    }
                }

                //hook/block or link: starts with [
                if (token.StartsWith('['))
                {
                    //link
                    if (token.StartsWith("[[") && token.EndsWith("]]") && !token.StartsWith("[[["))
                    {
                        previousTokenWantsNextNewline = true;
                        GabLink link = GetLink(token);

                        if (resetVarsAndAddLinks)
                        {
                            _links.Add(link);
                            if (_links.Count == 1) _linkSelected = 0; //if we are adding links, make the first selected
                        }
                            
                        visibleParts.Add(GetFormattedLinkText(link));
                        continue;
                    }
                    
                    if (ifElseStructureComplete || !showNextBlock)
                    {
                        previousTokenWantsNextNewline = false;
                        //we may not be done (an elseif or else could be coming) so ifElseStructureComplete remains unchanged
                        continue; //skip this block because previous if resulted in false
                    }
                        
                    //showNextBlock must be true at this point
                    if (token.EndsWith(']'))
                    {
                        previousTokenWantsNextNewline = false;
                        //it is a hook/block that should be processed and added
                        var startIndex = token.StartsWith("[\n") ? 2 : 1; //remove \n if it is right after [
                        var hookContent = token[startIndex..^1]; //remove [ and ]
                        visibleParts.Add(GetFormattedPassage(hookContent, resetVarsAndAddLinks));
                        ifElseStructureComplete = true; //whether we hit this from if, elseif, or else, we are done
                        continue;
                    }
                }

                //variable within text
                if (token.StartsWith('$'))
                {
                    previousTokenWantsNextNewline = true;
                    visibleParts.Add("" + GetValueImpl(token[1..])); //string add value of name without $
                    continue;
                }

                //all that should be left is regular text between, so add it to the visibleParts
                previousTokenWantsNextNewline = true;
                visibleParts.Add(PassageStylePrefix + token + PassageStyleSuffix);
            }
            
            //construct formatted passage using ordered visibleParts
            var formattedPassage = "";
            foreach (var part in visibleParts)
            {
                formattedPassage += part;
            }

            return formattedPassage;
            //return visibleParts.Aggregate("", (current, part) => current + part);
        }

        private static GabLink GetLink(string encodedLink)
        {
            if (encodedLink.StartsWith("[[") && encodedLink.EndsWith("]]"))
            {
                encodedLink = encodedLink[2..^2]; //remove [[ and ]]
            }

            string linkText, linkName;
            int indexOfArrow = encodedLink.IndexOf("->");
            if (indexOfArrow < 0)
            {
                linkText = encodedLink;
                linkName = linkText;
            }
            else
            {
                linkText = encodedLink[..indexOfArrow];
                if (linkText == "more") linkText = ""; //hide the more link
                linkName = encodedLink[(indexOfArrow + 2)..];
            }
            return new GabLink(linkText, linkName);
        }
        
        private string GetFormattedLinkText(GabLink link)
        { 
            if (link.text == "" 
                || _links[_linkSelected].text != link.text 
                || _links[_linkSelected].name != link.name)
                return OptionStylePrefix + link.text + OptionStyleSuffix;
            return SelectedOptionStylePrefix + link.text + SelectedOptionStyleSuffix;
        }

        private bool ProcessSetMacro(string setString)
        {
            //read variable name ($ to space)
            //Match $ all word characters up but to space (word characters are A-Za-z0-9_)
            //https://regex101.com/r/fP9KSj/1 but with space instead of boundary \b
            Regex re = new Regex(@"\$\w* ");

            var match = re.Match(setString);
            if (!match.Success)
            {
                print($"ConversateManager.ProcessSetMacro(): No variable found in set macro (use $ before variable name), ({setString})");
                return false;
            }
            var variableName = match.ToString();
            variableName = variableName[1..^1]; //remove $ and space
 
            //isolate expression (to the right of "to" found starting after variable name)
            var afterNameIndex = match.Index + match.Length;
            var startIndex = setString.ToLower().IndexOf("to", afterNameIndex) + 2;
            if (startIndex >= setString.Length)
            {
                print($"ConversateManager.ProcessSetMacro(): set macro has no expression after \"to\", ({setString})");
                return false;
            }

            var expression = setString[startIndex..]; //everything after "to"

            int result;
            //if it is true or false convert to integer 1 or 0 respectively
            if (bool.TryParse(expression.Trim(), out var boolValue))
            {
                result = boolValue ? 1 : 0;
            }
            else
            {
                //evaluate expression
                result = EvaluateNumberExpression(expression);
            }

            //remove and add variable with same name new result
            NumberVariables.Remove(variableName);
            NumberVariables.Add(variableName, result);

            //we made a change to a variable, remember for Conversation Ended event
            _variableChangesMade = true;
            
            //Trigger Visual Scripting event: On Variable Changed
            EventBus.Trigger(GabEventNames.VariableChanged, variableName);
            //OnVariableChanged?.Invoke(variableName); //for Unity events
            OnVariableChanged?.Invoke(variableName, result);

            return true;
        }

        private static bool EvaluateBooleanExpression(string expression)
        {
            //print("eval boolean expression: " + expression);
            
            Regex re = new Regex(@"\((?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!))\)");

            expression = expression.Trim();
            var parenMatches = re.Matches(expression);

            if (parenMatches.Count > 0)
            {
                // foreach (var e in parenMatches)
                // {
                //     print(e.ToString());
                // }

                foreach (var pMatch in parenMatches)
                {
                    //send this parenthetical to be evaluated (without parenthesis)
                    var subexpression = pMatch.ToString();
                    var subexpressionNoParens = subexpression[1..^1];
                    expression =
                        expression.Replace(subexpression,
                            "" + EvaluateBooleanExpression(subexpressionNoParens));
                }
            }
            
            //first check logical operator and
            var m = Regex.Match(expression.ToLower(), @"&&| and ");
            var indexOfFirstOperator = m.Success ? m.Index : -1;
            if (indexOfFirstOperator > 0)
            {
                int indexAfterOperator = indexOfFirstOperator + m.ToString().Length;
                string leftExp = expression[..indexOfFirstOperator];
                string rightExp = expression[indexAfterOperator..];
                return EvaluateBooleanExpression(leftExp) && EvaluateBooleanExpression(rightExp);
            }

            //now check for logical or
            m = Regex.Match(expression.ToLower(), @"\|\|| or ");
            indexOfFirstOperator = m.Success ? m.Index : -1;
            if (indexOfFirstOperator > 0)
            {
                int indexAfterOperator = indexOfFirstOperator + m.ToString().Length;
                string leftExp = expression[..indexOfFirstOperator];
                string rightExp = expression[indexAfterOperator..];
                return EvaluateBooleanExpression(leftExp) || EvaluateBooleanExpression(rightExp);
            }
            
            //now check for relational operators
            m = Regex.Match(expression.ToLower(), @" is not |!=| is |==|>=|>|<=|<|=");
            indexOfFirstOperator = m.Success ? m.Index : -1;
            if (indexOfFirstOperator > 0)
            {
                string relationalOperator = m.ToString();
                int indexAfterOperator = indexOfFirstOperator + relationalOperator.Length;
                
                string leftExp = expression[..indexOfFirstOperator];
                string rightExp = expression[indexAfterOperator..];
                
                if (relationalOperator is " is not " or "!=")
                    return EvaluateNumberExpression(leftExp) != EvaluateNumberExpression(rightExp);
                if (relationalOperator is " is " or "==" or "=")
                    return EvaluateNumberExpression(leftExp) == EvaluateNumberExpression(rightExp);
                switch (relationalOperator)
                {
                    case ">=": return EvaluateNumberExpression(leftExp) >= EvaluateNumberExpression(rightExp);
                    case ">": return EvaluateNumberExpression(leftExp) > EvaluateNumberExpression(rightExp);
                    case "<=": return EvaluateNumberExpression(leftExp) <= EvaluateNumberExpression(rightExp);
                    case "<": return EvaluateNumberExpression(leftExp) < EvaluateNumberExpression(rightExp);
                }
            }

            if (bool.TryParse(expression, out var boolValue)) return boolValue;

            if (int.TryParse(expression, out var intValue)) 
                return intValue > 0; //1 and up is interpreted as true, and 0 and below as false
            
            //hopefully we never get here.
            //Right now, there are no bool variables, but if added, they would also have to be parsed.
            return false;
        }

        private static int EvaluateNumberExpression(string expression)
        {
            //print("eval number expression: " + expression);
            
            // parenthesis first
            Regex re = new Regex(@"\((?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!))\)");

            expression = expression.Trim();
            var parenMatches = re.Matches(expression);
            //everything else will be taken care of recursively
            if (parenMatches.Count > 0)
            {
                // foreach (var e in parenMatches)
                // {
                //     print(e.ToString());
                // }
                
                foreach (var pMatch in parenMatches)
                {
                    //send the parenthetical to be evaluated without parenthesis
                    var exp = pMatch.ToString();
                    var expNoParen = exp[1..^1];
                    expression = expression.Replace(exp, ""+ EvaluateNumberExpression(expNoParen));
                }
            }

            // then multiplication and division, including modulus (skipping exponents)
            var indexOfFirstOperator = RegexIndexOf(expression, @"[*/%]");
            if (indexOfFirstOperator > 0)
            {
                string leftExp = expression[..indexOfFirstOperator];
                string rightExp = expression[(indexOfFirstOperator + 1)..];
                switch (expression[indexOfFirstOperator])
                {
                    case '*': return EvaluateNumberExpression(leftExp) * EvaluateNumberExpression(rightExp);
                    case '/': return EvaluateNumberExpression(leftExp) / EvaluateNumberExpression(rightExp);
                    case '%': return EvaluateNumberExpression(leftExp) % EvaluateNumberExpression(rightExp);
                }
            }
            // then addition and subtraction
            indexOfFirstOperator = RegexIndexOf(expression, @"[+\-]");
            if (indexOfFirstOperator > 0)
            {
                var leftExp = expression[..indexOfFirstOperator];
                var rightExp = expression[(indexOfFirstOperator + 1)..];
                
                //It's possible this is just a positive number or negative number left
                if (leftExp.Trim() == "")
                {
                    var multiplier = expression[indexOfFirstOperator] == '-' ? -1 : 1;
                    return multiplier * EvaluateNumberExpression(rightExp);
                }
                
                //adding or subtracting
                switch (expression[indexOfFirstOperator])
                {
                    case '+': return EvaluateNumberExpression(leftExp) + EvaluateNumberExpression(rightExp);
                    case '-': return EvaluateNumberExpression(leftExp) - EvaluateNumberExpression(rightExp);
                }
            }    
            //There are no operators left, so:
            //return the number value of the string that remains
            
            //Match $ all word characters up but to word boundary (word characters are A-Za-z0-9_)
            //https://regex101.com/r/fP9KSj/1
            re = new Regex(@"\$\w*\b");
            var match = re.Match(expression);
            if (match.Success)
            {
                var variableName = match.ToString()[1..]; //remove $
                return GetValue(variableName);
            }

            if (int.TryParse(expression, out var value)) return value;
            
            print($"ConversateManager.EvaluateNumberExpression(): Number/Variable, {expression}, " +
                  $"could not be parsed when evaluating expression. It may be a missing $ on a variable name " +
                  $"or a number with a decimal point.");
            return 0;
        }
        
        private static int RegexIndexOf(string str, string pattern)
        {
            var m = Regex.Match(str, pattern);
            return m.Success ? m.Index : -1;
        }

        public static List<GabLink> GetLinks(string passageText)
        {
            List<GabLink> links = new List<GabLink>();

            while (passageText.Length > 0)
            {
                var indexLinkOpen = passageText.IndexOf("[[");
                var indexContentStart = indexLinkOpen + 2;
                
                if (indexLinkOpen < 0) return links;

                int indexLinkClose = passageText.IndexOf("]]");
                if (indexLinkClose < 0)
                {
                    Debug.Log($"Unclosed link in conversation passage {passageText[indexLinkOpen..]} " +
                              $"[[ without ending ]]");
                    return links;
                }

                var linkRaw = passageText[indexContentStart..indexLinkClose];

                links.Add(GetLink(linkRaw));

                //update raw to cut out all previous text before the link
                
                passageText = passageText.Length > indexLinkClose+2 ? passageText[(indexLinkClose+2)..] : "";
            }

            return links;
        }
    }
}


