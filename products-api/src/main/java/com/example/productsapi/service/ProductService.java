package com.example.productsapi.service;

import com.example.productsapi.domain.Product;

import java.util.Collection;
import java.util.Optional;

public interface ProductService {
    void create(Product product);

    Collection<Product> readAll();

    Optional<Product> readById(long id);

    void update(Product product);

    void deleteById(long id);
}
