const http = require('http');
const url = require('url');
const htmlHandler = require('./htmlResponses.js');
const responseHandler = require('./responses.js');
const path = require('path');
const express = require('express');
const compression = require('compression');
const favicon = require('serve-favicon');
const bodyParser = require('body-parser');
const mongoose = require('mongoose');
const expressHandlebars = require('express-handlebars');


//router has routes which connects the urls to the controller methods
const router = require('./router.js');

//process.env.MONGODB_URI is a heroku variable, which can be configured via Heroku's config vars section
//string after mongodb://localhost is databaseName, can be anything
const dbURI = process.env.MONGODB_URI || 'mongodb://127.0.0.1/CocoonDB';

const port = process.env.PORT || process.env.NODE_PORT || 3000;

//call mongoose connect function and pas in the url, if it fails throw an error
mongoose.connect(dbURI).catch((err)=>{
  if(err){
    console.log('Could not connect to database');
    throw err;
  }
});

//get an express mvc server object
const app = express();

//app.use tells texpress to use options, which says to use /assets in a url path as a static mirror to our client folder
//basically any requests to /assets will go to the client folder instead
app.use('/assets', express.static(path.resolve(`${__dirname}/../client/`)));

//use compression and tell app how to use it
app.use(compression());

//parse form post requests as application/x-www-form-urlencoded
app.use(bodyParser.urlencoded({extended: false}));

//parse application/json body requests
//these are usually post requests or request with a body param in ajax
//or a web api request from a mobile app
//or another server or application
app.use(bodyParser.json());

//set up handlebars!
app.engine('handlebars', expressHandlebars.engine({
  defaultLayout: '',
}));
app.set('view engine', 'handlebars');

//set the views pat hto the template directory
//(needed for express to work)
app.set('views', `${__dirname}/../views`);

//get favicon
app.use(favicon(`${__dirname}/../client/img/favicon.png`));

//pass the app to the router to map the routes
router(app);




// Arduino Code:
// const httpServer = http.createServer(onRequest); // create server
// const io = require("socket.io")(app); // connect socket.io to server
// 
// const { SerialPort } = require('serialport');
// const { ReadlineParser } = require('@serialport/parser-readline');
// 
// const parser = new ReadlineParser({
  // delimiter: '\r\n'
// });
// 
// const arduinoPort = new SerialPort({
  // path: '/dev/cu.usbmodem101',
  // baudRate: 9600,
  // dataBits: 8,
  // parity: 'none',
  // stopBits: 1,
  // flowControl: false
// });
// 
// arduinoPort.pipe(parser); // parse data from arduino
// 
// io.on('connection', function (data) { // when a client connects
  // console.log("Node.js is listening");
// });
// 
// parser.on('data', function (data) { // when data is received from arduino
  // console.log('Data:', data);
// 
  // io.emit('data', data);
// });

app.listen(port, (err) => { // start server
  if(err) throw err;
  console.log(`Listening on port ${port}`);
});
