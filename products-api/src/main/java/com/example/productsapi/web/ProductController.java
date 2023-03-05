package com.example.productsapi.web;

import com.example.productsapi.domain.Product;
import com.example.productsapi.service.ProductService;
import com.example.productsapi.web.dto.CreateProductPayload;
import com.example.productsapi.web.dto.GetProductPayload;
import com.example.productsapi.web.dto.ProductMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.Collection;

@RestController
@RequestMapping("/products")
public class ProductController {

    @Autowired
    ProductService productService;

    @Autowired
    ProductMapper productMapper;

    @PutMapping("/{id}")
    public ResponseEntity<GetProductPayload> create(@PathVariable long id,
                                                    @RequestBody CreateProductPayload createProductPayload) {
        var product = new Product();
        productMapper.updateProduct(createProductPayload, product);
        product.setId(id);
        productService.create(product);
        return ResponseEntity.ok(productMapper.toGetPayload(product));
    }

    @GetMapping
    @ResponseStatus(HttpStatus.OK)
    public Collection<GetProductPayload> readAll() {
        return productService.readAll().stream().map(productMapper::toGetPayload).toList();
    }

    @GetMapping("/{id}")
    public ResponseEntity<GetProductPayload> readById(@PathVariable long id) {
        return ResponseEntity.of(productService.readById(id).map(productMapper::toGetPayload));
    }

    @DeleteMapping("/{id}")
    @ResponseStatus(HttpStatus.OK)
    public void deleteById(@PathVariable long id) {
        productService.deleteById(id);
    }
}
