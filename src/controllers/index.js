//pull in the mongoDB model

const models = require('../models');

//get the particular model
const {mod} = models;

//function to host the index page
const hostIndex = async (req, res) => {

    let id='unknown';

    try{
        // const doc = await mod.findOne({}, {}, {
            // sort: {'createdDate': 'descending'}
        // }).lean.exec();
        // 
        // if(doc){
            // id = doc.id;
            //}
        console.log('programming is hard');
        
    } catch(err){
        console.log(err);
        
    }

    res.render('index', {
        id:'unknown in testing environment',
        title:"home",
        pageName:"Home Page",
    });
};

//function to host the page1 page

const hostPage1 = async (req, res) => {
    try{
        const docs = await mod.find({}).lean().exec();
        return res.render('page1', {id:docs});
    }catch(err){
        console.log(err);
        return res.status(500).json({error: 'failed to catch id'});
    }
};

const hostPage2 = (req, res) => {
    res.render('page2');
}

const hostPage3 = (req, res) => {
    res.render('page3');
}

const getName = async (req, res) => {
    try{
        const doc = await mod.findOne({}, {}, {
            sort:{'createdDate': 'descending'}
        }).lean().exec();

        if(doc){
            return res.json({id:doc.id});
        }
        return res.status(404).json({error: 'No id found'});
    }catch(err){
        console.log(err);
        return res.status(500).json({error: 'Internal server error: something went wrong connecting to the database'});
    }
};

//function to create a new photo in database
const setPhoto = async (req, res) => {

    if(!req.body.id || !req.body.last){
        return res.status(400).json({error: 'id and last is required'});
    }

    const photoData = {
        id: `${req.body.id}`,
        last: `${req.body.last}`,
    };

    const newPhoto = new mod(photoData);

    try{
        await newPhoto.save();
        return res.status(201).json({
            id: newPhoto.id,
            last: newPhoto.last,
        });
    } catch (err){
        console.log(err);
        return res.status(500).json({error: 'Internal server error: Failed to save photo'});
    }
};

//function to search a photo by id
const getById = async (req, res) => {
    if(!req.query.id){
        return res.status(400).json({error: 'id is required'});
    }

    try{
        const doc = await mod.findOne({id: req.query.id}).exec();
    }catch(err){
        console.log(err);
        return res.status(500).json({error: 'Internal server error: something went wrong'});
    }

    if(!doc){
        return res.status(404).json({error: 'No photo found'});
    }

    //function gets through everything, then returns the photo
    return res.json({id: doc.id, last: doc.last});
};

//function to update the last photo added to the db
const updateLast = async (req, res) => {
    const updatePromise = mod.findOneAndUpdate({}, {$inc: {last: 1}},
        {
            returnDocument: 'after',
            sort: {'createdDate':'descending'},
        }).lean().exec();

    updatePromise.then((doc) => {
        res.json({id: doc.id, last: doc.last});
    });

    updatePromise.catch((err) => {
        console.log(err);
        res.status(500).json({error: 'Internal server error: something went wrong'});
    });
};

//function to handle 404 pages
const notFound = (req, res) => {
    res.status(404).render('notFound',{
        page:req.url,
    });
};

module.exports = {
    index: hostIndex,
    page1: hostPage1,
    page2: hostPage2,
    page3: hostPage3,
    getName,
    setPhoto,
    getById,
    updateLast,
    notFound,
};