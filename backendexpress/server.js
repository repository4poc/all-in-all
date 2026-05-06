import express from "express";
//import bodyParser from "body-parser";
import cors from "cors";
import path from "path";
import { fileURLToPath } from "url";
import morgan from "morgan";
import axios from "axios";
import {
  getCachedTodos,
  getCachedUsers,
  startUsersCacheRefresh,
} from "./middleware/dataCache.js";
import { ApolloServer } from "@apollo/server";
import { expressMiddleware } from "@as-integrations/express5";
import LoggerMiddleware from "./middleware/logger.js";
import errorHandler from "./middleware/errorHandler.js";

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

const app = express();
const PORT = 3000;
const typeDefs = `#graphql
  type Geo {
    lat: String
    lng: String
  }

  type Address {
    street: String
    suite: String
    city: String
    zipcode: String
    geo: Geo
  }

  type Company {
    name: String
    catchPhrase: String
    bs: String
  }

  type User {
    id: ID!
    name: String!
    username: String
    email: String
    address: Address
    phone: String
    website: String
    company: Company
  }

  type Query {
    getUsers: [User!]!
    getUser(id: ID!): User
  }
`;
const server = new ApolloServer({
  typeDefs: `
  type Geo {
    lat: String
    lng: String
  }

  type Address {
    street: String
    suite: String
    city: String
    zipcode: String
    geo: Geo
  }

  type Company {
    name: String
    catchPhrase: String
    bs: String
  }

  type User {
    id: ID!
    name: String!
    username: String
    email: String
    address: Address
    phone: String
    website: String
    company: Company
  }

  type Todo {
    id: ID!
    title: String!
    completed: Boolean
  }
  
  type Query {
    getTodos : [Todo]
    getUsers: [User!]
    getUser(id: ID!): User    
  }
  `,
  resolvers: {
    Query: {
      getTodos: () => getCachedUsers(),
      getUsers: () => getCachedTodos(),
    },
  },
});

app.use(cors());

// You don’t actually need the separate body-parser package anymore—Express.js has it built-in.
// bodyparser - for HTML form submissions, else req.body will be undefined
app.use(express.urlencoded({ extended: true }));

// bodyparser - for JSON (Postman / API calls), else req.body will be undefined
app.use(express.json());

await server.start();
app.use("/graphql", expressMiddleware(server));

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
