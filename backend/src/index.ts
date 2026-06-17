import express from 'express';
import pool from './db.js';

const app = express();
const port = parseInt(process.env.PORT ?? '3000', 10);

app.get('/vehicles', (req, res) => {
  res.json({
    message: 'no vehicles for now'
  });
});

app.listen(port, async () => {
  console.log(`Server is running on port ${port}`);
})