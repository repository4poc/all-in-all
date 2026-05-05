import express from "express";
//import bodyParser from "body-parser";
import cors from "cors";
import path from "path";
import { fileURLToPath } from "url";
import morgan from "morgan";
import axios from "axios";
import {
  getCachedUsers,
  startUsersCacheRefresh,
} from "./middleware/dataCache.js";
import LoggerMiddleware from "./middleware/logger.js";
import errorHandler from "./middleware/errorHandler.js";

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
//app.use(morgan("tiny"));

// use logger middleware
app.use(LoggerMiddleware);

// start cache loading when app starts
await startUsersCacheRefresh();

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

app.get("/data", (req, res) => {
  res.json(getCachedUsers());
});

app.post("/api/submit", (req, res, next) => {
  try {
    const { country, capital } = req.body;

    const isCountryPresent = questions.find(
      (q) => q.country.toLowerCase() === country?.toLowerCase(),
    );

    if (!isCountryPresent) {
      const err = new Error("country is not found");
      err.status = 404;
      return next(err);
    }

    const isCapitalPresent = questions.find(
      (q) =>
        q.country.toLowerCase() === country?.toLowerCase() &&
        q.capital.toLowerCase() === capital?.toLowerCase(),
    );

    if (!isCapitalPresent) {
      const err = new Error("country - capital mismatch");
      err.status = 400;
      return next(err);
    }

    return res.status(200).json({ message: "You won" });
  } catch (error) {
    next(error); // send unexpected errors to middleware
  }
});

// error middleware (must be last)
app.use(errorHandler);

app.listen(3000, "0.0.0.0", () => {
  console.log("Backendexpress running on port 3000");
});
