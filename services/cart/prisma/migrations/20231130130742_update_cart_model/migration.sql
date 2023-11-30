-- RedefineTables
PRAGMA foreign_keys=OFF;
CREATE TABLE "new_Cart" (
    "Id" TEXT NOT NULL PRIMARY KEY,
    "UserId" TEXT NOT NULL,
    "CartId" TEXT NOT NULL,
    "DetailsId" TEXT NOT NULL,
    "CreatedAt" DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" DATETIME NOT NULL,
    CONSTRAINT "Cart_CartId_fkey" FOREIGN KEY ("CartId") REFERENCES "Detail" ("Id") ON DELETE CASCADE ON UPDATE CASCADE
);
INSERT INTO "new_Cart" ("CartId", "CreatedAt", "DetailsId", "Id", "UpdatedAt", "UserId") SELECT "CartId", "CreatedAt", "DetailsId", "Id", "UpdatedAt", "UserId" FROM "Cart";
DROP TABLE "Cart";
ALTER TABLE "new_Cart" RENAME TO "Cart";
PRAGMA foreign_key_check;
PRAGMA foreign_keys=ON;
