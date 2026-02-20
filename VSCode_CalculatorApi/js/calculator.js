import { fetchCalculation } from "./api.js";
import {
  getExpression,
  setExpression,
  addCharacter,
  removeLastCharacter,
  clearExpression,
} from "./state.js";

const display = document.getElementById("display");

function updateDisplay() {
  const expr = getExpression();
  if (expr === "") {
    display.value = "0";
  } else {
    display.value = expr.replaceAll("*", "×");
  }
}

document
  .querySelectorAll(".number-button, .operation-button")
  .forEach((btn) => {
    btn.addEventListener("click", (e) => {
      const val = e.target.dataset.value || e.target.textContent;
      addCharacter(val);
      updateDisplay();
    });
  });

document.getElementById("clear").addEventListener("click", () => {
  clearExpression();
  updateDisplay();
});

document.getElementById("backspace").addEventListener("click", () => {
  removeLastCharacter();
  updateDisplay();
});

document.getElementById("equals").addEventListener("click", async () => {
  const expr = getExpression();
  if(!expr) return;

  try {
    const result = await fetchCalculation(expr);
    setExpression(result.toString());
    updateDisplay();
  } catch (error) {
    display.value = "Ошибка";
    clearExpression();
  }
});
