let internalExpression = "";

export const isOperator = (char) => ["+", "-", "*", "/"].includes(char);

export function getExpression() {
  return internalExpression;
}

export function setExpression(value) {
  internalExpression = value;
}

export function addCharacter(char) {
  const lastChar = internalExpression.slice(-1);

  if (isOperator(char)) {
    if (internalExpression.length === 0 && char !== "-") return;

    if (isOperator(lastChar)) {
      internalExpression = internalExpression.slice(0, -1) + char;
    } else {
      internalExpression += char;
    }
  } else if (char === ".") {
    if (lastChar !== ".") {
      internalExpression += char;
    }
  } else {
    internalExpression += char;
  }
}

export function removeLastCharacter() {
  internalExpression = internalExpression.slice(0, -1);
}

export function clearExpression() {
  internalExpression = "";
}
