package com.example.productsapi.web;

import com.example.productsapi.domain.Product;
import com.example.productsapi.service.ProductService;
import com.example.productsapi.web.dto.ProductDto;
import com.example.productsapi.web.dto.ProductMapper;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@CrossOrigin
@RequiredArgsConstructor
@RestController
@RequestMapping("/products")
@FieldDefaults(level = AccessLevel.PRIVATE)
public class ProductController {

    final ProductService productService;

    final ProductMapper productMapper;

    @GetMapping
    public @ResponseBody ResponseEntity<List<Product>> findAll() {
        return ResponseEntity.ok(productService.findAll());
    }

    @GetMapping("/{id}")
    public @ResponseBody ResponseEntity<Product> findById(@PathVariable Long id) {
        return ResponseEntity.of(productService.findById(id));
    }

    @PostMapping
    @ResponseStatus(HttpStatus.OK)
    public void save(@RequestBody ProductDto productDto) {
        productService.save(productMapper.toEntity(productDto));
    }

    @PutMapping("/{id}")
    @ResponseStatus(HttpStatus.OK)
    public void update(@PathVariable Long id, @RequestBody ProductDto productDto) {
        var product = new Product();
        product.setId(id);
        productService.save(productMapper.partialUpdate(productDto, product));
    }
}
