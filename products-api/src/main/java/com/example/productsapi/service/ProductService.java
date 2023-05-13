package com.example.productsapi.service;

import com.example.productsapi.domain.Product;

import java.util.List;
import java.util.Optional;

public interface ProductService {

    List<Product> findAll();

    Optional<Product> findById(Long id);

    void deleteById(Long id);

    Product save(Product product);
}
