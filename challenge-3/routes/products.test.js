// FILE: products.test.js
const request = require('supertest');
const express = require('express');
const bodyParser = require('body-parser');
const productsRouter = require('./products');

const app = express();
app.use(bodyParser.json());
app.use('/api/items', productsRouter);

describe('Products API', () => {
    it('should retrieve all items', async () => {
        const response = await request(app).get('/api/items');
        expect(response.status).toBe(200);
        expect(response.body.length).toBe(2);
        expect(response.body[0].name).toBe('Product1');
    });

    it('should retrieve an item by its ID', async () => {
        const response = await request(app).get('/api/items/1');
        expect(response.status).toBe(200);
        expect(response.body.name).toBe('Product1');
    });

    it('should return 404 for a non-existent item', async () => {
        const response = await request(app).get('/api/items/999');
        expect(response.status).toBe(404);
        expect(response.text).toBe('Product not found');
    });

    it('should create a new item', async () => {
        const newProduct = {
            name: 'Product3',
            description: 'Description3',
            price: 30.0
        };
        const response = await request(app).post('/api/items').send(newProduct);
        expect(response.status).toBe(201);
        expect(response.body.name).toBe('Product3');
        expect(response.body.id).toBe('3');
    });

    it('should handle server errors', async () => {
        const response = await request(app).post('/api/items').send({});
        expect(response.status).toBe(500);
        expect(response.text).toBe('Internal Server Error');
    });
});
