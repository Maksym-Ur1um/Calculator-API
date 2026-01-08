using CalculatorAPI.DTO;
using CalculatorAPI.Services.ServiceContract;

namespace CalculatorAPI.Services
{
    public class CalculationService : ICalculateService
    {
        public double Calculate(string input)
        {
            double result = EvaluateRPN(ConvertToRPN(GetTokens(input)));
            return result;
        }

        private List<string> GetTokens(string input)
        {
            List<string> tokens = new List<string>();
            string tempValue = "";
            foreach (var item in input)
            {
                if (char.IsDigit(item))
                {
                    tempValue += item;
                } else if (item == '.')
                {
                    tempValue += item;
                } else if (item == '-' && tempValue == "")
                {
                    tempValue += item;
                } else if (item == '+' || item == '-' || item == '*' || item == '/')
                {
                    if (tempValue != "")
                    {
                        tokens.Add(tempValue);
                    }
                    tokens.Add(item.ToString());
                    tempValue = "";
                }
            }
            if (tempValue != "")
            {
                tokens.Add(tempValue);
            }
            return tokens;
        }

        private Queue<string> ConvertToRPN(List<string> tokens)
        {
            Stack<string> operators = new Stack<string>();
            Queue<string> RPNExpression = new Queue<string>();

            foreach (var item in tokens)
            {
                if (double.TryParse(item, out var value))
                {
                    RPNExpression.Enqueue(item);
                }

                if (item == "+" || item == "-" || item == "*" || item == "/")
                {
                    while (operators.Count > 0)
                    {
                        if(GetPriority(item) <= GetPriority(operators.Peek())){
                            RPNExpression.Enqueue((string)operators.Pop());
                        } else 
                        {
                            break;
                        }
                    }
                    operators.Push(item);
                }
            }
            while (operators.Count > 0)
            {
                RPNExpression.Enqueue((string)operators.Pop());
            }
            return RPNExpression;
        }
        private double EvaluateRPN(Queue<string> RPNExpression)
        {
            double first_operand = 0;
            double second_operand = 0;
            Stack<double> operands = new Stack<double>();
            foreach(var item in RPNExpression)
            {
                if(double.TryParse(item, out var value))
                {
                    operands.Push(value);
                }
                if(item == "+" || item == "-" || item == "*" || item == "/")
                {
                    if(operands.Count == 1)
                    {
                        second_operand = operands.Pop();
                        first_operand = 0;
                    } else
                    {
                        second_operand = operands.Pop();
                        first_operand = operands.Pop();
                    }
                    switch(item)
                    {
                        case "+":
                            first_operand += second_operand;
                            operands.Push(first_operand);
                            break;
                        case "-":
                            first_operand -= second_operand;
                            operands.Push(first_operand);
                            break;
                        case "*":
                            first_operand *= second_operand;
                            operands.Push(first_operand);
                            break;
                        case "/":
                            first_operand /= second_operand;
                            operands.Push(first_operand);
                            break;
                    }
                    first_operand = 0; second_operand = 0;
                }
            }
            return operands.Pop();
        }

        private int GetPriority(string op)
        {
            if (op == "*" || op == "/")
                return 2;

            return 1;
        }
    }
}
