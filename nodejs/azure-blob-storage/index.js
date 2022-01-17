const http = require('http');
const { BlobServiceClient } = require('@azure/storage-blob');
const { v4: uuidv4 } = require('uuid');

const AZURE_STORAGE_CONNECTION_STRING = process.env.AZURE_STORAGE_CONNECTION_STRING;
const AZURE_STORAGE_CONTAINER = process.env.AZURE_STORAGE_CONTAINER;
const port = process.env.PORT || 3000;

if (!AZURE_STORAGE_CONNECTION_STRING) {
    console.log("The environment variable 'AZURE_STORAGE_CONNECTION_STRING' is not set.");
    process.exit(1);
}

if (!AZURE_STORAGE_CONTAINER) {
    console.log("The environment variable 'AZURE_STORAGE_CONTAINER' is not set.");
    process.exit(1);
}

const server = http.createServer(async (request, response) => {
    if (request.url != "/"){
        response.writeHead(401, { "Content-Type": "text/plain" });
        response.end("401 - NOT FOUND");
        return;
    }

    response.writeHead(200, { "Content-Type": "text/plain" });
    
    // Get Container
    const blobServiceClient = BlobServiceClient.fromConnectionString(AZURE_STORAGE_CONNECTION_STRING);

    const containerClient = blobServiceClient.getContainerClient(AZURE_STORAGE_CONTAINER);

    const blobName = 'hello-world-' + uuidv4() + '.txt';

    const blockBlobClient = containerClient.getBlockBlobClient(blobName);

    // Upload data to the blob
    const data = 'Hello, World!';
    const uploadBlobResponse = await blockBlobClient.upload(data, data.length);

    response.write("Listing blobs...\n");

    for await (const blob of containerClient.listBlobsFlat()) {
        response.write("\t" + blob.name + "\n");
    }

    const downloadBlockBlobResponse = await blockBlobClient.download(0);
    response.write('Blob content:');
    response.write('\t' + await streamToString(downloadBlockBlobResponse.readableStreamBody));

    response.end("Hello World!");
});

async function streamToString(readableStream) {
    return new Promise((resolve, reject) => {
        const chunks = [];
        readableStream.on("data", (data) => {
            chunks.push(data.toString());
        });
        readableStream.on("end", () => {
            resolve(chunks.join(""));
        });
        readableStream.on("error", reject);
    });
}

server.listen(port);

console.log("Server running at http://localhost:%d", port);