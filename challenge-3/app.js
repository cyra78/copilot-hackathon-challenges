   const express = require('express');
   const app = express();
   const port = 3000;

   app.use(express.json());

   const productsRouter = require('./routes/products');
   app.use('/api/items', productsRouter);
   app.use(express.static('public'));

   app.listen(port, () => {
       console.log(`Server is running on http://localhost:${port}`);
   });
   
