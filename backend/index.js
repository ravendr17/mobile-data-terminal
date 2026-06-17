import createApp from 'express';
const app = createApp();

const port = parseInt(process.env.PORT);

app.get('/vehicles', (req, res) => {
  res.json({
    message: 'no vehicles for now'
  });
});

app.listen(port, () => {
  console.log(`Server is running on port ${port}`);
})