import express from "express";
import bodyParser from "body-parser";
import cors from "cors";
import path from "path";
import { fileURLToPath } from "url";
import morgan from "morgan";

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

const app = express();
const PORT = 3000;

app.use(cors());

// You don’t actually need the separate body-parser package anymore—Express.js has it built-in.
// bodyparser - for HTML form submissions, else req.body will be undefined
app.use(express.urlencoded({ extended: true }));

// bodyparser - for JSON (Postman / API calls), else req.body will be undefined
app.use(express.json());

// for logging middleware
app.use(morgan("tiny"));

// for custom middleware
app.use((req, res, next) => {
  console.log("CustomMiddleware1 logic comes here..." + req.body);
  next();
});
app.use((req, res, next) => {
  console.log(
    "CustomMiddleware2 following CustomMiddleware1 logic comes here..." +
      req.body,
  );
  next();
});

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
app.get("/health", (req, res) => {
  res.status(200).json({
    status: "healthy",
  });
});

// Submit answer
app.post("/api/submit", (req, res) => {
  const { country, capital } = req.body;

  const isCountryPresent = questions.find((q) => q.country === country);

  if (!isCountryPresent) {
    return res.status(404).json({ message: "country is not found" });
  }

  const isCapitalPresent = questions.find((q) => q.capital === capital);

  if (!isCapitalPresent) {
    return res.status(404).json({ message: "country - capital mismatch" });
  }

  return res.status(200).json({ message: "You won" });
});

app.listen(3000, "0.0.0.0", () => {
  console.log("Backendexpress running on port 3000");
});
