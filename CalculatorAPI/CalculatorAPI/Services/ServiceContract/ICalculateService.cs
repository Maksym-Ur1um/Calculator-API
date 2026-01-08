using CalculatorAPI.DTO;

namespace CalculatorAPI.Services.ServiceContract
{
    public interface ICalculateService
    {
        public double Calculate(string input);
    }
}
