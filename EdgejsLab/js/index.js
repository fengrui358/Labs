const getUuid = require('./test');
const edgeFun = require('./edge');
console.log(getUuid());
edgeFun((error, result) => {
    console.log(result);
}, 'hello');