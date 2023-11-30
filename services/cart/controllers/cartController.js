const { PrismaClient } = require("@prisma/client");
const prisma = new PrismaClient();

const { createError } = require("../utils/error");

const init = async (req, res) => {
  res.send("hollo from cwd cart api.");
};

const createCart = async (req, res, next) => {
  try {
    const data = {
      ...req.body,
    };
    const cart = await prisma.cart.create({ data });
    res.status(200).json(cart);
  } catch (err) {
    next(err);
  }
};

const updateCart = async (req, res, next) => {
  try {
    const data = {
      ...req.body,
    };
    const updated = await prisma.cart.update({
      where: { id: req.params.id },
      data,
    });
    res.status(200).json(updated);
  } catch (err) {
    next(err);
  }
};

const deleteCart = async (req, res, next) => {
  try {
    await prisma.cart.delete({ where: { id: req.params.id } });
    res.status(200).json("Cart has been deleted.");
  } catch (err) {
    next(err);
  }
};

const getCart = async (req, res, next) => {
  try {
    const cart = await prisma.cart.findFirst({
      where: { id: req.params.id },
    });

    res.status(200).json(cart);
  } catch (err) {
    next(err);
  }
};

module.exports = {
  createCart,
  updateCart,
  deleteCart,
  getCart,
  init,
};
