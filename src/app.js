// const SerialPort = require('serialport');
const { SerialPort } = require('serialport')
const { ReadlineParser } = require('@serialport/parser-readline')

// const parsers = SerialPort.parsers;
const parser = new ReadlineParser({
    delimiter: '\r\n'
});

const port = new SerialPort({
    path: '/dev/cu.usbmodem1101',
    baudRate: 9600,
    dataBits: 8,
    parity: 'none',
    stopBits: 1,
    flowControl: false
});

port.pipe(parser);

parser.on('data', function (data) {
    console.log('Data:', data);
});
