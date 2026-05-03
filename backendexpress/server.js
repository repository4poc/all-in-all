const express = require("express");
const cors = require("cors");

const app = express();
const PORT = 3000;

app.use(cors());
app.use(express.json());

let questions = [
  {
    id: 1,
    country: "France",
    capital: "Paris",
  },
  {
    id: 2,
    country: "Finland",
    capital: "Helsinki",
  },
];

// Health check
app.get("/", (req, res) => {
  res.json({ message: "API is running" });
});

// Get all questions
app.get("/api/questions", (req, res) => {
  res.json(questions);
});

// Get one question
app.get("/api/questions/:id", (req, res) => {
  const question = questions.find((q) => q.id === Number(req.params.id));

  if (!question) {
    return res.status(404).json({ message: "Question not found" });
  }

  res.json(question);
});

// Submit answer
app.post("/api/submit", (req, res) => {
  const { questionId, answer } = req.body;

  const question = questions.find((q) => q.id === Number(questionId));

  if (!question) {
    return res.status(404).json({ message: "Question is not found" });
  }

  const isCorrect =
    question.capital.toLowerCase() === answer.trim().toLowerCase();

  res.json({
    correct: isCorrect,
    correctAnswer: question.capital,
  });
});

app.listen(PORT, () => {
  console.log(`Server running on http://localhost:${PORT}`);
});
