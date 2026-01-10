let input = document.querySelector('input')
const calculatorElements = document.querySelectorAll('.number-button, .operation-button')

calculatorElements.forEach((element) => {
    element.addEventListener('click', (_) => {
        const valueToAdd = element.dataset.value || element.textContent;
        addToInput(valueToAdd);
    });
});

const clearOperation = document.querySelector(".clear-button");
clearOperation.addEventListener('click', (_) => {
    input.value = "";
});

let addToInput = (x) => {
    input.value += x;
}

const equalsButton = document.querySelector("#equals");
equalsButton.addEventListener('click', async(event) => {
    const result = await calculate(input.value);
    input.value = result;
});

async function calculate(input) {
    const response = await fetch(
        `https://localhost:7056/api/calculate`,
         {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify({Expression: input})
        });
        const result = await response.json();
        return result
}