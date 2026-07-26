import "dotenv/config";

if (!process.env.PORT) throw new Error("MISSING ENV: PORT");
if (!process.env.DATABASE_URL) throw new Error("MISSING ENV: DATABASE_URL");

export const env = {
  PORT: process.env.PORT,
  DATABASE_URL: process.env.DATABASE_URL
};