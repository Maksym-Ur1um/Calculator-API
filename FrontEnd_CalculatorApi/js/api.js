// js/api.js
const API_URL = "https://localhost:7056/api/calculate";

export async function fetchCalculation(expression) {
  const response = await fetch(API_URL, {
    method: "POST",
    headers: { "Content-Type": "application/json;charset=utf-8" },
    body: JSON.stringify({ Expression: expression }),
  });

  if (!response.ok) {
    throw new Error("Invalid Expression");
  }

  return await response.json();
}
