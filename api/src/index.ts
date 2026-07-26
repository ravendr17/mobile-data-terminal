import express from "express";
import { env } from "./env.js";

const app = express();
const port = env.PORT;

app.use(express.json());

app.get("/hello", (req, res) => {
  res.send("Hello World");
});

app.listen(port, () => {
  console.log(`Server running at port ${port}`);
})