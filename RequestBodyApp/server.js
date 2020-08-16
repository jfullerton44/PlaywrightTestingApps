const bodyParser = require("body-parser");
const express = require("express");
const app = express();

app.use(bodyParser.urlencoded({ extended: false }));

app.get('/', (req, res) => {
  res.sendFile(`${__dirname}/index.html`);
});

app.get('/lol', (req, res) => {
  res.sendFile(`${__dirname}/index.html`);
});

app.post('/login', (req, res) => {
  const username = req.body.username;
  const password = req.body.password;
  console.log(`POST request: username is ${username} and password is ${password}`);
  res.set('Content-Type', 'text/html')
  res.write('<h1>Hello, World!</h1>');
  res.write('<br>');
  res.write(`<h1>POST request: username is ${username} and password is ${password}</h1>`);
  res.write('<br>');
  res.end(`Test Request Body`);
});

app.listen(7000, () => {
  console.log("Started on http://localhost:7000");
});