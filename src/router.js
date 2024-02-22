//import controllers
const controllers = require('./controllers');

//function to attach the router to the app

const router = (app) => {

    //connect the pages
    app.get('/page1', controllers.page1);
    app.get('/page2', controllers.page2);
    app.get('/page3', controllers.page3);
    app.get('/page4', controllers.page4);
    app.get('/getPhoto', controllers.getName);
    app.get('/getById', controllers.getById);

    app.get('/', controllers.index);
    app.get('/*', controllers.notFound);

    app.post('/setPhoto', controllers.setPhoto);
    app.post('/updateLast', controllers.updateLast);
};

module.exports = router;