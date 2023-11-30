const express = require("express");
const dotenv = require("dotenv");
const cors = require("cors");

const cartRoutes = require("./routes/cart");

const app = express();
dotenv.config();
app.use(cors());
app.use(express.json());

app.use("/api/cart", cartRoutes);

app.listen(process.env.PORT, () => {
  console.log("Cart server is listening on port: ", process.env.PORT);
});
