const http = require('http');
const url = require('url');
const htmlHandler = require('./htmlResponses.js');
const responseHandler = require('./responses.js');

const port = process.env.PORT || process.env.NODE_PORT || 3000;

const urlStruct = {
  '/': htmlHandler.getIndex,
  '/style.css': htmlHandler.getCSS,
  '/success': responseHandler.getSuccess,
  '/badRequest': responseHandler.getBadRequest,
  '/unauthorized': responseHandler.getUnauthorized,
  '/forbidden': responseHandler.getForbidden,
  '/internal': responseHandler.getInternal,
  '/notImplemented': responseHandler.getNotImplemented,
  notFound: responseHandler.getNotFound,
};

const onRequest = (request, response) => {
  const parsedUrl = url.parse(request.url);

  const acceptedTypes = request.headers.accept.split(',');

  const handler = urlStruct[parsedUrl.pathname];
  if (handler) {
    if (handler === responseHandler.getUnauthorized || handler === responseHandler.getBadRequest) {
      handler(request, response, acceptedTypes, parsedUrl.query);
    } else {
      handler(request, response, acceptedTypes);
    }
  } else {
    urlStruct.notFound(request, response, acceptedTypes);
  }
};

// http.createServer(onRequest).listen(port, () => {
//   console.log(`Listening on 127.0.0.1:${port}`);
// });


// Arduino Code:
const httpServer = http.createServer(onRequest); // create server
const io = require("socket.io")(httpServer); // connect socket.io to server

const { SerialPort } = require('serialport');
const { ReadlineParser } = require('@serialport/parser-readline');

const parser = new ReadlineParser({
  delimiter: '\r\n'
});

const arduinoPort = new SerialPort({
  path: '/dev/cu.usbmodem101',
  baudRate: 9600,
  dataBits: 8,
  parity: 'none',
  stopBits: 1,
  flowControl: false
});

arduinoPort.pipe(parser); // parse data from arduino

io.on('connection', function (data) { // when a client connects
  console.log("Node.js is listening");
});

parser.on('data', function (data) { // when data is received from arduino
  console.log('Data:', data);

  io.emit('data', data);
});

httpServer.listen(port, () => { // start server
  console.log(`Listening on 127.0.0.1:${port}`);
});