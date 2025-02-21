const express = require('express');
const router = express.Router();

let products = [
    { id: '1', name: 'Product1', description: 'Description1', price: 10.0 },
    { id: '2', name: 'Product2', description: 'Description2', price: 20.0 }
];

// GET /api/items: Retrieve all items
router.get('/', (req, res) => {
    res.json(products);
});

// GET /api/items/:id: Retrieve an item by its ID
router.get('/:id', (req, res) => {
    const product = products.find(p => p.id === req.params.id);
    if (!product) {
        return res.status(404).send('Product not found');
    }
    res.json(product);
});

// POST /api/items: Create a new item
router.post('/', (req, res) => {
    const newProduct = {
        id: (products.length + 1).toString(),
        name: req.body.name,
        description: req.body.description,
        price: req.body.price
    };
    products.push(newProduct);
    res.status(201).json(newProduct);
});

// PUT /api/items/:id: Update an existing item
router.put('/:id', (req, res) => {
    const product = products.find(p => p.id === req.params.id);
    if (!product) {
        return res.status(404).send('Product not found');
    }

    product.name = req.body.name;
    product.description = req.body.description;
    product.price = req.body.price;

    res.json(product);
});

// DELETE /api/items/:id: Delete an item
router.delete('/:id', (req, res) => {
    const productIndex = products.findIndex(p => p.id === req.params.id);
    if (productIndex === -1) {
        return res.status(404).send('Product not found');
    }

    products.splice(productIndex, 1);
    res.status(204).send();
});

module.exports = router;
