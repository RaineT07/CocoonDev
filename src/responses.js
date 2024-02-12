const respond = (request, response, status, content, type) => {
  response.writeHead(status, { 'Content-Type': type });
  response.write(content);
  response.end();
};

// const getResponse = (request, response, status, message, id, types) => {
//   const responseText = message;
//   let responseObj = {};

//   if (id.length !== 0) {
//     responseObj = {
//       responseText,
//       id,
//     };
//   } else {
//     responseObj = {
//       responseText,
//     };
//   }
//   console.log(types);
//   if (types[0] === 'text/xml') {
//     let responseXML = '<response>';
//     responseXML += responseObj.message;
//     if (id.length !== 0) responseXML += `<id>${responseObj.id}</id>`;
//     responseXML += '</response>';
//     return respond(request, response, status, responseXML, 'text/xml');
//   }

//   const responseStr = JSON.stringify(responseObj);
//   return respond(request, response, status, responseStr, 'application/json');
// };

const getSuccess = (request, response, acceptedTypes) => {
  const responseText = 'This is a successful response';
  //   console.log(acceptedTypes);
  let responseObj = {};

  responseObj = {
    message: responseText,
  };

  //   console.log(acceptedTypes);
  if (acceptedTypes[0] === 'text/xml') {
    let responseXML = '<response>';
    responseXML += `<message>${responseObj.message}</message>`;
    responseXML += '</response>';
    return respond(request, response, 200, responseXML, 'text/xml');
  }

  const responseStr = JSON.stringify(responseObj);
  return respond(request, response, 200, responseStr, 'application/json');
};

const getBadRequest = (request, response, acceptedTypes, param) => {
  let status = 400;
  let responseText = '';
  if (param !== 'valid=true') {
    responseText = 'Missing valid query parameter set to true';
  } else {
    responseText = 'This request has the required parameters';
    status = 200;
  }

  const responseObj = {
    message: responseText,
    id: 'badRequest',
  };

  if (acceptedTypes[0] === 'text/xml') {
    let responseXML = '<response>';
    responseXML += `<message>${responseObj.message}</message>`;
    responseXML += `<id>${responseObj.id}</id>`;
    responseXML += '</response>';
    return respond(request, response, status, responseXML, 'text/xml');
  }

  const responseStr = JSON.stringify(responseObj);
  return respond(request, response, 400, responseStr, 'application/json');
};

const getUnauthorized = (request, response, acceptedTypes, param) => {
  let responseText = '';
  let status = 401;
  if (param === 'loggedIn=yes') {
    responseText = 'You have successfully viewed the content.';
    status = 200;
  } else {
    responseText = 'missing loggedIn query parameter set to yes';
  }

  const responseObj = {
    message: responseText,
    id: 'unauthorized',
  };

  if (acceptedTypes[0] === 'text/xml') {
    let responseXML = '<response>';
    responseXML += `<message>${responseObj.message}</message>`;
    responseXML += `<id>${responseObj.id}</id>`;
    responseXML += '</response>';
    return respond(request, response, status, responseXML, 'text/xml');
  }

  const responseStr = JSON.stringify(responseObj);
  return respond(request, response, status, responseStr, 'application/json');
};

const getForbidden = (request, response, acceptedTypes) => {
  const responseText = 'You do not have access to this content.';

  const responseObj = {
    message: responseText,
    id: 'forbidden',
  };

  if (acceptedTypes[0] === 'text/xml') {
    let responseXML = '<response>';
    responseXML += `<message>${responseObj.message}</message>`;
    responseXML += `<id>${responseObj.id}</id>`;
    responseXML += '</response>';
    return respond(request, response, 403, responseXML, 'text/xml');
  }

  const responseStr = JSON.stringify(responseObj);
  return respond(request, response, 403, responseStr, 'application/json');
};
const getInternal = (request, response, acceptedTypes) => {
  const responseText = 'Internal Server Error. Something went wrong.';

  const responseObj = {
    message: responseText,
    id: 'internalError',
  };

  if (acceptedTypes[0] === 'text/xml') {
    let responseXML = '<response>';
    responseXML += `<message>${responseObj.message}</message>`;
    responseXML += `<id>${responseObj.id}</id>`;
    responseXML += '</response>';
    return respond(request, response, 500, responseXML, 'text/xml');
  }

  const responseStr = JSON.stringify(responseObj);
  return respond(request, response, 500, responseStr, 'application/json');
};

const getNotImplemented = (request, response, acceptedTypes) => {
  const responseText = 'A get request for this page has not been implemented yet. Check again later for updated content.';
  const responseObj = {
    message: responseText,
    id: 'notImplemented',
  };

  if (acceptedTypes[0] === 'text/xml') {
    let responseXML = '<response>';
    responseXML += `<message>${responseObj.message}</message>`;
    responseXML += `<id>${responseObj.id}</id>`;
    responseXML += '</response>';
    return respond(request, response, 501, responseXML, 'text/xml');
  }

  const responseStr = JSON.stringify(responseObj);
  return respond(request, response, 501, responseStr, 'application/json');
};

const getNotFound = (request, response, acceptedTypes) => {
  const responseText = 'The page you are looking for was not found.';
  const responseObj = {
    message: responseText,
    id: 'notFound',
  };

  if (acceptedTypes[0] === 'text/xml') {
    let responseXML = '<response>';
    responseXML += `<message>${responseObj.message}</message>`;
    responseXML += `<id>${responseObj.id}</id>`;
    responseXML += '</response>';
    return respond(request, response, 404, responseXML, 'text/xml');
  }

  const responseStr = JSON.stringify(responseObj);
  return respond(request, response, 404, responseStr, 'application/json');
};

module.exports = {
  getSuccess,
  getBadRequest,
  getUnauthorized,
  getForbidden,
  getInternal,
  getNotImplemented,
  getNotFound,
};
