const getUuid = require('./test');
const edge = function (callback, data) {
    callback(null, `${getUuid()} Node.js welcomes ${data}`);
};

module.exports = edge;