module.exports = async function(context, req) {
    context.log('Running...');

    const name = (req.query.name || (req.body && req.body.name));
    const responseMessage = name
        ? "Hello, " + name + "!"
        : "Hello, stranger!";

    context.res = {
        status: 200,
        body: responseMessage
    };
}