const express = require("express");
const dotenv = require("dotenv");
const cors = require("cors");

const app = express();
dotenv.config();
app.use(cors());
app.use(express.json());

app.listen(process.env.PORT, () => {
  console.log("Email server is listening on port: ", process.env.PORT);
});
