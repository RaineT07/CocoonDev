//require mongoose, which is a MongoDB library for node
//it connects in the app.js file, once it's connected, it's connected for the whole app
//as long as this is imported after connecting

const mongoose = require('mongoose');

//variable to hold the model
//a model is a data structure for data handling, it will be in JSON form for this project
//a mongoDB model is a database structure with the api attached
let ModModel = {};


//A DB Schema to define the structure of the data
const ModSchema = new mongoose.Schema({
    id: {
        type: String,
        required: true,
        unique: true,
    },
    last: {
        type: String,
        required: true,
    },
    createdDate: {
        type: Date,
        default: Date.now,
    },
});

//create the model based on the schema
ModModel = mongoose.model('Mod', ModSchema);

//export the model
module.exports = ModModel;