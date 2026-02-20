# Web Calculator

A full-stack web calculator application. This project implements a custom calculation engine on the backend using the Reverse Polish Notation (RPN) algorithm to parse and evaluate mathematical expressions safely.

## Features

- Parsing of mathematical strings without relying on built-in evaluation functions.
- Support for correct operator precedence (multiplication and division over addition and subtraction).
- Support for complex expressions with parentheses.
- Floating-point arithmetic and negative numbers support.
- Modular vanilla JavaScript frontend architecture (isolated API, State, and UI layers).
- Backend business logic covered by xUnit tests.

## Technology Stack

- Backend: C#, .NET, ASP.NET Core Web API
- Testing: xUnit
- Frontend: HTML5, CSS3, Vanilla JavaScript (ES6 Modules)

## Architecture Overview

The calculation logic is processed on the backend in three steps:
1. Lexer: Breaks the input string into structural tokens.
2. Parser: Converts infix notation to Reverse Polish Notation (RPN) using Dijkstra's Shunting-yard algorithm.
3. Evaluator: Computes the final result using a stack-based approach.

## How to Run

### 1. Backend API
1. Open the backend project directory in your terminal.
2. Build and run the project using the .NET CLI:
   dotnet run
3. The API will start and listen for requests (default: https://localhost:7056).

### 2. Frontend Web App
1. Open the frontend directory in a code editor.
2. Start a local development server (e.g., using the Live Server extension in VS Code).
   Note: Opening the index.html file directly in a browser via the file:// protocol will result in CORS errors due to ES6 module security policies.
3. Ensure the API_URL variable in js/api.js matches your local backend address.

## API Endpoints

### POST /api/calculate
Evaluates a mathematical expression and returns the result.

Request body (JSON):
{
  "Expression": "2+2*2"
}

Response (200 OK):
6