const router = require("express").Router();
const cartCtl = require("../controllers/cartController");
const { verify } = require("../utils/verify");

router.get("/", cartCtl.init);
router.get("/:id", verify, cartCtl.getCart);
router.post("/:id", verify, cartCtl.createCart);
router.put("/:id", verify, cartCtl.updateCart);
router.delete("/:id", verify, cartCtl.deleteCart);

module.exports = router;
