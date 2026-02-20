using CalculatorAPI.DTO;
using System.Globalization;

namespace CalculatorAPI.Services
{
    public class CalculationService : ICalculateService
    {
        enum TokenType
        {
            Literal,
            Plus,
            Minus,
            Multiply,
            Divide,
            LeftParen,
            RightParen
        }

        struct Token
        {
            public TokenType tokenType;
            public string value;
            public Token(TokenType tokentype, string value)
            {
                this.tokenType = tokentype;
                this.value = value;
            }
        }

        private static readonly Dictionary<TokenType, int> Priorities =
        new Dictionary<TokenType, int>()
        {
            {TokenType.LeftParen, 0 },
            {TokenType.Plus, 1 },
            {TokenType.Minus, 1},
            {TokenType.Multiply, 2},
            {TokenType.Divide, 2}
        };

        public double Calculate(string expression)
        {
            var Tokens = GetTokens(expression);
            var RPNExpression = ConvertToRPN(Tokens);
            double result = EvaluateRPN(RPNExpression);
            return result;
        }

        private List<Token> GetTokens(string expression)
        {
            List<Token> tokens = new List<Token>();
            string tempValue = "";

            foreach (var @char in expression)
            {
                if (char.IsDigit(@char))
                {
                    tempValue += @char;
                }
                else if (@char == '.' || @char == ',')
                {
                    tempValue += ".";
                }
                else if (@char == '-' && tempValue == "" && (tokens.Count == 0 || tokens.Last().tokenType == TokenType.LeftParen))
                {
                    tempValue += @char;
                }
                else
                {
                    if (tempValue != "")
                    {
                        tokens.Add(new Token(TokenType.Literal, tempValue));
                        tempValue = "";
                    }

                    switch (@char)
                    {
                        case '+': tokens.Add(new Token(TokenType.Plus, "+")); break;
                        case '-': tokens.Add(new Token(TokenType.Minus, "-")); break;
                        case '*': tokens.Add(new Token(TokenType.Multiply, "*")); break;
                        case '/': tokens.Add(new Token(TokenType.Divide, "/")); break;
                        case '(': tokens.Add(new Token(TokenType.LeftParen, "(")); break;
                        case ')': tokens.Add(new Token(TokenType.RightParen, ")")); break;
                    }
                }
            }

            if (tempValue != "")
            {
                tokens.Add(new Token(TokenType.Literal, tempValue));
            }
            return tokens;
        }

        private Queue<Token> ConvertToRPN(List<Token> tokens)
        {
            Stack<Token> operators = new Stack<Token>();
            Queue<Token> RPNExpression = new Queue<Token>();

            foreach (var token in tokens)
            {
                if (token.tokenType == TokenType.Literal)
                {
                    RPNExpression.Enqueue(token);
                }
                else if (token.tokenType == TokenType.LeftParen)
                {
                    operators.Push(token);
                }
                else if (token.tokenType == TokenType.RightParen)
                {
                    while (operators.Count > 0 && operators.Peek().tokenType != TokenType.LeftParen)
                    {
                        RPNExpression.Enqueue(operators.Pop());
                    }
                    if (operators.Count > 0) operators.Pop();
                }
                else
                {
                    while (operators.Count > 0)
                    {
                        if (Priorities[token.tokenType] <= Priorities[operators.Peek().tokenType])
                        {
                            RPNExpression.Enqueue(operators.Pop());
                        }
                        else
                        {
                            break;
                        }
                    }
                    operators.Push(token);
                }
            }

            while (operators.Count > 0)
            {
                RPNExpression.Enqueue(operators.Pop());
            }
            return RPNExpression;
        }

        private double EvaluateRPN(Queue<Token> RPNExpression)
        {
            double first_operand = 0;
            double second_operand = 0;
            Stack<double> operands = new Stack<double>();

            foreach (var token in RPNExpression)
            {
                if (token.tokenType == TokenType.Literal)
                {
                    operands.Push(double.Parse(token.value, CultureInfo.InvariantCulture));
                }
                else
                {
                    if (operands.Count == 1)
                    {
                        second_operand = operands.Pop();
                        first_operand = 0;
                    }
                    else
                    {
                        second_operand = operands.Pop();
                        first_operand = operands.Pop();
                    }

                    switch (token.tokenType)
                    {
                        case TokenType.Plus:
                            first_operand += second_operand;
                            operands.Push(first_operand);
                            break;
                        case TokenType.Minus:
                            first_operand -= second_operand;
                            operands.Push(first_operand);
                            break;
                        case TokenType.Multiply:
                            first_operand *= second_operand;
                            operands.Push(first_operand);
                            break;
                        case TokenType.Divide:
                            first_operand /= second_operand;
                            operands.Push(first_operand);
                            break;
                    }
                    first_operand = 0; second_operand = 0;
                }
            }
            return operands.Pop();
        }
    }
}